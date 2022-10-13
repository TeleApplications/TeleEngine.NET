using TeleEngine.NET.MathComponents.Vectors;

namespace TeleEngine.NET.Models
{
    public readonly struct ModelData
    {
        public ReadOnlyMemory<Vector3D> Vertices { get; }
        public ReadOnlyMemory<float> Indices { get; }

        public ModelData(Memory<Vector3D> vertices, Memory<float> indices)
        {
            Vertices = vertices;
            Indices = indices; 
        }
    }
}
