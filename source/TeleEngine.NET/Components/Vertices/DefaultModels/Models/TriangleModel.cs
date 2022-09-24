
namespace TeleEngine.NET.Components.Vertices.DefaultModels.Models
{
    public sealed class TriangleModel : ModelFactory<TriangleModel>
    {
        public override VertexModel Model =>
            new VertexModel()
            {
                Vertices = new float[]
                {
                      -1.0f, -1.0f, 1.0f, 1.0f,
                      -1.0f, 1.0f, -1.0f, 1.0f,

                       1.0f, -1.0f, -1.0f, 1.0f,
                       1.0f, 1.0f, -1.0f, 1.0f,
                       1.0f, -1.0f, 1.0f, 1.0f,
                       1.0f, -1.0f, 1.0f, 1.0f,
                       1.0f, 1.0f, -1.0f, 1.0f,
                       1.0f, 1.0f, 1.0f, 1.0f,

                      -1.0f, -1.0f, -1.0f, 1.0f,
                       1.0f, -1.0f, -1.0f, 1.0f,
                      -1.0f, -1.0f, 1.0f, 1.0f,
                      -1.0f, -1.0f, 1.0f, 1.0f,
                       1.0f, -1.0f, -1.0f, 1.0f,
                       1.0f, -1.0f, 1.0f, 1.0f,

                      -1.0f, 1.0f, -1.0f, 1.0f,
                      -1.0f, 1.0f, 1.0f, 1.0f,
                       1.0f, 1.0f, -1.0f, 1.0f,
                       1.0f, 1.0f, -1.0f, 1.0f,
                      -1.0f, 1.0f, 1.0f, 1.0f,
                       1.0f, 1.0f, 1.0f, 1.0f,

                      -1.0f, -1.0f, -1.0f, 1.0f,
                      -1.0f, 1.0f, -1.0f, 1.0f,
                       1.0f, -1.0f, -1.0f, 1.0f,
                       1.0f, -1.0f, -1.0f, 1.0f,
                      -1.0f, 1.0f, -1.0f, 1.0f,
                       1.0f, 1.0f, -1.0f, 1.0f, 

                      -1.0f, -1.0f, 1.0f, 1.0f,
                       1.0f, -1.0f, 1.0f, 1.0f,
                      -1.0f, 1.0f, 1.0f, 1.0f,
                      -1.0f, 1.0f, 1.0f, 1.0f,
                       1.0f, -1.0f, 1.0f, 1.0f,
                       1.0f, 1.0f, 1.0f, 1.0f,
                },
                Indexes = new uint[]
                {
                    0, 1, 3,
                    1, 2, 3
                }
            };
    }
}
