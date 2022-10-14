
namespace TeleEngine.NET.Components.Vertices
{
    public sealed class VertexModel
    {
        public Memory<float> Vertices { get; set; }
        public Memory<float> Indexes { get; set; }

        public VertexModel() { }

        public VertexModel(Memory<float> vertices, Memory<float> indexes) 
        {
            Vertices = vertices;
            Indexes = indexes;
        }
    }
}
