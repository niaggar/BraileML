using BraileML.Interface;
using MathNet.Numerics.LinearAlgebra;

namespace BraileML.Utils;

public class SoftMax : IActivationFunction
{
    public Vector<double> Activate(Vector<double> values)
    {
        var max = values.Max(); // To avoid overflow
        var exp = values.Subtract(max).Map(System.Math.Exp);
        var sum = exp.Sum();
        return exp.Divide(sum);
    }

    public Vector<double> Derivative(Vector<double> values)
    {
        return values.Map(x => x * (1 - x));
    }
}