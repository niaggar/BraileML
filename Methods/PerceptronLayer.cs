using BraileML.Interface;
using BraileML.Models;
using MathNet.Numerics.LinearAlgebra;

namespace BraileML.Methods;

public class PerceptronLayer
{
    public Neuron[] Neurons { get; set; }
    public IActivationFunction ActivationFunction { get; set; }
    
    public PerceptronLayer(int inputSize, int outputSize, IActivationFunction activationFunction)
    {
        ActivationFunction = activationFunction;
        Neurons = new Neuron[outputSize];
        for (var i = 0; i < outputSize; i++) Neurons[i] = new Neuron(inputSize);
    }
    
    public Vector<double> FeedForward(Vector<double> inputs)
    {
        var outputs = Vector<double>.Build.Dense(Neurons.Length);
        foreach (var neuron in Neurons)
        {
            neuron.FeedForward(inputs);
            outputs.Add(neuron.Output);
        }
        
        return ActivationFunction.Activate(outputs);
    }
    
    public void Train(Vector<double> inputs, Vector<double> error, double learningRate)
    {
        for (var i = 0; i < Neurons.Length; i++)
        {
            var neuron = Neurons[i];
            neuron.Train(inputs, error[i], learningRate);
        }
    }
}