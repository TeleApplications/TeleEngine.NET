
namespace TeleEngine.NET.Components.Vertices.DefaultModels.Models
{
    public sealed class TriangleModel : ModelFactory<TriangleModel>
    {
        public override VertexModel Model =>
            new VertexModel()
            {
                Vertices = new float[]
                {
                    0.5f,  0.5f, 0.0f,
                    0.5f, -0.5f, 0.0f,
                   -0.5f, -0.5f, 0.0f,
                   -0.5f,  0.5f, 0.5f
                },
                Indexes = new uint[]
                {
                    0, 1, 3,
                    1, 2, 3
                }
            };
    }
}
