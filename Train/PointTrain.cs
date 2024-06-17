using BraileML.Interface;
using BraileML.Models;
using MathNet.Numerics.LinearAlgebra;

namespace BraileML.Train;

public class PointTrain : ITrain
{
    public static char[] Labels = new char[] { '1', '2', '3', '4', '5', '6' };
    public static Vector<double> OneExpected = Vector<double>.Build.DenseOfArray(new double[] { 1, 0, 0, 0, 0, 0 });
    public static Vector<double> TwoExpected = Vector<double>.Build.DenseOfArray(new double[] { 0, 1, 0, 0, 0, 0 });
    public static Vector<double> ThreeExpected = Vector<double>.Build.DenseOfArray(new double[] { 0, 0, 1, 0, 0, 0 });
    public static Vector<double> FourExpected = Vector<double>.Build.DenseOfArray(new double[] { 0, 0, 0, 1, 0, 0 });
    public static Vector<double> FiveExpected = Vector<double>.Build.DenseOfArray(new double[] { 0, 0, 0, 0, 1, 0 });
    public static Vector<double> SixExpected = Vector<double>.Build.DenseOfArray(new double[] { 0, 0, 0, 0, 0, 1 });
    
    public Vector<double> Expected(char character)
    {
        return character switch
        {
            '1' => OneExpected,
            '2' => TwoExpected,
            '3' => ThreeExpected,
            '4' => FourExpected,
            '5' => FiveExpected,
            '6' => SixExpected,
            _ => throw new System.Exception("Invalid character"),
        };
    }

    public void SetExpected(DataPoint point)
    {
        point.Expected = Expected(point.Label);
    }
}