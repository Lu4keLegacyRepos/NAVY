using Perceptron.DataSet;
using Perceptron.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Perceptron
{
    public class Perceptron
    {
        public double[] Weights { get; set; }
        public double Bias { get; set; }
        public Func<double[], int> ActivationFunc { get; set; }
        public string Id { get; private set; }
        public double LearningRate { get; private set; }


        private List<IDataSet> trainData = null;
        private (int Actual, int Max) NumOfEpoch = (0,0);

        public Perceptron(int numOfInputs, double learningConstant = 0.001)
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
        }


        public double Guess(double[] input)
        {
            var sum = Bias;
            for (int i = 0; i < input.Count(); i++)
            {
                sum += input[i] * Weights[i];
            }

            return Math.Sign(sum);
        }

        public void Train(int numOfEpoch, List<IDataSet> trainData)
        {
            for (int i = 0; i < numOfEpoch; i++)
            {
                foreach (TrainingSet data in trainData)
                {
                    var output = Guess(data.Input);
                    var error = data.Output - output;
                    var index = trainData.IndexOf(data);
                    UpdateWeight((0, data.Input[0], error));
                    UpdateWeight((1, data.Input[1], error));
                }
            }
        }

        public void SetTrainData(int numOfEpoch, List<IDataSet> trainData)
        {
            this.trainData = trainData;
            this.NumOfEpoch = (0, numOfEpoch);
        }

        public List<IDataSet> TrainStep()
        {
            if (NumOfEpoch.Actual > NumOfEpoch.Max)
            {
                return null;
            }
            foreach (TrainingSet data in trainData)
            {
                var output = Guess(data.Input);
                var error = data.Output - output;
                UpdateWeight((0, data.Input[0], error));
                UpdateWeight((1, data.Input[1], error));
            }
            NumOfEpoch.Actual++;
            return trainData;
        }

        private void UpdateWeight((int index, double input, double error) data)
        => Weights[data.index] = Weights[data.index] + data.error * data.input * LearningRate;

    }
}
