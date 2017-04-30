using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PatternTools.MSParser;
using PatternTools.SQTParser;

namespace PatternTools.XIC
{
    [Serializable]
    public class IonCluster
    {
        decimal trapezoidalIntegration = -1;
        public List<SequenceHit> Sequences { get; set; }
        public List<IonLight> MyIons { get; set; }
        public double ReferenceMass { get; set; }
        public List<SQTLight> MyScans { get; set; }
        public int Z { get; set; }
        List<double> retTimeVector;
        List<double> intensityVector;
        List<MSFull> mySpectra;

        public List<MSFull> MySpectra
        {

            get
            {
                if (mySpectra == null)
                {
                    mySpectra = new List<MSFull>();
                }
                return mySpectra;
            }
            set
            {
                mySpectra = value;
            }
        }

        public List<double> RetTimeVector
        {
            get
            {
                if (retTimeVector == null)
                {
                    Quant();
                }
                else if (retTimeVector.Count == 0)
                {
                    Quant();
                }

                return retTimeVector;
            }
        }

        public List<double> IntensityVector
        {
            get
            {
                if (intensityVector == null)
                {
                    Quant();
                }
                else if (intensityVector.Count == 0)
                {
                    Quant();
                }

                return intensityVector;
            }
        }

        public decimal TrapezoidalIntegration
        {
            get
            {

                if (trapezoidalIntegration == -1)
                {
                    Quant();
                }

                return trapezoidalIntegration;
            }

        }

        public void Quant()
        {
            List<PatternTools.Point> points = new List<PatternTools.Point>(MyIons.Count);

            intensityVector = new List<double>(MyIons.Count);
            retTimeVector = new List<double>(MyIons.Count);

            try
            {
                foreach (IonLight i in MyIons)
                {
                    points.Add(new PatternTools.Point(i.RetentionTime, i.Intensity));
                    intensityVector.Add(i.Intensity);
                    retTimeVector.Add(i.RetentionTime);
                }

                trapezoidalIntegration = Math.Round((decimal)PatternTools.pTools.TrapezoidalIntegration(points), 1);
            }
            catch
            {
                trapezoidalIntegration = -1;
            }
        }


        public int MS1Count
        {
            get
            {
                return MyIons.Count;
            }
        }


        public IonCluster()
        {
            Sequences = new List<SequenceHit>();
            MyScans = new List<SQTLight>();
        }


        public IonCluster(IonLight thisIon)
        {
            Sequences = new List<SequenceHit>();
            ReferenceMass = thisIon.MZ;
            MyScans = new List<SQTLight>();
            MyIons = new List<IonLight>() { thisIon };
        }

        /// <summary>
        /// To be depricated
        /// </summary>
        /// <param name="theIons"></param>
        public IonCluster(List<Ion> ions)
        {
            Sequences = new List<SequenceHit>();
            MyScans = new List<SQTLight>();
            List<IonLight> theIons = ions.Select(a => new IonLight(a.MZ, a.Intensity, a.RetentionTime, a.ScanNumber)).ToList();
            theIons.Sort((a, b) => b.Intensity.CompareTo(a.Intensity));
            ReferenceMass = Math.Round(theIons[0].MZ, 5);
            theIons.Sort((a, b) => a.RetentionTime.CompareTo(b.RetentionTime));
            MyIons = theIons;
        }

        public IonCluster(IonLight thisIon, double refMass)
        {
            Sequences = new List<SequenceHit>();
            ReferenceMass = refMass;
            MyScans = new List<SQTLight>();
            MyIons = new List<IonLight>() { thisIon };
        }


        public IonCluster(List<IonLight> theIons)
        {
            Sequences = new List<SequenceHit>();
            MyScans = new List<SQTLight>();
            int index = theIons.FindIndex(a => a.Intensity == theIons.Max(b => b.Intensity));
            ReferenceMass = Math.Round(theIons[index].MZ, 5);
            MyIons = theIons;
            Clean2();

        }

        /// <summary>
        /// If your started an empty cluster you will need to initiate all new clusters
        /// </summary>
        /// <param name="thisIon"></param>
        public void StartCluster(IonLight thisIon)
        {
            Sequences = new List<SequenceHit>();
            ReferenceMass = thisIon.MZ;
            MyIons = new List<IonLight>() { thisIon };
        }

        /// <summary>
        /// if there are points in the same scan number, we only stay with the highest
        /// </summary>
        public void Clean2()
        {
            List<IonLight> tmpIons = new List<IonLight>(MyIons.Count) { MyIons[0] };

            for (int i = 1; i < MyIons.Count; i++)
            {
                if (MyIons[i].ScanNumber == tmpIons.Last().ScanNumber)
                {
                    if (MyIons[i].Intensity > tmpIons.Last().Intensity)
                    {
                        tmpIons[tmpIons.Count - 1] = MyIons[i];
                    }
                }
                else
                {
                    tmpIons.Add(MyIons[i]);
                }
            }

            retTimeVector = null;
            intensityVector = null;
            MyIons = tmpIons;
        }


    }
}
