using BraileML.Interface;
using BraileML.Models;
using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BraileML.Train;

public class TestTrain : ITrain
{
    public static char[] Labels = new char[] { '1', '2' };
    public static Vector<double> OneExpected = Vector<double>.Build.DenseOfArray(new double[] { 1, 0 });
    public static Vector<double> TwoExpected = Vector<double>.Build.DenseOfArray(new double[] { 0, 1 });

    public Vector<double> Expected(char character)
    {
        return character switch
        {
            '1' => OneExpected,
            '2' => TwoExpected,
            _ => throw new Exception("Invalid character")
        };
    }

    public void SetExpected(DataPoint point)
    {
        point.Expected = Expected(point.Label);
    }
}
