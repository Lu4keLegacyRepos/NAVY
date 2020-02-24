using Perceptron.DataSet;
using Perceptron.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Perceptron
{
    public class Perceptron
    {
        public LineEq LineEquations { get;private set; }
        public double[] Weights { get; set; }
        public double Bias { get; set; }
        public Func<double, int> ActivationFunc { get; set; }
        public string Id { get; private set; }
        public double LearningRate { get; private set; }
        public double Error { get; private set; }

        private List<IDataSet> trainData = null;
        private (int Actual, int Max) NumOfEpoch = (0, 0);

        public Perceptron(int numOfInputs, Func<double, int> activationFunc, double learningConstant = 0.001)
        {
            var rnd = new Random();
            LearningRate = learningConstant;
            Weights = new double[numOfInputs];
            Bias = rnd.NextDouble();
            for (int i = 0; i < numOfInputs; i++)
            {
                Weights[i] = rnd.NextDouble();
            }
            Id = Guid.NewGuid().ToString();
            ActivationFunc = activationFunc;
            LineEquations = new LineEq();
        }

        public Perceptron(int numOfInputs) : this(numOfInputs, Math.Sign)
        {

        }
        public Perceptron(int numOfInputs, LineEq lineEq) : this(numOfInputs, Math.Sign)
        {
            LineEquations = lineEq;
        }
        public Perceptron(int numOfInputs, double learningConstant = 0.001) : this(numOfInputs, Math.Sign, learningConstant)
        {

        }

        public double Guess(double[] input)
        {
            var sum = Bias;
            for (int i = 0; i < input.Count(); i++)
            {
                sum += input[i] * Weights[i];
            }

            return ActivationFunc(sum);
        }

        public void Train(int numOfEpoch, List<IDataSet> trainData)
        {
            for (int i = 0; i < numOfEpoch; i++)
            {
                foreach (TrainingSet data in trainData)
                {
                    var output = Guess(data.Input);
                    var error = data.Output - output;
                    UpdateWeight((0, data.Input[0], error));
                    UpdateWeight((1, data.Input[1], error));
                    UpdateBias(error);
                    Error = error;
                }
            }
        }

        public void SetTrainData(List<IDataSet> trainData)
        {
            this.trainData = trainData;
            this.NumOfEpoch = (0, 0);
        }

        public List<IDataSet> TrainStep()
        {
            foreach (TrainingSet data in trainData)
            {
                var output = Guess(data.Input);
                double error = data.Output - output;
                UpdateWeight((0, data.Input[0], error));
                UpdateWeight((1, data.Input[1], error));
                UpdateBias(error);
                Error = error;
            }
            NumOfEpoch.Actual++;
            return trainData;
        }

        private void UpdateWeight((int index, double input, double error) data)
        => Weights[data.index] = Weights[data.index] + data.error * data.input * LearningRate;

        private void UpdateBias(double error) => Bias = Bias + error * LearningRate;

    }
}
