using SharpGL;
using TeleEngine.NET.Intefaces;

namespace TeleEngine.NET.Views.CustomViews.Scene
{
    public sealed class SceneView : View
    {
        protected override OpenGL _openGL { get; set; } =
            new OpenGL()
            {
            };

        protected override IList<IComponent> Components => new List<IComponent>
        {
        };

        public SceneView(OpenGL openGL, int width, int height, int bitDepth) : base(openGL, width, height, bitDepth) 
        {
        }
    }
}
