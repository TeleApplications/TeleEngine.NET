using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using System.Diagnostics;
using System.Numerics;
using TeleEngine.NET.Components.CameraComponenets.Interfaces;
using TeleEngine.NET.Components.Vertices;
using TeleEngine.NET.Intefaces;

namespace TeleEngine.NET.Views
{
    public abstract partial class View : Intefaces.IView
    {
        public static IWindow CurrentViewWindow  { get; private set; }
        public static float DeltaTime { get; private set; }

        private static float lastDeltaTime;

        public IWindow ViewWindow { get; set; }
        public WindowOptions Options { get; set; }

        protected virtual IList<IComponent> Components { get; set; } = new List<IComponent>();

        public async Task AddComponent<T>(T component) where T : IComponent
        {
            component.ComponenetId = Components.Count;
            Components.Add(component);
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

    public abstract partial class View : IRenderable, IDisposable
    {
        private VertexData vertexData;
        private Stopwatch lastTickWatch = new();

        protected Stopwatch tickWatch = new();

        private static WindowOptions defaultOption = new()
            {
                Size = new(800, 600),
                Title = "DefaultTitle",
            };

        public GL OpenGL { get; set; }
        public abstract ICamera Camera { get; set; }

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
            CurrentViewWindow = ViewWindow;

            ViewWindow.Load += async() 
                => await StartViewAsync();
            ViewWindow.Render += async(double doubleHolder) 
                => await RenderViewAsync();
            ViewWindow.Update += async(double doubleHolder) 
                => await UpdateViewAsync(doubleHolder);

            Task.Run(() => ViewWindow.Run());
        }

        public void Inicializate() 
        {
            OpenGL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
            OpenGL.PatchParameter(GLEnum.PatchVertices, 3);
            OpenGL.Enable(EnableCap.DepthTest);

            for (int i = 0; i < InicializationActions.Count; i++)
            {
                InicializationActions[i].InicializateAction.Invoke(OpenGL);
            }
        }

        public async Task RenderViewAsync() 
        {
            OpenGL.Clear((uint)ClearBufferMask.ColorBufferBit | (uint)ClearBufferMask.DepthBufferBit);

            await RunComponentsRenderAction(async (IComponent currentComponent) =>
            {
                await currentComponent.RenderAsync(OpenGL, Camera);
                OpenGL.BindVertexArray(vertexData.VertexBufferPointer);
                vertexData = currentComponent.Data;
                tickWatch.Start();

                unsafe { OpenGL.DrawArrays(GLEnum.Triangles, 0, 128); }
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
            });
        }

        protected async Task UpdateViewAsync(double renderTime) 
        {

            ViewWindow.Title = $"{TickDifference}";
            await RunComponentsRenderAction(async (IComponent currentComponent) 
               => await currentComponent.UpdateAsync(OpenGL, Camera));

            TickDifference = CalculateTickDifference();
            lastTickWatch = tickWatch;
            tickWatch.Restart();

            var lastDelta = DeltaTime;
            DeltaTime = MathF.Abs((float)(renderTime - lastDeltaTime));
            lastDeltaTime = lastDelta;
        }

        private long CalculateTickDifference() 
        {
            long currentSmooth = tickWatch.ElapsedTicks - (tickWatch.ElapsedTicks / 100);
            return lastTickWatch.ElapsedTicks - currentSmooth;
        }

        private async Task RunComponentsRenderAction(Func<IComponent, Task> renderFunction)
        {
            if (Components.Count <= 0)
                await Task.CompletedTask;

            for (int i = 0; i < Components.Count; i++)
            {
                var currentInvoke = renderFunction.Invoke(Components[i]);
                await currentInvoke;
            }
        }

        public void Dispose() 
        {
            OpenGL.Dispose();
        }
    }
}
