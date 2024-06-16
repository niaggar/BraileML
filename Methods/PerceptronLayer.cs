using System.Linq.Expressions;
using BraileML.Interface;
using BraileML.Models;
using BraileML.Utils;
using MathNet.Numerics.LinearAlgebra;

namespace BraileML.Methods;

public class PerceptronLayer
{
    public Neuron[] Neurons { get; set; }
    public IActivationFunction ActivationFunction { get; set; }
    public Vector<double> Inputs { get; set; }
    public Vector<double> LayerOutputs { get; set; }
    public Vector<double> NeuronOutputs => Neurons.Select(x => x.Output).ToArray().ToVector();
    
    public PerceptronLayer(int inputSize, int outputSize, IActivationFunction activationFunction)
    {
        ActivationFunction = activationFunction;
        Neurons = new Neuron[outputSize];
        for (var i = 0; i < outputSize; i++) Neurons[i] = new Neuron(inputSize);
    }
    
    public Vector<double> FeedForward(Vector<double> inputs)
    {
        Inputs = inputs;
        
        var outputs = Vector<double>.Build.Dense(Neurons.Length);
        foreach (var neuron in Neurons)
        {
            neuron.FeedForward(inputs);
            outputs.Add(neuron.Output);
        }

        LayerOutputs = ActivationFunction.Activate(NeuronOutputs);
        return LayerOutputs;
    }
    
    public Vector<double> BackPropagation(Vector<double> deltaPrev)
    {
        var derivateActivation = ActivationFunction.Derivative(NeuronOutputs);
        var delta = deltaPrev.PointwiseMultiply(derivateActivation);
        
        for (var i = 0; i < Neurons.Length; i++)
        {
            var neuron = Neurons[i];
            var neuronDelta = delta[i];
            
            var dWeight = Inputs.Multiply(neuronDelta);
            var dBias = neuronDelta;
            
            neuron.BackPropagation(dWeight, dBias);
        }
        
        var weightsMatrix = Matrix<double>.Build.DenseOfColumnVectors(Neurons.Select(x => x.Weights));
        return weightsMatrix.Multiply(delta);
    }

    public void UpdateNeurons(double learningRate)
    {
        foreach (var neuron in Neurons)
        {
            neuron.UpdateWeightsAndBias(learningRate);
        }
    }
}