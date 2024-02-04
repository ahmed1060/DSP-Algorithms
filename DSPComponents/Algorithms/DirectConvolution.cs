using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class DirectConvolution : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public Signal OutputConvolvedSignal { get; set; }

        /// <summary>
        /// Convolved InputSignal1 (considered as X) with InputSignal2 (considered as H)
        /// </summary>
        public override void Run()
        {
            List<float> output = new List<float>();
            List<int> indcies = new List<int>();
            for (int i = 0; i < InputSignal1.Samples.Count; i++)
            {
                for (int j = 0; j < InputSignal2.Samples.Count; j++)
                {
                    int sum_of_boundry = i + j;
                    if (sum_of_boundry < output.Count)
                    {
                        output[sum_of_boundry] += (InputSignal2.Samples[j] * InputSignal1.Samples[i]);
                    }
                    else
                    {
                        output.Add(InputSignal2.Samples[j] * InputSignal1.Samples[i]);
                    }
                }
            }
            if (output[output.Count - 1] == 0)
            {
                output.RemoveAt(output.Count - 1);
            }
            OutputConvolvedSignal = new Signal(output, false);
            int min = InputSignal1.SamplesIndices.Min() + InputSignal2.SamplesIndices.Min();
            for (int i = 0; i < output.Count; i++)
            {
                indcies.Add(min);
                min++;
            }
            OutputConvolvedSignal.SamplesIndices = indcies;
        }
    }
}
