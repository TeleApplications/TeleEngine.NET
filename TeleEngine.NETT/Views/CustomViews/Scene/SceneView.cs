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

        public SceneView(IntPtr handle, int width, int height, int bitDepth = 1) : base(handle, width, height, bitDepth) 
        {
            Inicializate();
            _openGL.DrawText(0, 0, 0, 0, 0, "front", 16f, TickDifference.ToString());
            _openGL.Flush();
        }

        public async Task StartSceneViewAsync() 
        {
            //Task.WaitAny(StartViewAsync());
            await StartViewAsync();
            await StartRenderViewAsync();
        }
    }
}
