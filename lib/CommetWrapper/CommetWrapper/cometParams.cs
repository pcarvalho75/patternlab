using CometWrapper; 
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CometWrapper
{
    [Serializable]
    public class cometParams
    {
        public string SequenceDatabase { get; set; }
        
        public bool IonsA { get; set; }
        public bool IonsB { get; set; }
        public bool IonsC { get; set; }
        public bool IonsX { get; set; }
        public bool IonsY { get; set; }
        public bool IonsZ { get; set; }
        public bool IonsNL { get; set; }

        public double PrecursorMassTolerance { get; set; }
        public int Enzyme { get; set; }
        public int EnzymeSpecificity { get; set; }
        public int MissedCleavages { get; set; }
        public double FragmentBinTolerance { get; set; }
        public double FragmentBinOffset { get; set; }
        public int TheoreticalFragIons { get; set; }
        public int MaxVariableModsPerPeptide { get; set; }

        public double ClearMZRangeMin { get; set; }
        public double ClearMZRangeMax { get; set; }

        public double SearchMassRangeMin { get; set; }
        public double SearchMassRangeMax { get; set; }

        public List<Modification> MyModificationItems { get; set; }

        public cometParams() { }

        public string SerializeToXML()
        {
            string theOutput = "";

            XmlSerializer xmlSerializer = new XmlSerializer(this.GetType());

            StringWriter sw = new StringWriter();

            //Remmove namespaces
            System.Xml.Serialization.XmlSerializerNamespaces xs = new XmlSerializerNamespaces();
            xs.Add("", "");
            xmlSerializer.Serialize(sw, this, xs);

            theOutput = sw.ToString();
            sw.Close();
            return theOutput;
        }


    }
}
