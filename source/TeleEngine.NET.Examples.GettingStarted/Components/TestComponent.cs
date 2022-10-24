using Silk.NET.OpenGL;
using TeleEngine.NET.Components;
using TeleEngine.NET.Components.CameraComponenets.Interfaces;
using TeleEngine.NET.Components.Vertices;
using TeleEngine.NET.Models.Formats;

namespace TeleEngine.NET.Examples.GettingStarted.Components
{
    internal sealed class TestComponent : VertexComponent
    {
        public override Transform Transform { get; set; } =
            new()
            {
                Position = new(0, 0, 0),
                Rotation = System.Numerics.Quaternion.Identity,
                Scale = 0.75f
            };
        public override VertexModel Model =>
            new ObjFormat(Path.GetFullPath("Objects/teapot.obj")).CreateModel();

        public TestComponent() 
        {
            BaseColor = Color.White;
        }

        public override async Task UpdateAsync(GL openGL, ICamera camera)
        {
        }
    }
}
