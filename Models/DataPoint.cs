using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BraileML.Models;

public class DataPoint
{
    public Vector<double> Expected { get; set; }
    public Vector<double> Vector { get; set; }
    public char Label { get; set; }

    public DataPoint(Vector<double> vector, char label)
    {
        Vector = vector;
        Label = label;
    }

    public DataPoint(double[] vector, char label)
    {
        Vector = Vector<double>.Build.DenseOfArray(vector);
        Label = label;
    }
}