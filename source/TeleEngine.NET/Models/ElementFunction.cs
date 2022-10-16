
namespace TeleEngine.NET.Models
{
    public readonly struct ElementFunction<TSource, T> where T : unmanaged
    {
        public string ParameterName { get; }

        public Action<ReadOnlyMemory<char>, TSource> Function { get; } 

        public ElementFunction(string name, Action<ReadOnlyMemory<char>, TSource> function)
        {
            ParameterName = name;
            Function = function;
        }
    }

}
