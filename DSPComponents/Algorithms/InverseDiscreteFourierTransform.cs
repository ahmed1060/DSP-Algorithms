using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using System.Numerics; 
using DSPAlgorithms.DataStructures; 
namespace DSPAlgorithms.Algorithms 
{ 
    public class InverseDiscreteFourierTransform : Algorithm 
    { 
        public Signal InputFreqDomainSignal { get; set; } 
        public Signal OutputTimeDomainSignal { get; set; } 

        public override void Run() 
        { 
            float v = (1f / InputFreqDomainSignal.FrequenciesAmplitudes.Count); ; 
            
              List<float> sum_of_n = new List<float>();
              for (int n = 0; n < InputFreqDomainSignal.FrequenciesAmplitudes.Count; n++)
              { 
               Complex x_of_n = new Complex(0, 0);
                  for (int k = 0; k < InputFreqDomainSignal.FrequenciesAmplitudes.Count(); k++) 
                  { 
                      Complex x_of_k = new Complex(0, 0);
                      float theta = (float)(2 * Math.PI * k * n) / InputFreqDomainSignal.FrequenciesAmplitudes.Count;
                      x_of_k = new Complex(InputFreqDomainSignal.FrequenciesAmplitudes[k] * Math.Cos(InputFreqDomainSignal.FrequenciesPhaseShifts[k]), InputFreqDomainSignal.FrequenciesAmplitudes[k] * Math.Sin(InputFreqDomainSignal.FrequenciesPhaseShifts[k]));
                      x_of_n += v * x_of_k * new Complex(Math.Cos(theta), Math.Sin(theta));
                  } 
                  sum_of_n.Add((float)x_of_n.Magnitude);
              } 
            OutputTimeDomainSignal = new Signal(sum_of_n, false); 
        }
    } 
}