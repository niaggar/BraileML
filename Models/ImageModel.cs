using MathNet.Numerics.LinearAlgebra;

namespace BraileML.Models;

public class ImageModel
{
    public Guid Id { get; set; }
    public char Character { get; set; }
    public Matrix<double> Matrix { get; set; }
    public Vector<double> Vector => Vector<double>.Build.DenseOfArray(Matrix.ToColumnMajorArray());
    
    public ImageModel()
    {
        Id = Guid.NewGuid();
    }
    
    public ImageModel(char character, double[,] matrix)
    {
        Id = Guid.NewGuid();
        Character = character;
        Matrix = Matrix<double>.Build.DenseOfArray(matrix);
    }
}