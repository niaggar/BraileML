


using BraileML.Enums;
using BraileML.Interface;
using BraileML.Methods;
using BraileML.Train;
using BraileML.Utils;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;


// var imagesPath = @"/Users/niaggar/Developer/Training/Braille Dataset/";
// var saveImagesPath = @"/Users/niaggar/Developer/Training/Braille Dataset/Analisis/";
var imagesPath = @"/Users/niaggar/Developer/Training/ZeroOne Dataset/";
var saveImagesPath = @"/Users/niaggar/Developer/Training/ZeroOne Dataset/Analisis copy/";

var treshold = 0.51;
var fileManager = new FileManager();
var images = fileManager.ReadImagesOnesZeros(imagesPath);

var lenghtCeroLayer = images[0].Vector.Count;

var network = new PerceptronNetwork(lenghtCeroLayer, [64, 32, 2],  [new Sigmoid(), new Sigmoid(), new Sigmoid()]);
network.Train(images, new OneZeroTrain(), 1000, 0.1);

var imageTest = images[0];
var result = network.FeedForward(imageTest.Vector);

Console.WriteLine($"Character: {imageTest.Character}");
Console.WriteLine($"Result: {result}");


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

