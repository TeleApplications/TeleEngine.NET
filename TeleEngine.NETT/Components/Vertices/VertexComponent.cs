using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using System.Numerics;
using TeleEngine.NET.Intefaces;

namespace TeleEngine.NET.Components.Vertices
{
    public abstract class VertexComponent : IComponent
    {
        public int ComponenetId { get; set; }
        public abstract Transform Transform { get; set; }

        public Vector3 DefaultColor { get; set; } = new(1f, 1f, 1f);

        protected virtual GLEnum vertexMode { get; } = GLEnum.Lines;
        public abstract VertexModel Model { get;}
        public VertexData Data { get; set; }

        public unsafe virtual async Task StartAsync(GL openGL, IWindow window)
        {
            var currentHandle = VertexHelper.CreatePointer(openGL, Model);

            Data = currentHandle;
        }

        public virtual async Task UpdateAsync(GL openGL) { }
    }
}
