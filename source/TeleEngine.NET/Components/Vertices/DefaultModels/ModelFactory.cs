namespace TeleEngine.NET.Components.Vertices.DefaultModels
{
    // In a .NET 7 this abstract class is going to contain a static abstract
    // property due to zero extra allocation for each VertexModel
    public abstract class ModelFactory<T> where T : ModelFactory<T>, new()
    {
        public static T Shared = new();
        public abstract VertexModel Model { get; }
    }
}
