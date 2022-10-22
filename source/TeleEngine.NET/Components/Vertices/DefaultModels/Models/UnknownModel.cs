
namespace TeleEngine.NET.Components.Vertices.DefaultModels.Models
{
    internal sealed class UnknownModel : ModelFactory<UnknownModel>
    {
        public override VertexModel Model => new()
        {
            Vertices = new float[] { 1 },
            Indexes = new uint[] { 1 }
        };
    }
}
