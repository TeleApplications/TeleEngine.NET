using TeleEngine.NET.Views.CustomViews.Scene;

namespace GettingStartedExample
{
    public partial class MainPage : ContentPage
    {
        private SceneView currentView;
        public MainPage()
        {
            InitializeComponent();
            currentView = new(500, 500, 1);
            Task.Run(async () => await currentView.StartSceneViewAsync());
        }
    }
}