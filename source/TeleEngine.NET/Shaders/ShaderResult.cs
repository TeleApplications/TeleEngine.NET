
namespace TeleEngine.NET.Shaders
{
    internal sealed class ShaderResult<T>
    {
        public string ShaderName { get; }
        public Func<T> Result { get; }

        public ShaderResult(string shaderName, Func<T> result) 
        {
            ShaderName = shaderName;
            Result = result;
        }
    }
}
