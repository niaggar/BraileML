using System.Linq.Expressions;
using BraileML.Interface;
using BraileML.Models;
using BraileML.Utils;
using MathNet.Numerics.LinearAlgebra;

namespace BraileML.Methods;

public class PerceptronLayer
{
    public int InpuntSize;
    public int OutputSize;

    public Matrix<double> Weights;
    public Vector<double> Biases;
    public IActivationFunction ActivationFunction;

    private Matrix<double> DeltaWeights;
    private Vector<double> DeltaBiases;
    
    public PerceptronLayer(int inputSize, int outputSize, IActivationFunction activationFunction)
    {
        InpuntSize = inputSize;
        OutputSize = outputSize;
        ActivationFunction = activationFunction;
        Weights = Matrix<double>.Build.Random(outputSize, inputSize);
        Biases = Vector<double>.Build.Random(outputSize);
        DeltaWeights = Matrix<double>.Build.Dense(outputSize, inputSize);
        DeltaBiases = Vector<double>.Build.Dense(outputSize);
    }
    
    public Vector<double> FeedForward(Vector<double> inputs, LayerLearn? learn = null)
    {
        var neuronOutputs = Weights.Multiply(inputs).Add(Biases);
        var layerOutputs = ActivationFunction.Activate(neuronOutputs);

        if (learn != null)
        {
            learn.Inputs = inputs;
            learn.NeuronOutputs = neuronOutputs;
            learn.LayerOutputs = layerOutputs;
        }

        return layerOutputs;
    }

    public void OutputLayerBackPropagation(LayerLearn layerLearnData, Vector<double> expectedOutputs, ILossFunction cost)
    {
        var derivateActivation = ActivationFunction.Derivative(layerLearnData.NeuronOutputs);
        var derivateCost = cost.Derivative(expectedOutputs, layerLearnData.LayerOutputs);

        layerLearnData.Delta = derivateCost.PointwiseMultiply(derivateActivation);
    }

    public void HiddenLayerBackPropagation(LayerLearn layerLearnData, PerceptronLayer oldLayer, Vector<double> oldDeltaValues)
    {
        var weightsTranspose = oldLayer.Weights.Transpose();
        var deltaDotWeights = weightsTranspose.Multiply(oldDeltaValues);
        var derivateActivation = ActivationFunction.Derivative(layerLearnData.NeuronOutputs);

        layerLearnData.Delta = deltaDotWeights.PointwiseMultiply(derivateActivation);
    }

    public void UpdateGradients(LayerLearn layerLearnData)
    {
        // Calculate gradients
        // Derivate of the cost function with respect to the weights
        var deltaWeights = layerLearnData.Delta.ToColumnMatrix() * layerLearnData.Inputs.ToRowMatrix();
        // Derivate of the cost function with respect to the biases
        var deltaBiases = layerLearnData.Delta;

        DeltaWeights += deltaWeights;
        DeltaBiases += deltaBiases;
    }

    public void ApplyGradients(double learningRate)
    {
        Weights -= DeltaWeights.Multiply(learningRate);
        Biases -= DeltaBiases.Multiply(learningRate);

        DeltaWeights.Clear();
        DeltaBiases.Clear();
    }
}