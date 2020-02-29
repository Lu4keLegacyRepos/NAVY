using MathNet.Numerics.LinearAlgebra;
using System;

namespace HopfieldNetwork
{
    public class Network
    {
        private int numOfEpoch = 100;

        public int Dimension { get; }
        public int NeuronsCount { get; }
        public int PatternCount { get; private set; }
        public int MaxPatternCount { get; private set; }
        public Matrix<double> Memory { get; private set; }

        public Network(int dimension, int numOfEpoch = 100)
        {
            this.numOfEpoch = numOfEpoch;
            Dimension = dimension;
            NeuronsCount = Dimension * Dimension;
            MaxPatternCount = (int)(NeuronsCount / 2 * Math.Log(NeuronsCount));
            Memory = CreateMatrix.Dense<double>(NeuronsCount, NeuronsCount);
        }

        public void Train(Matrix<double> pattern)
        {
            if (pattern.RowCount != Dimension || pattern.ColumnCount != Dimension)
            {
                throw new Exception("Incompatible image dimensions");
            }
            if (PatternCount >= MaxPatternCount)
            {
                throw new Exception("Full Memory");
            }

            var tempMatrix = CreateMatrix.Dense<double>(NeuronsCount, NeuronsCount);
            // convert 0 => -1
            pattern.MapInplace(x => x == 0 ? -1 : x, Zeros.Include);

            // to column vector
            var col = pattern.AsColumnMajorArray();
            var columnVector = CreateMatrix.DenseOfColumnArrays(col);

            // create weighted matrix
            columnVector.TransposeAndMultiply(columnVector, tempMatrix);

            //set diagonal to 0
            tempMatrix = tempMatrix.Subtract(CreateMatrix.DenseIdentity<double>(NeuronsCount, NeuronsCount));

            // add pattern
            Memory = Memory + tempMatrix;
            PatternCount++;

        }

        public Matrix<double> Recognize(Matrix<double> pattern)
        {
            Console.WriteLine("Memory:");
            Console.Write(Memory.ToMatrixString());
            if (pattern.RowCount != Dimension || pattern.ColumnCount != Dimension)
            {
                throw new Exception("Incompatible image dimensions");
            }
            //var rtnVector = CreateMatrix.Dense<double>(Dimension, Dimension).AsColumnMajorArray();

            // convert 0 => -1
            pattern.MapInplace(x => x == 0 ? -1 : x, Zeros.Include);


            // to row vector
            var row = pattern.AsColumnMajorArray();
            var input = CreateMatrix.DenseOfRowArrays(row);

            var rtnVector = input.Clone().ToRowMajorArray();

            for (int e = 0; e < numOfEpoch; e++)
            {
                for (int i = 0; i < Memory.ColumnCount; i++)
                {
                    var memoryColumn = Memory.Column(i).ToColumnMatrix();
                    rtnVector[i] = Math.Sign(DotProduct(rtnVector, memoryColumn));
                }
            }

            var rtnMatrix = CreateMatrix.DenseOfColumnMajor<double>(Dimension, Dimension, rtnVector);
            rtnMatrix.MapInplace(x => x == 0 ? -1 : x, Zeros.Include);
            return rtnMatrix;
        }

        private double DotProduct(double[] rowVector, Matrix<double> columnMatrix)
        {
            if (rowVector.Length != columnMatrix.RowCount)
            {
                throw new Exception("Incompatible matrix dimensions");
            }
            double rtn = 0;
            for (int i = 0; i < rowVector.Length; i++)
            {
                rtn += rowVector[i] * columnMatrix[i, 0];
            }
            return rtn;
        }

    }
}
