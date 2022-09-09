
using Silk.NET.OpenGL;

namespace TeleEngine.NET.Components.Vertices
{
    internal static class VertexHelper<T> where T : unmanaged
    {
        public unsafe static uint CreatePointer(GL openGL, Span<T> vertices, GLEnum glEnum)
        {
            uint currentHandle = openGL.GenBuffer();
            openGL.BindBuffer(glEnum, currentHandle);
            openGL.BufferData<T>(BufferTargetARB.ArrayBuffer, vertices, BufferUsageARB.DynamicDraw);

            return currentHandle;
        }
    }
}
