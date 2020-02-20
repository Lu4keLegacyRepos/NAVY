using Perceptron.Enums;
using Perceptron.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Perceptron.DataSet
{
  public  class TestSet : IDataSet
    {
        public DataSetType Type => DataSetType.TrainingSet;
        public double[] Input { get; set; }

    }
}
