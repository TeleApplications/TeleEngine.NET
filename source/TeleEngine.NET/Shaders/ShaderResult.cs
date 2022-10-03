
namespace TeleEngine.NET.Shaders
{
    public sealed class ShaderResult<T, TOutput>
    {
        public string ShaderName { get; }
        public Func<T, TOutput> Result { get; }

        public ShaderResult(string shaderName, Func<T, TOutput> result) 
        {
            ShaderName = shaderName;
            Result = result;
        }
    }
}
