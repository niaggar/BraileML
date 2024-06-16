using BraileML.Models;
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
    
}