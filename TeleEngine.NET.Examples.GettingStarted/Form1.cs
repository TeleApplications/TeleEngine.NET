using Silk.NET.Windowing;
using TeleEngine.NET.Views.CustomViews.Scene;

namespace TeleEngine.NET.Examples.GettingStarted
{
    public partial class Form1 : Form
    {
        private SceneView currentScene;
        public Form1()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);

            var options = WindowOptions.Default;
            options.Size = new(800, 600);
            options.Title = "DefaultWindow";
            options.Position = new(0, 0);

            currentScene = new(options);
        }
    }
}