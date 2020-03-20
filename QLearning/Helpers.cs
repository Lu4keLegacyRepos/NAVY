using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QLearning
{
    public static class Helpers<T>
    {
        public static T[] GetRow(T[,] matrix, int rowNumber)
        {
            return Enumerable.Range(0, matrix.GetLength(1))
                    .Select(x => matrix[rowNumber, x])
                    .ToArray();
        }
    }




}
