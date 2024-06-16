using BraileML.Models;
using MathNet.Numerics.Statistics;
using MathNet.Numerics.LinearAlgebra.Double;

namespace BraileML.Methods;

public static class SingularValueDescomposition
{
    public static ResultSVD GetPrincipalComponents(DenseMatrix matrix, char[] characters)
    {
        var mean = matrix.Values.Mean();
        var matrixCentered = matrix.Subtract(mean);
        var desc = matrixCentered.Svd(true);
        
        var allPrincipalComponents = desc.U;
        
        var principalComponents = desc.U.SubMatrix(0, desc.U.RowCount, 0, 10);
        var projectedData = principalComponents.TransposeThisAndMultiply(matrixCentered);
        
        return new ResultSVD
        {
            Characters = characters,
            PrincipalComponents = principalComponents.ToArray(),
            ProjectedData = projectedData.ToArray()
        };
    }
}