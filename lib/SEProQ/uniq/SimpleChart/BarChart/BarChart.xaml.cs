using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Windows.Media.Effects;
using System.Text.RegularExpressions;

namespace SimpleChart.Charts
{
    /// <summary>
    /// Interaction logic for BarChart.xaml
    /// </summary>
    public partial class BarChart : UserControl
    {
        private string xAxisText;

        private double horizontalGridLineThickness;

        private double defaultXYalueFontSize = 12;
        private double defaultYValueFontSize = 12;
        double? maxData;

        double left = 5;

        double top;

        double topMargin = 40;
        double leftMargin = 50;
        double bottomMargin = 10;

        double spaceBetweenBars = 15;

        private DataRow barRow;  // to hold current row 

        Brush prevBrush;

        Brush legendTextColor;

        TextBlock txtXAxis;
        TextBlock txtTopTitle;
        TextBlock bubbleText;

        Color gridLineColor = Colors.LightGray;

        List<Legend> legends = new List<Legend>();

        public event EventHandler<BarEventArgs> BarClickHandler;

        public bool ShowValueOnBar { get; set; }
        public bool SmartAxisLabel { get; set; }

        public List<Legend> Legends
        {
            get { return legends; }
        }

        public double BarWidth { get; set; }
        public string Title { get; set; }
        public string ToolTipText { get; set; }
        public string XAxisField { get; set; }
        public bool EnableZooming { get; set; }

        public string QueryParam { get; set; }

        public Color GridLineColor { get; set; }

        public string XAxisText
        {
            get { return xAxisText; }
            set
            {
                xAxisText = value;
                txtXAxis.Text = value;
            }
        }


        public List<string> ValueField { get; set; }

        public Brush TextColor
        {
            get
            {
                return bubbleText.Foreground;
            }
            set
            {
                bubbleText.Foreground = value;
                txtTopTitle.Foreground = value;
                txtXAxis.Foreground = value;
            }
        }

        public double GridLineHorizontalThickness { get; set; }

        public bool ShowHorizontalGridLine { get; set; }

        public Brush BackGroundColor
        {
            get { return this.Background; }
            set
            {
                chartArea.Background = value;
                this.Background = value;
            }
        }

        public Brush LegendTextColor { get; set; }


        public DataSet DataSource { get; set; }

        public BarChart()
        {
            InitializeComponent();
            ValueField = new List<string>();
            InitChartControls();
            BarWidth = 60;
            horizontalGridLineThickness = 0.3;

            legendTextColor = new SolidColorBrush(Parser.GetDarkerColor(Colors.Black, 10));


            //GradientStopCollection gsc = new GradientStopCollection(2);
            //gsc.Add(new GradientStop(Colors.Black, 1));
            //gsc.Add(new GradientStop(Colors.Gray, 0));

            //GradientStopCollection gsc = new GradientStopCollection(1);
            //gsc.Add(new GradientStop(Colors.White, 0));

            //chartArea.Background = new LinearGradientBrush(gsc, 90);

        }

        /// <summary>
        /// Get max value data element.
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        double? GetMax(DataTable dt)
        {
            double? max = 0;
            double? tmp = 0;


            foreach (string valField in ValueField)
            {
                foreach (DataRow r in dt.Rows)
                {
                    if (!r.Table.Columns.Contains(valField))
                        continue;
                    if (r[valField] != DBNull.Value)
                    {
                        tmp = Convert.ToDouble(r[valField]);

                        if (tmp > max)
                            max = tmp;
                    }
                }
            }

            max = (max != null) ? max : 0;

            return max;
        }

        /// <summary>
        /// Reset the value field.
        /// </summary>
        public void Reset()
        {
            ValueField.Clear();
        }

