using BraileML.Interface;
using BraileML.Models;
using BraileML.Utils;
using MathNet.Numerics.LinearAlgebra;

namespace BraileML.Methods;

public class PerceptronNetwork
{
    private PerceptronLayer[] Layers { get; set; }
    public ILossFunction LossFunction => ((PerceptronOutputLayer)Layers[^1]).LossFunction;
    
    public PerceptronNetwork(LayerProps[] layers, ILossFunction lossFunction)
    {
        Layers = new PerceptronLayer[layers.Length];
        for (var i = 0; i < layers.Length - 1; i++)
        {
            Layers[i] = new PerceptronLayer(layers[i].InputSize, layers[i].OutputSize, layers[i].ActivationFunction);
        }
        
        Layers[^1] = new PerceptronOutputLayer(layers[^1].InputSize, layers[^1].OutputSize, layers[^1].ActivationFunction, lossFunction);
    }
    
    public Vector<double> Predict(Vector<double> inputs)
    {
        var result = FeedForward(inputs);
        return result;
    }
    
    public void Train(DataPoint[] points, ITrain train, int epochs, double learningRate)
    {
        for (var i = 0; i < epochs; i++)
        {
            
            
            foreach (var point in points)
            {
                var result = FeedForward(point.Vector);
                var expected = train.Expected(point.Label);
                
                BackPropagation(result, expected);
                foreach (var layer in Layers)
                {
                    layer.UpdateNeurons(learningRate);
                }
            }
            
           

            var accuracy = Accuracy(points, train);
            Console.WriteLine($"Epoch {i + 1} - Accuracy: {accuracy}");

            if (accuracy >= 0.95) break;
        }
        
        Console.WriteLine("Training finished");
    }
    
    private void BackPropagation(Vector<double> result, Vector<double> expected)
    {
        var endLayer = (PerceptronOutputLayer)Layers[^1];
        var deltaPrev = endLayer.BackPropagation(result, expected);
        
        for (var i = Layers.Length - 2; i >= 0; i--)
        {
            deltaPrev = Layers[i].BackPropagation(deltaPrev);
        }
    }
    
    private Vector<double> FeedForward(Vector<double> inputs)
    {
        var result = inputs;
        foreach (var layer in Layers)
        {
            result = layer.FeedForward(result);
        }
        
        return result;
    }
    
    public double Accuracy(DataPoint[] poitns, ITrain train)
    {
        var correct = 0;
        foreach (var point in poitns)
        {
            var result = Predict(point.Vector);
            var expectedVector = train.Expected(point.Label);
            
            var expected = expectedVector.MaximumIndex();
            var predicted = result.MaximumIndex();
            
            if (expected == predicted) correct++;
        }
        
        return (double)correct / poitns.Length;
    }
}