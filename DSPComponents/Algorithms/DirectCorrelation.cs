using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class DirectCorrelation : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public List<float> OutputNonNormalizedCorrelation { get; set; }
        public List<float> OutputNormalizedCorrelation { get; set; }

        public override void Run()
        {
            List<float> non_norm_correlation = new List<float>();
            List<float> shifted_signal = new List<float>();
            float summation = 0;

            if (InputSignal1.Periodic == true)
            {
                if (InputSignal2 == null)
                {
                    for (int j = 0; j < InputSignal1.Samples.Count; j++)
                    {
                        shifted_signal.Clear();
                        int shifting_num = j;
                        //shifting-----------------------------------------------------------
                        for (int i = 0; i < InputSignal1.Samples.Count - shifting_num; i++)
                        {
                            shifted_signal.Add(InputSignal1.Samples[i + shifting_num]);
                        }
                        if (shifting_num > 0)
                        {
                            for (int i = InputSignal1.Samples.Count - shifting_num, counter = 0; i < InputSignal1.Samples.Count; i++, counter++)
                            {
                                shifted_signal.Add(InputSignal1.Samples[0 + counter]);
                            }
                        }
                        //-------------------------------------------------------------------
                        for (int n = 0; n < InputSignal1.Samples.Count; n++)
                        {
                            summation += InputSignal1.Samples[n]
                                        * shifted_signal[n];
                        }
                        non_norm_correlation.Add(summation / InputSignal1.Samples.Count);
                        summation = 0;
                    }
                }
                else
                {
                    for (int j = 0; j < InputSignal1.Samples.Count; j++)
                    {
                        shifted_signal.Clear();
                        int shifting_num = j;
                        //shifting-----------------------------------------------------------
                        for (int i = 0; i < InputSignal2.Samples.Count - shifting_num; i++)
                        {
                            shifted_signal.Add(InputSignal2.Samples[i + shifting_num]);
                        }
                        if (shifting_num > 0)
                        {
                            for (int i = InputSignal2.Samples.Count - shifting_num, counter = 0; i < InputSignal2.Samples.Count; i++, counter++)
                            {
                                shifted_signal.Add(InputSignal2.Samples[0 + counter]);
                            }
                        }
                        //-------------------------------------------------------------------
                        for (int n = 0; n < InputSignal1.Samples.Count; n++)
                        {
                            summation += InputSignal1.Samples[n]
                                        * shifted_signal[n];
                        }
                        non_norm_correlation.Add(summation / InputSignal1.Samples.Count);
                        summation = 0;
                    }
                }
                OutputNonNormalizedCorrelation = non_norm_correlation;
            }
            //====================================================================================================================================================================
            else if (InputSignal1.Periodic == false)
            {
                if (InputSignal2 == null)
                {
                    for (int j = 0; j < InputSignal1.Samples.Count; j++)
                    {
                        shifted_signal.Clear();
                        int shifting_num = j;
                        //shifting-----------------------------------------------------------
                        for (int i = 0; i < InputSignal1.Samples.Count - shifting_num; i++)
                        {
                            shifted_signal.Add(InputSignal1.Samples[i + shifting_num]);
                        }
                        if (shifting_num > 0)
                        {
                            for (int i = InputSignal1.Samples.Count - shifting_num; i < InputSignal1.Samples.Count; i++)
                            {
                                shifted_signal.Add(0);
                            }
                        }
                        //-------------------------------------------------------------------
                        for (int n = 0; n < InputSignal1.Samples.Count; n++)
                        {
                            summation += InputSignal1.Samples[n]
                                        * shifted_signal[n];
                        }
                        non_norm_correlation.Add(summation / InputSignal1.Samples.Count);
                        summation = 0;
                    }
                }
                else
                {
                    for (int j = 0; j < InputSignal1.Samples.Count; j++)
                    {
                        shifted_signal.Clear();
                        int shifting_num = j;
                        //shifting-----------------------------------------------------------
                        for (int i = 0; i < InputSignal2.Samples.Count - shifting_num; i++)
                        {
                            shifted_signal.Add(InputSignal2.Samples[i + shifting_num]);
                        }
                        if (shifting_num > 0)
                        {
                            for (int i = InputSignal2.Samples.Count - shifting_num; i < InputSignal2.Samples.Count; i++)
                            {
                                shifted_signal.Add(0);
                            }
                        }
                        //-------------------------------------------------------------------
                        for (int n = 0; n < InputSignal1.Samples.Count; n++)
                        {
                            summation += InputSignal1.Samples[n]
                                        * shifted_signal[n];
                        }
                        non_norm_correlation.Add(summation / InputSignal1.Samples.Count);
                        summation = 0;
                    }
                }
                OutputNonNormalizedCorrelation = non_norm_correlation;
            }
            //Normalized+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            List<float> norm_correlation = new List<float>();
            double summation_1 = 0;
            double summation_2 = 0;
            if (InputSignal1.Periodic == true)
            {
                if (InputSignal2 == null)
                {
                    for (int j = 0; j < InputSignal1.Samples.Count; j++)
                    {
                        for (int n = 0; n < InputSignal1.Samples.Count; n++)
                        {
                            summation_1 += (InputSignal1.Samples[n] * InputSignal1.Samples[n]);
                            summation_2 += (InputSignal1.Samples[n] * InputSignal1.Samples[n]);
                        }
                        norm_correlation.Add((InputSignal1.Samples.Count * non_norm_correlation[j]) / (float)Math.Sqrt(summation_1 * summation_2));
                        summation_1 = 0;
                        summation_2 = 0;
                    }
                }
                else
                {
                    for (int j = 0; j < InputSignal1.Samples.Count; j++)
                    {
                        for (int n = 0; n < InputSignal1.Samples.Count; n++)
                        {
                            summation_1 += (InputSignal1.Samples[n] * InputSignal1.Samples[n]);
                            summation_2 += (InputSignal2.Samples[n] * InputSignal2.Samples[n]);
                        }
                        norm_correlation.Add((InputSignal1.Samples.Count * non_norm_correlation[j]) / (float)Math.Sqrt(summation_1 * summation_2));
                        summation_1 = 0;
                        summation_2 = 0;
                    }
                }
                OutputNormalizedCorrelation = norm_correlation;
            }
            //====================================================================================================================================================================
            else if (InputSignal1.Periodic == false)
            {
                if (InputSignal2 == null)
                {
                    for (int j = 0; j < InputSignal1.Samples.Count; j++)
                    {
                        for (int n = 0; n < InputSignal1.Samples.Count; n++)
                        {
                            summation_1 += (InputSignal1.Samples[n] * InputSignal1.Samples[n]);
                            summation_2 += (InputSignal1.Samples[n] * InputSignal1.Samples[n]);
                        }
                        norm_correlation.Add((InputSignal1.Samples.Count * non_norm_correlation[j]) / (float)Math.Sqrt(summation_1 * summation_2));
                        summation_1 = 0;
                        summation_2 = 0;
                    }
                }
                else
                {
                    for (int j = 0; j < InputSignal1.Samples.Count; j++)
                    {
                        for (int n = 0; n < InputSignal1.Samples.Count; n++)
                        {
                            summation_1 += (InputSignal1.Samples[n] * InputSignal1.Samples[n]);
                            summation_2 += (InputSignal2.Samples[n] * InputSignal2.Samples[n]);
                        }
                        norm_correlation.Add((InputSignal1.Samples.Count * non_norm_correlation[j]) / (float)Math.Sqrt(summation_1 * summation_2));
                        summation_1 = 0;
                        summation_2 = 0;
                    }
                }
                OutputNormalizedCorrelation = norm_correlation;
            }
        }
    }
}