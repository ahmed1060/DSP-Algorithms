using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class FastConvolution : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public Signal OutputConvolvedSignal { get; set; }

        /// <summary>
        /// Convolved InputSignal1 (considered as X) with InputSignal2 (considered as H)
        /// </summary>
        public override void Run()
        {
            List<float> amp = new List<float>();
            List<float> phase = new List<float>();
            List<float> real = new List<float>();
            List<float> imag = new List<float>();
            List<float> amp2 = new List<float>();
            List<float> phase2 = new List<float>();
            List<float> real2 = new List<float>();
            List<float> imag2 = new List<float>();
            List<float> amp3 = new List<float>();
            List<float> phase3 = new List<float>();

            int boundry_of_loop = InputSignal1.Samples.Count + InputSignal2.Samples.Count - 1;

            for (int i = InputSignal1.Samples.Count; i < boundry_of_loop; i++)
            {
                InputSignal1.Samples.Add(0);
            }
            for (int i = InputSignal2.Samples.Count; i < boundry_of_loop; i++)
            {
                InputSignal2.Samples.Add(0);
            }
            DiscreteFourierTransform dft1 = new DiscreteFourierTransform();
            dft1.InputTimeDomainSignal = InputSignal1;
            dft1.Run();
            amp = dft1.OutputFreqDomainSignal.FrequenciesAmplitudes;
            phase = dft1.OutputFreqDomainSignal.FrequenciesPhaseShifts;

            DiscreteFourierTransform dft2 = new DiscreteFourierTransform();
            dft2.InputTimeDomainSignal = InputSignal2;
            dft2.Run();
            amp2 = dft2.OutputFreqDomainSignal.FrequenciesAmplitudes;
            phase2 = dft2.OutputFreqDomainSignal.FrequenciesPhaseShifts;

            List<Complex> comp3 = new List<Complex>();
            for (int i = 0; i < dft2.OutputFreqDomainSignal.FrequenciesAmplitudes.Count; i++)
            {
                real.Add((float)(amp[i] * Math.Cos(phase[i])));
                imag.Add((float)(amp[i] * Math.Sin(phase[i])));
                real2.Add((float)(amp2[i] * Math.Cos(phase2[i])));
                imag2.Add((float)(amp2[i] * Math.Sin(phase2[i])));
                
                Complex comp1 = new Complex(real[i], imag[i]);
                Complex comp2 = new Complex(real2[i], imag2[i]);
                comp3.Add(comp1 * comp2);
            }
            for (int i = 0; i < comp3.Count; i++)
            {
                amp3.Add((float)Math.Sqrt(((comp3[i].Real)*(comp3[i].Real))+((comp3[i].Imaginary) * (comp3[i].Imaginary))));
                phase3.Add((float)Math.Atan2((comp3[i].Imaginary), (comp3[i].Real)));
            }
            List<float> freq= new List<float>();
            for(int i=0; i < freq.Count; i++)
            {
                freq.Add(0);
            }
            InverseDiscreteFourierTransform idft = new InverseDiscreteFourierTransform();
            idft.InputFreqDomainSignal = new Signal(false, freq, amp3, phase3);   
            idft.Run();

            OutputConvolvedSignal = new Signal(idft.OutputTimeDomainSignal.Samples, false);
        }
    }
}
