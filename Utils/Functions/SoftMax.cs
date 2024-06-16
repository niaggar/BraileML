using BraileML.Interface;
using MathNet.Numerics.LinearAlgebra;

namespace BraileML.Utils;

public class SoftMax : IActivationFunction
{
    public Vector<double> Activate(Vector<double> values)
    {
        var exp = values.Map(System.Math.Exp);
        var sum = exp.Sum();
        return exp.Divide(sum);
    }

    public Vector<double> Derivative(Vector<double> values)
    {
        var exp = values.Map(System.Math.Exp);
        var sum = exp.Sum();

        return (exp * sum - exp.PointwiseMultiply(exp)) / System.Math.Pow(sum, 2);
    }
}