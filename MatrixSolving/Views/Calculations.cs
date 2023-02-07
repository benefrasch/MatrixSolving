using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MatrixSolver.Calculation
{
    internal class Calculations
    {

        public static (double result, bool valid) Evaluate(string expression)
        {
            //empty string
            if (expression == "")
                return (0, true);
            try
            {
                DataTable table = new();
                table.Columns.Add("expression", typeof(string), expression);
                DataRow row = table.NewRow();
                table.Rows.Add(row);
                return (double.Parse((string)row["expression"]), true);
            }
            catch
            {
                return (0, false);
            }
        }


        public static double[] GaussianElimination(double[,] leftSide, double[] rightSide)
        {
            int n = leftSide.GetLength(0);

            for (int i = 0; i < n - 1; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    double factor = leftSide[j, i] / leftSide[i, i];
                    for (int k = i; k < n; k++)
                    {
                        leftSide[j, k] = leftSide[j, k] - factor * leftSide[i, k];
                    }
                    rightSide[j] = rightSide[j] - factor * rightSide[i];
                }
            }

            double[] x = new double[n];
            for (int i = n - 1; i >= 0; i--)
            {
                x[i] = rightSide[i];
                for (int j = i + 1; j < n; j++)
                {
                    x[i] = x[i] - leftSide[i, j] * x[j];
                }
                x[i] = x[i] / leftSide[i, i];
            }

            return x;
        }
    }
}
