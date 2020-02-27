using HopfieldNetwork;
using MathNet.Numerics.LinearAlgebra;
using System;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var net = new Network(2);
            double[,] x = {{ 1.0, 0},
               { 0, 1.0 }};

            net.Learn(Matrix<double>.Build.DenseOfArray(x));
        }
    }
}
