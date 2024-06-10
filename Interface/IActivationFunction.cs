using MathNet.Numerics.LinearAlgebra;

namespace BraileML.Interface;

public interface IActivationFunction
{
    Vector<double> Activate(Vector<double> values);
    Vector<double> Derivative(Vector<double> values);
}