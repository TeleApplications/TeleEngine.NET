using TeleEngine.NET.Views.CustomViews.Scene;

namespace TeleEngine.NET.Examples.GettingStarted
{
    public partial class Form1 : Form
    {
        private SceneView currentScene;
        public Form1()
        {
            InitializeComponent();
            currentScene = new(this.Handle, 500, 500, 8);
            this.Load += (object sender, EventArgs e) => Task.Run(async() => await currentScene.StartSceneViewAsync());
        }
    }
}