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
using System.Windows.Shapes;
using System.Data;
using System.Configuration;

namespace SimpleChart.Charts
{
    /// <summary>
    /// Interaction logic for ChartPlayGround.xaml
    /// </summary>
    public partial class ChartPlayGround : Window
    {
        public ChartPlayGround()
        {
            InitializeComponent();
        }

        private void DrawChart1()
        {
            // Resets the values
            chart1.Reset();

            // Setting this property provides angled dimension to the X-axis for more readability
            //chart1.SmartAxisLabel = true;

            // Set the chart title
            //chart1.Title = "Sales by Region";

            // Set the Y-Axis value
            chart1.ValueField.Add("Amount");
            chart1.ValueField.Add("Min");
            chart1.ValueField.Add("Id");

            // Set the chart tooltip.  {field} will be replaced by current bar value.
            // Need to improve in this little templating.
            chart1.ToolTipText = "Spec count: {field}";

            // Set the x-axis text
            chart1.XAxisText = "Sales";

            // Set the x-axis data field
            chart1.XAxisField = "Id";

            // Setting this value displays the bar value on top of the bar.
            //chart1.ShowValueOnBar = true;

            // Get the data required for displaying the graph.
            //DataSet dsetAll = QueryHelper.GetDataSet(ConfigurationManager.AppSettings["SALES_XML"]);

            DataSet dataSetQueries = new DataSet();
            DataTable dtQueries = new DataTable("Queries");
            dataSetQueries.Tables.Add(dtQueries);

            dtQueries.Columns.Add("Id", typeof(string));
            dtQueries.Columns.Add("Region", typeof(string));
            dtQueries.Columns.Add("Amount", typeof(string));
            dtQueries.Columns.Add("Min", typeof(string));
            dtQueries.Columns.Add("Max", typeof(string));

            DataRow newRow = dtQueries.NewRow();
            newRow["Id"] = "1";
            newRow["Region"] = "Europe";
            newRow["Amount"] = "100";
            newRow["Min"] = "120";
            newRow["Max"] = "150";
            dtQueries.Rows.Add(newRow);

            newRow = dtQueries.NewRow();
            newRow["Id"] = "2";
            newRow["Region"] = "America";
            newRow["Amount"] = "110";
            newRow["Min"] = "60";
            newRow["Max"] = "70";
            dtQueries.Rows.Add(newRow);

            newRow = dtQueries.NewRow();
            newRow["Id"] = "3";
            newRow["Region"] = "America";
            newRow["Amount"] = "110";
            newRow["Min"] = "60";
            newRow["Max"] = "70";
            dtQueries.Rows.Add(newRow);

            newRow = dtQueries.NewRow();
            newRow["Id"] = "4";
            newRow["Region"] = "America";
            newRow["Amount"] = "110";
            newRow["Min"] = "60";
            newRow["Max"] = "70";
            dtQueries.Rows.Add(newRow);

            chart1.DataSource = dataSetQueries;

            // Set the datasource
            //chart1.DataSource = newDS;
            //chart1.DataSource = dsetAll;

            // Generate the Graph
            chart1.Generate();


            //// Draw CHART 2.
            //// Reset the chart
            //chart2.Reset();

            //// Setting this property provides angled dimension to the X-axis for more readability
            //chart2.SmartAxisLabel = false;

            //// Set the chart title
            //chart2.Title = "Sales Summary by Region";

            //// Add the range of values to the chart.
            //chart2.ValueField.Add("Min");
            //chart2.ValueField.Add("Max");
            //chart2.ValueField.Add("Avg");

            //// Set the chart tooltip.  {field} will be replaced by current bar value.
            //// Need to improve in this little templating.
            //chart2.ToolTipText = "Sales($): {field}";

            //// Set the x-axis text
            //chart2.XAxisText = "Sales";

            //// Set the x-axis field
            //chart2.XAxisField = "Region";

            //// Setting this value displays the bar value on top of the bar.
            //chart2.ShowValueOnBar = true;

            //// Get the data
            //dsetAll = QueryHelper.GetDataSet(ConfigurationManager.AppSettings["SALES1_XML"]);

            //// Assign to datasource
            //chart2.DataSource = dsetAll;

            //// Generate the chart
            //chart2.Generate();
        }

