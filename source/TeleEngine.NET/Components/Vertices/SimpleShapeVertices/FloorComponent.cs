
namespace TeleEngine.NET.Components.Vertices.SimpleShapeVertices
{
    public sealed class FloorComponent : VertexComponent
    {
        private const float floorIndex = 2.5f;

        public int VerticesCount { get; set; } = 200;
        public override VertexModel Model => GenerateVertexModel();
        public override Transform Transform { get; set; }

        private VertexModel GenerateVertexModel() 
        {
            var currentVertexModel = new VertexModel();
            for (int i = 0; i < VerticesCount; i++)
            {
                float currentValue = -floorIndex + (i / 4);
            }
            return currentVertexModel;
        }
    }
}
