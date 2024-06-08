using MathNet.Numerics.LinearAlgebra;

namespace BraileML.Models;

public class ImagePoint
{
    public char Character { get; set; }
    public Vector<double> Point { get; set; }
}