using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using PatternTools;

namespace Regrouper.GroupingElements.Parser
{
    public interface IParser
    {
        Parameters MyParameters { get; set; }
        //List<ResultEntry> MyResultPackages { get; set; }
        List<DirectoryClassDescription> MyDirectoryDescriptionDictionary { get; set; }

        void ParseDirs(List<DirectoryClassDescription> dirs);
        void SavePatternLabProjectFile(string fileName, string projectDescription);
        void ProcessParsedData();
        
    }
}
