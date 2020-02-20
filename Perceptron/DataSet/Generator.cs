using Perceptron.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Perceptron.DataSet
{
    public static class Generator
    {
        /// <summary>
        /// generate set of 2D points for  y = 4x - 5
        /// </summary>
        /// <param name="numOfItems"></param>
        /// <returns></returns>
        public static List<TrainingSet> GenerateTrainSet(int numOfItems, Func<double[], int> activationFunc, int coordLimit = 500)
        {
            var rnd = new Random();

            var rtn = new List<TrainingSet>();
            for (int i = 0; i < numOfItems; i++)
            {
                var input = new double[]
                    {
                        (double)rnd.Next(coordLimit),
                        (double)rnd.Next(coordLimit)
                    };
                rtn.Add(new TrainingSet()
                {
                    Input = input,
                    Output = activationFunc(input)
                });
            }
            return rtn;
        }

        private int activateFunc(double[] x) => switch


        public static List<TestSet> GenerateTestSet(int numOfItems, int coordLimit = 500)
        {
            var rnd = new Random();

            var rtn = new List<TestSet>();
            for (int i = 0; i < numOfItems; i++)
            {
                var input = new double[]
                    {
                        (double)rnd.Next(coordLimit),
                        (double)rnd.Next(coordLimit)
                    };
                rtn.Add(new TestSet()
                {
                    Input = input
                });
            }
            return rtn;
        }
    }
}
