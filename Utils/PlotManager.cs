using BraileML.Models;
using ScottPlot;

namespace BraileML.Utils;

public class PlotManager
{
    public static void PlotPoints(ImagePoint[] points, string title, string xLabel, string yLabel, string saveRoute)
    {
        var plt = new Plot();
        plt.Title(title);
        plt.XLabel(xLabel);
        plt.YLabel(yLabel);
        
        var groups = points.GroupBy(p => p.Character);
        foreach (var group in groups)
        {
            var xs = group.Select(p => p.Point[0]).ToArray();
            var ys = group.Select(p => p.Point[1]).ToArray();
            
            var scatter = plt.Add.ScatterPoints(xs, ys);
            scatter.MarkerSize = 10;
            scatter.LegendText = group.Key.ToString();
        }

        plt.ShowLegend();
        plt.SavePng(saveRoute, 1000, 1000);
    }
    
}