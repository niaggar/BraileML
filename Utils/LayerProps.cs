using BraileML.Interface;

namespace BraileML.Utils;

public class LayerProps(int inputSize, int outputSize, IActivationFunction activationFunction)
{
    public int InputSize { get; set; } = inputSize;
    public int OutputSize { get; set; } = outputSize;
    public IActivationFunction ActivationFunction { get; set; } = activationFunction;
}