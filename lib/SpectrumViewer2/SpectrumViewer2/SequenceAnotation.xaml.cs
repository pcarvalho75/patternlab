using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SpectrumViewer2
{
    /// <summary>
    /// Interaction logic for SequenceAnotation.xaml
    /// </summary>
    public partial class SequenceAnotation : UserControl
    {
        public SequenceAnotation()
        {
            InitializeComponent();
        }

        public double MyTopOffsetText { get; set; } = 15;
        public double MyFontSize { get; set; } = 18;
        public double MyStep { get; set; } = 10;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="annotations">The Sequence Element, Top Anotation, Bottom Annotation</param>
        public void Plot(List<Tuple<string, string, string>> annotations)
        {
            CanvasSequence.Children.Clear();

            double widthSum = 0;

            foreach (Tuple<string, string, string> i in annotations)
            {
                TextBlock textBlock = new TextBlock();
                textBlock.Foreground = new SolidColorBrush(Colors.Black);

                textBlock.FontFamily = new FontFamily("Courier New");
                textBlock.FontSize = MyFontSize;

                if (i.Item1.Contains("("))
                {
                    textBlock.FontWeight = FontWeights.Bold; 
                }

                textBlock.Text = PatternTools.pTools.CleanPeptide(i.Item1, true);

                Size tbSize = MeasureString(textBlock);

                Canvas.SetLeft(textBlock, widthSum);
                Canvas.SetTop(textBlock, MyTopOffsetText);

                CanvasSequence.Children.Add(textBlock);
                

                //include the pipe
                if (i.Item2.Length > 0 || i.Item3.Length > 0)
                {
                    Line l = new Line();

                    l.X1 = 0;
                    l.X2 = 0;
                    l.Y1 = 0;
                    l.Y2 = tbSize.Height * 1.1;

                    l.Stroke = System.Windows.Media.Brushes.Black;
                    l.StrokeThickness = 1;

                    Canvas.SetLeft(l, tbSize.Width + widthSum + MyStep/2);
                    Canvas.SetTop(l, MyTopOffsetText - (tbSize.Height * 0.15));
                    CanvasSequence.Children.Add(l);

                    double tickSize = 10;

                    if (i.Item2.Length > 0)
                    {
                        Line l2 = new Line();

                        l2.X1 = 0;
                        l2.X2 = tickSize;  // 150 too far
                        l2.Y1 = tickSize;
                        l2.Y2 =  0;

                        l2.Stroke = System.Windows.Media.Brushes.Blue;
                        l2.StrokeThickness = 1;

                        Canvas.SetLeft(l2, tbSize.Width + widthSum + MyStep / 2);
                        Canvas.SetTop(l2, MyTopOffsetText - (tbSize.Height * 0.15) - tickSize);
                        CanvasSequence.Children.Add(l2);

                        //And now add the lable
                        TextBlock tb = new TextBlock();
                        tb.HorizontalAlignment = HorizontalAlignment.Left;
                        tb.FontSize = 8;
                        //tb.Text = i.Item2;
                        CanvasSequence.Children.Add(tb);
                        Size tbLabel = MeasureString(tb);
                        Canvas.SetLeft(tb, tbSize.Width + widthSum + MyStep + l2.X2);
                        
                        //Canvas.SetTop(tb,);
                    }

                    if (i.Item3.Length > 0)
                    {
                        Line l3 = new Line();

                        l3.X1 = 0;
                        l3.X2 = -tickSize;
                        l3.Y1 = 0;
                        l3.Y2 = tickSize;

                        l3.Stroke = System.Windows.Media.Brushes.Red;
                        l3.StrokeThickness = 1;

                        Canvas.SetLeft(l3, tbSize.Width + widthSum + MyStep / 2);
                        Canvas.SetTop(l3, (MyTopOffsetText - (tbSize.Height * 0.15)) + (tbSize.Height * 1.1) );
                        CanvasSequence.Children.Add(l3);
                    }

                }
                widthSum += tbSize.Width + MyStep;

            }
            widthSum += MyStep;

            Console.WriteLine(widthSum);
            CanvasSequence.Width = widthSum;
            
        }

        private Size MeasureString(TextBlock candidate)
        {
            var formattedText = new FormattedText(
                candidate.Text,
                CultureInfo.CurrentUICulture,
                FlowDirection.LeftToRight,
                new Typeface(candidate.FontFamily, candidate.FontStyle, candidate.FontWeight, candidate.FontStretch),
                candidate.FontSize,
                Brushes.Black);

            return new Size(formattedText.Width, formattedText.Height);
        }
    }
}
