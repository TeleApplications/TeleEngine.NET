
namespace TeleEngine.NET.Models.Interfaces
{
    public interface IFormat
    {
        public abstract string Name { get; } 
        public ReadOnlyMemory<string> ObjectData { get; set; }

        public Task<ModelData> CreateModelAsync() { return default!; }

        public bool ValideteFormat();
    }
}
