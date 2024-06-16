using BraileML.Interface;
using MathNet.Numerics.LinearAlgebra;

namespace BraileML.Models;

public class Neuron(int inputSize)
{
    public Vector<double> Weights { get; set; } = Vector<double>.Build.Random(inputSize);
    public double Bias { get; set; } = new Random().NextDouble();
    public double Output { get; set; } = 0;
    private Vector<double> AverageDWeights { get; set; } = Vector<double>.Build.Dense(inputSize);
    private double AverageDBias { get; set; } = 0;
    
    public void FeedForward(Vector<double> inputs)
    {
        Output = Weights.DotProduct(inputs) + Bias;
    }

    public void BackPropagation(Vector<double> dWeight, double dBias)
    {
        AverageDWeights = (AverageDWeights + dWeight) / 2;
        AverageDBias = (AverageDBias + dBias) / 2;
    }
    
    public void UpdateWeightsAndBias(double learningRate)
    {
        Weights -= AverageDWeights * learningRate;
        Bias -= AverageDBias * learningRate;
        
        AverageDWeights = Vector<double>.Build.Dense(Weights.Count);
        AverageDBias = 0;
    }
}