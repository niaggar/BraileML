using BraileML.Methods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BraileML.Models;

public class NetworkLearn
{
    public LayerLearn[] LayersLearn { get; set; }

    public NetworkLearn(PerceptronLayer[] perceptronLayers)
    {
        LayersLearn = new LayerLearn[perceptronLayers.Length];
        for (var i = 0; i < perceptronLayers.Length; i++)
        {
            LayersLearn[i] = new LayerLearn(perceptronLayers[i]);
        }
    }
}