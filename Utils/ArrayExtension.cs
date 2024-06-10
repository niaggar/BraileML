using MathNet.Numerics.LinearAlgebra;

namespace BraileML.Utils;

public static class ArrayExtension
{
    public static Vector<double> ToVector(this double[] array)
    {
        return Vector<double>.Build.Dense(array);
    }
}