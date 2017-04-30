using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SEPRPackage;
using Microsoft.Glee.Drawing;

namespace SEProcessor.Result
{
    public partial class GroupGraph : UserControl
    {
        public Graph g { get; set; }


       public void GenerateGraph(List<MyProtein> myProteins, MyProtein mainProtein)
       {
           g = new Graph("Protein Group");

           foreach (MyProtein p in myProteins)
           {
               g.AddNode(p.Locus);
           }

           Node n = g.FindNode(mainProtein.Locus);
           n.Attr.Fontcolor = Microsoft.Glee.Drawing.Color.Red;

           List<GraphLink> theLinks = new List<GraphLink>();

           foreach (MyProtein p in myProteins)
           {

               List<MyProtein> linkedProteins = myProteins.FindAll(a => a.DistinctPeptides.Intersect(p.DistinctPeptides).ToList().Count > 0);
               linkedProteins.Remove(p);

               foreach (MyProtein mp in linkedProteins)
               {
                   if (!theLinks.Exists(a => a.To.Equals(p.Locus) && a.From.Equals(mp.Locus)))
                   {
                       theLinks.Add(new GraphLink(mp.Locus, p.Locus));
                       Edge e = new Edge(p.Locus, "", mp.Locus);
                       e.Attr.ArrowHeadAtTarget = ArrowStyle.None;

                       if (mp.Locus.Equals(mainProtein.Locus) || p.Locus.Equals(mainProtein.Locus))
                       {
                           e.Attr.Color = Microsoft.Glee.Drawing.Color.Red;
                       }
                       g.Edges.Add(e);
                   }
                   else
                   {

                   }
               }
           }
           gViewer.Graph = g;
       }
        
        
        public GroupGraph()
        {
            InitializeComponent();
        }

        private void gViewer_Load(object sender, EventArgs e)
        {

        }
    }

    class GraphLink
    {
        public string To { get; set; }
        public string From { get; set; }

        public GraphLink(string to, string from) {
            To = to;
            From = from;
        }
    }
}
