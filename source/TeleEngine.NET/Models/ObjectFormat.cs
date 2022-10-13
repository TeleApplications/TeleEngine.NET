using TeleEngine.NET.Models.Interfaces;

namespace TeleEngine.NET.Models
{
    public abstract class ObjectFormat : IFormat
    {
        public abstract string Name { get; }
        public ReadOnlyMemory<string> ObjectData { get; set; }

        public ObjectFormat(string path) 
        {
            Task.Run(async() => ObjectData = await File.ReadAllLinesAsync(path));
        }

        public abstract Task<ModelData> CreateModelAsync();

        public virtual bool ValideteFormat() => ObjectData.Length > 0;
    }
}
