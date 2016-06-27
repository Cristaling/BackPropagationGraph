using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackPropagationGraph
{
    class NeuronLayer
    {
        
        List<double> outputs = new List<double>();
        List<Neuron> neurons = new List<Neuron>();

        List<double> deltas = new List<double>();
        List<double> oldWeights = new List<double>();

        int weightNumber;

        public NeuronLayer(int neuronNr, int inputNr)
        {
            for (int i = 1; i <= neuronNr; i++)
            {
                neurons.Add(new Neuron(this, inputNr));
            }
            weightNumber = (inputNr + 1) * neuronNr;
        }

        public List<double> getDeltas()
        {
            return deltas;
        }

        public void updateOutputWeights(List<double> targetOutputs, double learnRatio)
        {
            deltas.Clear();
            oldWeights.Clear();
            int index = 0;
            foreach (Neuron neuron in neurons)
            {
                neuron.updateOutputWeights(targetOutputs[index], learnRatio);
                index++;
            }
        }

        public void updateWeights(List<double> nextWeights, List<double> nextDeltas, double learnRatio)
        {
            deltas.Clear();
            oldWeights.Clear();
            int index = 0;
            foreach(Neuron neuron in neurons)
            {
                neuron.updateWeights(nextWeights, nextDeltas, learnRatio, index);
                index++;
            }
        }

        public List<double> getOldWeights()
        {
            return oldWeights;
        }

        public void setWeights(List<double> newWeights)
        {
            int index = 0;
            foreach(Neuron neuron in neurons)
            {
                List<double> newWeightsN = new List<double>();
                int weightNr = neuron.getWeights().Count;
                for(int i = 1; i <= weightNr; i++)
                {
                    newWeightsN.Add(newWeights[index]);
                    index++;
                }
                neuron.setWeights(newWeightsN);
            }
        }

        public int getWeightNumber()
        {
            return weightNumber;
        }

        public List<double> getWeights()
        {
            List<double> result = new List<double>();
            foreach (Neuron neuron in neurons)
            {
                foreach (double weight in neuron.getWeights())
                {
                    result.Add(weight);
                }
            }
            return result;
        }

        public void setInputs(List<double> newInputs)
        {
            outputs = new List<double>();
            foreach(Neuron neuron in neurons)
            {
                neuron.setInputs(newInputs);
                outputs.Add(neuron.getOutput());
            }
        }

        public List<double> getOutputs()
        {
            return outputs;
        }

    }
}
