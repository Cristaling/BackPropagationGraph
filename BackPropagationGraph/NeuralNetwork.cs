using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackPropagationGraph
{
    class NeuralNetwork
    {

        List<NeuronLayer> layers = new List<NeuronLayer>();
        List<double> outputs = new List<double>();
        List<double> targetOutputs;
        double totalError;

        public NeuralNetwork(int inputNr, int outputNr, int hiddenNr, int hiddenSize)
        {
            layers.Add(new NeuronLayer(hiddenSize, inputNr));
            for (int i = 1; i < hiddenNr; i++)
            {
                layers.Add(new NeuronLayer(hiddenSize, hiddenSize));
            }
            layers.Add(new NeuronLayer(outputNr, hiddenSize));
        }

        public void trainNetwork(List<double> targetOutputs, double trainRatio)
        {
            layers[layers.Count - 1].updateOutputWeights(targetOutputs, trainRatio);
            List<double> nextDeltas = layers[layers.Count - 1].getDeltas();
            List<double> nextWeights = layers[layers.Count - 1].getOldWeights();
            for(int i=layers.Count - 2; i > 0; i--)
            {
                layers[i].updateWeights(nextWeights, nextDeltas, trainRatio);
                nextDeltas = layers[i].getDeltas();
                nextWeights = layers[i].getOldWeights();
            }
        }

        public void setWeights(List<double> newWeights)
        {
            int index = 0;
            foreach (NeuronLayer neuronLayer in layers)
            {
                List<double> newWeightsL = new List<double>();
                int weightNr = neuronLayer.getWeightNumber();
                for (int i = 1; i <= weightNr; i++)
                {
                    newWeightsL.Add(newWeights[index]);
                    index++;
                }
                neuronLayer.setWeights(newWeightsL);
            }
        }

        public List<double> getWeights()
        {
            List<double> result = new List<double>();
            foreach (NeuronLayer neuronLayer in layers)
            {
                List<double> layerWeights = neuronLayer.getWeights();
                foreach (double weight in layerWeights)
                {
                    result.Add(weight);
                }
            }
            return result;
        }

        public void setInputs(List<double> newInputs)
        {
            List<double> result = null;
            foreach (NeuronLayer neuronLayer in layers)
            {
                if (result == null)
                {
                    neuronLayer.setInputs(newInputs);
                }
                else
                {
                    neuronLayer.setInputs(result);
                }
                result = neuronLayer.getOutputs();
            }
            outputs = result;
        }

        public List<double> getOutputs()
        {
            return outputs;
        }

        public void setTargetOutputs(List<double> wantedOutputs)
        {
            targetOutputs = wantedOutputs;
        }

        public void calculateError()
        {
            totalError = 0;
            int index = 0;
            foreach(double dd in outputs)
            {
                totalError += (targetOutputs[index] - dd) * (targetOutputs[index] - dd);
                index++;
            }
            totalError /= 2;
        }

    }
}
