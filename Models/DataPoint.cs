using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BraileML.Models;

public class DataPoint(Vector<double> vector, char label)
{
    public Vector<double> Vector { get; set; } = vector;
    public char Label { get; set; } = label;
}