        private void DrawChart2()
        {
            // Resets the values
            chart1.Reset();

            // Setting this property provides angled dimension to the X-axis for more readability
            //chart1.SmartAxisLabel = true;

            // Set the chart title
            //chart1.Title = "Sales by Region";

            // Set the Y-Axis value
            chart1.ValueField.Add("Amount");
            chart1.ValueField.Add("Min");
            chart1.ValueField.Add("Max");
            chart1.ValueField.Add("Id");

            // Set the chart tooltip.  {field} will be replaced by current bar value.
            // Need to improve in this little templating.
            chart1.ToolTipText = "Spec count: {field}";

            // Set the x-axis text
            //chart1.XAxisText = "Sales";

            // Set the x-axis data field
            chart1.XAxisField = "Id";

            // Setting this value displays the bar value on top of the bar.
            chart1.ShowValueOnBar = false;

            // Get the data required for displaying the graph.
            //DataSet dsetAll = QueryHelper.GetDataSet(ConfigurationManager.AppSettings["SALES_XML"]);

            DataSet dataSetQueries = new DataSet();
            DataTable dtQueries = new DataTable("Queries");
            dataSetQueries.Tables.Add(dtQueries);

            dtQueries.Columns.Add("Id", typeof(string));
            dtQueries.Columns.Add("Region", typeof(string));
            dtQueries.Columns.Add("Amount", typeof(string));
            dtQueries.Columns.Add("Min", typeof(string));
            dtQueries.Columns.Add("Max", typeof(string));

            DataRow newRow = dtQueries.NewRow();
            newRow["Id"] = "1";
            newRow["Region"] = "Europe";
            newRow["Amount"] = "100";
            newRow["Min"] = "200";
            newRow["Max"] = "350";
            dtQueries.Rows.Add(newRow);

            newRow = dtQueries.NewRow();
            newRow["Id"] = "2";
            newRow["Region"] = "America";
            newRow["Amount"] = "150";
            newRow["Min"] = "60";
            newRow["Max"] = "70";
            dtQueries.Rows.Add(newRow);

            newRow = dtQueries.NewRow();
            newRow["Id"] = "3";
            newRow["Region"] = "Test";
            newRow["Amount"] = "50";
            newRow["Min"] = "10";
            newRow["Max"] = "370";
            dtQueries.Rows.Add(newRow);

            newRow = dtQueries.NewRow();
            newRow["Id"] = "4";
            newRow["Region"] = "Fiocruz";
            newRow["Amount"] = "350";
            newRow["Min"] = "260";
            newRow["Max"] = "170";
            dtQueries.Rows.Add(newRow);

            newRow = dtQueries.NewRow();
            newRow["Id"] = "5";
            newRow["Region"] = "Diogo";
            newRow["Amount"] = "120";
            newRow["Min"] = "160";
            newRow["Max"] = "80";
            dtQueries.Rows.Add(newRow);

            chart1.DataSource = dataSetQueries;

            // Set the datasource
            //chart1.DataSource = newDS;
            //chart1.DataSource = dsetAll;

            // Generate the Graph
            chart1.Generate();

        }

