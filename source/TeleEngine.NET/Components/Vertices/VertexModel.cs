
namespace TeleEngine.NET.Components.Vertices
{
    public sealed class VertexModel
    {
        public ReadOnlyMemory<float> Vertices { get; set; }
        public ReadOnlyMemory<uint> Indexes { get; set; }

        public VertexModel() { }

        public VertexModel(ReadOnlyMemory<float> vertices, ReadOnlyMemory<uint> indexes) 
        {
            Vertices = vertices;
            Indexes = indexes;
        }
    }
}
