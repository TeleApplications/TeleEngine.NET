
namespace TeleEngine.NET.Models
{
    public sealed class DataSet<T> 
    {
        public Memory<T> Data { get; set; }
        public int Count { get; set; }

        public DataSet(Memory<T> data, int count = 0) 
        {
            Data = data;
            Count = count;
        }
    }

    public sealed class ObjectData<T> where T : unmanaged
    {
        public DataSet<T> Vertices { get; set; }
        public DataSet<T> Textures { get; set; }
        public DataSet<T> Normals { get; set; }
        public DataSet<T> Faces { get; set; }

        public ObjectData() { }

        public static ObjectData<T> Create(int length) =>
            new ObjectData<T>()
            {
                Vertices = new (new T[length]),
                Textures = new (new T[length]),
                Normals = new (new T[length]),
                Faces = new (new T[length]),
            };
    }
}
