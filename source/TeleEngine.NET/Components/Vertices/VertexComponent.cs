using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using System.Drawing;
using System.Numerics;
using TeleEngine.NET.Components.CameraComponenets.Interfaces;
using TeleEngine.NET.Intefaces;
using TeleEngine.NET.Shaders;

namespace TeleEngine.NET.Components.Vertices
{
    public abstract class VertexComponent : IComponent
    {
        protected IWindow? baseWindow;
        protected ShaderCore? vertexShader { get; set; }

        public abstract Transform Transform { get; set; }
        public abstract VertexModel Model { get; }

        public Color BaseColor { get; set; } = Color.Green;
        public VertexData Data { get; set; }
        public int ComponenetId { get; set; }

        public virtual async Task StartAsync(GL openGL, IWindow window)
        {
            if (vertexShader is null)
                throw new Exception("Current component didn't have any shader data");

            var currentHandle = VertexHelper.CreatePointer(openGL, Model);
            Data = currentHandle;
            baseWindow = window;
            await InicializateAsync(openGL);
        }

        public virtual async Task UpdateAsync(GL openGL, ICamera camera)
        {
        }

        public virtual async Task RenderAsync(GL openGL, ICamera camera) 
        {
            vertexShader.SetValues(camera, camera.ShaderResults);
            vertexShader.SetValue("uModel", Transform.CalculateMatrixTransform());

            var vectorColor = new Vector3(BaseColor.R, BaseColor.G, BaseColor.B);
            vertexShader.SetValue("vColor", vectorColor);
            openGL.UseProgram(vertexShader.ShaderHandle);
        }


        private async Task InicializateAsync(GL openGL) 
        {
            unsafe 
            {
                openGL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * ((uint)sizeof(float)), ((void*)(0 * sizeof(float))));
            }
            vertexShader = ShaderCore.CreateDefaultShader(openGL);
            await vertexShader.BindAsync();
        }
    }
}
