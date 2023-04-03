using System.Text;
using Microsoft.VisualBasic.CompilerServices;

namespace GraphMatrix;

public class Matrix
{
    protected double[,] A;

    public int Rows => A.GetLength(0);
    public int Columns => A.GetLength(1);

    public Matrix(int n, int m, double? fill)
    {
        A = new double[n, m];

        if (fill.HasValue)
            Fill(fill.Value);
    }

    public Matrix(Matrix matrix)
    {
        A = matrix.A;
    }

    public void Fill(double fill)
    {
        for (var i = 0; i < Rows; i++)
        {
            for (var j = 0; j < Columns; j++)
            {
                A[i, j] = fill;
            }
        }
    }

    public void Print()
    {
        var result = new StringBuilder();
        for (uint i = 0; i < Rows; i++)
        {
            var value = string.Join(" ", Enumerable.Range(0, Columns).Select(k => A[i, k]).ToArray());
            result.AppendLine(value);
        }

        Console.WriteLine(result);
    }

    public int this[int i, int j]
    {
        get => (int)A[i, j];
        set => A[i, j] = value;
    }
}