using MSFileReaderLib;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PatternTools.MSParserLight
{
    public class ParserUltraLightRAW
    {
        Regex isMS1 = new Regex("Full ms ", RegexOptions.Compiled);
        Regex isMS1_1 = new Regex("Full lock ms ", RegexOptions.Compiled);
        Regex isMS2 = new Regex("Full ms2", RegexOptions.Compiled);
        Regex isMS3 = new Regex("Full ms3", RegexOptions.Compiled);

        private const PrecursorMassType PRECURSOR_MASS_TYPE = PrecursorMassType.Monoisotopic;
        private const bool ALWAYS_USE_PRECURSOR_CHARGE_STATE_RANGE = false;
        private const double HYDROGEN_MASS = 1.00782503214;
        private bool GET_PRECURSOR_MZ_AND_INTENSITY_FROM_MS1 = true;

        public ParserUltraLightRAW() { }

        /// <summary>
        /// Method responsible for parsing RAW File.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public List<MSUltraLight> ParseFile(String rawFile, short fileIndex = -1, int MsnLevel = 2, List<int> ScanNumbers = null, bool toPrint_console = true)
        {
            if (!File.Exists(rawFile))
            {
                throw new Exception("ParserUltraLightRAW error:: Unable to find the file " + rawFile);
            }

            MSFileReaderLib.IXRawfile2 raw = (MSFileReaderLib.IXRawfile2)new MSFileReader_XRawfile();
            raw.Open(rawFile);
            raw.SetCurrentController(0, 1);

            Dictionary<int, double[,]> ms1s = null;
            if (GET_PRECURSOR_MZ_AND_INTENSITY_FROM_MS1)
            {
                ms1s = new Dictionary<int, double[,]>();
            }

            int first_scan_number = -1;
            raw.GetFirstSpectrumNumber(ref first_scan_number);
            int last_scan_number = -1;
            raw.GetLastSpectrumNumber(ref last_scan_number);

            object progress_lock = new object();
            int spectra_processed = 0;
            int old_progress = 0;

            MSUltraLight[] myTMSArray = new MSUltraLight[last_scan_number + 2];

            //We will only retrieve certain scan numbers
            if (ScanNumbers == null)
            {
                ScanNumbers = new List<int>();
                for (int scan_number = first_scan_number; scan_number <= last_scan_number + 1; scan_number++)
                {
                    ScanNumbers.Add(scan_number);
                }
            }

            ScanNumbers.Sort();


            foreach (int scan_number in ScanNumbers)
            {
                spectra_processed++;
                string scan_filter = null;
                raw.GetFilterForScanNum(scan_number, ref scan_filter);

                if (String.IsNullOrEmpty(scan_filter))
                {
                    Console.WriteLine("Scan " + scan_number + " is null");
                    continue;
                }

                if (MsnLevel == 1)
                {
                    if (!isMS1.IsMatch(scan_filter))
                    {
                        if (!isMS1_1.IsMatch(scan_filter))
                        {
                            continue;
                        }
                    }
                }
                else if (MsnLevel == 2)
                {
                    if (!isMS2.IsMatch(scan_filter))
                    {
                        continue;
                    }
                }
                else if (MsnLevel == 3)
                {
                    if (!isMS3.IsMatch(scan_filter))
                    {
                        continue;
                    }
                }


                string spectrum_id = "controllerType=0 controllerNumber=1 scan=" + scan_number.ToString();

                double retention_time = double.NaN;
                raw.RTFromScanNum(scan_number, ref retention_time);

                double precursor_mz;
                double precursor_intensity;
                int precursor_scanNumber;


                double ionInjectionTime;
                int charge;

                if (MsnLevel > 1)
                {
                    GetPrecursor(ms1s, raw, scan_number, scan_filter, first_scan_number, out precursor_mz, out precursor_intensity, out precursor_scanNumber);
                    DeterminePrecursorParams(raw, scan_number, out charge, out ionInjectionTime);
                }
                else
                {
                    charge = -1;
                    precursor_mz = -1;
                    precursor_intensity = -1;
                }

                double[,] label_data = GetFragmentationData(raw, scan_number, scan_filter);

                List<Tuple<float, float>> peaks = new List<Tuple<float, float>>(label_data.GetLength(1));
                for (int peak_index = label_data.GetLowerBound(1); peak_index <= label_data.GetUpperBound(1); peak_index++)
                {
                    // Calculate index in list of ions object
                    int index = peak_index - label_data.GetLowerBound(1);

                    // Get fragment ion m/z and intensity from RAW file
                    float mz = (float)label_data[(int)RawLabelDataColumn.MZ, peak_index];
                    float intensity = (float)label_data[(int)RawLabelDataColumn.Intensity, peak_index];

                    // Create new ion object
                    peaks.Add(new Tuple<float, float>(mz, intensity));
                }

                if (peaks.Count > 0)
                {
                    MSUltraLight ms = new MSUltraLight((float)retention_time,
                                                                scan_number,
                                                                peaks,
                                                                new List<Tuple<float, short>>() { new Tuple<float, short>((float)precursor_mz, (short)charge) },
                                                                (float)precursor_intensity,
                                                                1, //Instrument type
                                                                (short)MsnLevel,
                                                                fileIndex);

                    myTMSArray[scan_number] = ms;
                }

                lock (progress_lock)
                {
                    int new_progress = (int)((double)spectra_processed / (last_scan_number - first_scan_number + 1) * 100);
                    if (new_progress > old_progress)
                    {
                        old_progress = new_progress;
                        if (toPrint_console)
                        {
                            int currentLineCursor = Console.CursorTop;
                            Console.SetCursorPosition(0, Console.CursorTop);
                            Console.Write(" Reading RAW File: " + old_progress + "%");
                            Console.SetCursorPosition(0, currentLineCursor);
                        }
                        else
                            Console.Write(" Reading RAW File: " + old_progress + "%");
                    }

                }
            }

            Console.Write(" Reading RAW File: 100%");
            return myTMSArray.Where(a => a != null).ToList();
        }

        private float[] FilterPeaks(List<float> mZsList, List<float> intensitiesList, double absoluteThreshold, double relativeThresholdPercent, int maximumNumberOfPeaks, out float[] intensitiesArray)
        {
            //List<Ion> filtered_peaks = new List<Ion>(peaks);
            if (mZsList.Count != intensitiesList.Count)
            {
                intensitiesArray = null;
                return null;
            }
            double relative_threshold = -1.0;
            if (relativeThresholdPercent > 0.0)
            {
                double max_intensity = -1.0;
                foreach (float intensity in intensitiesList)
                {
                    //double intensity = peak.Intensity;
                    if (intensity > max_intensity)
                    {
                        max_intensity = intensity;
                    }
                }
                relative_threshold = max_intensity * relativeThresholdPercent / 100.0;
            }

            double threshold = Math.Max(absoluteThreshold, relative_threshold);

            int p = 0;
            while (p < intensitiesList.Count)
            {
                float intensity = intensitiesList[p];
                //Ion peak = filtered_peaks[p];
                if (intensity < threshold)
                {
                    intensitiesList.RemoveAt(p);
                    mZsList.RemoveAt(p);
                }
                else
                {
                    p++;
                }
            }

            if (maximumNumberOfPeaks > 0 && intensitiesList.Count > maximumNumberOfPeaks)
            {
                intensitiesList.RemoveRange(maximumNumberOfPeaks, intensitiesList.Count - maximumNumberOfPeaks);
                mZsList.RemoveRange(maximumNumberOfPeaks, mZsList.Count - maximumNumberOfPeaks);
            }

            intensitiesArray = intensitiesList.ToArray();
            return mZsList.ToArray();
        }


        private double[,] GetFragmentationData(IXRawfile2 raw, int scanNumber, string scanFilter)
        {
            double[,] data;
            if (scanFilter.Contains("FTMS"))
            {
                object labels_obj = null;
                object flags_obj = null;
                raw.GetLabelData(ref labels_obj, ref flags_obj, ref scanNumber);
                data = (double[,])labels_obj;
            }
            else
            {
                double centroid_peak_width = double.NaN;
                object mass_list = null;
                object peak_flags = null;
                int array_size = -1;
                raw.GetMassListFromScanNum(scanNumber, null, 0, 0, 0, 1, ref centroid_peak_width, ref mass_list, ref peak_flags, ref array_size);
                data = (double[,])mass_list;
            }

            return data;
        }

        private void DeterminePrecursorParams(IXRawfile2 raw, int scanNumber, out int charge, out double ionInjectTime)
        {
            object trailer_labels_obj = null;
            object trailer_values_obj = null;
            int trailer_array_size = -1;
            raw.GetTrailerExtraForScanNum(scanNumber, ref trailer_labels_obj, ref trailer_values_obj, ref trailer_array_size);
            string[] trailer_labels = (string[])trailer_labels_obj;
            string[] trailer_values = (string[])trailer_values_obj;

            charge = -1;
            ionInjectTime = -1.0;
            for (int trailer_index = trailer_labels.GetLowerBound(0); trailer_index <= trailer_labels.GetUpperBound(0); trailer_index++)
            {
                if (trailer_labels[trailer_index].StartsWith("Charge"))
                {
                    charge = int.Parse(trailer_values[trailer_index]);
                }
                if (trailer_labels[trailer_index].Contains("Ion Injection"))
                {
                    ionInjectTime = double.Parse(trailer_values[trailer_index]);
                }
            }
        }

        private void GetPrecursor(IDictionary<int, double[,]> ms1s, IXRawfile2 raw, int scanNumber, string scanFilter, int firstScanNumber, out double mz, out double intensity, out int precursor_ScanNumber)
        {
            precursor_ScanNumber = -1;
            if (PRECURSOR_MASS_TYPE == PrecursorMassType.Isolation)
            {
                mz = GetIsolationMZ(scanFilter);
            }
            else if (PRECURSOR_MASS_TYPE == PrecursorMassType.Monoisotopic)
            {
                mz = GetMonoisotopicMZ(raw, scanNumber, scanFilter);
            }
            if (GET_PRECURSOR_MZ_AND_INTENSITY_FROM_MS1)
            {
                GetAccurateMZAndIntensity(ms1s, raw, scanNumber, firstScanNumber, ref mz, out intensity, out precursor_ScanNumber);
            }
            else
            {
                intensity = double.NaN;
            }
        }

        private bool GetAccurateMZAndIntensity(IDictionary<int, double[,]> ms1s, IXRawfile2 raw, int scanNumber, int firstScanNumber, ref double mz, out double intensity, out int precursor_scanNumber)
        {
            scanNumber--;
            precursor_scanNumber = -1;
            while (scanNumber >= firstScanNumber)
            {
                string scan_filter = null;
                raw.GetFilterForScanNum(scanNumber, ref scan_filter);

                if (scan_filter.Contains(" ms "))
                {
                    precursor_scanNumber = scanNumber;

                    double[,] ms1;
                    lock (ms1s)
                    {
                        if (!ms1s.TryGetValue(scanNumber, out ms1))
                        {
                            if (scan_filter.Contains("FTMS"))
                            {
                                object labels_obj = null;
                                object flags_obj = null;
                                raw.GetLabelData(ref labels_obj, ref flags_obj, ref scanNumber);
                                ms1 = (double[,])labels_obj;
                            }
                            else
                            {
                                double centroid_peak_width = double.NaN;
                                object mass_list = null;
                                object peak_flags = null;
                                int array_size = -1;
                                raw.GetMassListFromScanNum(scanNumber, null, 0, 0, 0, 1, ref centroid_peak_width, ref mass_list, ref peak_flags, ref array_size);
                                ms1 = (double[,])mass_list;
                            }
                            ms1s.Add(scanNumber, ms1);

                        }
                    }

                    int index = -1;
                    for (int i = ms1.GetLowerBound(1); i <= ms1.GetUpperBound(1); i++)
                    {
                        if (index < 0 || Math.Abs(ms1[0, i] - mz) < Math.Abs(ms1[0, index] - mz))
                        {
                            index = i;
                        }
                    }
                    if (index >= 0)
                    {
                        mz = ms1[0, index];
                        intensity = ms1[1, index];
                        return true;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    scanNumber--;
                }
            }

            intensity = double.NaN;
            return false;
        }

        private double GetMonoisotopicMZ(IXRawfile2 raw, int scanNumber, string scanFilter)
        {
            object labels_obj = null;
            object values_obj = null;
            int array_size = -1;
            raw.GetTrailerExtraForScanNum(scanNumber, ref labels_obj, ref values_obj, ref array_size);
            string[] labels = (string[])labels_obj;
            string[] values = (string[])values_obj;
            for (int i = labels.GetLowerBound(0); i <= labels.GetUpperBound(0); i++)
            {
                if (labels[i].StartsWith("Monoisotopic M/Z"))
                {
                    double monoisotopic_mz = double.Parse(values[i], CultureInfo.InvariantCulture);
                    if (monoisotopic_mz > 0.0)
                    {
                        return monoisotopic_mz;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return GetIsolationMZ(scanFilter);
        }

        private double GetIsolationMZ(string scanFilter)
        {
            string temp_scan_filter = "";
            temp_scan_filter = scanFilter.Substring(0, scanFilter.LastIndexOf('@'));

            while (temp_scan_filter.Contains('@'))
            {
                temp_scan_filter = temp_scan_filter.Substring(0, temp_scan_filter.LastIndexOf('@'));
            }

            double isolation_mz = double.Parse(temp_scan_filter.Substring(temp_scan_filter.LastIndexOf(' ') + 1), CultureInfo.InvariantCulture);

            return isolation_mz;
        }

        internal enum PrecursorMassType
        {
            Isolation,
            Monoisotopic
        }

        internal enum RawLabelDataColumn
        {
            MZ = 0,
            Intensity = 1,
            Resolution = 2,
            NoiseBaseline = 3,
            NoiseLevel = 4,
            Charge = 5
        }
    }
}