        /// <summary>
        /// Creates the chart based on the datasource.
        /// </summary>
        public void Generate()
        {
            try
            {
                legends.Clear();
                chartArea.Children.Clear();
                left = 10;

                // Setup chart elements.
                //AddChartControlsToChart();
                // Setup chart area.
                SetUpChartArea();

                // Will be made more generic in the next versions.
                DataTable dt = (DataSource as DataSet).Tables[0];

                if (null != dt)
                {
                    // if no data found draw empty chart.
                    if (dt.Rows.Count == 0)
                    {
                        DrawEmptyChart();
                        return;
                    }

                    // Hide the nodata found text.
                    //txtNoData.Visibility = Visibility.Hidden;

                    // Get the max y-value.  This is used to calculate the scale and y-axis.
                    maxData = GetMax(dt);

                    // Prepare the chart for rendering.  Does some basic setup.
                    PrepareChartForRendering();

                    // Get the total bar count.
                    int barCount = dt.Rows.Count;

                    // If more than 1 value field, then this is a group chart.
                    bool isSeries = ValueField.Count > 1;

                    // no legends added yet.
                    bool legendAdded = false;  // no legends yet added.

                    // For each row in the datasource
                    foreach (DataRow row in dt.Rows)
                    {
                        // Set the barwidth.  This is required to adjust the size based on available no. of 
                        // bars.
                        SetBarWidth(barCount);

                        // Draw x-axis label based on datarow.
                        DrawXAxisLabel(row, ValueField.Count);

                        // For each row the current series is initialized to 0 to indicate start of series.
                        int currentSeries = 0;

                        // For each value in the datarow, draw the bar.
                        foreach (string valField in ValueField)
                        {
                            if (null == valField)
                                continue;

                            if (!row.Table.Columns.Contains(valField))
                                continue;

                            // Draw bar for each value.
                            DrawBar(isSeries, legendAdded, row, ref currentSeries, valField);

                        }
                        legendAdded = true;

                        // Set up location for next bar in series.
                        if (isSeries)
                            left = left + spaceBetweenBars;

                    }

                    // Reset the chartarea to accomdodate all the chart elements.
                    if ((left + BarWidth) > chartArea.Width)
                    {
                        chartArea.Width = left + BarWidth + leftMargin;
                        leftMargin = 58;
                    }

                    // Draw the x-axis.
                    DrawXAxis();

                    // Draw the y-axis.
                    DrawYAxis();
                    // Draw the legend.
                    //DrawLegend();
                }
            }
            catch (Exception ex)
            {
                // TODO: Finalize exception handling strategy.
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Draws a bar
        /// </summary>
        /// <param name="isSeries">Whether current bar is in a series or group.</param>
        /// <param name="legendAdded">Indicates whether to add legend.</param>
        /// <param name="row">The current bar row.</param>
        /// <param name="currentSeries">The current series.  Used to group series and color code bars.</param>
        /// <param name="valField">Value is fetched from the datasource from this field.</param>
        private void DrawBar(bool isSeries, bool legendAdded, DataRow row, ref int currentSeries, string valField)
        {
            double val = 0.0;

            if (row[valField] == DBNull.Value)
                val = 0;
            else
                val = Convert.ToDouble(row[valField]);

            // Calculate bar value.
            double? calValue = (((float)val * 100 / maxData)) *
                    (chartArea.Height - bottomMargin - topMargin) / 100;

            Rectangle rect = new Rectangle();

            // Setup bar attributes.
            SetBarAttributes(calValue, rect);

            // Color the bar.
            Color stroke = Helper.GetColorByIndex(currentSeries);
            //Color stroke = Helper.GetDarkColorByIndex(currentSeries);
            
            rect.Fill = new SolidColorBrush(stroke);
            rect.Stroke = new SolidColorBrush(Color.FromArgb(70, 0, 0, 0));
            rect.StrokeThickness = 2;

            // Setup bar events.
            SetBarEvents(rect);

            // Add the legend if not added.
            if (isSeries && !legendAdded)
            {
                legends.Add(new Legend(stroke, ValueField[currentSeries]));
            }

            // Calculate bar top and left position.
            top = (chartArea.Height - bottomMargin) - rect.Height;
            Canvas.SetTop(rect, top);
            Canvas.SetLeft(rect, left + leftMargin);

            // Add bar to chart area.
            chartArea.Children.Add(rect);

            // Display value on bar if set to true.
            if (ShowValueOnBar)
            {
                DisplayYValueOnBar(val, rect);
            }

            // Create Bar object and assign to the rect.
            rect.Tag = new Bar(val, row, valField);

            // Calculate the new left postion for subsequent bars.
            if (isSeries)
                left = left + rect.Width;
            else
                left = left + BarWidth + spaceBetweenBars;

            // Increment the series
            currentSeries++;
        }

        /// <summary>
        /// Setup bar events.
        /// </summary>
        /// <param name="rect"></param>
        private void SetBarEvents(Rectangle rect)
        {
            rect.MouseLeftButtonUp += new MouseButtonEventHandler(Bar_MouseLeftButtonUp);
            rect.MouseEnter += new MouseEventHandler(Bar_MouseEnter);
            rect.MouseLeave += new MouseEventHandler(Bar_MouseLeave);
        }

        /// <summary>
        /// Setup bar attributes.
        /// </summary>
        /// <param name="currentSeries"></param>
        /// <param name="calValue"></param>
        /// <param name="rect"></param>
        /// <returns></returns>
        private void SetBarAttributes(double? calValue, Rectangle rect)
        {
            rect.Width = BarWidth;
            if (calValue < 1)
                rect.Height = 1;
            else
                rect.Height = calValue.Value;

            rect.HorizontalAlignment = HorizontalAlignment.Left;
            rect.VerticalAlignment = VerticalAlignment.Center;
            rect.StrokeThickness = 1;

        }

        /// <summary>
        /// Display y-value on bar.
        /// </summary>
        /// <param name="val"></param>
        /// <param name="rect"></param>
        private void DisplayYValueOnBar(double val, Rectangle rect)
        {
            TextBlock yValue = new TextBlock();
            yValue.Text = val.ToString();
            yValue.Width = 80;
            yValue.Foreground = TextColor;
            yValue.HorizontalAlignment = HorizontalAlignment.Center;
            yValue.TextAlignment = TextAlignment.Center;
            yValue.FontSize = defaultYValueFontSize;

            yValue.MouseEnter += new MouseEventHandler(yValue_MouseEnter);
            yValue.MouseLeave += new MouseEventHandler(yValue_MouseLeave);
            chartArea.Children.Add(yValue);
            Canvas.SetTop(yValue, top - 10);
            Canvas.SetLeft(yValue, left + (rect.Width / 2));
        }

        private void SetBarWidth(int barCount)
        {
            BarWidth = (chartArea.Width - (spaceBetweenBars * ValueField.Count * barCount) -
                (leftMargin * 3)) / (barCount * ValueField.Count);

            // check min bar width
            if (BarWidth < 10)
            {
                BarWidth = 10;
                spaceBetweenBars = 10;
            }
            if (BarWidth > 100)
                BarWidth = 100;
        }

        private void DrawXAxisLabel(DataRow row, int barCount)
        {
            // Setup XAxis label
            TextBlock markText = new TextBlock();
            markText.Text = row[XAxisField].ToString();
            double currentBarWidth = BarWidth > 70 ? 70 : BarWidth < 0 ? 10 : BarWidth;
            markText.Width = currentBarWidth * barCount;

            //markText.TextTrimming = TextTrimming.CharacterEllipsis;
            markText.HorizontalAlignment = HorizontalAlignment.Stretch;

            markText.Foreground = TextColor;
            //markText.HorizontalAlignment = HorizontalAlignment.Center;
            markText.TextAlignment = TextAlignment.Center;
            markText.FontSize = 12;

            //markText.MouseEnter += new MouseEventHandler(XText_MouseEnter);
            //markText.MouseLeave += new MouseEventHandler(XText_MouseLeave);

            if (SmartAxisLabel)
            {
                Transform st = new SkewTransform(0, 20);
                markText.RenderTransform = st;
            }

            chartArea.Children.Add(markText);
            Canvas.SetTop(markText, chartArea.Height - bottomMargin + 10);  // adjust y location
            Canvas.SetLeft(markText, left + 25 + leftMargin / 2);

            #region Thickness
            Line yaxis = new Line();
            yaxis.X1 = (left + 25 + leftMargin / 2) + (currentBarWidth * barCount / 2);
            yaxis.Y1 = chartArea.Height - bottomMargin;
            yaxis.X2 = (left + 25 + leftMargin / 2) + (currentBarWidth * barCount / 2);
            yaxis.Y2 = chartArea.Height - bottomMargin + 5;
            yaxis.Stroke = new SolidColorBrush(Color.FromArgb(70, 0, 0, 0));
            yaxis.StrokeThickness = 2;
            chartArea.Children.Add(yaxis);
            #endregion

        }

        /// <summary>
        /// Prepares the chart for rendering.  Sets up control width and location.
        /// </summary>
        private void PrepareChartForRendering()
        {
            Canvas.SetTop(txtXAxis, chartArea.Height - 20);
            Canvas.SetLeft(txtXAxis, leftMargin);

            txtTopTitle.Width = this.Width;
            txtTopTitle.FontSize = 14;
            txtTopTitle.Text = Title;
            txtTopTitle.TextAlignment = TextAlignment.Center;
            Canvas.SetTop(txtTopTitle, 0);
            Canvas.SetLeft(txtTopTitle, leftMargin);
        }

        /// <summary>
        /// Sets up the chart area with default values
        /// </summary>
        private void SetUpChartArea()
        {
            if (!EnableZooming)
            {
                //zoomSlider.Visibility = Visibility.Hidden;
            }

            if (chartArea.Height.ToString() == "NaN")
                chartArea.Height = 450;

            chartArea.Height = 450;
            //chartArea.Height = chartArea.Height;

            if (this.Width.ToString() == "NaN")
                this.Width = 800;

            chartArea.Width = 750;
            //chartArea.Width = this.Width;
        }

        /// <summary>
        /// Draws an empty chart.
        /// </summary>
        private void DrawEmptyChart()
        {
            //txtNoData.Visibility = Visibility.Visible;
            //Canvas.SetTop(txtNoData, chartArea.Height / 2);
            //Canvas.SetLeft(txtNoData, chartArea.Width / 2);
        }

        void yValue_MouseLeave(object sender, MouseEventArgs e)
        {
            TextBlock tb = (sender as TextBlock);
            tb.FontSize = defaultXYalueFontSize;
        }

        void yValue_MouseEnter(object sender, MouseEventArgs e)
        {
            TextBlock tb = (sender as TextBlock);
            tb.FontSize = 10;
        }

        void XText_MouseLeave(object sender, MouseEventArgs e)
        {
            TextBlock tb = (sender as TextBlock);
            tb.FontSize = defaultXYalueFontSize;
        }

        void XText_MouseEnter(object sender, MouseEventArgs e)
        {
            TextBlock tb = (sender as TextBlock);
            tb.FontSize = 10;
        }


        /// <summary>
        /// Initialize chart controls.
        /// </summary>
        private void InitChartControls()
        {
            txtTopTitle = new TextBlock();
            txtXAxis = new TextBlock();
            bubbleText = new TextBlock();

            bubbleText.FontSize = 12;

            Transform tf = new ScaleTransform(1.5, 1.5, 12, 24);
            bubbleText.RenderTransform = tf;


        }

        /// <summary>
        /// Add chart controls to chart.  This creates a basic layout for the chart.
        /// </summary>
        private void AddChartControlsToChart()
        {
            chartArea.Children.Add(txtXAxis);
            chartArea.Children.Add(txtTopTitle);
            chartArea.Children.Add(bubbleText);
        }


        /// <summary>
        /// Draw chart legends.
        /// </summary>
        private void DrawLegend()
        {
            if (legends == null || legends.Count == 0)
                return;

            // Initialize legend location.
            double legendX1 = leftMargin + txtXAxis.Text.Length + 100;
            double legendWidth = 20;

            // Draw all legends
            foreach (Legend legend in legends)
            {
                Line legendShape = new Line();

                legendShape.Stroke = new SolidColorBrush(legend.LegendColor);
                legendShape.StrokeDashCap = PenLineCap.Round;
                legendShape.StrokeThickness = 8;

                legendShape.StrokeStartLineCap = PenLineCap.Round;
                legendShape.StrokeEndLineCap = PenLineCap.Triangle;


                legendShape.X1 = legendX1;
                legendShape.Y1 = chartArea.Height - 10;
                legendShape.X2 = legendX1 + legendWidth;
                legendShape.Y2 = chartArea.Height - 10;

                chartArea.Children.Add(legendShape);

                TextBlock txtLegend = new TextBlock();
                txtLegend.Text = legend.LegendText;
                txtLegend.Foreground = legendTextColor;

                chartArea.Children.Add(txtLegend);
                Canvas.SetTop(txtLegend, chartArea.Height - 20);
                Canvas.SetLeft(txtLegend, legendShape.X2 + 2);

                legendX1 += legendWidth + 30 + txtLegend.Text.Length;
            }
        }


        /// <summary>
        /// Draws XAxis
        /// </summary>
        private void DrawXAxis()
        {
            // Draw axis
            Line xaxis = new Line();
            xaxis.X1 = leftMargin - 25;
            xaxis.Y1 = chartArea.Height - bottomMargin;
            xaxis.X2 = this.chartArea.Width;
            xaxis.Y2 = chartArea.Height - bottomMargin;

            xaxis.Stroke = new SolidColorBrush(Color.FromArgb(70, 0, 0, 0));
            xaxis.StrokeThickness = 2;
            chartArea.Children.Add(xaxis);

        }

        /// <summary>
        /// Draws YAxis.  Here we use the maxData vlaue calculated earlier.  This method also
        /// sets up the y-axis marker.
        /// </summary>
        private void DrawYAxis()
        {
            // Drawing yaxis is as simple as adding a line control at appropriate location.
            Line yaxis = new Line();
            yaxis.X1 = leftMargin;
            yaxis.Y1 = 0;
            yaxis.X2 = leftMargin;
            yaxis.Y2 = chartArea.Height - bottomMargin + 15;
            yaxis.Stroke = new SolidColorBrush(Color.FromArgb(70, 0, 0, 0));
            yaxis.StrokeThickness = 2;
            chartArea.Children.Add(yaxis);

            // Set the scale factor for y-axis marker.
            double scaleFactor = 3;

            // this value is used to increment the y-axis marker value.
            double yMarkerValue = maxData.Value / scaleFactor;

            //double yMarkerValue = yMarkerValueCurrent % 5 == 0 ? yMarkerValueCurrent : yMarkerValueCurrent + (5 - (yMarkerValueCurrent % 5));

            // This value is used to increment the y-axis marker location.
            double scale = 5;  // default value 5.

            // get the scale based on the current max y value and other chart element area adjustments.
            scale = (((float)yMarkerValue * 100 / maxData.Value)) *
                (chartArea.Height - bottomMargin - topMargin) / 100;

            double y1 = chartArea.Height - bottomMargin;

            double yAxisValue = 0;

            // Adjust the top location for next marker.
            y1 -= scale;

            // Increment the y-marker value.
            yAxisValue += yMarkerValue;

            for (int i = 0; i < scaleFactor; i++)
            {
                // Add y-axis marker line chart.
                Line marker = AddMarkerLineToChart(y1);

                // Draw horizontal grid based on marker location.
                DrawHorizontalGrid(marker.X1, y1);

                // Add the y-marker to the chart.
                AddMarkerTextToChart(y1, yAxisValue);

                // Adjust the top location for next marker.
                y1 -= scale;

                // Increment the y-marker value.
                yAxisValue += yMarkerValue;
            }
        }

        /// <summary>
        /// Add the marker line to chart.
        /// </summary>
        /// <param name="top">The top location where the marker is to be placed.</param>
        /// <returns>The marker line.  This is used for drawing the horizontal grid line.</returns>
        private Line AddMarkerLineToChart(double top)
        {
            Line marker = new Line();
            marker.StrokeDashArray = new DoubleCollection(new[] { 3.0 });
            marker.X1 = leftMargin - 5;
            marker.Y1 = top;
            marker.X2 = this.chartArea.Width;
            //marker.X2 = marker.X1 + 4;
            marker.Y2 = top;
            marker.Stroke = new SolidColorBrush(Color.FromArgb(70, 0, 0, 0));
            marker.StrokeThickness = 0.7;
            chartArea.Children.Add(marker);
            return marker;
        }

        /// <summary>
        /// Add marker text to chart on yaxis.
        /// </summary>
        /// <param name="top">The top location.</param>
        /// <param name="markerTextValue">The marker text value.</param>
        private void AddMarkerTextToChart(double top, double markerTextValue)
        {
            TextBlock markText = new TextBlock();
            markText.Text = markerTextValue.ToString("E01");
            markText.Width = 50;
            markText.FontSize = 12;
            markText.Foreground = TextColor;
            markText.HorizontalAlignment = HorizontalAlignment.Right;
            markText.TextAlignment = TextAlignment.Right;
            chartArea.Children.Add(markText);

            Canvas.SetTop(markText, top - 10);        // adjust y location
            Canvas.SetLeft(markText, leftMargin - 60);
        }

        /// <summary>
        /// Draw horizontal Grid, if ShowHorizontalGridLine property is set.
        /// </summary>
        /// <param name="x1">starting left postion</param>
        /// <param name="y1">starting top postion</param>
        private void DrawHorizontalGrid(double x1, double y1)
        {
            if (!ShowHorizontalGridLine)
                return;

            Line gridLine = new Line();
            gridLine.X1 = x1;
            gridLine.Y1 = y1;
            gridLine.X2 = chartArea.Width;
            gridLine.Y2 = y1;

            gridLine.StrokeThickness = horizontalGridLineThickness;

            gridLine.Stroke = new SolidColorBrush(GridLineColor);

            chartArea.Children.Add(gridLine);

        }

        void Bar_MouseLeave(object sender, MouseEventArgs e)
        {
            Rectangle rect = (sender as Rectangle);
            rect.Fill = prevBrush;
            prevBrush = null;

        }

        void Bar_MouseEnter(object sender, MouseEventArgs e)
        {
            Rectangle rect = (sender as Rectangle);
            prevBrush = rect.Fill;

            rect.Fill = new SolidColorBrush(Colors.LightGreen);

            Bar b = rect.Tag as Bar;
            ToolTip tip = new ToolTip();

            barRow = b.BarRow;
            tip.Content = MatchToolTipTemplate(b.ValueField);

            rect.ToolTip = tip;

        }

        /// <summary>
        /// Match tooltip template and replace fields.  Need help in improving this.
        /// Searches for a tokein in {} and replaces with the actual value from DataSource.
        /// Supports only single token replacement.  Need to add support for multiple token replacement.
        /// </summary>
        /// <returns></returns>

        public string MatchToolTipTemplate(string valueField)
        {
            //string matchExpression = @"{\w+}";
            //string matchExpression = @"{field}";
            //MatchEvaluator matchField = MatchEvaluatorField;

            return (ToolTipText.Replace("{field}", GetResolvedTemplateValue(valueField)));

            //return Regex.Replace(ToolTipText, matchExpression, matchField);
        }

        private string GetResolvedTemplateValue(string valueField)
        {
            string newText = "";
            try
            {
                newText = barRow[valueField].ToString();
            }
            catch { }
            return newText;
        }


        [Obsolete]
        private string MatchEvaluatorField(Match m)
        {
            string newText = m.Value.Replace('{', ' ');
            newText = newText.Replace('}', ' ');
            try
            {
                newText = barRow[newText.Trim()].ToString();
            }
            catch { }
            return newText;
        }


        void Bar_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Rectangle rect = (sender as Rectangle);
            Bar b = rect.Tag as Bar;

            if (BarClickHandler != null)
                BarClickHandler(this, new BarEventArgs(b));

        }
    }
}
