using Silk.NET.Windowing;
using TeleEngine.NET.Components.CameraComponenets.Cameras;
using TeleEngine.NET.Components.CameraComponenets.Interfaces;

namespace TeleEngine.NET.Views.CustomViews.Scene
{
    public class SceneView : View
    {
        public override ICamera Camera { get; set; } =
            new PerespectiveCamera();
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
