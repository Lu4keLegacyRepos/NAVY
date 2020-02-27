using NeuralNetwork.Enum;
using System;

namespace NeuralNetwork
{
    public class Layer
    {
        public int NodesCount { get; private set; }
        public LayerType LayerType { get; }
        public Layer Next { get; set; }

        private Perceptron.Perceptron[] nodes;

        public Layer(int nodesCount, (int numOfInputs, Func<double, int> activationFunc) percepsData, LayerType layerType)
        {
            NodesCount = nodesCount;
            LayerType = layerType;
            nodes = new Perceptron.Perceptron[NodesCount];
            for (int i = 0; i < NodesCount; i++)
            {
                nodes[i] = new Perceptron.Perceptron(percepsData.numOfInputs, percepsData.activationFunc);
            }
        }
    }
}
