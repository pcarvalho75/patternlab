using MAligner.MAligner;
using PatternTools.FastaParser;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace MAligner.MCompress
{
    public class Compress
    {
        public FastaFileParser MyFastaSequences { get; set; }

        public Compress() 
        { }


        public void CompressByIdentity(FastaFileParser myFastaFileParser, double Identity, string saveDB = null)
        {
            if (Identity > 1)
            {
                throw new Exception("Maximum allowed identity = 1");
            }

            MyFastaSequences = myFastaFileParser;

            Console.WriteLine("The original DB has " + MyFastaSequences.MyItems.Count + " sequences.");

            MyFastaSequences.MyItems.Sort((a, b) => a.Sequence.Length.CompareTo(b.Sequence.Length));

            Console.WriteLine("Converting sequences to byte array.");
            MyFastaSequences.IncludeByteSequences();
            Console.WriteLine("Finished converting to byte array.");
            
            MAligner.Aligner ma = new Aligner();

            ConcurrentDictionary<FastaItem, List<FastaItem>> toExclude = new ConcurrentDictionary<FastaItem, List<FastaItem>>(); //The ID to exlclude and the items it matches

            ParallelOptions options = new ParallelOptions();
            options.MaxDegreeOfParallelism = Environment.ProcessorCount * 2;

            Console.WriteLine("Limiting number of threads to {0}.", options.MaxDegreeOfParallelism );

            int eliminated = 0;
            int count = 0;

            Parallel.For(0, MyFastaSequences.MyItems.Count - 1, options, i =>
            //for (int i = 0; i < fp.MyItems.Count - 1; i++) 
            {
                Interlocked.Increment(ref count);
                Console.WriteLine("Testing sequence no {0} ", count);
                //Determine speed;
                int speed = 1;
                if (MyFastaSequences.MyItems[i].Sequence.Length >= 6)
                {
                    speed = (int)Math.Round((double)MyFastaSequences.MyItems[i].Sequence.Length / 6) + 1;
                }

                List<FastaItem> matching = new List<FastaItem>();
                for (int j = i + 1; j < MyFastaSequences.MyItems.Count; j++)
                {

                    //i is peptide, j is protein

                    int identity = ma.IDScore(MyFastaSequences.MyItems[i].SequenceInBytes, MyFastaSequences.MyItems[j].SequenceInBytes, speed);
                    double approximateIdentityPercentage = (double)identity / ((double)MyFastaSequences.MyItems[i].SequenceInBytes.Length / (double)speed);

                    if (approximateIdentityPercentage >= Identity * 0.75)
                    {
                        int identityFull = ma.IDScore(MyFastaSequences.MyItems[i].SequenceInBytes, MyFastaSequences.MyItems[j].SequenceInBytes, 1);
                        double identityPercentage = (double)identityFull / (double)MyFastaSequences.MyItems[i].SequenceInBytes.Length;
                        if (identityPercentage >= Identity)
                        {
                            matching.Add(MyFastaSequences.MyItems[j]);
                        }
                    }

                }

                if (matching.Count > 0)
                {
                    toExclude.TryAdd(MyFastaSequences.MyItems[i], matching);
                    Interlocked.Increment(ref eliminated);
                    Console.WriteLine("Sequences eliminated {0} ", eliminated);
                }
               
            }
            );

            //Reduce the DB
            Console.WriteLine();
            Console.WriteLine("Reducing DB.");

            List<FastaItem> holders = toExclude.Keys.ToList();

            holders.Sort((a,b) => a.Sequence.Length.CompareTo(b.Sequence.Length));


            foreach (KeyValuePair<FastaItem, List<FastaItem>> kvp in toExclude)
            {

                foreach (FastaItem fiMatches in kvp.Value)
                {
                    fiMatches.Description += " Includes: " + kvp.Key.SequenceIdentifier;
                }
            }

            //Eliminate sequences from original DB
            MyFastaSequences.MyItems = MyFastaSequences.MyItems.Except(holders).ToList();

            Console.WriteLine("Sequences eliminated: " + eliminated + " | " + "DB size with " + Identity + ": " + ((count+1) - eliminated)  + " | " + "DB Size: " + (count+1));

            if (saveDB != null)
            {
                MyFastaSequences.SaveDB(saveDB);
                Console.WriteLine("Done saving DB to disk");
            }
           
        }


        public void CompressByIdentity(string fastaDB, double Identity, string saveDB = null) 
        {

            if (Identity > 1)
            {
                throw new Exception("Maximum allowed identity = 1");
            }


            MyFastaSequences = new PatternTools.FastaParser.FastaFileParser();
            MyFastaSequences.ParseFile(new StreamReader(fastaDB), false, PatternTools.FastaParser.DBTypes.IDSpaceDescription);

            CompressByIdentity(MyFastaSequences, Identity, saveDB);

        }
 
    }

}
