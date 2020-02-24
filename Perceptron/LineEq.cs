namespace Perceptron
{
    public class LineEq
    {
        public double a { get; set; } = 4;
        public double b { get; set; } = -5;

        public double GetY(double x) => a * x + b;
        public double GetX(double y) => y / a - b / a;

        //public double GetX((double wy, double y) Y) => (-Y.wy * Y.y - b) / a;

        public double GetLineEqToZero(double x, double y) => a * x + b - y;
    }
}
