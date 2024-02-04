using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSPAlgorithms.Algorithms
{
    public class DC_Component: Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal OutputSignal { get; set; }

        public override void Run()
        {
            float sum = 0;
            float mean = 0;
            
            List<float> out_put = new List<float>();
            
            for (int i= 0; i < InputSignal.Samples.Count; i++)
            {
                sum += InputSignal.Samples[i];
            }

            mean = sum / InputSignal.Samples.Count;

            for(int j =0; j < InputSignal.Samples.Count; j++)
            {
                out_put.Add(InputSignal.Samples[j] - mean);
            }

            OutputSignal = new Signal(out_put, InputSignal.Periodic);
        }
    }
}
