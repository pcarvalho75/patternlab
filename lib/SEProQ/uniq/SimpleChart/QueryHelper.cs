namespace SimpleChart.Charts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Data;
    using System.Configuration;

    public static class QueryHelper
    {
        public static DataSet GetDataSet(string xmlFileName)
        {
            DataSet ds = new DataSet("dsSales");
            ds.ReadXml(xmlFileName);

            return ds;
        }
    }
}
