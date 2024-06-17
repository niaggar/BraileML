using BraileML.Enums;
using BraileML.Models;
using ScottPlot;
using SkiaSharp;
using MathNet.Numerics.LinearAlgebra;
using System.Drawing;

namespace BraileML.Utils;

public class FileManager
{
    private static int numberOfImages = 20;
    private static char[] Characters = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
    private static char[] Characters2 = { '1', '0' };
    private static string[] Types = { "dim", "whs" };
    private static char[] Points = { '1', '2', '3', '4', '5', '6' };

    
    public static ImageModel[] ReadImages(string path)
    {
        var images = new List<ImageModel>();
        
        foreach (var character in Characters)
        {
            for (int i = 0; i < numberOfImages; i++)
            {
                for (int j = 0; j < Types.Length; j++)
                {
                    var filePath = $"{path}{character}1.JPG{i}{Types[j]}.jpg";
                    using Stream stream = File.OpenRead(filePath);
                    var image = SKBitmap.Decode(stream);
                
                    var matrix = new double[image.Width, image.Height];
                    for (var x = 0; x < image.Width; x++)
                    {
                        for (var y = 0; y < image.Height; y++)
                        {
                            var color = image.GetPixel(x, y);
                            var red = color.Red;
                            var green = color.Green;
                            var blue = color.Blue;
                        
                            var bitValue = (0.299 * red + 0.587 * green + 0.114 * blue);
                            matrix[x, y] = bitValue / 255;
                        }
                    }
                
                    images.Add(new ImageModel(character, matrix));
                }
            }
        }
        
        return images.ToArray();
    }
    
    public static ImageModel[] ReadImagesOnesZeros(string path)
    {
        var images = new List<ImageModel>();
        
        foreach (var character in Characters2)
        {
            for (int i = 1; i <= 30; i++)
            {
                var charNAme = character == '1' ? "Ones" : "Zeros";
                var filePath = $"{path}{charNAme}-{i}.jpg";
                using Stream stream = File.OpenRead(filePath);
                var image = SKBitmap.Decode(stream);
                
                var matrix = new double[image.Width, image.Height];
                for (var x = 0; x < image.Width; x++)
                {
                    for (var y = 0; y < image.Height; y++)
                    {
                        var color = image.GetPixel(x, y);
                        var red = color.Red;
                        var green = color.Green;
                        var blue = color.Blue;
                        
                        var bitValue = (0.299 * red + 0.587 * green + 0.114 * blue);
                        matrix[x, y] = bitValue / 255;
                    }
                }
                
                images.Add(new ImageModel(character, matrix));
            }
        }
        
        return images.ToArray();
    }

    public static Models.DataPoint[] ReadDataPoints(string path)
    {
        var dataPoints = new List<Models.DataPoint>();
        
        var lines = File.ReadAllLines(path);
        foreach (var line in lines)
        {
            var values = line.Split(',');
            var label = values[^1][0];
            var vector = values[..^1].Select(x => double.Parse(x)).ToArray().ToVector();
            
            dataPoints.Add(new Models.DataPoint(vector, label));
        }
        
        return dataPoints.ToArray();
    }

    public static ImageModel[] ReadImagesPoints(string path)
    {
        var images = new List<ImageModel>();

        foreach (var point in Points)
        {
            for (int i = 0; i < 75; i++)
            {
                var filePath = $"{path}{point}-{i}.jpg";
                using Stream stream = File.OpenRead(filePath);
                var image = SKBitmap.Decode(stream);

                var matrix = new double[image.Width, image.Height];
                for (var x = 0; x < image.Width; x++)
                {
                    for (var y = 0; y < image.Height; y++)
                    {
                        var color = image.GetPixel(x, y);
                        var red = color.Red;
                        var green = color.Green;
                        var blue = color.Blue;

                        //var bitValue = (0.299 * red + 0.587 * green + 0.114 * blue);
                        var bitValue = (0.587 * red + 0.114 * green + 0.299 * blue);
                        //var bitValue = (red + green + blue) / 3;
                        matrix[x, y] = bitValue / 255;
                    }
                }

                images.Add(new ImageModel(point, matrix));
            }
        }

        return images.ToArray();
    }

    public static ImageModel[] ReadImagesPointsAlphabet(string path)
    {
        var images = new List<ImageModel>();

        for (int i = 0; i < Characters.Length; i++)
        {
            for (int j = 0; j < 1; j++)
            {
                var character = Characters[i];
                var filePath = $"{path}{character}-{j}.jpg";
                using Stream stream = File.OpenRead(filePath);
                var image = SKBitmap.Decode(stream);

                var matrix = new double[image.Width, image.Height];
                for (var x = 0; x < image.Width; x++)
                {
                    for (var y = 0; y < image.Height; y++)
                    {
                        var color = image.GetPixel(x, y);
                        var red = color.Red;
                        var green = color.Green;
                        var blue = color.Blue;

                        //var bitValue = (0.299 * red + 0.587 * green + 0.114 * blue);
                        var bitValue = (0.587 * red + 0.114 * green + 0.299 * blue);
                        //var bitValue = (red + green + blue) / 3;
                        matrix[x, y] = bitValue / 255;
                    }
                }

                images.Add(new ImageModel(character, matrix));
            }
        }

        return images.ToArray();
    }

    public static void SaveImage(string path, ImageModel imageModel)
    {
        var bitmap = new SKBitmap(imageModel.Matrix.ColumnCount, imageModel.Matrix.RowCount);
        for (var x = 0; x < imageModel.Matrix.ColumnCount; x++)
        {
            for (var y = 0; y < imageModel.Matrix.RowCount; y++)
            {
                var value = (byte)(imageModel.Matrix[x, y] * 255);
                var color = new SKColor(value, value, value);
                bitmap.SetPixel(x, y, color);
            }
        }
        
        using var image = SKImage.FromBitmap(bitmap);
        using var data = image.Encode(SKEncodedImageFormat.Jpeg, 100);
        using var stream = File.OpenWrite(path);
        data.SaveTo(stream);
    }
}