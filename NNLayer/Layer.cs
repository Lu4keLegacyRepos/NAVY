using System;

namespace NeuralNetwork
{
    public class Layer
    {
        public int NodesCount { get; private set; }
        private Perceptron.Perceptron[] nodes;

        public Layer(int nodesCount, (int numOfInputs, Func<double, int> activationFunc) percepsData)
        {
            NodesCount = nodesCount;
            nodes = new Perceptron.Perceptron[NodesCount];
            for (int i = 0; i < NodesCount; i++)
            {
                nodes[i] = new Perceptron.Perceptron(percepsData.numOfInputs, percepsData.activationFunc);
            }
        }
    }
}
