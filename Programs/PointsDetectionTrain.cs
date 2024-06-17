using BraileML.Methods;
using BraileML.Models;
using BraileML.Train;
using BraileML.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BraileML.Programs;

public class PointsDetectionTrain : IDisposable
{
    static double momentum = 0.98;
    static double regularization = 0.1;
    static double learningRate = 0.01;
    static int epochs = 500000;

    ExportData exportTrend;
    ExportData exportNetwork;
    PerceptronNetwork network;

    public void Run()
    {
        var path = @"C:\Users\nicho\Desktop\TrainImages\Processed\";
        var images = FileManager.ReadImagesPoints(path);
        var size = images[0].Vector.Count;
        
        var trainingDataAll = images.Select(image => image.ToDataPoint()).ToArray(); 
        var layers = new LayerProps[]
        {
            new (size, 40, new Sigmoid()),
            new (40, 20, new Sigmoid()),
            new (20, 6, new Sigmoid())
        };

        var train = new PointTrain();
        foreach (var point in trainingDataAll)
        {
            train.SetExpected(point);
        }

        var (trainData, validateData) = DataSetUtil.SplitData(trainingDataAll, 0.80f);
        var trainBatches = DataSetUtil.CreateMiniBatches(trainData, 40);
        network = new PerceptronNetwork(layers, new NormSquare());
        exportTrend = new ExportData(@"C:\Users\nicho\Desktop\TrainImages\Trend-images-2.csv");
        exportNetwork = new ExportData(@"C:\Users\nicho\Desktop\TrainImages\Network-images-2.dat");

        for (var i = 0; i < epochs; i++)
        {
            DataSetUtil.ShuffleBatches(trainBatches);
            foreach (var batch in trainBatches)
            {
                network.Train(batch.data, learningRate, regularization, momentum);
            }


            if (i % 1 != 0) continue;
            var accuracy = network.Accuracy(trainingDataAll);
            Console.WriteLine($"Epoch: {i}, Accuracy: {accuracy.Item2}, Loss: {accuracy.Item1}");
            exportTrend.ExportTrend(i, accuracy.Item2, accuracy.Item1);

            if (accuracy.Item1 <= 0.05)
            {
                break;
            }
        }

        exportNetwork.ExportNetwork(network);
        exportNetwork.Close();
        exportTrend.Close();
    }

    public void Dispose()
    {
        exportNetwork.ExportNetwork(network);
        exportNetwork.Close();
        exportTrend.Close();
    }
}
