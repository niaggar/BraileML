using BraileML.Enums;
using BraileML.Models;
using ScottPlot;
using SkiaSharp;

namespace BraileML.Utils;

public class FileManager
{
    private int numberOfImages = 20;
    private char[] Characters = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };
    private char[] Characters2 = { '1', '0' };
    
    public ImageModel[] ReadImages(string path)
    {
        var images = new List<ImageModel>();
        
        foreach (var character in Characters)
        {
            for (int i = 0; i < numberOfImages; i++)
            {
                var filePath = $"{path}{character}1.JPG{i}dim.jpg";
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
    
    public ImageModel[] ReadImagesOnesZeros(string path)
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
}