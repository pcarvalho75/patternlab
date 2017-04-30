using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLP
{
    class Program
    {
        static void Main(string[] args)
        {
            PatternLabProject ppl = new PatternLabProject(@"C:\Users\paulo_000\Desktop\Test\SparseMatrix.txt", @"C:\Users\paulo_000\Desktop\Test\Index.txt", "MyDescription");
            ppl.Save(@"C:\Users\paulo_000\Desktop\Test\my.ppl");

            //PatternLabProject ppl = new PatternLabProject(@"C:\Users\paulo_000\Desktop\Test\my.ppl");

            //ppl.MySparseMatrix.saveMatrix(@"C:\Users\paulo_000\Desktop\Test\m.txt" );
            //ppl.MyIndex.WriteGPI(@"C:\Users\paulo_000\Desktop\Test\i.txt");
            //Console.WriteLine("Done.");
        }
    }
}
