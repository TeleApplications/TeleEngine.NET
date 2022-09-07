using SharpGL;
using System.Collections.Immutable;
using System.Drawing;
using System.Numerics;

namespace TeleEngine.NET.Components.Vertices.SimpleShapeVertices
{
    public sealed class TriangleComponent : VertexComponent
    {
        private Color color;
        protected override ImmutableArray<Vector3> vertices 
            => ImmutableArray.Create
            (
                new Vector3[]
                {
                    new(0.5f, 1f, 0f),
                    new(0f, 0f, 0f),
                    new(1f, 0f, 0f),
                }
            );

        public TriangleComponent(Color triangleColor) 
        {
            color = triangleColor;
        }

        public override Task StartAsync(OpenGL openGL)
        {
            byte[] colorRgbArray = {color.R, color.G, color.B}; 
            openGL.Color(colorRgbArray);
            return base.StartAsync(openGL);
        }
    } 
}
