using BraileML.Interface;
using BraileML.Models;
using MathNet.Numerics.LinearAlgebra;

namespace BraileML.Train;

public class BraileABCTrain : ITrain
{
    public static char[] Labels = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };
    public static Vector<double> AExpected = Vector<double>.Build.DenseOfArray(new double[] { 1, 0, 0, 0, 0, 0, 0, 0 });
    public static Vector<double> BExpected = Vector<double>.Build.DenseOfArray(new double[] { 0, 1, 0, 0, 0, 0, 0, 0 });
    public static Vector<double> CExpected = Vector<double>.Build.DenseOfArray(new double[] { 0, 0, 1, 0, 0, 0, 0, 0 });
    public static Vector<double> DExpected = Vector<double>.Build.DenseOfArray(new double[] { 0, 0, 0, 1, 0, 0, 0, 0 });
    public static Vector<double> EExpected = Vector<double>.Build.DenseOfArray(new double[] { 0, 0, 0, 0, 1, 0, 0, 0 });
    public static Vector<double> FExpected = Vector<double>.Build.DenseOfArray(new double[] { 0, 0, 0, 0, 0, 1, 0, 0 });
    public static Vector<double> GExpected = Vector<double>.Build.DenseOfArray(new double[] { 0, 0, 0, 0, 0, 0, 1, 0 });
    public static Vector<double> HExpected = Vector<double>.Build.DenseOfArray(new double[] { 0, 0, 0, 0, 0, 0, 0, 1 });
    
    public Vector<double> Expected(char character)
    {
        return character switch
        {
            'a' => AExpected,
            'b' => BExpected,
            'c' => CExpected,
            'd' => DExpected,
            'e' => EExpected,
            'f' => FExpected,
            'g' => GExpected,
            'h' => HExpected,
            _ => throw new Exception("Invalid character")
        };
    }

    public void SetExpected(DataPoint point)
    {
        point.Expected = Expected(point.Label);
    }
}