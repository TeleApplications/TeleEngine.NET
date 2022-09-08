using SharpGL;
using System.Runtime.InteropServices;
using TeleEngine.NET.Views.CustomViews.Scene;

namespace TeleEngine.NET.Examples.GettingStarted
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hwnd, int nCmdShow);

        private SceneView currentScene;
        public Form1()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);

            int width = 500;
            int height = 500;

            nint windowHandle = Win32.CreateWindowEx(Win32.WindowStylesEx.WS_EX_LEFT, "TestWindow", "Window", Win32.WindowStyles.WS_VISIBLE, 0, 0, width, height, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
            //ShowWindow(windowHandle, 0);

            var formHandle = this.Handle;
            currentScene = new(Win32.GetDC(formHandle), 500, 500, 8);
        }
    }
}