        private void DrawChart3()
        {
            // Resets the values
            chart1.Reset();

            // Setting this property provides angled dimension to the X-axis for more readability
            //chart1.SmartAxisLabel = true;

            // Set the chart title
            //chart1.Title = "Sales by Region";

            // Set the Y-Axis value
            chart1.ValueField.Add("Amount");
            chart1.ValueField.Add("Min");
            chart1.ValueField.Add("Max");
            chart1.ValueField.Add("Amount2");
            chart1.ValueField.Add("Min2");
            chart1.ValueField.Add("Max2");
            chart1.ValueField.Add("Amount3");
            chart1.ValueField.Add("Min3");
            chart1.ValueField.Add("Max3");
            chart1.ValueField.Add("Amount4");
            chart1.ValueField.Add("Min4");
            chart1.ValueField.Add("Max4");
            chart1.ValueField.Add("Id");

            // Set the chart tooltip.  {field} will be replaced by current bar value.
            // Need to improve in this little templating.
            chart1.ToolTipText = "Spec count: {field}";

            // Set the x-axis text
            //chart1.XAxisText = "Sales";

            // Set the x-axis data field
            chart1.XAxisField = "Id";

            // Setting this value displays the bar value on top of the bar.
            chart1.ShowValueOnBar = false;

            // Get the data required for displaying the graph.
            //DataSet dsetAll = QueryHelper.GetDataSet(ConfigurationManager.AppSettings["SALES_XML"]);

            DataSet dataSetQueries = new DataSet();
            DataTable dtQueries = new DataTable("Queries");
            dataSetQueries.Tables.Add(dtQueries);

            dtQueries.Columns.Add("Id", typeof(string));
            dtQueries.Columns.Add("Region", typeof(string));
            dtQueries.Columns.Add("Amount", typeof(string));
            dtQueries.Columns.Add("Min", typeof(string));
            dtQueries.Columns.Add("Max", typeof(string));
            dtQueries.Columns.Add("Region2", typeof(string));
            dtQueries.Columns.Add("Amount2", typeof(string));
            dtQueries.Columns.Add("Min2", typeof(string));
            dtQueries.Columns.Add("Max2", typeof(string));
            dtQueries.Columns.Add("Region3", typeof(string));
            dtQueries.Columns.Add("Amount3", typeof(string));
            dtQueries.Columns.Add("Min3", typeof(string));
            dtQueries.Columns.Add("Max3", typeof(string));
            dtQueries.Columns.Add("Region4", typeof(string));
            dtQueries.Columns.Add("Amount4", typeof(string));
            dtQueries.Columns.Add("Min4", typeof(string));
            dtQueries.Columns.Add("Max4", typeof(string));

            DataRow newRow = dtQueries.NewRow();
            newRow["Id"] = "1";
            newRow["Region"] = "Europe";
            newRow["Amount"] = "100";
            newRow["Min"] = "200";
            newRow["Max"] = "350";
            newRow["Region2"] = "Europe";
            newRow["Amount2"] = "100";
            newRow["Min2"] = "200";
            newRow["Max2"] = "350";
            newRow["Region3"] = "Europe";
            newRow["Amount3"] = "100";
            newRow["Min3"] = "200";
            newRow["Max3"] = "350";
            newRow["Region4"] = "Europe";
            newRow["Amount4"] = "100";
            newRow["Min4"] = "200";
            newRow["Max4"] = "350";
            dtQueries.Rows.Add(newRow);

            newRow = dtQueries.NewRow();
            newRow["Id"] = "2";
            newRow["Region"] = "America";
            newRow["Amount"] = "150";
            newRow["Min"] = "60";
            newRow["Max"] = "70";
            dtQueries.Rows.Add(newRow);

            newRow = dtQueries.NewRow();
            newRow["Id"] = "3";
            newRow["Region"] = "Test";
            newRow["Amount"] = "50";
            newRow["Min"] = "10";
            newRow["Max"] = "370";
            dtQueries.Rows.Add(newRow);

            newRow = dtQueries.NewRow();
            newRow["Id"] = "4";
            newRow["Region"] = "Fiocruz";
            newRow["Amount"] = "350";
            newRow["Min"] = "260";
            newRow["Max"] = "170";
            dtQueries.Rows.Add(newRow);

            newRow = dtQueries.NewRow();
            newRow["Id"] = "5";
            newRow["Region"] = "Diogo";
            newRow["Amount"] = "120";
            newRow["Min"] = "160";
            newRow["Max"] = "80";
            dtQueries.Rows.Add(newRow);

            newRow = dtQueries.NewRow();
            newRow["Id"] = "6";
            newRow["Region"] = "Diogo";
            newRow["Amount"] = "120";
            newRow["Min"] = "160";
            newRow["Max"] = "80";
            dtQueries.Rows.Add(newRow);

            newRow = dtQueries.NewRow();
            newRow["Id"] = "7";
            newRow["Region"] = "Diogo";
            newRow["Amount"] = "120";
            newRow["Min"] = "160";
            newRow["Max"] = "80";
            dtQueries.Rows.Add(newRow);

            newRow = dtQueries.NewRow();
            newRow["Id"] = "8";
            newRow["Region"] = "Diogo";
            newRow["Amount"] = "120";
            newRow["Min"] = "160";
            newRow["Max"] = "80";
            dtQueries.Rows.Add(newRow);

            chart1.DataSource = dataSetQueries;

            // Set the datasource
            //chart1.DataSource = newDS;
            //chart1.DataSource = dsetAll;

            // Generate the Graph
            chart1.Generate();

        }

        private void ButtonPlot_Click(object sender, RoutedEventArgs e)
        {
            DrawChart1();
        }

        private void ButtonPlot_Click_1(object sender, RoutedEventArgs e)
        {
            DrawChart2();
        }

        private void ButtonPlot_Click_2(object sender, RoutedEventArgs e)
        {
            DrawChart3();
        }
    }
}
