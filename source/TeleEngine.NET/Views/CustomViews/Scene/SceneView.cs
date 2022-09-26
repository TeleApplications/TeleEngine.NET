using Silk.NET.Windowing;

namespace TeleEngine.NET.Views.CustomViews.Scene
{
    public class SceneView : View
    {
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
