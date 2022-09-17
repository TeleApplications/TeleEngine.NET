using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using System.Numerics;
using TeleEngine.NET.Intefaces;

namespace TeleEngine.NET.Components.Vertices
{
    public abstract class VertexComponent : IComponent
    {
        protected Shader vertexShader { get; set; }
        protected virtual GLEnum vertexMode { get; } = GLEnum.Lines;

        public abstract Transform Transform { get; set; }
        public abstract VertexModel Model { get; }

        public Vector3 DefaultColor { get; set; } = new(1f, 1f, 1f);
        public VertexData Data { get; set; }
        public int ComponenetId { get; set; }

        public virtual async Task StartAsync(GL openGL, IWindow window)
        {
            vertexShader = Shader.CreateDefaultShader(openGL);
            await vertexShader.BindAsync();

            var currentHandle = VertexHelper.CreatePointer(openGL, Model);
            Data = currentHandle;
        }

        public virtual async Task UpdateAsync(GL openGL)
        {
        }

        public virtual async Task RenderAsync(GL openGL) 
        {
            openGL.UseProgram(vertexShader.ShaderHandle);
            //vertexShader.SetValue("model", Transform.MatrixTransform);
        }
    }
}
