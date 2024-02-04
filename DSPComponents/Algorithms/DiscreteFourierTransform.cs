using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;
using System.Numerics;
using System.Diagnostics;

namespace DSPAlgorithms.Algorithms
{
    public class DiscreteFourierTransform : Algorithm
    {
        public Signal InputTimeDomainSignal { get; set; }
        public float InputSamplingFrequency { get; set; }
        public Signal OutputFreqDomainSignal { get; set; }

        public List<float> amplitude_list = new List<float>();
        public List<float> phaseshift_list = new List<float>();
        public List<float> frequency_list = new List<float>();
        public override void Run()
        {
            double sumreal = 0;
            double sumcoeff = 0;
            amplitude_list = new List<float>();
            phaseshift_list = new List<float>();
            frequency_list = new List<float>();


            for (int k = 0; k < InputTimeDomainSignal.Samples.Count; k++)
            {
                for (int n = 0; n < InputTimeDomainSignal.Samples.Count; n++)
                {
                    double theta = (k * 2 * Math.PI * n / InputTimeDomainSignal.Samples.Count);
                    double real_num = (InputTimeDomainSignal.Samples[n]*Math.Cos(theta));
                    double coeff = ((-1)*InputTimeDomainSignal.Samples[n]*Math.Sin(theta));
                    
                    sumreal += real_num;
                    sumcoeff += coeff;
                }

                // FrequenciesAmplitudes ***Done***
                double amplitude = Math.Sqrt( (sumreal*sumreal) + Math.Pow(sumcoeff, 2));
                amplitude_list.Add((float)amplitude);

                // FrequenciesPhaseShifts ***Done***
                //double sigma = (2*Math.PI*InputSamplingFrequency)/ InputTimeDomainSignal.Samples.Count;
                double phaseshift = Math.Atan2(sumcoeff , sumreal);
                //if (phaseshift < 0)
                //{
                //    phaseshift = phaseshift + sigma;
                //}
                //else if(phaseshift > 0)
                //{
                //    phaseshift = phaseshift - sigma; ;
                //}
                phaseshift_list.Add((float)phaseshift);

                frequency_list.Add(k);

                sumreal = 0;
                sumcoeff = 0;
            }

            OutputFreqDomainSignal = new Signal(InputTimeDomainSignal.Periodic,frequency_list,amplitude_list, phaseshift_list);
        }
    }
}
