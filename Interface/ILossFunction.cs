using MathNet.Numerics.LinearAlgebra;

namespace BraileML.Interface;

public interface ILossFunction
{
    double Calculate(Vector<double> expected, Vector<double> result);
    Vector<double> Derivative(Vector<double> expected, Vector<double> result);
}