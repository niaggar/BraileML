using BraileML.Interface;
using MathNet.Numerics.LinearAlgebra;

namespace BraileML.Utils;

public class Blanck : IActivationFunction
{
    public Vector<double> Activate(Vector<double> values)
    {
        return values;
    }

    public Vector<double> Derivative(Vector<double> values)
    {
        return values;
    }
}