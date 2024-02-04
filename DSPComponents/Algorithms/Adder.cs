using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Adder : Algorithm
    {

        public List<Signal> InputSignals { get; set; }
        public Signal OutputSignal { get; set; }

        public override void Run()
        {
            List<float> sum = new List<float>();

            for (int i = 0; i < (InputSignals.Count) - 1; i++)
            {
                if(sum.Count == 0) 
                {
                    if (InputSignals[i].Samples.Count > InputSignals[i + 1].Samples.Count) 
                    {
                        for (int z = InputSignals[i + 1].Samples.Count; z >= InputSignals[i].Samples.Count; z++)
                        {
                            InputSignals[i + 1].Samples.Add(0);
                        }

                        for (int j = 0; j < InputSignals[i].Samples.Count; j++)
                        {
                            sum.Add(InputSignals[i].Samples[j] + InputSignals[i + 1].Samples[j]);
                        }
                        OutputSignal = new Signal(sum, InputSignals[i].Periodic);
                    }
                    else if (InputSignals[i].Samples.Count < InputSignals[i + 1].Samples.Count)
                    {
                        for (int z = InputSignals[i].Samples.Count; z >= InputSignals[i+1].Samples.Count; z++)
                        {
                            InputSignals[i].Samples.Add(0);
                        }

                        for (int j = 0; j < InputSignals[i].Samples.Count; j++)
                        {
                            sum.Add(InputSignals[i].Samples[j] + InputSignals[i + 1].Samples[j]);
                        }
                        OutputSignal = new Signal(sum, InputSignals[i].Periodic);
                    }
                    else
                    {
                        for (int j = 0; j < InputSignals[i].Samples.Count; j++)
                        {
                            sum.Add(InputSignals[i].Samples[j] + InputSignals[i + 1].Samples[j]);
                        }
                        OutputSignal = new Signal(sum, InputSignals[i].Periodic);
                    }
                }
                
                else
                {
                    if (sum.Count > InputSignals[i + 1].Samples.Count)
                    {
                        for (int z = InputSignals[i + 1].Samples.Count; z >= sum.Count; z++)
                        {
                            InputSignals[i + 1].Samples.Add(0);
                        }

                        for (int j = 0; j < InputSignals[i].Samples.Count; j++)
                        {
                            sum.Insert(j, (sum.ElementAt(j) + InputSignals[i + 1].Samples[j]));
                        }
                    }else if(sum.Count < InputSignals[i + 1].Samples.Count)
                    {
                        for (int z = sum.Count; z >= InputSignals[i + 1].Samples.Count; z++)
                        {
                            sum.Add(0);
                        }

                        for (int j = 0; j < InputSignals[i].Samples.Count; j++)
                        {
                            sum.Insert(j, (sum.ElementAt(j) + InputSignals[i + 1].Samples[j]));
                        }
                    }
                    else
                    {
                        for (int j = 0; j < InputSignals[i].Samples.Count; j++)
                        {
                            sum.Insert(j, (sum.ElementAt(j) + InputSignals[i + 1].Samples[j]));
                        }
                    }

                }
            }
            OutputSignal = new Signal(sum, InputSignals[0].Periodic);
        }
    }
}