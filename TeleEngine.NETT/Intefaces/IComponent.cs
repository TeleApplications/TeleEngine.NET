using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using System.Numerics;
using TeleEngine.NET.Components;
using TeleEngine.NET.Components.Vertices;

namespace TeleEngine.NET.Intefaces
{
    public interface IComponent
    {
        public int ComponenetId { get; set; }
        public abstract Transform Transform { get; set; }

        public abstract VertexModel Model { get; }
        public VertexData Data { get; set; }

        public async Task StartAsync(GL openGL, IWindow window) { }

        public async Task UpdateAsync(GL openGL) { }
    }
}
