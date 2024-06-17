using BraileML.Enums;
using BraileML.Methods;
using BraileML.Models;
using BraileML.Programs;
using BraileML.Train;
using BraileML.Utils;
using MathNet.Numerics.LinearAlgebra.Double;


var momentum = 0.98;
var regularization = 0.1;
var epochs = 500000;

#region TestData

//var dataTest = @"C:\Users\nicho\Desktop\testData.csv";
//var points = FileManager.ReadDataPoints(dataTest);
//var layers = new LayerProps[]
//{
//    new (2, 10, new Sigmoid()),
//    new (10, 4, new Sigmoid()),
//    new (4, 2, new Sigmoid())
//};

//var train = new TestTrain();
//foreach (var point in points)
//{
//    train.SetExpected(point);
//}

//var (trainData, validateData) = DataSetUtil.SplitData(points, 1f);
//var trainBatches = DataSetUtil.CreateMiniBatches(trainData, 20);
//var network = new PerceptronNetwork(layers, new NormSquare());
//var exportTrend = new ExportData(@"C:\Users\nicho\Downloads\Braille Dataset\Trend.csv");
//var exportNetwork = new ExportData(@"C:\Users\nicho\Downloads\Braille Dataset\Network.dat");

//for (var i = 0; i < epochs; i++)
//{
//    DataSetUtil.ShuffleBatches(trainBatches);
//    foreach (var batch in trainBatches)
//    {
//        network.Train(batch.data, 0.05, regularization, momentum);
//    }


//    if (i % 1 != 0) continue;
//    var accuracy = network.Accuracy(points);
//    Console.WriteLine($"Epoch: {i}, Accuracy: {accuracy.Item2}, Loss: {accuracy.Item1}");
//    exportTrend.ExportTrend(i, accuracy.Item2, accuracy.Item1);

//    if (accuracy.Item1 <= 0.1)
//    {
//        break;
//    }
//}

//exportNetwork.ExportNetwork(network);
//exportNetwork.Close();
//exportTrend.Close();


//var classOneTest = new DataPoint([5, 8], '1');
//var classTwoTest = new DataPoint([15, 16], '2');


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

#endregion

#region Numbers

//var momentum = 0.9;
//var regularization = 0.1;
//var epochs = 6000;
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
//    new (64, 32, new Sigmoid()),
//    new (32, 2, new Sigmoid())
//};

//var (trainData, validateData) = DataSetUtil.SplitData(points, 1f);
//var trainBatches = DataSetUtil.CreateMiniBatches(trainData, 30);
//var network = new PerceptronNetwork(layers, new NormSquare());

//for (var i = 0; i < epochs; i++)
//{
//    DataSetUtil.ShuffleBatches(trainBatches);
//    foreach (var batch in trainBatches)
//    {
//        network.Train(batch.data, 0.01, regularization, momentum);
//    }

//    var accuracy = network.Accuracy(points);
//    Console.WriteLine($"Epoch: {i}, Accuracy: {accuracy.Item2}, Loss: {accuracy.Item1}");
//}

//var imageTest = points[0];
//var expectedVector2 = train.Expected(imageTest.Label);
//var result2 = network.Predict(imageTest.Vector, TestTrain.Labels);
//Console.WriteLine($"Character: {imageTest.Label}");
//Console.WriteLine($"Result: {result2.prediction}");
//Console.WriteLine($"---------");

#endregion

#region Braile downloaded

//var imagesPath = @"/Users/niaggar/Developer/Training/Braille Dataset/";
//var imagesPath = @"C:\Users\nicho\Downloads\Braille Dataset\";
//var images = FileManager.ReadImages(imagesPath);
//var points = images.Select(x => x.ToDataPoint()).ToArray();

//var train = new BraileABCTrain();
//foreach (var point in points)
//{
//    train.SetExpected(point);
//}

//var layers = new LayerProps[]
//{
//    new (points[0].Vector.Count, 40, new Sigmoid()),
//    new (40, 20, new Sigmoid()),
//    new (20, 26, new Sigmoid())
//};

