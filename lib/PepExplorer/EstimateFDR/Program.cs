using PatternTools;
using PepExplorer2.Result;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EstimateFDR
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            if (true)
            {
                string heatMatrixFile = @"C:\Users\pcarvalho\Desktop\HeatMatrix.txt";
                string line = "";

                StreamReader sr = new StreamReader(heatMatrixFile);

                List<List<double>> list = new List<List<double>>();


                while ((line = sr.ReadLine()) != null)
                {
                    List<double> l = Regex.Split(line, "[,|\t]").ToList().Select(a => double.Parse(a)).ToList();

                    //non-linear manipulation
                    for (int i = 0; i < l.Count; i++)
                    {
                        l[i] = Math.Log(1800 - l[i]);
                    }
                    list.Add(l);
                }

                HeatMap heat = new HeatMap();
                heat.MyHeatMap.MyMatrix = list;
                double min = list.Min(a => a.Min());
                double max = list.Min(a => a.Max());

                heat.MyHeatMap.Plot(min, ((max - min) / 2) + min, max, 500, 500);
                //heat.MyHeatMap.Plot(min, min, max, 500, 500);

                heat.ShowDialog();


            } 
            else if (false)
            {

                StreamWriter file = new StreamWriter(@"C:\Users\lepre_000\Workspace\pepGridResult\heatMap\map-1.txt");

                file.WriteLine("open" + "\t" + "extend" + "\t" + "no charge separation" + "\t" + "with charge separation");

                string[] fileList = Directory.GetFiles(@"C:\Users\lepre_000\SkyDrive\PepExplorerFiles\Results\PFUPCC\result", "*.pex");

                Regex regex = new Regex(@"open_(\d{1,2})_extend_(\d{1,2})_tophit_(\d)");

                List<int> openValues = new List<int>();
                List<int> extendValues = new List<int>();
                List<int> topValues = new List<int>();

                foreach (string s in fileList)
                {
                    FileInfo fi = new FileInfo(s);
                    string fileName = fi.Name;

                    Match match = regex.Match(fileName);

                    openValues.Add(Convert.ToInt16(match.Groups[1].Value));
                    extendValues.Add(Convert.ToInt16(match.Groups[2].Value));
                    topValues.Add(Convert.ToInt16(match.Groups[3].Value));
                }

                openValues.Sort();
                extendValues.Sort();
                topValues.Sort();

                double[,] heatMatrix = new double[openValues.Distinct().Count(), extendValues.Distinct().Count()];

                foreach (string s in fileList)
                {
                    ResultPackage rp = ResultPackage.DeserializeResultPackage(s);

                    FileInfo fi = new FileInfo(s);
                    string fileName = fi.Name;

                    Console.Write("processando arquivo " + fileName + "... ");

                    int ProteinsNoChargeSeparation = rp.FilterRegistries(0.05, false).Count;
                    int ProteinsChargeSeparation = rp.FilterRegistries(0.05, true).Count;

                    Match match = regex.Match(fileName);
                    if (match.Success)
                    {
                        int open = int.Parse(match.Groups[1].Value);
                        int extend = int.Parse(match.Groups[2].Value);
                        int topHit = int.Parse(match.Groups[3].Value);

                        int x = openValues.Distinct().ToList().IndexOf(open);
                        int y = extendValues.Distinct().ToList().IndexOf(extend);

                        heatMatrix[x, y] = ProteinsChargeSeparation;

                        file.WriteLine(match.Groups[1].Value + "\t" + match.Groups[2].Value + "\t" + ProteinsNoChargeSeparation + "\t" + ProteinsChargeSeparation );                            
                        file.Flush();

                    }
                    else
                    {
                        Console.WriteLine("Error during file name parsing");
                    }

                    Console.WriteLine("OK");
                }

                StreamWriter sw = new StreamWriter(@"C:\Users\lepre_000\Workspace\pepGridResult\heatMap\HeatMatrix.txt");

                for (int i = 0; i < openValues.Distinct().Count(); i++)
                {
                    for (int j = 0; j < extendValues.Distinct().Count(); j++)
                    {
                        sw.Write(heatMatrix[i, j] + "\t");
                    }
                    sw.WriteLine("");
                }

                sw.Close();
            }
            else
            {

                List<StreamReader> streamReaderList = new List<StreamReader>();

                StreamReader sr1 = new StreamReader(@"C:\Users\lepre_000\Workspace\pepGridResult\heatMap\map-1.csv");
                StreamReader sr2 = new StreamReader(@"C:\Users\lepre_000\Workspace\pepGridResult\heatMap\map-2.csv");
                StreamReader sr3 = new StreamReader(@"C:\Users\lepre_000\Workspace\pepGridResult\heatMap\map-3.csv");

                streamReaderList.Add(sr1);
                //streamReaderList.Add(sr2);
                //streamReaderList.Add(sr3);

                List<double> tuple;
                List<List<double>> listOfLists = new List<List<double>>();

                string line;

                double[,] matrix = new double[10, 10];
                foreach (StreamReader sr in streamReaderList)
                {
                    while ((line = sr.ReadLine()) != null)
                    {                        
                        string[] values = line.Split('\t');

                        tuple = new List<double>();
                        int v1 = int.Parse(values[0]);
                        int v2 = int.Parse(values[1]);
                        double v3 = double.Parse(values[2]);

                        tuple.Add(v1);
                        tuple.Add(v2);
                        tuple.Add(v3);
                        matrix[v1-1, v2-1] = v3;

                        listOfLists.Add(tuple);
                    }
                    sr.Close();
                }

                listOfLists.Clear();

                double min = double.MaxValue;
                double max = double.MinValue;

                for (int i = 0; i < 9; i++) {
                    List<double> l = new List<double>();
                    for (int j = 0; j <9; j++) {
                        double n = matrix[i, j];
                        l.Add(n);
                        if (n < min)
                        {
                            min = n;
                        }

                        if (n > max)
                        {
                            max = n;
                        }

                    }
                    listOfLists.Add(l);
                }

                HeatMap heat = new HeatMap();
                heat.MyHeatMap.MyMatrix = listOfLists;
                heat.MyHeatMap.Plot(min, ((max - min)/ 2)+min, max, 500, 500);

                heat.ShowDialog();

                

            }

            Console.WriteLine(".");
        }


    }
}
