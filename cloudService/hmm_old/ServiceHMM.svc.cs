using PatternTools.FastaParser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace hmm3
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class HMM : IHMM
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public string Scans(List<FastaItem> theSequences)
        {
            return null;
        }

        public string Scan(string fastaSequence)
        {
            //Dump the fasta seqeuence to a file
            StreamWriter sw = new StreamWriter(@"C:\Users\pcarvalho\Desktop\pfamWorkDir\seq.txt");
            sw.WriteLine(fastaSequence);
            sw.Close();

            string applicationCall = @"hmmscan --domtblout theTable.txt --cpu 3 E:\ProteinDatabases\PFam\Pfam-A.hmm seq.txt";
            return fastaSequence;
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
