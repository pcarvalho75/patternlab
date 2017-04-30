using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PatternTools.Deisotoping
{
    public class SignalGenerator
    {
        List<Spline3> regressors;

        public SignalGenerator()
        {

            regressors = new List<Spline3> {
                new Spline3(GetModel(Properties.Resources.splinePeak0)),
                new Spline3(GetModel(Properties.Resources.splinePeak1)),
                new Spline3(GetModel(Properties.Resources.splinePeak2)),
                new Spline3(GetModel(Properties.Resources.splinePeak3)),
                new Spline3(GetModel(Properties.Resources.splinePeak4)),
                new Spline3(GetModel(Properties.Resources.splinePeak5)),
                new Spline3(GetModel(Properties.Resources.splinePeak6)),
                new Spline3(GetModel(Properties.Resources.splinePeak7)),
                new Spline3(GetModel(Properties.Resources.splinePeak8)),
                new Spline3(GetModel(Properties.Resources.splinePeak9)),
                new Spline3(GetModel(Properties.Resources.splinePeak10)),
                new Spline3(GetModel(Properties.Resources.splinePeak11)),
                new Spline3(GetModel(Properties.Resources.splinePeak12)),
                new Spline3(GetModel(Properties.Resources.splinePeak13)),
                new Spline3(GetModel(Properties.Resources.splinePeak14)),
                new Spline3(GetModel(Properties.Resources.splinePeak15)),
                new Spline3(GetModel(Properties.Resources.splinePeak16)),
                new Spline3(GetModel(Properties.Resources.splinePeak17)),
                new Spline3(GetModel(Properties.Resources.splinePeak18)),
                new Spline3(GetModel(Properties.Resources.splinePeak19)),
                new Spline3(GetModel(Properties.Resources.splinePeak20)),
                new Spline3(GetModel(Properties.Resources.splinePeak21)),
                new Spline3(GetModel(Properties.Resources.splinePeak22)),
                new Spline3(GetModel(Properties.Resources.splinePeak23)),
                new Spline3(GetModel(Properties.Resources.splinePeak24))

            };


        }

        private double[] GetModel(string p)
        {
            string[] lines = Regex.Split(p, "\n");

            double[] model = new double[lines.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                model[i] = double.Parse(lines[i]);
            }

            return model;

        }

        public List<double> GetSignal(int numPeaks, double mass, double minIntensity)
        {

            if (numPeaks > 25)
            {
                throw (new Exception("Maximum of nine peaks"));
            }

            double[] signal = new double[numPeaks + 1];

            if (mass > 30000)
            {
                mass = 30000;
            }
            else if (mass < 500)
            {
                mass = 500;
            }

            int peakCounter = 0;
            for (int i = 0; i < 25; i++)
            {
                double kResult = regressors[i].splineinterpolation(mass);
                if (kResult < 0)
                {
                    kResult = 0;
                }
                else if (kResult > 1)
                {
                    kResult = 1;
                }
                signal[i] = kResult;
                peakCounter++;
                if (peakCounter > numPeaks)
                {
                    break;
                }
            }
            List<double> result = new List<double>(signal);
            result.RemoveAll((a) => a < minIntensity);
            return (result);                                                                                                                  
        }
    }
}
