using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using TeleEngine.NET.Components.Vertices.DefaultModels.Models;

namespace TeleEngine.NET.Components.Vertices.SimpleShapeVertices
{
    public sealed class TriangleComponent : VertexComponent
    {
        public override Transform Transform { get; set; } =
            new Transform()
            {
                Position = new(0f, 0f, 0),
                Rotation = new(0f, 0f, 0f, 0f),
                Scale = 1 
            };

        public override VertexModel Model => TriangleModel.Shared.Model;

        public TriangleComponent() 
        {
        }

        public override Task StartAsync(GL openGL, IWindow window)
        {
            return base.StartAsync(openGL, window);
        }

        public override Task UpdateAsync(GL openGL)
        {
            var currentTransform = Transform;

            float rotationIndex = ((float)ComponenetId / 1000);
            currentTransform.Rotation = new(((currentTransform.Rotation.X + rotationIndex)), currentTransform.Rotation.Y + 0.001f, currentTransform.Rotation.Z, currentTransform.Rotation.W);
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
