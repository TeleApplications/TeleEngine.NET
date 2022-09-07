using SharpGL;
using TeleEngine.NET.Components.Vertices.SimpleShapeVertices;
using TeleEngine.NET.Intefaces;

namespace TeleEngine.NET.Views.CustomViews.Scene
{
    public sealed class SceneView : View
    {
        protected override OpenGL _openGL { get; set; } = new();
        protected override IList<IComponent> Components => new List<IComponent>
        {
            new TriangleComponent(Colors.White)
        };

        public SceneView(int width, int height, int bitDepth = 1) : base(width, height, bitDepth) 
        {
            _openGL.Create(SharpGL.Version.OpenGLVersion.OpenGL4_4, RenderContextType.NativeWindow, width, height, bitDepth, null);
            _openGL.DrawText(0, 0, 255, 255, 255, "front", 16f, TickDifference.ToString());
        }

        public async Task StartSceneViewAsync() 
        {
            Task.WaitAny(StartViewAsync());
            await StartRenderViewAsync();
        }
    }
}
