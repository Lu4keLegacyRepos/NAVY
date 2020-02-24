using Perceptron.Enums;
using Perceptron.Interfaces;

namespace Perceptron.DataSet
{
    public class TrainingSet : IDataSet
    {
        public DataSetType Type => DataSetType.TrainingSet;
        public double[] Input { get; set; }
        public double Output { get; set; }


    }
}
