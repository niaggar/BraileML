using BraileML.Interface;
using MathNet.Numerics.LinearAlgebra;

namespace BraileML.Utils;

public class Sigmoid : IActivationFunction
{
    public Vector<double> Activate(Vector<double> values)
    {
        return values.Map(x => 1 / (1 + Math.Exp(-x)));
    }

    public Vector<double> Derivative(Vector<double> values)
    {
        return values.Map(x => Math.Exp(-x) / Math.Pow(1 + Math.Exp(-x), 2));
    }
}