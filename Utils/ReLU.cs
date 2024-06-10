using BraileML.Interface;
using MathNet.Numerics.LinearAlgebra;

namespace BraileML.Utils;

public class ReLU : IActivationFunction
{
    public Vector<double> Activate(Vector<double> values)
    {
        return values.Map(x => x < 0 ? 0 : x);
    }

    public Vector<double> Derivative(Vector<double> values)
    {
        return values.Map(x => x < 0 ? 0.0 : 1.0);
    }
}