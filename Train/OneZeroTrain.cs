using BraileML.Interface;
using MathNet.Numerics.LinearAlgebra;

namespace BraileML.Train;

public class OneZeroTrain : ITrain
{
    public static Vector<double> OneExpected = Vector<double>.Build.DenseOfArray(new double[] { 1, 0 });
    public static Vector<double> ZeroExpected = Vector<double>.Build.DenseOfArray(new double[] { 0, 1 });
    
    public Vector<double> Expected(char character)
    {
        return character switch
        {
            '1' => OneExpected,
            '0' => ZeroExpected,
            _ => throw new Exception("Invalid character")
        };
    }
}