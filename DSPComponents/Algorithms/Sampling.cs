using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSPAlgorithms.Algorithms
{
    public class Sampling : Algorithm
    {
        public int L { get; set; } //upsampling factor
        public int M { get; set; } //downsampling factor
        public Signal InputSignal { get; set; }
        public Signal OutputSignal { get; set; }
        public override void Run()
        {
            if (L > 0 && M == 0)
            {
                //up sample by L factor and then apply low pass filter
                List<float> afterup = new List<float>();
                for (int i = 0; i < InputSignal.Samples.Count + ((L - 1) * (InputSignal.Samples.Count - 1)); i++)
                {
                    int numofzeros = L - 1;
                    afterup.Add(InputSignal.Samples[i]);
                    if (i == (InputSignal.Samples.Count - 1))
                    {
                        break;
                    }
                    for (int j = 0; j < numofzeros; j++)
                    {
                        afterup.Add(0);
                    }
                }

                Signal mySignal = new Signal(afterup, false);
                FIR b = new FIR();
                b.InputTimeDomainSignal = mySignal;
                b.InputFilterType = DSPAlgorithms.DataStructures.FILTER_TYPES.LOW;
                b.InputFS = 8000;
                b.InputStopBandAttenuation = 50;
                b.InputCutOffFrequency = 1500;
                b.InputTransitionBand = 500;
                b.Run();
                OutputSignal = b.OutputYn;
            }
            else if (L == 0 && M > 0)
            {
                //apply filter first and thereafter down sample by M factor
                List<float> afterdown = new List<float>();
                FIR b = new FIR();
                b.InputTimeDomainSignal = InputSignal;
                b.InputFilterType = DSPAlgorithms.DataStructures.FILTER_TYPES.LOW;
                b.InputFS = 8000;
                b.InputStopBandAttenuation = 50;
                b.InputCutOffFrequency = 1500;
                b.InputTransitionBand = 500;
                b.Run();
                for (int i = 0; i < b.OutputYn.Samples.Count; i += M)
                {
                    afterdown.Add(b.OutputYn.Samples[i]);
                }
                Signal mySignal = new Signal(afterdown, false);
                OutputSignal = mySignal;
            }
            else if (L > 0 && M > 0)
            {
                // up sample by L factor and then apply filter first and thereafter down sample by M factor
                List<float> afterdown = new List<float>();
                List<float> afterup = new List<float>();
                for (int i = 0; i < InputSignal.Samples.Count + ((L - 1) * (InputSignal.Samples.Count - 1)); i++)
                {
                    int numofzeros = L - 1;
                    afterup.Add(InputSignal.Samples[i]);
                    if (i == (InputSignal.Samples.Count - 1))
                    {
                        break;
                    }
                    for (int j = 0; j < numofzeros; j++)
                    {
                        afterup.Add(0);
                    }
                }
                Signal mySignal = new Signal(afterup, false);
                FIR b = new FIR();
                b.InputTimeDomainSignal = mySignal;
                b.InputFilterType = DSPAlgorithms.DataStructures.FILTER_TYPES.LOW;
                b.InputFS = 8000;
                b.InputStopBandAttenuation = 50;
                b.InputCutOffFrequency = 1500;
                b.InputTransitionBand = 500;
                b.Run();
                for (int i = 0; i < b.OutputYn.Samples.Count; i += M)
                {
                    afterdown.Add(b.OutputYn.Samples[i]);
                }
                Signal mySigna2 = new Signal(afterdown, false);
                OutputSignal = mySigna2;
            }
            else if (L == 0 && M == 0)
            {
                throw new Exception("can't have both values as 0");
            }
        }
    }
}