using BackPropagationGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackPropagationGraph
{
    class Neuron
    {
        
        List<double> weights = new List<double>();
        List<double> inputs;

        NeuronLayer parent;

        double output;

        public Neuron(NeuronLayer nLayer, int inputNr)
        {
            parent = nLayer;
            for (int i = 0; i <= inputNr; i++)
            {
                weights.Add(MathUtility.getRandomWeight());
                //Console.WriteLine("Initialized weights {0}", weights[i]);
            }
        }
        
        public void setWeights(List<double> newWeights)
        {
            weights = newWeights;
        }

        public List<double> getWeights()
        {
            return weights;
        }

        public void setInputs(List<double> newInputs)
        {
            inputs = newInputs;
            calculateOutput();
        }

        public void updateOutputWeights(double targetOutput, double learnRatio)
        {
            double delta = output - targetOutput;
            delta *= output * (1 - output);
            parent.getDeltas().Add(delta);
            for (int i = 0; i < weights.Count - 1; i++)
            {
                parent.getOldWeights().Add(weights[i]);
                weights[i] -= learnRatio * delta * inputs[i];
            }
        }

        public void updateWeights(List<double> nextWeights, List<double> nextDeltas, double learnRatio, int index)
        {
            double delta = 0;
            int perNeuronWeights = nextWeights.Count / nextDeltas.Count;
            int index2 = 0;
            foreach(double relDelta in nextDeltas)
            {
                delta += relDelta * nextWeights[index2 * perNeuronWeights + index];
                index2++;
            }
            delta *= output * (1 - output);
            parent.getDeltas().Add(delta);
            for(int i=0;i<weights.Count - 1;i++)
            {
                parent.getOldWeights().Add(weights[i]);
                weights[i] -= learnRatio * delta * inputs[i];
            }
        }

        public void calculateOutput()
        {
            double sum = 0;
            int num = 0;
            foreach(double input in inputs)
            {
                sum += input * weights[num];
                num++;
            }
            sum += weights[num];
            output =  MathUtility.signoid(sum);
        }

        public double getOutput()
        {
            return output;
        }

    }
}
