using System;
using System.Collections.Generic;
using System.Text;

namespace Perceptron.Helpers
{
    public static class ExtensionMethods
    {

        public static double Map(this double value, (double from, double to) from, (double from, double to) to)
        {
            return (value - from.from) / (from.to - from.from) * (to.to - to.from) + to.from;
        }
    }
}
