using PatternTools;
using PatternTools.FastaParser;
using PepExplorer2.DeNovo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace PepExplorer2.Result2
{
    [Serializable]
    public class ResultPckg2
    {
        List<string> maxParsProtList;

        public List<string> MaxParsimonyProteinList {
            get
            {
                if (maxParsProtList == null || maxParsProtList.Count == 0)
                {
                    maxParsProtList = GenerateListOfMaxParsimony();
                    return maxParsProtList;
                }
                else
                {
                    return maxParsProtList;
                }
            }
        }
        public List<FastaItem> MyFasta { get; set; }
        public ProgramArgs Arguments { get; set; }
        public List<AlignmentResult> Alignments { get; set; }
        public List<KeyValuePair<string, List<DeNovoRegistry>>> MyUnmappedRegistries {get; set;}

        public ResultPckg2() { }

        public ResultPckg2(List<FastaItem> myFasta, ProgramArgs args, List<AlignmentResult> alignments, List<KeyValuePair<string, List<DeNovoRegistry>>> unmappedRegistries) 
        {
            MyFasta = myFasta;
            Arguments = args;
            Alignments = alignments;
            MyUnmappedRegistries = unmappedRegistries;
            GenerateListOfMaxParsimony();
        }

        /// <summary>
        /// This method should only be called if we wish to update the list, otherwise use MaxPasimonyProteinList to not calculate the same thing over and over
        /// </summary>
        /// <returns></returns>
        public List<string> GenerateListOfMaxParsimony()
        {
            maxParsProtList = new List<string>();

            List<FastaItem> prots = PatternTools.ObjectCopier.Clone(MyFasta);
            List<AlignmentResult> alns = PatternTools.ObjectCopier.Clone(Alignments);


            Console.WriteLine("Maximum parsimony routine initiated for a list of " + prots.Count + " proteins and " + alns.Count + "Alignments.");

            while (alns.Count > 0)
            {
                List<KeyValuePair<FastaItem, List<AlignmentResult>>> mappings = new List<KeyValuePair<FastaItem, List<AlignmentResult>>>();

                foreach (FastaItem f in prots)
                {
                    List<AlignmentResult> aP = alns.FindAll(a => a.ProtIDs.Contains(f.SequenceIdentifier));
                    mappings.Add(new KeyValuePair<FastaItem, List<AlignmentResult>>(f, aP));
                }

                mappings.Sort((a, b) => b.Value.Count.CompareTo(a.Value.Count));

                alns = alns.Except(mappings[0].Value).ToList();
                Console.WriteLine("Alignments to go " + alns.Count);

                maxParsProtList.Add(mappings[0].Key.SequenceIdentifier);

                //To optimize this procedure we can keep checking the delta, if the delta is 1 we can stop the procedure.
            }

            maxParsProtList = maxParsProtList.Distinct().ToList();

            Console.WriteLine("MaxPars procedure concluded, final list: " + maxParsProtList.Count);


            return maxParsProtList;

        }


        /// Binary serializer.
        /// </summary>
        /// <param name="fileName">The path to save the file.</param>
        public void SerializeResultPackage(string fileName)
        {
            Stream stream = File.Open(fileName, FileMode.Create);
            BinaryFormatter bformatter = new BinaryFormatter();
            bformatter.Serialize(stream, this);
            stream.Close();
        }

        /// <summary>
        /// Binary deserializer.
        /// </summary>
        /// <param name="fileName">The path to load the file.</param>
        /// <returns>Package object</returns>
        public static ResultPckg2 DeserializeResultPackage(string fileName)
        {
            Stream stream = File.Open(fileName, FileMode.Open);
            BinaryFormatter bformatter = new BinaryFormatter();
            ResultPckg2 Result = (ResultPckg2)bformatter.Deserialize(stream);
            stream.Close();
            return Result;
        }

        public SparseMatrix GenerateSparseMatrix()
        {
            SparseMatrix sm = new SparseMatrix();

            List<int> dims = new List<int> {1, 2};
            foreach (AlignmentResult ar in Alignments)
            {
                int label = -1;

                if (!ar.ProtIDs.Exists(a => a.StartsWith("Reverse")))
                {
                    label = 1;
                }

                string cleanedSequence = PatternTools.pTools.CleanPeptide(ar.DeNovoRegistries[0].PtmSequence, true);
                List<double> values = new List<double> { ar.SimilarityScore / (double)cleanedSequence.Length , ar.DeNovoRegistries.Max(a => a.DeNovoScore) };
                sparseMatrixRow smr = new sparseMatrixRow(label, dims, values);
                smr.FileName = ar.DeNovoRegistries[0].PtmSequence;

                sm.addRow(smr);
            }

            return sm;
        }

        public int UniqueRegionCount(List<AlignmentResult> alignments)
        {
            return alignments.Count(a => a.ProtIDs.Count == 1);
        }

        public void SaveUnmappedRegistries(string fileName)
        {
            StreamWriter sw = new StreamWriter(fileName);

            MyUnmappedRegistries.Sort((a, b) => b.Value.Max(x => x.DeNovoScore).CompareTo(a.Value.Max(y => y.DeNovoScore)));

            foreach (KeyValuePair<string, List<DeNovoRegistry>> reg in MyUnmappedRegistries)
            {
                List<string> items = reg.Value.Select(a => "[" + a.FileName + "::" + a.ScanNumber + "]").ToList();
                sw.WriteLine(">" + reg.Key + " DeNovo Score: " + reg.Value.Max(a => a.DeNovoScore) + " " +string.Join(" ", items));
                sw.WriteLine(reg.Key);
            }

            sw.Close();
        }
    }
}
