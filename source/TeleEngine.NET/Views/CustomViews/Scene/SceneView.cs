using Silk.NET.Windowing;
using TeleEngine.NET.Components.Vertices.SimpleShapeVertices;
using TeleEngine.NET.Intefaces;

namespace TeleEngine.NET.Views.CustomViews.Scene
{
    public sealed class SceneView : View
    {
        protected override IList<IComponent> Components { get; set; } = new List<IComponent>
        {
            new TriangleComponent()
            {
                Transform = new()
                {
                    Position = new(0f, 0f, 2f),
                    Rotation = new(0f, 0f, 0f, 0f),
                    Scale = 1f
                }
            },
            //new Camera()
        };

        public SceneView(WindowOptions windowOptions) : base(windowOptions) 
        {
            var currentTask = Task.Run(() => 
            {
                ViewWindow.Load += () =>
                {
                    Inicializate();
                };
            });
        }
    }
}
