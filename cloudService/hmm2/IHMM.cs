using PatternTools.FastaParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace hmm3
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IHMM
    {

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        HMMResult[] Scan(string fastaSequence);

        [OperationContract]
        HMMResult[] Scans(Dictionary<string,string> fastaSequences);

        //[OperationContract]
        //CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: Add your service operations here
    }

}
