﻿using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using System.Drawing;
using System.Numerics;
using TeleEngine.NET.Intefaces;
using TeleEngine.NET.Shaders;

namespace TeleEngine.NET.Components.Vertices
{
    public abstract class VertexComponent : IComponent
    {
        public Color BaseColor { get; set; } = Color.White;

        protected ShaderCore vertexShader { get; set; }
        protected virtual GLEnum vertexMode { get; } = GLEnum.Lines;

        public abstract Transform Transform { get; set; }
        public abstract VertexModel Model { get; }

        public VertexData Data { get; set; }
        public int ComponenetId { get; set; }

        public virtual async Task StartAsync(GL openGL, IWindow window)
        {
            var currentHandle = VertexHelper.CreatePointer(openGL, Model);
            Data = currentHandle;
            await InicializateAsync(openGL);
        }

        public virtual async Task UpdateAsync(GL openGL)
        {
        }

        public virtual async Task RenderAsync(GL openGL) 
        {
            vertexShader.SetValue("model", Transform.MatrixTransform);

            var vectorColor = new Vector3(BaseColor.R, BaseColor.G, BaseColor.B);
            vertexShader.SetValue("vColor", vectorColor);
            openGL.UseProgram(vertexShader.ShaderHandle);
        }


        private async Task InicializateAsync(GL openGL) 
        {
            unsafe 
            {
                openGL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * ((uint)sizeof(float)), ((void*)(0 * sizeof(float))));
                openGL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 5 * ((uint)sizeof(float)), ((void*)(3 * sizeof(float))));
                openGL.VertexAttribPointer(2, 4, VertexAttribPointerType.Float, false, 5 * ((uint)sizeof(float)), ((void*)(3 * sizeof(float))));
            }
            vertexShader = ShaderCore.CreateDefaultShader(openGL);
            await vertexShader.BindAsync();
        }
    }
}
