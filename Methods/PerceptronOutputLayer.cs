using BraileML.Interface;
using MathNet.Numerics.LinearAlgebra;

namespace BraileML.Methods;

public class PerceptronOutputLayer : PerceptronLayer
{
    public ILossFunction LossFunction { get; set; }
    
    public PerceptronOutputLayer(int inputSize, int outputSize, IActivationFunction activationFunction, ILossFunction lossFunction) 
        : base(inputSize, outputSize, activationFunction)
    {
        LossFunction = lossFunction;
    }

    public Vector<double> BackPropagation(Vector<double> result, Vector<double> expected)
    {
        var derivateLoss = LossFunction.Derivative(expected, result);
        var derivateActivation = ActivationFunction.Derivative(NeuronOutputs);
        var delta = derivateLoss.PointwiseMultiply(derivateActivation);
        
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
}