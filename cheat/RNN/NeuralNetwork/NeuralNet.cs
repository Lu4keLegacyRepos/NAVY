using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetwork
{
    public class NeuralNet
    {
        int inputNodes;
        int hiddenNodes;
        int outputNodes;

        Layer HiddenLayer;
        Layer OutputLayer;
        public NeuralNet(int inputs, int hidden, int outputs, Func<double, double> activationFc, Func<double, double> activationFc_derivitiv)
        {
            inputNodes = inputs;
            hiddenNodes = hidden;
            outputNodes = outputs;

            HiddenLayer = new Layer(hiddenNodes, inputNodes) { ActivationFc = activationFc, ActivationFc_derivitiv=activationFc_derivitiv };
            OutputLayer = new Layer(outputNodes, hiddenNodes) { ActivationFc = activationFc, ActivationFc_derivitiv = activationFc_derivitiv };
            HiddenLayer.Weights.Randomize();
            OutputLayer.Weights.Randomize();

            HiddenLayer.Bias = new Matrix(hiddenNodes, 1);
            OutputLayer.Bias = new Matrix(outputNodes, 1);
            HiddenLayer.Bias.Randomize();
            OutputLayer.Bias.Randomize();

            HiddenLayer.ConnectTo(OutputLayer);

        }


        public Matrix Predict(double[] inp)
        {
            Matrix inputs = new Matrix(inp);
            HiddenLayer.Inputs = inputs;
            HiddenLayer.ComputeOut();
            return OutputLayer.Outputs;

        }

        public void Train(double[] inp, double[] outp, double learningRate)
        {
            Matrix inputs = new Matrix(inp);
            Matrix targets = new Matrix(outp);

            HiddenLayer.Inputs = inputs;
            HiddenLayer.ComputeOut();

            OutputLayer.UpdateWeights(targets, learningRate);


        }
    }
}
