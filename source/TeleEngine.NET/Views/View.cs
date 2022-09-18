﻿using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using System.Diagnostics;
using System.Numerics;
using TeleEngine.NET.Components.Vertices;
using TeleEngine.NET.Intefaces;

namespace TeleEngine.NET.Views
{
    public abstract partial class View : Intefaces.IView
    {
        public IWindow ViewWindow { get; set; }
        public WindowOptions Options { get; set; }

        protected virtual IList<IComponent> Components { get; set; }

        public async Task AddComponent<T>(T component) where T : IComponent
        {
            component.ComponenetId = Components.Count;
            Components.Add(component);
            await component.StartAsync(OpenGL, ViewWindow);
            await component.UpdateAsync(OpenGL);
        }

        public void RemoveComponent<T>(T component) where T : IComponent
        {
            var componentVectorId = new Vector<int>(component.ComponenetId);
            int diferrence = Components.Count - Vector<int>.Count;

            for (int i = 0; i < diferrence; i++)
            {
                var currentVector = new Vector<int>(i);
                if (Vector.EqualsAll(componentVectorId, currentVector)) 
                {
                    Components.RemoveAt(i);
                    return;
                }
            }

            for (int j = diferrence; j < Components.Count; j++)
            {
                int currentComponentId = component.ComponenetId;
                if (currentComponentId == j) 
                {
                    Components.RemoveAt(j);
                    return;
                }
            }
        }
    }

    public abstract partial class View : IRenderable
    {
        private VertexData vertexData;
        private static WindowOptions defaultOption = new()
            {
                Size = new(800, 600),
                Title = "DefaultTitle",
            };

        protected Stopwatch tickWatch = new();

        private bool isRunning = true;
        private Stopwatch lastTickWatch = new();

        public GL OpenGL { get; set; }
        public double DeltaTime { get; private set; }
        public long TickDifference { get; protected set; } = 0;

        public List<InicializationAction<GL>> InicializationActions { get; set; } =
            new();

        public View(WindowOptions? options) 
        {
            var currentOptions = options ?? defaultOption;
            Options = currentOptions;

            InicializationActions.Add(new InicializationAction<GL>((GL currentOpenGL) =>
            {
                currentOpenGL.Enable(GLEnum.DepthTest);
            }));
            ViewWindow = Window.Create(Options);

            ViewWindow.Load += async() => { await StartViewAsync(); };
            ViewWindow.Render += async(double doubleHolder) => await RenderViewAsync(doubleHolder);
            ViewWindow.Update += async(double doubleHolder) => { await UpdateViewAsync(); };

            ViewWindow.Run();
        }

        public void Inicializate() 
        {
            OpenGL.PolygonMode(GLEnum.FrontAndBack, GLEnum.Fill);
            OpenGL.PatchParameter(GLEnum.PatchVertices, 3);

            for (int i = 0; i < InicializationActions.Count; i++)
            {
                InicializationActions[i].InicializateAction.Invoke(OpenGL);
            }
        }

        public async Task RenderViewAsync(double renderTime) 
        {
            DeltaTime = renderTime; 
            OpenGL.Clear((uint)ClearBufferMask.ColorBufferBit);

            await RunComponentsRenderAction(async (IComponent currentComponent) =>
            {
                await currentComponent.RenderAsync(OpenGL);
                OpenGL.BindVertexArray(vertexData.VertexBufferPointer);
                vertexData = currentComponent.Data;
                tickWatch.Start();

                unsafe 
                {
                    OpenGL.DrawElements(PrimitiveType.Triangles, 256, DrawElementsType.UnsignedInt, null);
                }
            });
        }

        public virtual async Task StartViewAsync()
        {
            OpenGL = GL.GetApi(ViewWindow); 
            await RunComponentsRenderAction(async (IComponent currentComponent) =>
            {
                await currentComponent.StartAsync(OpenGL, ViewWindow);
                vertexData = currentComponent.Data;
                tickWatch.Start();
                OpenGL.Flush();
            });
        }

        protected async Task UpdateViewAsync() 
        {
            ViewWindow.Title = $"{TickDifference}";
            await RunComponentsRenderAction(async (IComponent currentComponent) 
               => await currentComponent.UpdateAsync(OpenGL));

            TickDifference = CalculateTickDifference();
            lastTickWatch = tickWatch;
            tickWatch.Restart();
        }

        private long CalculateTickDifference() 
        {
            long currentSmooth = tickWatch.ElapsedTicks - (tickWatch.ElapsedTicks / 100);
            return lastTickWatch.ElapsedTicks - currentSmooth;
        }

        private async Task RunComponentsRenderAction(Func<IComponent, Task> renderFunction)
        {
            for (int i = 0; i < Components.Count; i++)
            {
                var currentInvoke = renderFunction.Invoke(Components[i]);
                await currentInvoke;
            }
        }

        public async Task StopViewAsync() 
        {
            var objectHolder = new object();
            lock (objectHolder) 
            {
                isRunning = false;
            }
        }

    }
}