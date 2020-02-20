using Perceptron.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Perceptron.Interfaces
{
    public interface IDataSet
    {
        DataSetType Type { get; }
        double[] Input { get; set; }
    }
}
