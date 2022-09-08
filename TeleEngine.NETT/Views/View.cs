using SharpGL;
using System.Diagnostics;
using System.Numerics;
using TeleEngine.NET.Intefaces;

namespace TeleEngine.NET.Views
{
    public abstract partial class View : Intefaces.IView
    {
        public int RenderWidth { get; set; }
        public int RenderHeight { get; set; }
        public int BitDepth { get; set; }
        protected virtual IList<IComponent> Components { get; private set; }

        public async Task AddComponent<T>(T component) where T : IComponent
        {
            component.ComponenetId = Components.Count;
            Components.Add(component);
            await component.StartAsync(_openGL);
            await component.UpdateAsync(_openGL);
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
        protected abstract OpenGL _openGL { get; set; }
        protected Stopwatch tickWatch = new();

        private bool isRunning = true;
        private Stopwatch lastTickWatch = new();

        public long TickDifference { get; protected set; } = 0;
        public List<InicializationAction<OpenGL>> InicializationActions { get; set; } =
            new();


        public View(nint handle, int width, int height, int bitDepth) 
        {
            InicializationActions.Add(new InicializationAction<OpenGL>((OpenGL currentOpenGL) =>
            {
                _openGL.CreateFromExternalContext(SharpGL.Version.OpenGLVersion.OpenGL4_4, width, height, bitDepth, handle, IntPtr.Zero, IntPtr.Zero);
                _openGL.Enable(OpenGL.GL_DEPTH_TEST);
                _openGL.Viewport(0, 0, width, height);
                _openGL.MakeCurrent();
            }));

            RenderWidth = width;
            RenderHeight = height;
            BitDepth = bitDepth;
        }

        public void Inicializate() 
        {
            for (int i = 0; i < InicializationActions.Count; i++)
            {
                InicializationActions[i].InicializateAction.Invoke(_openGL);
            }
        }

        public virtual async Task StartViewAsync() 
            => await RunComponentsRenderAction(async(IComponent currentComponent) 
                => {_openGL.MakeCurrent(); await currentComponent.StartAsync(_openGL); tickWatch.Start(); });

        protected async Task StartRenderViewAsync() 
        {
            while (TickDifference <= 0 && isRunning) 
            {
                await RunComponentsRenderAction(async (IComponent currentComponent) 
                    => await currentComponent.UpdateAsync(_openGL));

                TickDifference = CalculateTickDifference();
                lastTickWatch = tickWatch;
                tickWatch.Restart();
            }
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
