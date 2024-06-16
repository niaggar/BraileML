using BraileML.Enums;
using BraileML.Methods;
using BraileML.Models;
using BraileML.Train;
using BraileML.Utils;
using MathNet.Numerics.LinearAlgebra.Double;





//var dataTest = @"C:\Users\nicho\Desktop\testData.csv";
//var points = FileManager.ReadDataPoints(dataTest);
//var layers = new LayerProps[]
//{
//    new (2, 4, new Sigmoid()),
//    new (4, 4, new Sigmoid()),
//    new (4, 2, new Sigmoid())
//};

//var train = new TestTrain();
//foreach (var point in points)
//{
//    train.SetExpected(point);
//}


//var network = new PerceptronNetwork(layers, new NormSquare());
//network.Train(points, 10000, 0.1);


//var classOneTest = new DataPoint([ 5, 8 ], '1');
//var classTwoTest = new DataPoint([ 15, 16 ], '2');


//var expectedVector1 = train.Expected(classOneTest.Label);
//var result1 = network.Predict(classOneTest.Vector, TestTrain.Labels);
//Console.WriteLine($"Character: {classOneTest.Label}");
//Console.WriteLine($"Result: {result1.prediction}");
//Console.WriteLine($"---------");


//var expectedVector2 = train.Expected(classTwoTest.Label);
//var result2 = network.Predict(classTwoTest.Vector, TestTrain.Labels);
//Console.WriteLine($"Character: {classTwoTest.Label}");
//Console.WriteLine($"Result: {result2.prediction}");
//Console.WriteLine($"---------");





//var imagesPath = @"C:\Users\nicho\Downloads\ZeroOne Dataset\";
//var images = FileManager.ReadImagesOnesZeros(imagesPath);
//var points = images.Select(x => x.ToDataPoint()).ToArray();
//var lenghtCeroLayer = points[0].Vector.Count;

//var train = new OneZeroTrain();
//foreach (var point in points)
//{
//    train.SetExpected(point);
//}

//var layers = new LayerProps[]
//{
//    new (lenghtCeroLayer, 64, new Sigmoid()),
//    new (64, 200, new Sigmoid()),
//    new (200, 2, new Sigmoid())
//};

//var network = new PerceptronNetwork(layers, new NormSquare());
//network.Train(points, 2000, 1);

//var imageTest = points[0];
//var expectedVector2 = train.Expected(imageTest.Label);
//var result2 = network.Predict(imageTest.Vector, TestTrain.Labels);
//Console.WriteLine($"Character: {imageTest.Label}");
//Console.WriteLine($"Result: {result2.prediction}");
//Console.WriteLine($"---------");



var imagesPath = @"/Users/niaggar/Developer/Training/Braille Dataset/";
var images = FileManager.ReadImages(imagesPath);
var points = images.Select(x => x.ToDataPoint()).ToArray();

var matrix = DenseMatrix.OfColumnVectors(points.Select(x => x.Vector));
var characters = images.Select(x => x.Character).ToArray();
var result = SingularValueDescomposition.GetPrincipalComponents(matrix, characters);
var pointsPCA = result.GetImagePoints(new[]
{
    PrincipalComponentEnum.PC1,
    PrincipalComponentEnum.PC2,
    PrincipalComponentEnum.PC3,
    PrincipalComponentEnum.PC4,
    PrincipalComponentEnum.PC5,
    PrincipalComponentEnum.PC6,
    PrincipalComponentEnum.PC7,
    PrincipalComponentEnum.PC8,
    PrincipalComponentEnum.PC9,
    PrincipalComponentEnum.PC10,
});

var train = new BraileABCTrain();
foreach (var point in pointsPCA)
{
    train.SetExpected(point);
}

var layers = new LayerProps[]
{
    new (10, 40, new Sigmoid()),
    new (40, 30, new Sigmoid()),
    new (30, 26, new Sigmoid())
};

var network = new PerceptronNetwork(layers, new NormSquare());
network.Train(pointsPCA, 10000, 0.01);

var imageTest = pointsPCA[35];
var expectedVector2 = train.Expected(imageTest.Label);
var result2 = network.Predict(imageTest.Vector, BraileABCTrain.Labels);
Console.WriteLine($"Character: {imageTest.Label}");
Console.WriteLine($"Result: {result2.prediction}");
Console.WriteLine($"---------");


var saveImagesPath = @"/Users/niaggar/Developer/Training/Braille Dataset/PCA/";

var pointsPCA12 = result.GetImagePoints(new[] { PrincipalComponentEnum.PC1, PrincipalComponentEnum.PC2 });
var pointsPCA13 = result.GetImagePoints(new[] { PrincipalComponentEnum.PC1, PrincipalComponentEnum.PC3 });
var pointsPCA14 = result.GetImagePoints(new[] { PrincipalComponentEnum.PC1, PrincipalComponentEnum.PC4 });
var pointsPCA23 = result.GetImagePoints(new[] { PrincipalComponentEnum.PC2, PrincipalComponentEnum.PC3 });
var pointsPCA24 = result.GetImagePoints(new[] { PrincipalComponentEnum.PC2, PrincipalComponentEnum.PC4 });
var pointsPCA34 = result.GetImagePoints(new[] { PrincipalComponentEnum.PC3, PrincipalComponentEnum.PC4 });


PlotManager.PlotPoints(pointsPCA12, "PCA", "PC1", "PC2", Path.Combine(saveImagesPath, "PCA12.png"));
PlotManager.PlotPoints(pointsPCA13, "PCA", "PC1", "PC3", Path.Combine(saveImagesPath, "PCA13.png"));
PlotManager.PlotPoints(pointsPCA14, "PCA", "PC1", "PC4", Path.Combine(saveImagesPath, "PCA14.png"));
PlotManager.PlotPoints(pointsPCA23, "PCA", "PC2", "PC3", Path.Combine(saveImagesPath, "PCA23.png"));
PlotManager.PlotPoints(pointsPCA24, "PCA", "PC2", "PC4", Path.Combine(saveImagesPath, "PCA24.png"));
PlotManager.PlotPoints(pointsPCA34, "PCA", "PC3", "PC4", Path.Combine(saveImagesPath, "PCA34.png"));



