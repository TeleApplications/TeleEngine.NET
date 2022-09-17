using Silk.NET.Windowing;

namespace TeleEngine.NET.Intefaces
{
    public interface IView
    {
        public IWindow ViewWindow { get; set; }
        public WindowOptions Options { get; set; }
    }
}
