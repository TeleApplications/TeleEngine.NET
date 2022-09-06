using SharpGL;
using System.Collections.Immutable;
using System.Numerics;

namespace TeleEngine.NET.Components.Vertices.SimpleShapeVertices
{
    public sealed class TriangleComponent : VertexComponent
    {
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

        public TriangleComponent(Color color) 
        {
        }

        public override Task StartAsync(OpenGL openGL)
        {
            return base.StartAsync(openGL);
        }
    } 
}
