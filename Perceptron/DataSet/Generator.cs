using System;
using System.Collections.Generic;

namespace Perceptron.DataSet
{
    public static class Generator
    {
        /// <summary>
        /// generate set of 2D points 
        /// </summary>
        /// <param name="numOfItems"></param>
        /// <returns></returns>
        public static List<TrainingSet> GenerateTrainSet(int numOfItems, Func<(double x, double y), int> lineFunction, int coordLimit = 500)
        {
            var rnd = new Random();

            var rtn = new List<TrainingSet>();
            for (int i = 0; i < numOfItems; i++)
            {
                var input = new double[]
                    {
                        (double)rnd.Next(-coordLimit/2,coordLimit/2),
                        (double)rnd.Next(-coordLimit/2,coordLimit/2)
                    };
                rtn.Add(new TrainingSet()
                {
                    Input = input,
                    Output = lineFunction((input[0], input[1]))
                });
            }
            return rtn;
        }



        public static List<TestSet> GenerateTestSet(int numOfItems, int coordLimit = 500)
        {
            var rnd = new Random();

            var rtn = new List<TestSet>();
            for (int i = 0; i < numOfItems; i++)
            {
                var input = new double[]
                    {
                        (double)rnd.Next(-coordLimit/2,coordLimit/2),
                        (double)rnd.Next(-coordLimit/2,coordLimit/2)
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
