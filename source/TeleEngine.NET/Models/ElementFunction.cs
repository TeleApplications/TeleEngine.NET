
namespace TeleEngine.NET.Models
{
    public readonly struct ElementFunction<TSource, T>
    {
        public string ParameterName { get; }
        public Func<ReadOnlyMemory<char>, TSource, T> Function { get; }

        public ElementFunction(string name, Func<ReadOnlyMemory<char>, TSource, T> function) 
        {
            ParameterName = name;
            Function = function;
        }
    }

}
