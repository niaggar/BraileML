using BraileML.Train;
using BraileML.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BraileML.Programs;

public class BraileABC
{
    public void Run1()
    {
        // Prepare the data
        var path = @"C:\Users\nicho\Downloads\Braille Dataset\";
        var network = DataControl.LoadNetwork(@"C:\Users\nicho\Downloads\Braille Dataset\Network-images-2.dat");

        var images = FileManager.ReadImages(path);

        var numImageToTest = 5;
        for (var i = 0; i < numImageToTest; i++)
        {
            // Take a random image
            var index = new Random().Next(images.Length);
            var image = images[index];
            var prediction = network.Predict(image.Vector, BraileABCTrain.Labels);
            Console.WriteLine($"Character: {image.Character}");
            Console.WriteLine($"Result: {prediction.prediction}");
            Console.WriteLine($"---------");
        }
    }

    public void Run2()
    {
        // Prepare the data
        var path = @"C:\Users\nicho\Desktop\TrainImages\Convinations\Processed\";
        var network = DataControl.LoadNetwork(@"C:\Users\nicho\Downloads\Braille Dataset\Network-images-2.dat");

        var images = FileManager.ReadImagesPointsAlphabet(path);
        var numImageToTest = 26;
        for (var i = 0; i < numImageToTest; i++)
        {
            // Take a random image
            var image = images[i];
            var prediction = network.Predict(image.Vector, BraileABCTrain.Labels);

            Console.WriteLine($"Character: {image.Character}");
            Console.WriteLine($"Result: {prediction.prediction}");
            Console.WriteLine($"---------");
        }
    }
}
