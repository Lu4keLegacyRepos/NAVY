using NeuralNetwork;
using System;
using System.Collections.Generic;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            NeuralNet nn = new NeuralNet(2, 2, 1, x => 1 / (1 + Math.Exp(-x)), y => y * (1 - y));

            nn.Predict(new double[] { 0, 0 }).Print();
            nn.Predict(new double[] { 0, 1 }).Print();
            nn.Predict(new double[] { 1, 1 }).Print();
            nn.Predict(new double[] { 1, 0 }).Print();

            List<Tuple<double[], double[]>> dataSet = new List<Tuple<double[], double[]>>();
            dataSet.Add(new Tuple<double[], double[]>(new double[] { 0, 0 }, new double[] { 0 }));
            dataSet.Add(new Tuple<double[], double[]>(new double[] { 0, 1 }, new double[] { 1 }));
            dataSet.Add(new Tuple<double[], double[]>(new double[] { 1, 0 }, new double[] { 1 }));
            dataSet.Add(new Tuple<double[], double[]>(new double[] { 1, 1 }, new double[] { 0 }));

            Console.WriteLine("Training");
            Random r = new Random();
            for (int i = 0; i < 200000; i++)
            {
                var trainSet = dataSet[r.Next(dataSet.Count)];
                nn.Train(trainSet.Item1, trainSet.Item2,0.2);
            }
            Console.WriteLine("Training Complete");


            nn.Predict(new double[] { 0, 0 }).Print();
            nn.Predict(new double[] { 0, 1 }).Print();
            nn.Predict(new double[] { 1, 1 }).Print();
            nn.Predict(new double[] { 1, 0 }).Print();
            Console.ReadKey();
        }
    }
}
