using MathNet.Numerics.LinearAlgebra;
using System;

namespace HopfieldNetwork
{
    public class Network
    {
        public int Dimension { get; }
        public int NeuronsCount { get; }
        public int PatternCount { get;private set; }
        public int MaxPatternCount { get;private set; }
        public Matrix<double> Memory { get; private set; }

        public Network(int dimension)
        {
            Dimension = dimension;
            NeuronsCount = Dimension * Dimension;
            MaxPatternCount = (int)(Dimension / 2 * Math.Log(Dimension));
            Memory = CreateMatrix.Dense<double>(NeuronsCount, NeuronsCount);
        }

        public void Learn(Matrix<double> pattern)
        {
            if(pattern.RowCount!= Dimension || pattern.ColumnCount != Dimension)
            {
                throw new Exception("Incompatible image dimensions");
            }
            if (PatternCount >= MaxPatternCount)
            {
                throw new Exception("Full Memory");
            }

            var tempMatrix= CreateMatrix.Dense<double>(NeuronsCount, NeuronsCount);
            // convert 0 => -1
            pattern.MapInplace(x => x == 0 ? -1 : x, Zeros.Include);

            // to column vector
            var col = pattern.AsColumnMajorArray();
            var columnVector= CreateMatrix.DenseOfColumnArrays(col);

            // create weighted matrix
            columnVector.TransposeAndMultiply(columnVector, tempMatrix);

            //set diagonal to 0
            tempMatrix.SetDiagonal(CreateVector.Dense<double>(NeuronsCount, 0));

            // add pattern
            Memory = Memory + tempMatrix;
            PatternCount++;

        }
    }
}
