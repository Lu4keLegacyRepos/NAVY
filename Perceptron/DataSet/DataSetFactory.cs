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

        private List<TrainingSet> CreateTrainingSet(LineEq lineEq) 
        {
            return Generator.GenerateTrainSet(100, (data) => Math.Sign(4 * data.x - 5 - data.y));
        }


        public List<IDataSet> Create(DataSetType type, LineEq lineEq=null) => type switch
        {
            DataSetType.TestSet => CreateTestSet().ToList<IDataSet>(),
            DataSetType.TrainingSet => CreateTrainingSet(lineEq).ToList<IDataSet>(),
            _ => throw new Exception("WTF")
        };

    }
}
