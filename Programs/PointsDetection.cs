using BraileML.Train;
using BraileML.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BraileML.Programs;

public class PointsDetection
{
    public void Run1()
    {
        // Prepare the data
        var path = @"C:\Users\nicho\Desktop\TrainImages\Processed\";
        var network = DataControl.LoadNetwork(@"C:\Users\nicho\Desktop\TrainImages\Network-images-2.dat");

        var saveImages = @"C:\Users\nicho\Desktop\TrainImages\Results\";

        var images = FileManager.ReadImagesPoints(path);
        var trainingDataAll = images.Select(image => image.ToDataPoint()).ToArray();
        
        var train = new PointTrain();
        foreach (var point in trainingDataAll)
        {
            train.SetExpected(point);
        }

        var numImageToTest = 20;
        for (var i = 0; i < numImageToTest; i++)
        {
            // Take a random image
            var index = new Random().Next(images.Length);
            var image = images[index];
            var prediction = network.Predict(image.Vector, PointTrain.Labels);
            Console.WriteLine($"Character: {image.Character}");
            Console.WriteLine($"Result: {prediction.prediction}");
            Console.WriteLine($"Vector: {string.Join(',', prediction.result)}");
            Console.WriteLine($"---------");

            PlotManager.PlotBraileAsHitMap(image.Character, prediction.result, saveImages, index);
        }
    }

    public void Run2()
    {
        // Prepare the data
        var path = @"C:\Users\nicho\Desktop\TrainImages\Convinations\Processed\";
        var network = DataControl.LoadNetwork(@"C:\Users\nicho\Desktop\TrainImages\Network-images-2.dat");

        var saveImages = @"C:\Users\nicho\Desktop\TrainImages\Convinations\Results\";

        var images = FileManager.ReadImagesPointsAlphabet(path);
        var numImageToTest = 26;
        for (var i = 0; i < numImageToTest; i++)
        {
            // Take a random image
            var image = images[i];
            var prediction = network.Predict(image.Vector, BraileABCTrain.Labels);

            Console.WriteLine($"Character: {image.Character}");
            Console.WriteLine($"Result: {prediction.prediction}");
            Console.WriteLine($"Vector: {string.Join(',', prediction.result)}");
            Console.WriteLine($"---------");

            PlotManager.PlotBraileAsHitMap(image.Character, prediction.result, saveImages);
        }
    }
}
