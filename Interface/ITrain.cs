using MathNet.Numerics.LinearAlgebra;

namespace BraileML.Interface;

public interface ITrain
{
    Vector<double> Expected(char character);
}