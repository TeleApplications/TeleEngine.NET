namespace TeleEngine.NET.Models.Interfaces
{
    public interface IFormat
    {
        public string Name { get; }

        public ReadOnlyMemory<float> LoadData(string path);

        public bool ValideteFormat(string path);
    }
}
