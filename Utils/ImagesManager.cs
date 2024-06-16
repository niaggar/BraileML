using BraileML.Models;
using MathNet.Numerics.LinearAlgebra;

namespace BraileML.Utils;

public static class ImagesManager
{
    static Random _random = new();
    
    public static void Treshold(double threshold, ref ImageModel imageModel)
    {
        imageModel.Matrix.MapIndexedInplace((i, j, value) => value > threshold ? 1 : 0);
    }
    
    public static void Resize(int width, int height, ref ImageModel imageModel)
    {
        // Resize the image to the specified width and height
        var resizedMatrix = Matrix<double>.Build.Dense(width, height);
        
        var originalWidth = imageModel.Matrix.ColumnCount;
        var originalHeight = imageModel.Matrix.RowCount;
        
        var xRatio = originalWidth / (double)width;
        var yRatio = originalHeight / (double)height;
        
        // Loop through the resized matrix, filling it with the values from the original matrix
        // method: Mean value
        for (var i = 0; i < width; i++)
        {
            for (var j = 0; j < height; j++)
            {
                var x = (int)(i * xRatio);
                var y = (int)(j * yRatio);
                
                var sum = 0.0;
                for (var k = 0; k < xRatio; k++)
                {
                    for (var l = 0; l < yRatio; l++)
                    {
                        sum += imageModel.Matrix[x + k, y + l];
                    }
                }
                
                resizedMatrix[i, j] = sum / (xRatio * yRatio);
            }
        }
        
        imageModel.Matrix = resizedMatrix;
    }
    
    public static void RandomNoise(double noise, ref ImageModel imageModel)
    {
        imageModel.Matrix.MapInplace(value => value + (value * noise * (_random.NextDouble() - 0.5)));
    }
    
    public static void Rotate(double angle, ref ImageModel imageModel)
    {
        var radians = angle * Math.PI / 180;
        
        var cos = Math.Cos(radians);
        var sin = Math.Sin(radians);
        
        var originalWidth = imageModel.Matrix.ColumnCount;
        var originalHeight = imageModel.Matrix.RowCount;
        
        var rotatedMatrix = Matrix<double>.Build.Dense(originalWidth, originalHeight);
        rotatedMatrix.MapInplace(value => 1);
        
        var centerX = originalWidth / 2;
        var centerY = originalHeight / 2;
        
        for (var i = 0; i < originalWidth; i++)
        {
            for (var j = 0; j < originalHeight; j++)
            {
                var x = i - centerX;
                var y = j - centerY;
                
                var newX = (int)(x * cos - y * sin + centerX);
                var newY = (int)(x * sin + y * cos + centerY);
                
                if (newX >= 0 && newX < originalWidth && newY >= 0 && newY < originalHeight)
                {
                    rotatedMatrix[i, j] = imageModel.Matrix[newX, newY];
                }
            }
        }
        
        imageModel.Matrix = rotatedMatrix;
    }
    
    public static void Zoom(double zoom, ref ImageModel imageModel)
    {
        // Zoom the image by the specified factor, maintaining the center of the image and the size
        var originalWidth = imageModel.Matrix.ColumnCount;
        var originalHeight = imageModel.Matrix.RowCount;
        
        var zoomedMatrix = Matrix<double>.Build.Dense(originalWidth, originalHeight);
        zoomedMatrix.MapInplace(value => 1);
        
        var centerX = originalWidth / 2;
        var centerY = originalHeight / 2;
        
        for (var i = 0; i < originalWidth; i++)
        {
            for (var j = 0; j < originalHeight; j++)
            {
                var x = i - centerX;
                var y = j - centerY;
                
                var newX = (int)(x * zoom + centerX);
                var newY = (int)(y * zoom + centerY);
                
                if (newX >= 0 && newX < originalWidth && newY >= 0 && newY < originalHeight)
                {
                    zoomedMatrix[i, j] = imageModel.Matrix[newX, newY];
                }
            }
        }
    }
    
    public static void Translate(int x, int y, ref ImageModel imageModel)
    {
        // Translate the image by the specified amount in the x and y directions
        var originalWidth = imageModel.Matrix.ColumnCount;
        var originalHeight = imageModel.Matrix.RowCount;
        
        var translatedMatrix = Matrix<double>.Build.Dense(originalWidth, originalHeight);
        translatedMatrix.MapInplace(value => 1);
        
        for (var i = 0; i < originalWidth; i++)
        {
            for (var j = 0; j < originalHeight; j++)
            {
                var newX = i + x;
                var newY = j + y;
                
                if (newX >= 0 && newX < originalWidth && newY >= 0 && newY < originalHeight)
                {
                    translatedMatrix[i, j] = imageModel.Matrix[newX, newY];
                }
            }
        }
        
        imageModel.Matrix = translatedMatrix;
    }
}