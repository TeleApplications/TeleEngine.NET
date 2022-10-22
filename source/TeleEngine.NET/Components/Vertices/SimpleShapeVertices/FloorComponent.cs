
namespace TeleEngine.NET.Components.Vertices.SimpleShapeVertices
{
    public sealed class FloorComponent : VertexComponent
    {
        private const float floorIndex = 2.5f;
        private const int verticesLength = 4;

        public int VerticesCount { get; set; } = 200;
        public override VertexModel Model => new()
        {
            Vertices = new float[] 
            {
                   -0.5f,  0, -0.5f,  0.0f, 1.0f,
                    0.5f,  0, -0.5f,  0.0f, 1.0f,
                    0.5f,  0f,  0.5f,  0.0f, 1.0f,

                   -0.5f,  0, 0.5f,  0.0f, 1.0f,
                   0.5f,  0, 0.5f,  0.0f, 1.0f,
                   -0.5f,  0f,  -0.5f,  0.0f, 1.0f,

                   -1.5f,  0, -0.5f,  0.0f, 1.0f,
                    0.5f,  0, -0.5f,  0.0f, 1.0f,
                    0.5f,  0f,  0.5f,  0.0f, 1.0f,

                   -1.5f,  0, 0.5f,  0.0f, 1.0f,
                   0.5f,  0, 0.5f,  0.0f, 1.0f,
                   -1.5f,  0f,  -0.5f,  0.0f, 1.0f,

                   -2.5f,  0, -0.5f,  0.0f, 1.0f,
                    0.5f,  0, -0.5f,  0.0f, 1.0f,
                    0.5f,  0f,  0.5f,  0.0f, 1.0f,

                   -2.5f,  0, 0.5f,  0.0f, 1.0f,
                   0.5f,  0, 0.5f,  0.0f, 1.0f,
                   -2.5f,  0f,  -0.5f,  0.0f, 1.0f,

                   -0.5f,  0, -0.5f,  0.0f, 1.0f,
                    0.5f,  0, -0.5f,  0.0f, 1.0f,
                    0.5f,  0f,  0.5f,  0.0f, 1.0f,

                   -0.5f,  0, 0.5f,  0.0f, 1.0f,
                   0.5f,  0, 0.5f,  0.0f, 1.0f,
                   -0.5f,  0f,  -0.5f,  0.0f, 1.0f,
            },
            Indexes = new uint[] 
            {
               0
            }
        };

        public override Transform Transform { get; set; } = new()
        {
            Position = new(0, -1, 0),
            Rotation = System.Numerics.Quaternion.Identity,
            Scale = 1f
        };
    }
}
