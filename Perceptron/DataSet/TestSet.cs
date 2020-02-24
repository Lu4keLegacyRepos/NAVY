using Perceptron.Enums;
using Perceptron.Interfaces;

namespace Perceptron.DataSet
{
    public class TestSet : IDataSet
    {
        public DataSetType Type => DataSetType.TrainingSet;
        public double[] Input { get; set; }

    }
}
