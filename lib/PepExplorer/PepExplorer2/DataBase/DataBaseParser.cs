using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PepExplorer2.DataBase
{
    public class DataBaseParser
    {

        /// <summary>
        /// Identifier regex object
        /// </summary>
        private Regex RegIdentifier;

        /// <summary>
        /// Description regex object
        /// </summary>
        private Regex RegDescription;


        /// <summary>
        /// The main element for database parsing.
        /// </summary>
        /// <param name="dbOpt"> DataBaseOption object</param>
        public DataBaseParser(DataBaseOption dbOpt)
        {

            if (dbOpt == DataBaseOption.Generic)
            {
                RegIdentifier = new Regex(@"^>([^\s]*)", RegexOptions.Compiled);
                RegDescription = new Regex(@"\s(.*)", RegexOptions.Compiled);
            }
            else if (dbOpt == DataBaseOption.NCBI)
            {
                RegIdentifier = new Regex(@">gi\|([0-9]*)", RegexOptions.Compiled);
                RegDescription = new Regex(@"\s+(.*)", RegexOptions.Compiled);
            }
            else if (dbOpt == DataBaseOption.SwissProt)
            {
                RegIdentifier = new Regex(@"^>.+\|(.+)\|", RegexOptions.Compiled);
                RegDescription = new Regex(@"\|(.+)$", RegexOptions.Compiled);
            }


        }

        public List<DataBaseRegistry> ParseDataBase(string file)
        {

            List<DataBaseRegistry> MyFastaEntries = new List<DataBaseRegistry>();
            StreamReader sr = new StreamReader(file);
            string line;

            DataBaseRegistry ftemp = new DataBaseRegistry("-1", "-1", "-1");

            while ((line = sr.ReadLine()) != null)
            {

                if (line.Contains(">"))
                {
                    MyFastaEntries.Add(ftemp);

                    Match mIdentifier = null;
                    Match mDescription = null;

                    string identifier = null;
                    string description = null;

                    mIdentifier = RegIdentifier.Match(line);
                    mDescription = RegDescription.Match(line);


                    identifier = mIdentifier.ToString();
                    identifier = identifier.Replace(">", "");
                    identifier = identifier.Replace("gi|", "");
                    identifier = identifier.Replace("|", "");

                    description = mDescription.Value.ToString();
                    description = description.Replace(">", "");
                    description = description.Replace("|", "");

                    ftemp = new DataBaseRegistry(identifier, description, "");
                }
                else
                {
                    ftemp.Sequence += line;
                }
            }

            MyFastaEntries.RemoveAt(0);
            MyFastaEntries.Add(ftemp);

            return MyFastaEntries;
        }

    }
}
