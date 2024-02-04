using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSPAlgorithms.Algorithms
{
    public class Derivatives: Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal FirstDerivative { get; set; }
        public Signal SecondDerivative { get; set; }

        public override void Run()
        {
            List<float> first_derivative_samples = new List<float>();
            List<float> second_derivative_samples = new List<float>();

            //first_derivative_samples.Add( InputSignal.Samples[0] );
            for (int i = 1; i < InputSignal.Samples.Count ; i++)
            {
                first_derivative_samples.Add( InputSignal.Samples[i]
                                              - InputSignal.Samples[i - 1] );
            }
            
            //second_derivative_samples.Add( InputSignal.Samples[1] 
            //                               - (2 * InputSignal.Samples[0]) );
            for (int j = 1; j < InputSignal.Samples.Count-1; j++)
            {
                second_derivative_samples.Add( InputSignal.Samples[j+1] 
                                               - (2 * InputSignal.Samples[j]) 
                                               + InputSignal.Samples[j - 1] );
            }
            //second_derivative_samples.Add( -(2 * InputSignal.Samples[InputSignal.Samples.Count - 1])
            //                               + InputSignal.Samples[InputSignal.Samples.Count - 2] );

            FirstDerivative = new Signal(first_derivative_samples,false);
            SecondDerivative = new Signal(second_derivative_samples,false);
        }
    }
}
