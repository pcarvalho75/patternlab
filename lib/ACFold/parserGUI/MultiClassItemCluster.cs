using System;
using System.Collections.Generic;
using System.Text;

namespace parserGUI
{
    class MultiClassItemCluster
    {
        List<MultiClassItem> myItems = new List<MultiClassItem>();
        public int Distance { get; set;}

        public MultiClassItemCluster(int distance)
        {
            this.Distance = distance;
        }

        public List<MultiClassItem> MyItems
        {
            get { return myItems; }
            set { myItems = value; }
        }
    }
}
