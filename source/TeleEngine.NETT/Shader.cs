using Silk.NET.OpenGL;
using TeleEngine.NET.Intefaces;

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
