using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using System.Drawing;
using System.Numerics;

namespace TeleEngine.NET.Components.Vertices.SimpleShapeVertices
{
    public sealed class TriangleComponent : VertexComponent
    {
        private Color color;

        public override Transform Transform { get; set; } =
            new Transform()
            {
                Position = new(20, 1, 1),
                Rotation = Quaternion.Identity,
                Scale = 2 
            };

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

        public TriangleComponent(Color triangleColor) 
        {
            color = triangleColor;
        }

        public override Task StartAsync(GL openGL, IWindow window)
        {
            return base.StartAsync(openGL, window);
        }

        public override Task UpdateAsync(GL openGL)
        {
            var currentTransform = Transform;
            currentTransform.Position = new(((currentTransform.Position.X + 5)), currentTransform.Position.Y, currentTransform.Position.Z);
            Transform = new Transform()
            {
                Position = currentTransform.Position,
                Rotation = currentTransform.Rotation,
                Scale = currentTransform.Scale
            };


            return base.UpdateAsync(openGL);
        }
    } 
}
