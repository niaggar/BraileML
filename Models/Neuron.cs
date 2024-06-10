using BraileML.Interface;
using MathNet.Numerics.LinearAlgebra;

namespace BraileML.Models;

public class Neuron
{
    public Vector<double> Weights { get; set; }
    public double Bias { get; set; }
    public double Output { get; set; }
    
    
    public Neuron(int inputSize)
    {
        Weights = Vector<double>.Build.Random(inputSize);
        Bias = new Random().NextDouble();
    }
    
    public void FeedForward(Vector<double> inputs)
    {
        Output = Weights.DotProduct(inputs) + Bias;
    }
    
    public void Train(Vector<double> inputs, double error, double learningRate)
    {
        Weights = Weights.Add(inputs.Multiply(error * learningRate));
        Bias += error * learningRate;
    }
}