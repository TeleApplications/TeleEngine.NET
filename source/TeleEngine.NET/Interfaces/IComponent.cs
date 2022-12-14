using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using TeleEngine.NET.Components;
using TeleEngine.NET.Components.CameraComponenets.Interfaces;
using TeleEngine.NET.Components.Vertices;

namespace TeleEngine.NET.Intefaces
{
    public interface IComponent
    {
        public int ComponenetId { get; set; }
        public VertexData Data { get; set; }

        public abstract Transform Transform { get; set; }
        public abstract VertexModel Model { get; }

        public async Task StartAsync(GL openGL, IWindow window) { }

        public async Task UpdateAsync(GL openGL, ICamera camera) { }

        public async Task RenderAsync(GL openGL, ICamera camera) { }
    }
}
