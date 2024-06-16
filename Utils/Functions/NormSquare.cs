using BraileML.Interface;
using MathNet.Numerics.LinearAlgebra;

namespace BraileML.Utils;

public class NormSquare : ILossFunction
{
    public double Calculate(Vector<double> expected, Vector<double> result)
    {
        var sum = expected.Subtract(result).PointwisePower(2).Sum();
        return sum;
    }

    public Vector<double> Derivative(Vector<double> expected, Vector<double> result)
    {
        var derivative = result.Subtract(expected).Multiply(2);
        return derivative;
    }
}