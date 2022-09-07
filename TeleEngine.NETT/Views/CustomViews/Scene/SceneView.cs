using SharpGL;
using System.Drawing;
using TeleEngine.NET.Components.Vertices.SimpleShapeVertices;
using TeleEngine.NET.Intefaces;

namespace TeleEngine.NET.Views.CustomViews.Scene
{
    public sealed class SceneView : View
    {
        protected override OpenGL _openGL { get; set; } = new();
        protected override IList<IComponent> Components => new List<IComponent>
        {
            new TriangleComponent(Color.Black)
        };

        public SceneView(IntPtr handle, int width, int height, int bitDepth = 1) : base(width, height, bitDepth) 
        {
            //_openGL.CreateFromExternalContext(SharpGL.Version.OpenGLVersion.OpenGL4_4, width, height, bitDepth, handle, IntPtr.Zero, IntPtr.Zero);
            _openGL.Create(SharpGL.Version.OpenGLVersion.OpenGL4_4, RenderContextType.FBO, width, height, bitDepth, null);
            _openGL.MakeCurrent();
            _openGL.ClearColor(1, 1, 1, 1);
            _openGL.DrawText(0, 0, 0, 0, 0, "front", 16f, TickDifference.ToString());
            _openGL.Flush();
        }

        public async Task StartSceneViewAsync() 
        {
            Task.WaitAny(StartViewAsync());
            await StartRenderViewAsync();
        }
    }
}
