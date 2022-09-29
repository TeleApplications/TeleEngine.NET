using System.Runtime.InteropServices;

namespace TeleEngine.NET.InputManager
{
    internal static class InteropHelper
    {
        [DllImport("user32.dll")]
        public static extern int GetAsyncKeyState(int key);
    }
}
