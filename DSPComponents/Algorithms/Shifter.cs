using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Shifter : Algorithm
    {
        public Signal InputSignal { get; set; }
        public int ShiftingValue { get; set; }
        public Signal OutputShiftedSignal { get; set; }

        public override void Run()
        {
            if (InputSignal.Periodic == false)
            {
                for (int i = 0; i < InputSignal.Samples.Count; i++)
                {
                    InputSignal.SamplesIndices[i] = InputSignal.SamplesIndices[i] - ShiftingValue;
                    //System.Console.WriteLine(shifted_indicies_list[i]);
                }
            }
            else if (InputSignal.Periodic == true)
            {
                for (int i = 0; i < InputSignal.Samples.Count; i++)
                {
                    InputSignal.SamplesIndices[i] = InputSignal.SamplesIndices[i] + ShiftingValue;
                    //System.Console.WriteLine(shifted_indicies_list[i]);
                }
            }
            
            OutputShiftedSignal = InputSignal;
        }
    }
}