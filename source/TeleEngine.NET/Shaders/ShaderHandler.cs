namespace TeleEngine.NET.Shaders
{
    internal readonly struct ShaderHandler
    {
        public uint VertexHandle { get; }
        public uint FragmentHandle { get; }

        public ShaderHandler(uint vertex, uint fragment)
        {
            VertexHandle = vertex;
            FragmentHandle = fragment;
        }
    }
}
