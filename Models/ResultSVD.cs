using System.Drawing;
using BraileML.Enums;
using MathNet.Numerics.LinearAlgebra;

namespace BraileML.Models;

public class ResultSVD
{
    public char[] Characters { get; set; }
    public double[,] PrincipalComponents { get; set; }
    public double[,] ProjectedData { get; set; }

    public double[] GetPC(PrincipalComponentEnum pc)
    {
        var index = (int)pc;
        var pcValues = new double[PrincipalComponents.GetLength(1)];
        for (var i = 0; i < PrincipalComponents.GetLength(1); i++)
        {
            pcValues[i] = PrincipalComponents[index, i];
        }

        return pcValues;
    }
    
    public double[] GetProjected(PrincipalComponentEnum pc)
    {
        var index = (int)pc;
        var pcValues = new double[ProjectedData.GetLength(0)];
        for (var i = 0; i < ProjectedData.GetLength(0); i++)
        {
            pcValues[i] = ProjectedData[i, index];
        }

        return pcValues;
    }
    
    public DataPoint[] GetImagePoints(PrincipalComponentEnum[] components)
    {
        var imagePoints = new DataPoint[ProjectedData.GetLength(1)];
        for (var i = 0; i < ProjectedData.GetLength(1); i++)
        {
            var character = Characters[i];
            var point = new DataPoint(Vector<double>.Build.Dense(components.Length), character);

            for (int j = 0; j < components.Length; j++)
            {
                var pcIndex = (int)components[j];
                var value = ProjectedData[pcIndex, i];
                
                point.Vector[j] = value;
            }
            
            imagePoints[i] = point;
        }

        return imagePoints;
    }
}