using TeleEngine.NET.Components.Vertices;

namespace TeleEngine.NET.Models.Interfaces
{
    public interface IFormat
    {
        public abstract string Name { get; }

        public VertexModel CreateModel();

        public bool ValideteFormat();
    }
}
