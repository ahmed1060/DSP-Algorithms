﻿using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace DSPAlgorithms.Algorithms
{
    public class PracticalTask2 : Algorithm
    {
        public String SignalPath { get; set; }
        public float Fs { get; set; }
        public float miniF { get; set; }
        public float maxF { get; set; }
        public float newFs { get; set; }
        public int L { get; set; } //upsampling factor
        public int M { get; set; } //downsampling factor
        public Signal OutputFreqDomainSignal { get; set; }

public override void Run()
        {
            Signal InputSignal = LoadSignal(SignalPath);
            //****1----------------------------------------------------------------------------------------
            //--display--inputsignal

            //****2----------------------------------------------------------------------------------------
            FIR x = new FIR();
            x.InputTimeDomainSignal = InputSignal;
            x.InputFilterType = FILTER_TYPES.BAND_PASS;
            x.InputF1 = miniF;
            x.InputF2 = maxF;
            x.InputFS = Fs;

            x.InputStopBandAttenuation = 50;
            x.InputTransitionBand = 500;
            x.Run();
            //save-----
            string fullpath = "E:/5th Semester/Digital Signal Processing/Labs/Lab 8/Practical exam Task/Practical task 2/result.ds";
            using (StreamWriter writer = new StreamWriter(fullpath))
            {
                writer.WriteLine(1); // freq or time
                writer.WriteLine(0); // periodic or no
                writer.WriteLine(x.OutputYn.Samples.Count);
                for (int i = 0; i < x.OutputYn.Samples.Count; i++)
                {
                    writer.Write(x.OutputYn.SamplesIndices[i]);
                    writer.Write("");
                    writer.Write(x.OutputYn.Samples[i]);
                    writer.WriteLine("");
                }
            }

            //****3----------------------------------------------------------------------------------------
            if (newFs >= (2*maxF))
            {
                Sampling samp = new Sampling();
                samp.InputSignal = x.OutputYn;
                samp.L = L;
                samp.M = M;
                samp.Run();
                //save-----
                //string fullpath_2 = "E:/5th Semester/Digital Signal Processing/Labs/Lab 8/Practical exam Task/Practical task 2/result_2.ds";
                //using (StreamWriter writer = new StreamWriter(fullpath_2))
                //{
                //    writer.WriteLine(1); // freq or time
                //    writer.WriteLine(0); // periodic or no
                //    writer.WriteLine(samp.OutputSignal.Frequencies.Count);
                //    for (int i = 0; i < x.OutputYn.Frequencies.Count; i++)
                //    {
                //        writer.Write(samp.OutputSignal.Frequencies[i]);
                //        writer.Write("");
                //        writer.Write(samp.OutputSignal.FrequenciesAmplitudes[i]);
                //        writer.Write(" ");
                //        writer.WriteLine(samp.OutputSignal.FrequenciesPhaseShifts[i]);
                //    }
                //}

                //****4----------------------------------------------------------------------------------------
                DC_Component dc_c = new DC_Component();
                dc_c.InputSignal =samp.OutputSignal;
                dc_c.Run();
                //save-----
                //string fullpath_3 = "E:/5th Semester/Digital Signal Processing/Labs/Lab 8/Practical exam Task/Practical task 2/result_3.ds";
                //using (StreamWriter writer = new StreamWriter(fullpath_3))
                //{
                //    //writer.Flush();
                //    writer.WriteLine(1); // freq or time
                //    writer.WriteLine(dc_c.OutputSignal.Periodic); // periodic or no
                //    writer.WriteLine(dc_c.OutputSignal.Frequencies.Count);
                //    for (int i = 0; i < x.OutputYn.Frequencies.Count; i++)
                //    {
                //        writer.Write(dc_c.OutputSignal.Frequencies[i]);
                //        writer.Write("");
                //        writer.Write(dc_c.OutputSignal.FrequenciesAmplitudes[i]);
                //        writer.Write(" ");
                //        writer.WriteLine(dc_c.OutputSignal.FrequenciesPhaseShifts[i]);
                //    }
                //}

                //****5----------------------------------------------------------------------------------------
                //--display--4

                //****6----------------------------------------------------------------------------------------
                Normalizer n = new Normalizer();
                n.InputSignal = dc_c.OutputSignal;
                n.InputMaxRange = 1;
                n.InputMinRange = -1;
                n.Run();
                //save-----
                //string fullpath_4 = "E:/5th Semester/Digital Signal Processing/Labs/Lab 8/Practical exam Task/Practical task 2/result_4.ds";
                //using (StreamWriter writer = new StreamWriter(fullpath_4))
                //{
                //    //writer.Flush();
                //    writer.WriteLine(1); // freq or time
                //    writer.WriteLine(n.OutputNormalizedSignal.Periodic); // periodic or no
                //    writer.WriteLine(n.OutputNormalizedSignal.Frequencies.Count);
                //    for (int i = 0; i < x.OutputYn.Frequencies.Count; i++)
                //    {
                //        writer.Write(n.OutputNormalizedSignal.Frequencies[i]);
                //        writer.Write("");
                //        writer.Write(n.OutputNormalizedSignal.FrequenciesAmplitudes[i]);
                //        writer.Write(" ");
                //        writer.WriteLine(n.OutputNormalizedSignal.FrequenciesPhaseShifts[i]);
                //    }
                //}

                //****7----------------------------------------------------------------------------------------
                //--display--6

                //****8----------------------------------------------------------------------------------------
                DiscreteFourierTransform dft = new DiscreteFourierTransform();
                dft.InputSamplingFrequency =Fs;
                dft.InputTimeDomainSignal = n.OutputNormalizedSignal;
                dft.Run();
                //save-----
                //string fullpath_5 = "E:/5th Semester/Digital Signal Processing/Labs/Lab 8/Practical exam Task/Practical task 2/result_5.ds";
                //using (StreamWriter writer = new StreamWriter(fullpath_5))
                //{
                //    //writer.Flush();
                //    writer.WriteLine(1); // freq or time
                //    writer.WriteLine(dft.OutputFreqDomainSignal.Periodic); // periodic or no
                //    writer.WriteLine(dft.OutputFreqDomainSignal.Frequencies.Count);
                //    for (int i = 0; i < x.OutputYn.Frequencies.Count; i++)
                //    {
                //        writer.Write(dft.OutputFreqDomainSignal.Frequencies[i]);
                //        writer.Write("");
                //        writer.Write(dft.OutputFreqDomainSignal.FrequenciesAmplitudes[i]);
                //        writer.Write(" ");
                //        writer.WriteLine(dft.OutputFreqDomainSignal.FrequenciesPhaseShifts[i]);
                //    }
                //}

                //****9----------------------------------------------------------------------------------------
                //--display--8

                OutputFreqDomainSignal = dft.OutputFreqDomainSignal;

                //for (int i = 0; i < OutputFreqDomainSignal.Frequencies.Count; i++)
                //{
                //    Console.WriteLine(OutputFreqDomainSignal.Frequencies[i]);
                //}
            }
            else
            {
                System.Console.WriteLine("newFs is not valid");
                //save-----
                //update_file(samp.OutputSignal, 0, 0);

                //****4----------------------------------------------------------------------------------------
                DC_Component dc_c = new DC_Component();
                dc_c.InputSignal = x.OutputYn;
                dc_c.Run();
                //save-----
                //string fullpath_3 = "E:/5th Semester/Digital Signal Processing/Labs/Lab 8/Practical exam Task/Practical task 2/result_3.ds";
                //using (StreamWriter writer = new StreamWriter(fullpath_3))
                //{
                //    //writer.Flush();
                //    writer.WriteLine(1); // freq or time
                //    writer.WriteLine(dc_c.OutputSignal.Periodic); // periodic or no
                //    writer.WriteLine(dc_c.OutputSignal.Frequencies.Count);
                //    for (int i = 0; i < x.OutputYn.Frequencies.Count; i++)
                //    {
                //        writer.Write(dc_c.OutputSignal.Frequencies[i]);
                //        writer.Write("");
                //        writer.Write(dc_c.OutputSignal.FrequenciesAmplitudes[i]);
                //        writer.Write(" ");
                //        writer.WriteLine(dc_c.OutputSignal.FrequenciesPhaseShifts[i]);
                //    }
                //}

                //****5----------------------------------------------------------------------------------------
                //--display--4

                //****6----------------------------------------------------------------------------------------
                Normalizer n = new Normalizer();
                n.InputSignal = dc_c.OutputSignal;
                n.InputMaxRange = 1;
                n.InputMinRange = -1;
                n.Run();
                //save-----
                //string fullpath_4 = "E:/5th Semester/Digital Signal Processing/Labs/Lab 8/Practical exam Task/Practical task 2/result_4.ds";
                //using (StreamWriter writer = new StreamWriter(fullpath_4))
                //{
                //    //writer.Flush();
                //    writer.WriteLine(1); // freq or time
                //    writer.WriteLine(n.OutputNormalizedSignal.Periodic); // periodic or no
                //    writer.WriteLine(n.OutputNormalizedSignal.Frequencies.Count);
                //    for (int i = 0; i < x.OutputYn.Frequencies.Count; i++)
                //    {
                //        writer.Write(n.OutputNormalizedSignal.Frequencies[i]);
                //        writer.Write("");
                //        writer.Write(n.OutputNormalizedSignal.FrequenciesAmplitudes[i]);
                //        writer.Write(" ");
                //        writer.WriteLine(n.OutputNormalizedSignal.FrequenciesPhaseShifts[i]);
                //    }
                //}

                //****7----------------------------------------------------------------------------------------
                //--display--6

                //****8----------------------------------------------------------------------------------------
                DiscreteFourierTransform dft = new DiscreteFourierTransform();
                dft.InputSamplingFrequency =Fs;
                dft.InputTimeDomainSignal = n.OutputNormalizedSignal;
                dft.Run();
                //save-----
                //string fullpath_5 = "E:/5th Semester/Digital Signal Processing/Labs/Lab 8/Practical exam Task/Practical task 2/result_5.ds";
                //using (StreamWriter writer = new StreamWriter(fullpath_5))
                //{
                //    //writer.Flush();
                //    writer.WriteLine(1); // freq or time
                //    writer.WriteLine(dft.OutputFreqDomainSignal.Periodic); // periodic or no
                //    writer.WriteLine(dft.OutputFreqDomainSignal.Frequencies.Count);
                //    for (int i = 0; i < x.OutputYn.Frequencies.Count; i++)
                //    {
                //        writer.Write(dft.OutputFreqDomainSignal.Frequencies[i]);
                //        writer.Write("");
                //        writer.Write(dft.OutputFreqDomainSignal.FrequenciesAmplitudes[i]);
                //        writer.Write(" ");
                //        writer.WriteLine(dft.OutputFreqDomainSignal.FrequenciesPhaseShifts[i]);
                //    }
                //}

                //****9----------------------------------------------------------------------------------------
                //--display--8

                OutputFreqDomainSignal = dft.OutputFreqDomainSignal;
            }

            
        }

        public Signal LoadSignal(string filePath)
        {
            Stream stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            var sr = new StreamReader(stream);

            var sigType = byte.Parse(sr.ReadLine());
            var isPeriodic = byte.Parse(sr.ReadLine());
            long N1 = long.Parse(sr.ReadLine());

            List<float> SigSamples = new List<float>(unchecked((int)N1));
            List<int> SigIndices = new List<int>(unchecked((int)N1));
            List<float> SigFreq = new List<float>(unchecked((int)N1));
            List<float> SigFreqAmp = new List<float>(unchecked((int)N1));
            List<float> SigPhaseShift = new List<float>(unchecked((int)N1));

            if (sigType == 1)
            {
                SigSamples = null;
                SigIndices = null;
            }

            for (int i = 0; i < N1; i++)
            {
                if (sigType == 0 || sigType == 2)
                {
                    var timeIndex_SampleAmplitude = sr.ReadLine().Split();
                    SigIndices.Add(int.Parse(timeIndex_SampleAmplitude[0]));
                    SigSamples.Add(float.Parse(timeIndex_SampleAmplitude[1]));
                }
                else
                {
                    var Freq_Amp_PhaseShift = sr.ReadLine().Split();
                    SigFreq.Add(float.Parse(Freq_Amp_PhaseShift[0]));
                    SigFreqAmp.Add(float.Parse(Freq_Amp_PhaseShift[1]));
                    SigPhaseShift.Add(float.Parse(Freq_Amp_PhaseShift[2]));
                }
            }

            if (!sr.EndOfStream)
            {
                long N2 = long.Parse(sr.ReadLine());

                for (int i = 0; i < N2; i++)
                {
                    var Freq_Amp_PhaseShift = sr.ReadLine().Split();
                    SigFreq.Add(float.Parse(Freq_Amp_PhaseShift[0]));
                    SigFreqAmp.Add(float.Parse(Freq_Amp_PhaseShift[1]));
                    SigPhaseShift.Add(float.Parse(Freq_Amp_PhaseShift[2]));
                }
            }

            stream.Close();
            return new Signal(SigSamples, SigIndices, isPeriodic == 1, SigFreq, SigFreqAmp, SigPhaseShift);
        }
    }
}
