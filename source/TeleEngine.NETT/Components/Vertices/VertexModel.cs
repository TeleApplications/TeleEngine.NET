
namespace TeleEngine.NET.Components.Vertices
{
    public struct VertexModel
    {
        public Memory<float> Vertices { get; set; }
        public Memory<uint> Indexes { get; set; }
    }
}
