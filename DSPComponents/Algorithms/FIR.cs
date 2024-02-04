using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class FIR : Algorithm
    {
        public Signal InputTimeDomainSignal { get; set; }
        public FILTER_TYPES InputFilterType { get; set; }
        public float InputFS { get; set; }
        public float? InputCutOffFrequency { get; set; }
        public float? InputF1 { get; set; }
        public float? InputF2 { get; set; }
        public float InputStopBandAttenuation { get; set; }
        public float InputTransitionBand { get; set; }
        public Signal OutputHn { get; set; }
        public Signal OutputYn { get; set; }

        public override void Run()
        {
            List<float> samples = new List<float>();
            int N = 0;
            int n_max = 0;
            int n_min = 0;
            //----window---------------------------------------------------------------------------------------------
            List<double> w = new List<double>();
            List<int> index = new List<int>();
            double delta_f = InputTransitionBand / InputFS;

            if (InputStopBandAttenuation <= 21)
            {
                N = (int)(0.9 / delta_f);
                if (N % 2 == 0)
                {
                    N += 1;
                }
                else
                {
                    N += 2;
                }
                n_max = (N - 1) / 2;
                n_min = -1 * n_max;
                for (int x = n_min; x <= n_max; x++)
                {
                    w.Add(1);
                    index.Add(x);
                }
            }
            else if (InputStopBandAttenuation <= 44)
            {
                N = (int)(3.1 / delta_f);
                if (N % 2 == 0)
                {
                    N += 1;
                }
                else
                {
                    N += 2;
                }
                n_max = (N - 1) / 2;
                n_min = -1 * n_max;
                for (int x = n_min; x <= n_max; x++)
                {
                    w.Add((0.5 + (0.5 * Math.Cos(2 * Math.PI * x / N))));
                    index.Add(x);
                }
            }
            else if (InputStopBandAttenuation <= 53)
            {
                N = (int)(3.3 / delta_f);
                if (N % 2 == 0)
                {
                    N += 1;
                }
                else
                {
                    N += 2;
                }
                n_max = (N - 1) / 2;
                n_min = -1 * n_max;
                for (int x = n_min; x <= n_max; x++)
                {
                    w.Add((0.54 + (0.46 * Math.Cos(2 * Math.PI * x / N))));
                    index.Add(x);
                }
            }
            else if (InputStopBandAttenuation <= 74)
            {
                N = (int)(5.5 / delta_f);
                if (N % 2 == 0)
                {
                    N += 1;
                }
                else
                {
                    N += 2;
                }
                n_max = (N - 1) / 2;
                n_min = -1 * n_max;
                for (int x = n_min; x <= n_max; x++)
                {
                    w.Add((0.42 + (0.5 * Math.Cos(2 * Math.PI * x / (N - 1))) + (0.08 * Math.Cos(4 * Math.PI * x / (N - 1)))));
                    index.Add(x);
                }
            }
            //----hd---------------------------------------------------------------------------------------------
            List<double> hd = new List<double>();
            float? InputCutOffFrequency_low = (InputCutOffFrequency + (InputTransitionBand / 2)) / InputFS;
            float? InputCutOffFrequency_high = (InputCutOffFrequency - (InputTransitionBand / 2)) / InputFS;
            float? InputF1_pass = (InputF1 - (InputTransitionBand / 2)) / InputFS;
            float? InputF2_pass = (InputF2 + (InputTransitionBand / 2)) / InputFS;
            float? InputF1_stop = (InputF1 + (InputTransitionBand / 2)) / InputFS;
            float? InputF2_stop = (InputF2 - (InputTransitionBand / 2)) / InputFS;

            if (InputFilterType == FILTER_TYPES.LOW)
            {
                for (int n = n_min; n <= n_max; n++)
                {
                    if (n == 0)
                    {
                        hd.Add((2 * (double)InputCutOffFrequency_low));
                    }
                    else
                    {
                        hd.Add((2 * (double)InputCutOffFrequency_low * Math.Sin((n * 2 * Math.PI * (double)InputCutOffFrequency_low)) / (n * 2 * Math.PI * (double)InputCutOffFrequency_low)));
                    }
                }
            }
            else if (InputFilterType == FILTER_TYPES.HIGH)
            {
                for (int n = n_min; n <= n_max; n++)
                {
                    if (n == 0)
                    {
                        hd.Add((1 - (2 * (double)InputCutOffFrequency_high)));
                    }
                    else
                    {
                        hd.Add(((-2) * (double)InputCutOffFrequency_high * Math.Sin((n * 2 * Math.PI * (double)InputCutOffFrequency_high)) / (n * 2 * Math.PI * (double)InputCutOffFrequency_high)));
                    }
                }
            }
            else if (InputFilterType == FILTER_TYPES.BAND_PASS)
            {
                for (int n = n_min; n <= n_max; n++)
                {
                    if (n == 0)
                    {
                        hd.Add((2 * ((double)InputF2_pass - (double)InputF1_pass)));
                    }
                    else
                    {
                        hd.Add(((2) * (double)InputF2_pass * Math.Sin((n * 2 * Math.PI * (double)InputF2_pass)) / (n * 2 * Math.PI * (double)InputF2_pass))
                             - ((2) * (double)InputF1_pass * Math.Sin((n * 2 * Math.PI * (double)InputF1_pass)) / (n * 2 * Math.PI * (double)InputF1_pass)));
                    }
                }
            }
            else if (InputFilterType == FILTER_TYPES.BAND_STOP)
            {
                for (int n = n_min; n <= n_max; n++)
                {
                    if (n == 0)
                    {
                        hd.Add((1 - (2 * ((double)InputF2_stop - (double)InputF1_stop))));
                    }
                    else
                    {
                        hd.Add(((2) * (double)InputF1_stop * Math.Sin((n * 2 * Math.PI * (double)InputF1_stop)) / (n * 2 * Math.PI * (double)InputF1_stop))
                             - ((2) * (double)InputF2_stop * Math.Sin((n * 2 * Math.PI * (double)InputF2_stop)) / (n * 2 * Math.PI * (double)InputF2_stop)));
                    }
                }
            }

            List<float> result = new List<float>();
            OutputHn = new Signal(samples, false);

            for (int n = 0; n < N; n++)
            {
                result.Add((float)hd[n] * (float)w[n]);
                //Console.WriteLine("index " + index[n] + " result " + result[n]);
            }

            OutputHn.Samples = result;
            OutputHn.SamplesIndices = index;

            DirectConvolution dc = new DirectConvolution();
            dc.InputSignal1 = InputTimeDomainSignal;
            dc.InputSignal2 = OutputHn;
            dc.Run();
            OutputYn = dc.OutputConvolvedSignal;

            //-----Save To File**************************************************************************************

            string fileName = "FIR_Coefficients.txt";
            var signal_type = 0;
            FileStream file = new FileStream(fileName, FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(file);
            sw.WriteLine(signal_type.ToString());
            sw.WriteLine(Convert.ToInt32(OutputHn.Periodic).ToString());
            sw.WriteLine(N.ToString());
            for (int i = 0; i < N; i++)
                sw.WriteLine(OutputHn.SamplesIndices[i].ToString() + " " + OutputHn.Samples[i].ToString());
            sw.Close();
            file.Close();
        }
    }
}