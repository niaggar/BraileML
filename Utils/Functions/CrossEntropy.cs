using BraileML.Interface;
using MathNet.Numerics.LinearAlgebra;

namespace BraileML.Utils;

public class CrossEntropy : ILossFunction
{
    public double Calculate(Vector<double> expected, Vector<double> result)
    {
        var sum = expected.PointwiseMultiply(result.PointwiseLog()).Sum();
        return -sum;
    }

    public Vector<double> Derivative(Vector<double> expected, Vector<double> result)
    {
        var derivative = expected.PointwiseDivide(result);
        return -derivative;
    }
}