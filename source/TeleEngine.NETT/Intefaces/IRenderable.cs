
using Silk.NET.OpenGL;

namespace TeleEngine.NET.Intefaces
{
    public interface IRenderable
    {
        public GL OpenGL { get; set; }

        public virtual async Task StartViewAsync() { }
    }
}
