namespace Perceptron
{
    public class LineEq
    {
        public double GetY((double x, double bias, double wx, double wy) data) => (-data.wx * data.x - data.bias) / data.wy;
        public double GetX((double y, double bias, double wx, double wy) data) => (-data.wy * data.y - data.bias) / data.wx;

        public double GetLineEqToZero((double x, double y, double bias, double wx, double wy) data) => data.wx * data.x + data.wy * data.y + data.bias;

        public string ToString((double bias, double wx, double wy) data)
        {
            return $"Y={(-(data.bias/data.wy)/(data.bias/data.wx)).ToString("#.##")} X + {(-data.bias/data.wy).ToString("#.##")}";
        }
    }
}
