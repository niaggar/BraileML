using BraileML.Models;

namespace BraileML.Utils;

public static class ImagesManager
{
    public static void Treshold(double threshold, ref ImageModel imageModel)
    {
        imageModel.Matrix.MapIndexedInplace((i, j, value) => value > threshold ? 1 : 0);
    }
}