//var (trainData, validateData) = DataSetUtil.SplitData(points, 1f);
//var trainBatches = DataSetUtil.CreateMiniBatches(trainData, 30);
//var network = new PerceptronNetwork(layers, new NormSquare());
//var exportTrend = new ExportData(@"C:\Users\nicho\Downloads\Braille Dataset\Trend-images-2.csv");
//var exportNetwork = new ExportData(@"C:\Users\nicho\Downloads\Braille Dataset\Network-images-2.dat");

//for (var i = 0; i < epochs; i++)
//{
//    DataSetUtil.ShuffleBatches(trainBatches);
//    foreach (var batch in trainBatches)
//    {
//        network.Train(batch.data, 0.01, regularization, momentum);
//    }


//    if (i % 10 != 0) continue;
//    var accuracy = network.Accuracy(points);
//    Console.WriteLine($"Epoch: {i}, Accuracy: {accuracy.Item2}, Loss: {accuracy.Item1}");
//    exportTrend.ExportTrend(i, accuracy.Item2, accuracy.Item1);

//    if (accuracy.Item1 <= 0.15)
//    {
//        break;
//    }
//}

//exportNetwork.ExportNetwork(network);
//exportNetwork.Close();
//exportTrend.Close();

//var imageTest = points[35];
//var expectedVector2 = train.Expected(imageTest.Label);
//var result2 = network.Predict(imageTest.Vector, BraileABCTrain.Labels);
//Console.WriteLine($"Character: {imageTest.Label}");
//Console.WriteLine($"Result: {result2.prediction}");
//Console.WriteLine($"---------");


//////var saveImagesPath = @"/Users/niaggar/Developer/Training/Braille Dataset/PCA/";
////var saveImagesPath = @"C:\Users\nicho\Downloads\Braille Dataset\PCA\";

////var pointsPCA12 = result.GetImagePoints(new[] { PrincipalComponentEnum.PC1, PrincipalComponentEnum.PC2 });
////var pointsPCA13 = result.GetImagePoints(new[] { PrincipalComponentEnum.PC1, PrincipalComponentEnum.PC3 });
////var pointsPCA14 = result.GetImagePoints(new[] { PrincipalComponentEnum.PC1, PrincipalComponentEnum.PC4 });
////var pointsPCA23 = result.GetImagePoints(new[] { PrincipalComponentEnum.PC2, PrincipalComponentEnum.PC3 });
////var pointsPCA24 = result.GetImagePoints(new[] { PrincipalComponentEnum.PC2, PrincipalComponentEnum.PC4 });
////var pointsPCA34 = result.GetImagePoints(new[] { PrincipalComponentEnum.PC3, PrincipalComponentEnum.PC4 });


////PlotManager.PlotPoints(pointsPCA12, "PCA", "PC1", "PC2", Path.Combine(saveImagesPath, "PCA12.png"));
////PlotManager.PlotPoints(pointsPCA13, "PCA", "PC1", "PC3", Path.Combine(saveImagesPath, "PCA13.png"));
////PlotManager.PlotPoints(pointsPCA14, "PCA", "PC1", "PC4", Path.Combine(saveImagesPath, "PCA14.png"));
////PlotManager.PlotPoints(pointsPCA23, "PCA", "PC2", "PC3", Path.Combine(saveImagesPath, "PCA23.png"));
////PlotManager.PlotPoints(pointsPCA24, "PCA", "PC2", "PC4", Path.Combine(saveImagesPath, "PCA24.png"));
////PlotManager.PlotPoints(pointsPCA34, "PCA", "PC3", "PC4", Path.Combine(saveImagesPath, "PCA34.png"));

#endregion



//new PrepareData().Run1();


//new PointsDetectionTrain().Run();


//new PointsDetection().Run1();
//Console.WriteLine("--------------------------------------------------------------");
//new PointsDetection().Run2();


new BraileABC().Run1();
Console.WriteLine("--------------------------------------------------------------");
new BraileABC().Run2();


