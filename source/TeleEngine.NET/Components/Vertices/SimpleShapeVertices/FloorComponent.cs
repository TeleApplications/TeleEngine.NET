
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
                -2.25f, 0, floorIndex,
                -2.25f, 0, -floorIndex,
                floorIndex, 0, -2.25f,
                -floorIndex, 0, -2.25f,
            },
            Indexes = new uint[] 
            {
               0, 1, 2
            }
        };

        public override Transform Transform { get; set; } = new()
        {
            Position = new(0, -2, 0),
            Rotation = System.Numerics.Quaternion.Identity,
            Scale = 1f
        };
    }
}
