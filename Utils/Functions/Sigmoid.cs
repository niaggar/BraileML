using BraileML.Interface;
using MathNet.Numerics.LinearAlgebra;

namespace BraileML.Utils;

public class Sigmoid : IActivationFunction
{
    public Vector<double> Activate(Vector<double> values)
    {
        return values.Map(x => 1 / (1 + System.Math.Exp(-x)));
    }

    public Vector<double> Derivative(Vector<double> values)
    {
        return values.Map(x => x * (1 - x));
    }
}