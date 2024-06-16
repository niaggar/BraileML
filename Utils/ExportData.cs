using BraileML.Methods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BraileML.Utils;

public class ExportData
{
    string path;
    StreamWriter stream;

    public ExportData(string path)
    {
        this.path = path;
        stream = new StreamWriter(path);
    }

    public void ExportTrend(int epoch, double accuracy, double loss)
    {
        stream.WriteLine($"{epoch},{accuracy},{loss}");
    }

    public void ExportNetwork(PerceptronNetwork network)
    {
        stream.WriteLine($"Network,{network.Layers.Length}");

        for (var i = 0; i < network.Layers.Length; i++)
        {
            var layer = network.Layers[i];
            stream.WriteLine($"Layer,{layer.InpuntSize},{layer.OutputSize},{layer.ActivationFunction.GetType().Name}");

            for (var j = 0; j < layer.Weights.RowCount; j++)
            {
                var weights = layer.Weights.Row(j).ToArray();
                stream.WriteLine($"Weights,{string.Join(',', weights)}");
            }

            var biases = layer.Biases.ToArray();
            stream.WriteLine($"Biases,{string.Join(',', biases)}");
        }
    }

    public void Close()
    {
        stream.Close();
        stream.Dispose();
    }
}