using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using System.Drawing;
using TeleEngine.NET.Components.Vertices.DefaultModels.Models;

namespace TeleEngine.NET.Components.Vertices.SimpleShapeVertices
{
    public sealed class TriangleComponent : VertexComponent
    {
        private Color color;

        public override Transform Transform { get; set; } =
            new Transform()
            {
                Position = new(0.02f, 0, 1),
                Rotation = new(0f, 0f, 0f, 0f),
                Scale = 1 
            };

        public override VertexModel Model => TriangleModel.Shared.Model;

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

            currentTransform.Rotation = new(((currentTransform.Rotation.X + 0.001f)), currentTransform.Rotation.Y, currentTransform.Rotation.Z, currentTransform.Rotation.W);
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
