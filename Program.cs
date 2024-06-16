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



var imagesPath = @"C:\Users\nicho\Downloads\Braille Dataset\";
var images = FileManager.ReadImages(imagesPath);
var points = images.Select(x => x.ToDataPoint()).ToArray();

var matrix = DenseMatrix.OfColumnVectors(points.Select(x => x.Vector));
var characters = images.Select(x => x.Character).ToArray();
var result = SingularValueDescomposition.GetPrincipalComponents(matrix, characters);
var pointsPCA12 = result.GetImagePoints(new[] { PrincipalComponentEnum.PC1, PrincipalComponentEnum.PC2, PrincipalComponentEnum.PC3 });

var train = new BraileABCTrain();
foreach (var point in pointsPCA12)
{
    train.SetExpected(point);
}

var layers = new LayerProps[]
{
    new (3, 20, new Sigmoid()),
    new (20, 15, new Sigmoid()),
    new (15, 8, new Sigmoid())
};

var network = new PerceptronNetwork(layers, new NormSquare());
network.Train(pointsPCA12, 10000, 0.05);

var imageTest = pointsPCA12[35];
var expectedVector2 = train.Expected(imageTest.Label);
var result2 = network.Predict(imageTest.Vector, BraileABCTrain.Labels);
Console.WriteLine($"Character: {imageTest.Label}");
Console.WriteLine($"Result: {result2.prediction}");
Console.WriteLine($"---------");

