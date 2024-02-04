using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Folder : Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal OutputFoldedSignal { get; set; }
        public override void Run()
        {
            InputSignal.Samples.Reverse();

            for (int i = 0; i < InputSignal.Samples.Count; i++)
            {
                InputSignal.SamplesIndices[i] = InputSignal.SamplesIndices[i] * (-1);
            
               // System.Console.WriteLine("indicis" + InputSignal.SamplesIndices[i] + "signal" + InputSignal.Samples[i]);
            }
            InputSignal.SamplesIndices.Reverse();

            if(InputSignal.Periodic == true) { InputSignal.Periodic = false; }
            else { InputSignal.Periodic = true;}

            OutputFoldedSignal = InputSignal;
        }
    }
}
