


using BraileML.Enums;
using BraileML.Interface;
using BraileML.Methods;
using BraileML.Models;
using BraileML.Train;
using BraileML.Utils;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System.Drawing;






var dataTest = @"C:\Users\nicho\Desktop\testData.csv";
var points = FileManager.ReadDataPoints(dataTest);
var layers = new LayerProps[]
{
    new (2, 10, new Sigmoid()),
    new (10, 5, new Sigmoid()),
    new (5, 2, new Sigmoid())
};

var train = new TestTrain();
var network = new PerceptronNetwork(layers, new NormSquare());
network.Train(points, train, 100000, 0.1);

var classOneTest = new DataPoint(TestTrain.OneExpected, '1');
var classTwoTest = new DataPoint(TestTrain.TwoExpected, '2');


var expectedVector1 = train.Expected(classOneTest.Label);
var result1 = network.Predict(classOneTest.Vector);
Console.WriteLine($"Character: {classOneTest.Label}");
Console.WriteLine($"Expected: {expectedVector1[0]}, {expectedVector1[1]}");
Console.WriteLine($"Result: {result1[0]}, {result1[1]}");


var expectedVector2 = train.Expected(classTwoTest.Label);
var result2 = network.Predict(classTwoTest.Vector);
Console.WriteLine($"Character: {classTwoTest.Label}");
Console.WriteLine($"Expected: {expectedVector2[0]} ,  {expectedVector2[1]}");
Console.WriteLine($"Result: {result2[0]} ,  {result2[1]}");








// var imagesPath = @"/Users/niaggar/Developer/Training/Braille Dataset/";
// var saveImagesPath = @"/Users/niaggar/Developer/Training/Braille Dataset/Analisis/";
//var imagesPath = @"/Users/niaggar/Developer/Training/ZeroOne Dataset/";
//var saveImagesPath = @"/Users/niaggar/Developer/Training/ZeroOne Dataset/Analisis copy/";

//var treshold = 0.51;
//var fileManager = new FileManager();
//var images = fileManager.ReadImagesOnesZeros(imagesPath);

//var lenghtCeroLayer = images[0].Vector.Count;

//var layers = new LayerProps[]
//{
//    new (lenghtCeroLayer, 100, new SoftMax()),
//    new (100, 50, new SoftMax()),
//    new (50, 2, new Sigmoid())
//};

//var network = new PerceptronNetwork(layers, new NormSquare());
//network.Train(images, new OneZeroTrain(), 200, 0.01);

//var imageTest = images[0];
//var result = network.Predict(imageTest.Vector);

//Console.WriteLine($"Character: {imageTest.Character}");
//Console.WriteLine($"Result: {result}");


// for (var i = 0; i < images.Length; i++)
// {
//     ImagesManager.Treshold(treshold, ref images[i]);
// }
//
//
//
// var matrix = DenseMatrix.OfColumnVectors(images.Select(x => x.Vector));
//
// var characters = images.Select(x => x.Character).ToArray();
// var result = SingularValueDescomposition.GetPrincipalComponents(matrix, characters);
//
// var pointsPCA12 = result.GetImagePoints(new[] { PrincipalComponentEnum.PC1, PrincipalComponentEnum.PC2 });
// var pointsPCA13 = result.GetImagePoints(new[] { PrincipalComponentEnum.PC1, PrincipalComponentEnum.PC3 });
// var pointsPCA14 = result.GetImagePoints(new[] { PrincipalComponentEnum.PC1, PrincipalComponentEnum.PC4 });
// var pointsPCA23 = result.GetImagePoints(new[] { PrincipalComponentEnum.PC2, PrincipalComponentEnum.PC3 });
// var pointsPCA24 = result.GetImagePoints(new[] { PrincipalComponentEnum.PC2, PrincipalComponentEnum.PC4 });
// var pointsPCA34 = result.GetImagePoints(new[] { PrincipalComponentEnum.PC3, PrincipalComponentEnum.PC4 });
//
//
// PlotManager.PlotPoints(pointsPCA12, "PCA", "PC1", "PC2", Path.Combine(saveImagesPath, "PCA12.png"));
// PlotManager.PlotPoints(pointsPCA13, "PCA", "PC1", "PC3", Path.Combine(saveImagesPath, "PCA13.png"));
// PlotManager.PlotPoints(pointsPCA14, "PCA", "PC1", "PC4", Path.Combine(saveImagesPath, "PCA14.png"));
// PlotManager.PlotPoints(pointsPCA23, "PCA", "PC2", "PC3", Path.Combine(saveImagesPath, "PCA23.png"));
// PlotManager.PlotPoints(pointsPCA24, "PCA", "PC2", "PC4", Path.Combine(saveImagesPath, "PCA24.png"));
// PlotManager.PlotPoints(pointsPCA34, "PCA", "PC3", "PC4", Path.Combine(saveImagesPath, "PCA34.png"));

