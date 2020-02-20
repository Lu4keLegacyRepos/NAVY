using Perceptron.Interfaces;

namespace Perceptron.DataSet
{
    public class DataSetFactory
    {

        public IDataSet CreateTrainingSet(double[] input, double output)
        {
            return new TrainingSet() { Input = input, Output = output };
        }

        public IDataSet CreateTestSet(double[] input)
        {
            return new TrainingSet() { Input = input };
        }
    }
}
