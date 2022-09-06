using SharpGL;
using System.Numerics;

namespace TeleEngine.NET.Intefaces
{
    public interface IComponent
    {
        public int ComponenetId { get; set; }

        public Vector3 Position { get; set; }
        public Vector3 Rotation { get; set; }

        public async Task StartAsync(OpenGL openGL) { }

        public async Task UpdateAsync(OpenGL openGL) { }
    }
}
