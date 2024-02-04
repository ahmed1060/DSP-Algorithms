using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;

namespace DSPAlgorithms.Algorithms
{
    public class SinCos: Algorithm
    {
        public string type { get; set; }
        public float A { get; set; }
        public float PhaseShift { get; set; }
        public float AnalogFrequency { get; set; }
        public float SamplingFrequency { get; set; }
        public List<float> samples { get; set; }
        public override void Run()
        {
            float sin_Output = 0;
            float cos_Output = 0;

            samples = new List<float>();

            if (type == "sin")
            {
                for (int i = 0; i < SamplingFrequency; i++)
                {
                    sin_Output = A * (float)Math.Sin(((2 * (Math.PI) * (i) * AnalogFrequency) / (SamplingFrequency)) + PhaseShift);

                    samples.Add(sin_Output);
                }
                
            }
            else if (type == "cos")
            {
                for (int j = 0; j < SamplingFrequency; j++)
                {
                    cos_Output = A * (float)Math.Cos(((2 * (Math.PI) * (j) * AnalogFrequency) / (SamplingFrequency)) + PhaseShift);

                    samples.Add(cos_Output);
                    //System.Console.WriteLine(cos_Output +"is added in"+j);
                }
            }
            
        }
    }
}
