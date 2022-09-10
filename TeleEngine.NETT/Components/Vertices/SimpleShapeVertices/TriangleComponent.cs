using Silk.NET.OpenGL;
using System.Drawing;

namespace TeleEngine.NET.Components.Vertices.SimpleShapeVertices
{
    public sealed class TriangleComponent : VertexComponent
    {
        private Color color;

        protected override Span<float> vertices =>
            new float[]
            {
                1f, 1F, 1f,
                1f, 0.5f, 0,
                1f, 0, 0.5f
            };

        public TriangleComponent(Color triangleColor) 
        {
            color = triangleColor;
        }

        public override Task StartAsync(GL openGL)
        {
            openGL.BlendColor(color);
            return base.StartAsync(openGL);
        }
    } 
}
