using BraileML.Interface;
using BraileML.Models;
using BraileML.Utils;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace BraileML.Methods;

public class PerceptronNetwork
{
    private PerceptronLayer[] Layers { get; set; }
    
    public PerceptronNetwork(int layerCero, int[] layersSizes, IActivationFunction[] activationFunction)
    {
        Layers = new PerceptronLayer[layersSizes.Length];
        Layers[0] = new PerceptronLayer(layerCero, layersSizes[0], activationFunction[0]);
        for (var i = 1; i < layersSizes.Length; i++)
        {
            Layers[i] = new PerceptronLayer(layersSizes[i - 1], layersSizes[i], activationFunction[i]);
        }
    }
    
    public Vector<double> FeedForward(Vector<double> inputs)
    {
        var result = inputs;
        foreach (var layer in Layers)
        {
            result = layer.FeedForward(result);
        }
        
        return result;
    }
    
    public void Train(ImageModel[] images, ITrain train, int epochs, double learningRate)
    {
        for (var i = 0; i < epochs; i++)
        {
            foreach (var image in images)
            {
                var result = FeedForward(image.Vector);
                var error = train.Expected(image.Character) - result;
                
                for (var j = Layers.Length - 1; j >= 0; j--)
                {
                    var inputs = j == 0 
                        ? image.Vector
                        : Layers[j - 1].Neurons.Select(x => x.Output).ToArray().ToVector();
                    Layers[j].Train(inputs, error, learningRate);
                    error = Layers[j].Neurons.Select(x => x.Weights * error).ToArray().ToVector();
                }
            }
        }
        
        Console.WriteLine("Training finished");
    }
    
    
}