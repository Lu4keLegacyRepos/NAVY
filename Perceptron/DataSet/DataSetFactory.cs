using Perceptron.Enums;
using Perceptron.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Perceptron.DataSet
{
    public class DataSetFactory
    {

        public List<TestSet> CreateTestSet()
        {
            return Generator.GenerateTestSet(100);
        }

        private List<TrainingSet> CreateTrainingSet()
        {
            return Generator.GenerateTrainSet(50, (data) => Math.Sign(4 * data.x - data.y - 5));
        }


        public List<IDataSet> Create(DataSetType type) => type switch
        {
            DataSetType.TestSet => CreateTestSet().ToList<IDataSet>(),
            DataSetType.TrainingSet => CreateTrainingSet().ToList<IDataSet>(),
            _ => throw new Exception("WTF")
        };

    }
}
