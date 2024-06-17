using BraileML.Methods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BraileML.Interface;
using MathNet.Numerics.LinearAlgebra;

namespace BraileML.Utils;

public class DataControl
{
    string path;
    StreamWriter stream;

    public DataControl(string path)
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

    public static PerceptronNetwork LoadNetwork(string path)
    {
        var stream = new StreamReader(path);
        var layers = new List<PerceptronLayer>();

        while (!stream.EndOfStream)
        {
            var line = stream.ReadLine();
            var parts = line.Split(',');

            if (parts[0] == "Network")
            {
                var layerCount = int.Parse(parts[1]);
                for (var i = 0; i < layerCount; i++)
                {
                    var layerLine = stream.ReadLine().Split(',');
                    var inputSize = int.Parse(layerLine[1]);
                    var outputSize = int.Parse(layerLine[2]);
                    var activation = (IActivationFunction)Activator.CreateInstance(Type.GetType($"BraileML.Utils.{layerLine[3]}"));
                    var weights = new List<double[]>();
                    var biases = new List<double>();

                    while (!stream.EndOfStream)
                    {
                        var layerData = stream.ReadLine().Split(',');

                        if (layerData[0] == "Weights")
                        {
                            weights.Add(layerData.Skip(1).Select(double.Parse).ToArray());
                        }
                        else if (layerData[0] == "Biases")
                        {
                            biases = layerData.Skip(1).Select(double.Parse).ToList();
                            break;
                        }
                    }

                    var layer = new LayerProps(inputSize, outputSize, activation);
                    var networkLayer = new PerceptronLayer(layer, Matrix<double>.Build.DenseOfRowArrays(weights), Vector<double>.Build.DenseOfEnumerable(biases));
                    layers.Add(networkLayer);
                }
            }
        }

        stream.Close();
        stream.Dispose();

        return new PerceptronNetwork(layers.ToArray(), new NormSquare());
    }
}