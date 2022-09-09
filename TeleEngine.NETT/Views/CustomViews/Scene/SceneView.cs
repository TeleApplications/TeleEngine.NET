using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using System.Drawing;
using TeleEngine.NET.Components.Vertices.SimpleShapeVertices;
using TeleEngine.NET.Intefaces;

namespace TeleEngine.NET.Views.CustomViews.Scene
{
    public sealed class SceneView : View
    {
        protected override IList<IComponent> Components => new List<IComponent>
        {
            new TriangleComponent(Color.Black)
        };

        public SceneView(WindowOptions windowOptions) : base(windowOptions) 
        {
            ViewWindow.Run();
            OpenGL = GL.GetApi(ViewWindow);

            Inicializate();
        }

        public async Task StartSceneViewAsync() 
        {
            await StartViewAsync();
            await StartRenderViewAsync();
        }
    }
}
