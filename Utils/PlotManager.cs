using BraileML.Models;
using MathNet.Numerics.LinearAlgebra;
using ScottPlot;
using DataPoint = BraileML.Models.DataPoint;

namespace BraileML.Utils;

public class PlotManager
{
    public static void PlotPoints(DataPoint[] points, string title, string xLabel, string yLabel, string saveRoute)
    {
        var plt = new Plot();
        plt.Title(title);
        plt.XLabel(xLabel);
        plt.YLabel(yLabel);
        
        var groups = points.GroupBy(p => p.Label);
        foreach (var group in groups)
        {
            var xs = group.Select(p => p.Vector[0]).ToArray();
            var ys = group.Select(p => p.Vector[1]).ToArray();
            
            var scatter = plt.Add.ScatterPoints(xs, ys);
            scatter.MarkerSize = 10;
            scatter.LegendText = group.Key.ToString();
        }

        plt.ShowLegend();
        plt.SavePng(saveRoute, 1000, 1000);
    }


    public static void PlotBraileAsHitMap(char real, Vector<double> result, string savePath, int i = 0)
    {
        var matrix = new double[3, 2];
        matrix[0, 0] = result[0];
        matrix[0, 1] = result[1];
        matrix[1, 0] = result[2];
        matrix[1, 1] = result[3];
        matrix[2, 0] = result[4];
        matrix[2, 1] = result[5];

        var plt = new Plot();
        plt.Title($"Character: {real}");
        plt.Add.Heatmap(matrix);
        //plt.Layout.Frameless();
        plt.Axes.Margins(0, 0);
        plt.SavePng($"{savePath}{real}-{i}.png", 250, 500);
    }
}