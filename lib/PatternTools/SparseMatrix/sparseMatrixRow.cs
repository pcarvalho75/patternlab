/*
 * Created by SharpDevelop.
 * User: paulo
 * Date: 1/14/2007
 * Time: 12:05 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace PatternTools
{
    [Serializable]
	public class sparseMatrixRow {
		int lable;

        List<int> dims = new List<int>();
        List<double> values = new List<double>();
        double extraParam;
        public string FileName {get; set;}
		
		
        //Constructors----------------------------------------
        public sparseMatrixRow (int lable) {
			this.lable = lable;
		}

        public sparseMatrixRow(int lable, List<int> dims, List<double> values)
        {
            this.lable = lable;
            this.dims = dims;
            this.values = values;
        }

        public sparseMatrixRow(int lable, List<int> dims, List<float> values)
        {
            this.lable = lable;
            this.dims = dims;
            this.values = values.Select(a => (double)a).ToList();
        }

        public sparseMatrixRow(int lable, List<int> dims, List<double> values, string fileName)
        {
            this.lable = lable;
            this.dims = dims;
            this.values = values;
            this.FileName = fileName;
        }

        public double ExtraParam {
            get { return extraParam; }
            set { extraParam = value; }
        }

        public double getValueForDim(int dim)
        {
            double value = 0;
            //Find the index
            try {
                int index = this.dims.IndexOf(dim);
                return (this.values[index]);

            } catch {
                return value;
            }

        }

        public sparseMatrixRow Clone () {

            sparseMatrixRow r = new sparseMatrixRow(lable);

            List<int> dimsN = new List<int>();
            List<double> valuesN = new List<double>();
            for (int i = 0; i < dims.Count; i++)
            {
                dimsN.Add(dims[i]);
                valuesN.Add(values[i]);
            }

            r.dims = dimsN;
            r.values = valuesN;
            r.FileName = this.FileName;

            return (r);
        }

        /// <summary>
        /// Returns the sum of the values in the values array
        /// </summary>
        /// <returns></returns>
        public double totalValuesCount()
        {
            double result = 0;
            for (int i = 0; i < values.Count; i++)
            {
                result += values[i];
            }
            return(result);
        }


        /// <summary>
        /// Transforms this input vector to a vector of norm 1
        /// </summary>
        public void ConvertToUnitVector()
        {
            double denominator = 0;

            for (int i = 0; i < values.Count; i++)
            {
                denominator += values[i] * values[i];
            }
            denominator = Math.Sqrt(denominator);

            for (int i = 0; i < values.Count; i++)
            {
                values[i] /= denominator;
            }
        }

        public void sortVector()
        {
            SortedDictionary<int, double> tsd = new SortedDictionary<int, double>();
            for (int i = 0; i < values.Count; i++)
            {
                tsd.Add(dims[i], values[i]);
            }

            dims.Clear();
            values.Clear();

            foreach (KeyValuePair<int, double> kvp in tsd)
            {
                dims.Add(kvp.Key);
                values.Add(kvp.Value);
            }

        }
        //---------------------------------------------------

        public List<double> Values
        {
			get { return values; }
			set { values = value; }
		}

        public List<int> Dims {
            get { return dims; }
            set { dims = value; }
        }
		
		public int Lable {
			get { return lable; }
			set { lable = value; }
        }


        public void RemoveDim(int d)
        {
            int index = dims.IndexOf(d);
            dims.RemoveAt(index);
            values.RemoveAt(index);
        }
    } // end struct
}
