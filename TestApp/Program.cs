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
            var net = new Network(5);
            double[,] pattern = 
                {{ 0,1,1,1,0 },
                {  0,1,0,1,0 },
                {  0,1,1,1,0 },
                {  0,1,0,1,0 },
                {  0,1,1,1,0 }};
            var patternMatrix = Matrix<double>.Build.DenseOfArray(pattern);
            double[,] broken =
                {{ 0,1,1,0,0 },
                {  0,1,0,1,0 },
                {  0,0,1,1,0 },
                {  0,1,0,0,0 },
                {  0,1,1,1,0 }};

            var brokenMatrix = Matrix<double>.Build.DenseOfArray(broken);

            net.Train(patternMatrix);
            var rtn = net.Recognize(brokenMatrix);

            Console.WriteLine("pattern: ");
            Console.Write(patternMatrix.ToMatrixString() + "\n\n");

            Console.WriteLine("broken: ");
            Console.Write(brokenMatrix.ToMatrixString() + "\n\n");

            Console.WriteLine("memory: ");
            Console.Write(net.Memory.ToMatrixString() + "\n\n");

            Console.WriteLine("output: ");
            Console.Write(rtn.ToMatrixString() + "\n\n");
            Console.ReadLine();
        }
    }
}
