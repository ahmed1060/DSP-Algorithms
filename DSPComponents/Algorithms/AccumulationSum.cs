﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;


namespace DSPAlgorithms.Algorithms
{
    public class AccumulationSum : Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal OutputSignal { get; set; }

        public override void Run()
        {
            List<float> samp = new List<float>();

            float sum = 0;
            for (int i = 0; i < InputSignal.Samples.Count; i++)
            {
                sum = 0;
                for (int j = i; j >= 0; j--)
                {
                    sum = sum + InputSignal.Samples[j];
                }
                samp.Add(sum);
            }
            OutputSignal = new Signal(samp, false);
        }
    }
}
