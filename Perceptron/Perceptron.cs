using Perceptron.DataSet;
using Perceptron.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Perceptron
{
    public class Perceptron
    {
        public double[] Weights { get; set; }
        public double Bias { get; set; }
        public Func<double[],int> ActivationFunc { get; set; }
        public string Id { get;private set; }
        public  double  LearningRate { get; private set; }

        public Perceptron(int numOfInputs, double learningConstant = 0.01)
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
                sum += input[i] * Weights[i] ;
            }

            return Math.Sign(sum);
        }

        public void Train(int numOfEpoch, IEnumerable<IDataSet> trainData)
        {
            for (int i = 0; i < numOfEpoch; i++)
            {
                foreach (TrainingSet data in trainData)
                {
                    var output = Guess(data.Input);
                    var error = data.Output - output;
                    UpdateWeight((i, data.Input[i], error));
                }
            }
        }

        private void UpdateWeight((int index,double input, double error) data)
        => Weights[data.index] = Weights[data.index] + data.error * data.input * LearningRate;
        
    }
}
