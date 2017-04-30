using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using SEPRPackage;
using PatternTools.SQTParser;
using PatternTools;

namespace Regrouper.GroupingElements.Parser
{
    public abstract class ParserBase
    {
        public List<ResultEntry> MyResultPackages { get; set; }
        public List<DirectoryClassDescription> MyDirectoryDescriptionDictionary { get; set; }
        public Parameters MyParameters { get; set; }




        public void ParseDirs(List<DirectoryClassDescription> myDirectoryDescriptionDictionary)
        {

            MyDirectoryDescriptionDictionary = myDirectoryDescriptionDictionary;
            MyResultPackages = new List<ResultEntry>();

            foreach (DirectoryClassDescription dcd in myDirectoryDescriptionDictionary)
            {
                //Get all Sepro Files in this and in deeper directories
                //FileInfo[] fileInfo = dcd.MyDirectory.GetFiles("*.sepr");
                
                FileInfo[] fileInfo = new DirectoryInfo(dcd.MyDirectoryFullName).GetFiles("*.sepr", SearchOption.AllDirectories);

                foreach (FileInfo file in fileInfo)
                {
                    //First lets unserialize the object
                    Console.WriteLine("Loading .. "+ file.FullName);
                    ResultPackage rp = ResultPackage.Load(file.FullName);

                    //Lets free um some ram
                    foreach (SQTScan s in rp.MyProteins.AllSQTScans) {
                        s.MSLight = null;
                    }

                    MyResultPackages.Add(new ResultEntry(rp, file, dcd.ClassLabel));
                }

                Console.WriteLine("Done loading.");
            }
        }

        public void ParseDir(DirectoryClassDescription dir)
        {
            MyDirectoryDescriptionDictionary = new List<DirectoryClassDescription>();
            MyDirectoryDescriptionDictionary.Add(dir);
            MyResultPackages = new List<ResultEntry>();

            FileInfo[] fileInfo = new DirectoryInfo(dir.MyDirectoryFullName).GetFiles("*.sepr", SearchOption.AllDirectories);

            foreach (FileInfo file in fileInfo)
            {
                //First lets unserialize the object
                ResultPackage rp = ResultPackage.Load(file.FullName);

                //Lets free um some ram
                foreach (SQTScan s in rp.MyProteins.AllSQTScans)
                {
                    s.MSLight = null;
                }

                MyResultPackages.Add(new ResultEntry(rp, file, dir.ClassLabel));
            }

        }
    }
}
