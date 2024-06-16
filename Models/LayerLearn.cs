using BraileML.Methods;
using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BraileML.Models;

public class LayerLearn
{
    public Vector<double> Inputs;          // from previous layer
    public Vector<double> NeuronOutputs;   // before activation function
    public Vector<double> LayerOutputs;    // after activation function
    public Vector<double> Delta;           // error from next layer

    public LayerLearn(PerceptronLayer perceptronLayer)
    {
        Inputs = Vector<double>.Build.Dense(perceptronLayer.InpuntSize);
        NeuronOutputs = Vector<double>.Build.Dense(perceptronLayer.OutputSize);
        LayerOutputs = Vector<double>.Build.Dense(perceptronLayer.OutputSize);
        Delta = Vector<double>.Build.Dense(perceptronLayer.OutputSize);
    }

    public void Clear()
    {
        Inputs.Clear();
        NeuronOutputs.Clear();
        LayerOutputs.Clear();
        Delta.Clear();
    }
}