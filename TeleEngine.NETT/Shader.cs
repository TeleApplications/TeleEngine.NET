using Silk.NET.OpenGL;
using TeleEngine.NET.Interfaces;

namespace TeleEngine.NET
{
    internal sealed class Shader : IBindable
    {
        private GL openGL;
        public uint ShaderHande { get; }

        public async Task BindAsync() 
        {
        }
    }
}
