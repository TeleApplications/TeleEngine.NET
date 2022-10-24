using Silk.NET.OpenGL;

namespace TeleEngine.NET.Components.Vertices
{
    public readonly struct VertexData 
    {
        public uint ArrayBufferPointer { get; }
        public uint ElementBufferPointer { get; }
        public uint VertexBufferPointer { get; }

        public VertexData(uint arrayBuffer, uint elementBuffer, uint vertexBuffer) 
        {
            ArrayBufferPointer = arrayBuffer;
            ElementBufferPointer = elementBuffer;
            VertexBufferPointer = vertexBuffer;
        }
    }

    internal static class VertexHelper
    {
        public static VertexData CreatePointer(GL openGL, VertexModel model)
        {
            var vao = openGL.GenVertexArray();
            openGL.BindVertexArray(vao);

            var vbo = CreateHandle(ref openGL, model.Vertices.Span, BufferTargetARB.ArrayBuffer);
            var ebo = CreateHandle(ref openGL, model.Indexes.Span, BufferTargetARB.ElementArrayBuffer);

            return new VertexData(vbo, ebo, vao);
        }

        public static uint CreateShaderPointer(GL openGL, ShaderType shaderType, string source) 
        {
            uint shaderHandle = openGL.CreateShader(shaderType);
            openGL.ShaderSource(shaderHandle, source);
            openGL.CompileShader(shaderHandle);

            return shaderHandle;
        }

        private unsafe static uint CreateHandle<T>(ref GL openGL, ReadOnlySpan<T> vertices, BufferTargetARB glEnum) where T : unmanaged
        {
            var currentHandle = openGL.GenBuffer();
            openGL.BindBuffer(glEnum, currentHandle);

            nuint currentSize = (uint)(sizeof(T) * vertices.Length);
            fixed (void* data = vertices)
            {
                openGL.BufferData(glEnum, currentSize, data, BufferUsageARB.StaticDraw);
            }
            return currentHandle;
        }
    }
}
