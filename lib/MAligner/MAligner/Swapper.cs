using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAligner.MAligner
{
    public static class Swapper
    {
        public static List< char[]> Swap(char[] theString)
        {
            List<char[]> theResult = new List<char[]>(theString.Length);
            List<char[]> theNewResult = new List<char[]>();

            theResult.Add(theString);
            for (int i = 1; i < theString.Length; i++)
            {
                if (!theString[i - 1].Equals(theString[i]))
                {
                    theResult.Add(SwapAtPos(theString, i));
                }
            }

            return theResult;
        }

        static char[] SwapAtPos(char[] theEntry, int pos)
        {
            char[] EntryClone = (char[])theEntry.Clone();
            EntryClone[pos] = theEntry[pos - 1];
            EntryClone[pos - 1] = theEntry[pos];
            return EntryClone;
        }
    }
}
