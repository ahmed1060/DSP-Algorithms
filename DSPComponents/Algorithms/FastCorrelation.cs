using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class FastCorrelation : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public List<float> OutputNonNormalizedCorrelation { get; set; }
        public List<float> OutputNormalizedCorrelation { get; set; }

        public Signal df1;
        public Signal df2;
        List<float> real1 = new List<float>();
        List<float> imaginary1 = new List<float>();
        List<float> real2 = new List<float>();
        List<float> imaginary2 = new List<float>();
        List<Complex> comp3 = new List<Complex>();
        List<float> idftinputAmp = new List<float>();
        List<float> idftinputPhase = new List<float>();
        List<float> idftinputfreq = new List<float>();
        List<float> non_norm_correlation = new List<float>();

        List<float> norm_correlation = new List<float>();
        double summation_1 = 0;
        double summation_2 = 0;
        public override void Run()
        {
            if (InputSignal2 == null)
            {
                DiscreteFourierTransform dft1 = new DiscreteFourierTransform();
                dft1.InputTimeDomainSignal = InputSignal1;
                dft1.Run();
                df1 = dft1.OutputFreqDomainSignal;

                for (int i = 0; i < dft1.OutputFreqDomainSignal.FrequenciesAmplitudes.Count; i++)
                {
                    real1.Add((float)(df1.FrequenciesAmplitudes[i] * Math.Cos(df1.FrequenciesPhaseShifts[i])));
                    imaginary1.Add((float)(df1.FrequenciesAmplitudes[i] * Math.Sin(df1.FrequenciesPhaseShifts[i])));

                    Complex comp1 = new Complex(real1[i], (-1) * imaginary1[i]);
                    Complex comp2 = new Complex(real1[i], imaginary1[i]);
                    comp3.Add((comp1 * comp2) / InputSignal1.Samples.Count);
                }
                for (int i = 0; i < comp3.Count; i++)
                {
                    idftinputAmp.Add((float)Math.Sqrt(((comp3[i].Real) * (comp3[i].Real)) + ((comp3[i].Imaginary) * (comp3[i].Imaginary))));
                    idftinputPhase.Add((float)Math.Atan2((comp3[i].Imaginary), (comp3[i].Real)));
                    idftinputfreq.Add(i);
                }
                InverseDiscreteFourierTransform idft = new InverseDiscreteFourierTransform();
                idft.InputFreqDomainSignal = new Signal(false, idftinputfreq, idftinputAmp, idftinputPhase);
                idft.Run();
                OutputNonNormalizedCorrelation = idft.OutputTimeDomainSignal.Samples;
                non_norm_correlation = idft.OutputTimeDomainSignal.Samples;

                //**normalized * *********************************************************************************
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
                OutputNormalizedCorrelation = norm_correlation;
            }
            else
            {
                DiscreteFourierTransform dft1 = new DiscreteFourierTransform();
                dft1.InputTimeDomainSignal = InputSignal1;
                dft1.Run();
                df1 = dft1.OutputFreqDomainSignal;

                DiscreteFourierTransform dft2 = new DiscreteFourierTransform();
                dft2.InputTimeDomainSignal = InputSignal2;
                dft2.Run();
                df2 = dft2.OutputFreqDomainSignal;

                for (int i = 0; i < dft1.OutputFreqDomainSignal.FrequenciesAmplitudes.Count; i++)
                {
                    real1.Add((float)(df1.FrequenciesAmplitudes[i] * Math.Cos(df1.FrequenciesPhaseShifts[i])));
                    imaginary1.Add((float)(df1.FrequenciesAmplitudes[i] * Math.Sin(df1.FrequenciesPhaseShifts[i])));

                    real2.Add((float)(df2.FrequenciesAmplitudes[i] * Math.Cos(df2.FrequenciesPhaseShifts[i])));
                    imaginary2.Add((float)(df2.FrequenciesAmplitudes[i] * Math.Sin(df2.FrequenciesPhaseShifts[i])));

                    Complex comp1 = new Complex(real1[i], (-1) * imaginary1[i]);
                    Complex comp2 = new Complex(real2[i], imaginary2[i]);
                    comp3.Add((comp1 * comp2) / InputSignal1.Samples.Count);
                }
                for (int i = 0; i < comp3.Count; i++)
                {
                    idftinputAmp.Add((float)Math.Sqrt(((comp3[i].Real) * (comp3[i].Real)) + ((comp3[i].Imaginary) * (comp3[i].Imaginary))));
                    idftinputPhase.Add((float)Math.Atan2((comp3[i].Imaginary), (comp3[i].Real)));
                    idftinputfreq.Add(i);
                }
                InverseDiscreteFourierTransform idft = new InverseDiscreteFourierTransform();
                idft.InputFreqDomainSignal = new Signal(false,idftinputfreq,idftinputAmp,idftinputPhase);
                idft.Run();
                OutputNonNormalizedCorrelation = idft.OutputTimeDomainSignal.Samples;
                non_norm_correlation = idft.OutputTimeDomainSignal.Samples;

                //**normalized * *********************************************************************************
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
                OutputNormalizedCorrelation = norm_correlation;
            }
        }
    }
}