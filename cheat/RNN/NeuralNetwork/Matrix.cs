using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetwork
{
    public class Matrix
    {

        private double[,] _data;

        public int Rows { get; set; }
        public int Cols { get; set; }
        public int Length { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rows"> num of neurons</param>
        /// <param name="cols">num of inputs</param>
        public Matrix(int rows, int cols)
        {
            Rows = rows;
            Cols = cols;
            Length = Cols * Rows;
            _data = new double[Rows, Cols];
        }

        public Matrix(double[] arr)
        {
            Rows = arr.Length;
            Cols = 1;
            Length = Cols * Rows;
            _data = new double[Rows, Cols];

            for (int i = 0; i < Rows; i++)
            {
                this[i, 0] = arr[i];
            }
        }

        /// <summary>
        /// Random num between -1 and 1
        /// </summary>
        internal void Randomize()
        {
            Map(x => {
                var r = new Random();
                return r.NextDouble() * 2 - 1;
            });
        }

        internal static Matrix Multiply(Matrix a, Matrix b)
        {
            if (a.Cols != b.Rows) throw new Exception("Columns of A must match rows of B.");
            Matrix tmp = new Matrix(a.Rows, b.Cols);
            tmp.Map((i, j, x) => {
                double sum = 0;
                for (int k = 0; k < a.Cols; k++)
                {
                    sum += a[i, k] * b[k, j];
                }
                return sum;
            });
            return tmp;
        }

        internal static Matrix Transpose(Matrix m)
        {
            Matrix nMat = new Matrix(m.Cols, m.Rows);
            nMat.Map((i, j, x) => m[j, i]);
            return nMat;
        }

        internal static Matrix Copy(Matrix m)
        {
            Matrix nMat = new Matrix(m.Rows,m.Cols);
            nMat.Map((i, j, x) => m[i, j]);
            return nMat;
        }

        internal static Matrix Subtract(Matrix a, Matrix b)
        {
            if (a.Rows != b.Rows || a.Cols != b.Cols) throw new Exception("Columns and Rows of A must match Columns and Rows of B.");

            Matrix nMat = Matrix.Copy(a);
            nMat.Subtract(b);
            return nMat;
        }

        /// <summary>
        /// Hadamar product
        /// </summary>
        /// <param name="b"></param>
        internal void Multiply(Matrix b)
        {
            if (Rows != b.Rows || Cols != b.Cols) throw new Exception("Columns and Rows of A must match Columns and Rows of B.");
            Map((i, j, x) => x * b[i, j]);
        }
        /// <summary>
        /// Scalar product
        /// </summary>
        /// <param name="b"></param>
        internal void Multiply(double b)
        {
            Map((x) => x * b);
        }


        internal void Add(Matrix mat)
        {
            if (Rows != mat.Rows || Cols != mat.Cols) throw new Exception("Columns and Rows of A must match Columns and Rows of B.");
            Map((i, j, x) => x + mat[i, j]);
        }
        internal void Add(double n)
        {
            Map(x => x + n);
        }

        internal void Subtract(Matrix mat)
        {
            if (Rows != mat.Rows || Cols != mat.Cols) throw new Exception("Columns and Rows of A must match Columns and Rows of B.");
            Map((i, j, x) => x - mat[i, j]);
        }
        internal void Subtract(double n)
        {
            Map(x => x - n);
        }


        internal void Map(Func<double, double> p)
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    this[i, j] = p(this[i, j]);
                }
            }
        }


        internal void Map(Func<int,int,double, double> p)
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    this[i, j] = p(i,j,this[i, j]);
                }
            }
        }

        internal double[] ToArray()
        {
            List<double> rtn = new List<double>();
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    rtn.Add(this[i, j]);
                }
            }
            return rtn.ToArray();
        }


        public void Print()
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < this[i].Length; j++)
                {
                    Console.Write(this[i, j] + " ");
                }
                Console.WriteLine("");
            }
            Console.WriteLine(" -       -       - ");

        }

        public double this[int row,int col]
        {
            get { return _data[row, col]; }
            set { _data[row, col] = value; }
        }

        public double[] this[int row]
        {
            get
            {
                double[] tmp = new double[Cols];
                for (int i = 0; i < Cols; i++)
                {
                    tmp[i] = _data[row, i];
                }
                return tmp;
            }
            set
            {
                for (int i = 0; i < Cols; i++)
                {
                    _data[row, i] = value[i];
                }
            }
        }


    }
}
