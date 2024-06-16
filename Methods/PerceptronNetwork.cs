using BraileML.Interface;
using BraileML.Models;
using BraileML.Utils;
using MathNet.Numerics.LinearAlgebra;

namespace BraileML.Methods;

public class PerceptronNetwork
{
    private PerceptronLayer[] Layers;
    private NetworkLearn NetworkLearn;
    private ILossFunction LossFunction;
    
    public PerceptronNetwork(LayerProps[] layers, ILossFunction lossFunction)
    {
        LossFunction = lossFunction;
        Layers = new PerceptronLayer[layers.Length];

        for (var i = 0; i < layers.Length; i++)
        {
            var inputSize = layers[i].InputSize;
            var outputSize = layers[i].OutputSize;
            var activationFunction = layers[i].ActivationFunction;
            
            Layers[i] = new PerceptronLayer(inputSize, outputSize, activationFunction);
        }

        NetworkLearn = new NetworkLearn(Layers);
    }
    
    public (char prediction, Vector<double> result) Predict(Vector<double> inputs, char[] labels)
    {
        for (var i = 0; i < Layers.Length; i++)
        {
            var layer = Layers[i];
            inputs = layer.FeedForward(inputs);
        }

        var result = inputs;
        var prediction = labels[result.MaximumIndex()];

        return (prediction, result);
    }
    
    public void Train(DataPoint[] points, int epochs, double learningRate)
    {
        for (var i = 0; i < epochs; i++)
        {
            foreach (var point in points)
            {
                UpdateGradient(point);
            }

            for (var j = 0; j < Layers.Length; j++)
            {
                Layers[j].ApplyGradients(learningRate);
                NetworkLearn.LayersLearn[j].Clear();
            }

            var accuracy = Accuracy(points);
            
            Console.WriteLine($"Epoch: {i}, Accuracy: {accuracy.Item2}, Loss: {accuracy.Item1}");
        }
    }

    private void UpdateGradient(DataPoint data)
    {
        var inputsToNextLayer = data.Vector;
        for (int i = 0; i < Layers.Length; i++)
        {
            inputsToNextLayer = Layers[i].FeedForward(inputsToNextLayer, NetworkLearn.LayersLearn[i]);
        }

        // -- Backpropagation --
        var outputLayer = Layers[^1];
        var outputLearnData = NetworkLearn.LayersLearn[^1];

        // Update output layer gradients
        outputLayer.OutputLayerBackPropagation(outputLearnData, data.Expected, LossFunction);
        outputLayer.UpdateGradients(outputLearnData);

        // Update all hidden layer gradients
        for (int i = Layers.Length - 2; i >= 0; i--)
        {
            var layerLearnData = NetworkLearn.LayersLearn[i];
            var hiddenLayer = Layers[i];
            var oldLayer = Layers[i + 1];
            var oldDeltaValues = NetworkLearn.LayersLearn[i + 1].Delta;

            hiddenLayer.HiddenLayerBackPropagation(layerLearnData, oldLayer, oldDeltaValues);
            hiddenLayer.UpdateGradients(layerLearnData);
        }
    }
    
    public (double, double) Accuracy(DataPoint[] poitns)
    {
        var correct = 0;
        var loss = 0.0;
        foreach (var point in poitns)
        {
            var inputs = point.Vector;
            for (var i = 0; i < Layers.Length; i++)
            {
                var layer = Layers[i];
                inputs = layer.FeedForward(inputs);
            }


            var result = inputs;
            var expectedVector = point.Expected;
            loss += LossFunction.Calculate(expectedVector, result);
            
            var expected = expectedVector.MaximumIndex();
            var predicted = result.MaximumIndex();
            
            if (expected == predicted) correct++;
        }

        return (loss / poitns.Length, (double)correct / poitns.Length);
    }
}