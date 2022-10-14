using TeleEngine.NET.Components.Vertices;
using TeleEngine.NET.Models.Interfaces;

namespace TeleEngine.NET.Models
{
    public abstract class ObjectFormat : IFormat
    {
        public abstract string Name { get; }
        public ReadOnlyMemory<string> ObjectData { get; set; }

        public ObjectFormat(string path) 
        {
            ObjectData = File.ReadAllLines(path);
        }

        public abstract Task<VertexModel> CreateModelAsync();

        public virtual bool ValideteFormat() => ObjectData.Length > 0;
    }
}
