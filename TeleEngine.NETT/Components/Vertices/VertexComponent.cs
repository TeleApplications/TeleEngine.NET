using Silk.NET.OpenGL;
using System.Numerics;
using TeleEngine.NET.Intefaces;

namespace TeleEngine.NET.Components.Vertices
{
    public abstract class VertexComponent : IComponent
    {
        public int ComponenetId { get; set; }
        public Vector3 DefaultColor { get; set; } = new(1f, 1f, 1f);

        protected abstract Span<float> vertices { get; }
        protected virtual GLEnum vertexMode { get; } = GLEnum.Lines;

        public Vector3 Position { get; set; } = Vector3.Zero;
        public Vector3 Rotation { get; set; } = Vector3.Zero;

        public unsafe virtual async Task StartAsync(GL openGL)
        {
            uint currentHandle = VertexHelper<float>.CreatePointer(openGL, vertices, vertexMode);
            fixed (void* data = vertices) 
            {
                openGL.BindVertexArray((uint)data);
            }
            openGL.DrawArrays(PrimitiveType.Lines, 0, (uint)vertices.Length);
        }

        public virtual async Task UpdateAsync(GL openGL) { }
    }
}
