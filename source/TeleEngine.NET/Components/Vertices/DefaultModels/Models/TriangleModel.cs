
namespace TeleEngine.NET.Components.Vertices.DefaultModels.Models
{
    public sealed class TriangleModel : ModelFactory<TriangleModel>
    {
        public override VertexModel Model =>
            new VertexModel()
            {
                Vertices = new float[]
                {-0.5f, -0.5f, 0.0f, 
                           0.0f, -0.5f, 0.0f, 
                         -0.25f,  0.0f, 0.0f,
                         -0.25f,  0.0f, 0.0f,
                          0.25f,  0.0f, 0.0f,
                          0.0f,   0.5f, 0.0f,
                          0.0f,  -0.5f, 0.0f,
                          0.5f,  -0.5f, 0.0f,
                          0.25f,  0.0f, 0.0f
    },
                Indexes = new uint[]
                {
        2, 1, 4,
        1, 3, 4,
        0, 3, 1,
        3, 5, 4
    }
            };
    }
}
