using System.Collections.Immutable;
using System.Globalization;
using TeleEngine.NET.Components.Vertices;

namespace TeleEngine.NET.Models.Formats
{
    internal readonly struct ParameterFunction<TSource, T>
    {
        public string ParameterName { get; }
        public Func<ReadOnlyMemory<char>, TSource, T> Function { get; }

        public ParameterFunction(string name, Func<ReadOnlyMemory<char>, TSource, T> function) 
        {
            ParameterName = name;
            Function = function;
        }
    }

        //In the future it's going to have a proper implementation
        //of textures and overall more abstract pattern
    public sealed class ObjFormat : ObjectFormat
    {
        private ImmutableArray<ParameterFunction<ObjFormat, ReadOnlyMemory<float>>> verticesFuntions =
            ImmutableArray.Create
            (
                new ParameterFunction<ObjFormat, ReadOnlyMemory<float>>("v", (ReadOnlyMemory<char> line, ObjFormat format) => 
                {
                    return format.SeparateData<float>(line, (char)32);
                })
            );

        public override string Name => nameof(ObjFormat);

        public ObjFormat(string path) : base(path) { }

        public override async Task<VertexModel> CreateModelAsync()
        {
            int dataLength = ObjectData.Length;
            Memory<float> vertices = new float[dataLength * 5];

            int verticesCount = 0;
            for (int i = 0; i < dataLength; i++)
            {
                ReadOnlyMemory<char> lineCharacters = ObjectData.Span[i].ToCharArray();
                int charactersCount = CalculateFirstSeparateLength(lineCharacters, (char)32);

                int index = lineCharacters.Length - (Math.Abs((lineCharacters.Length / 1) - 1)); 
                int currentLength = (index + Math.Abs(index)) / 2;

                var symbols = lineCharacters[0..currentLength];
                var currentFunction = symbols.Length > 0 && (byte)symbols.Span[0] != 32 ? FindFunction(symbols.Span.ToString()) : default;

                if (currentFunction.Function is not null) 
                {
                    var verticesData = currentFunction.Function.Invoke(lineCharacters[2..], this);
                    verticesData.CopyTo(vertices[verticesCount..]);
                    verticesCount += verticesData.Length;
                }
            }

            return new VertexModel(vertices[0..verticesCount], new float[] { 0 }); 
        }

        private ParameterFunction<ObjFormat, ReadOnlyMemory<float>> FindFunction(string symbol) 
        {
            for (int i = 0; i < verticesFuntions.Length; i++)
            {
                var currentFunction = verticesFuntions[i];
                if (currentFunction.ParameterName == symbol)
                    return currentFunction;
            }
            return default;
        }

        private int CalculateFirstSeparateLength(ReadOnlyMemory<char> characters, char separator) 
        {
            for (int i = 0; i < characters.Length; i++)
            {
                var currentCharacter = characters.Span[i];
                if (currentCharacter == separator)
                    return i;
            }
            return 0;
        }

        private ReadOnlyMemory<T> SeparateData<T>(ReadOnlyMemory<char> line, char separator) where T : unmanaged
        {
            var indexes = new List<T>();

            int lastIndex = 0;
            for (int i = 0; i < line.Length; i++)
            {
                if ((byte)line.Span[i] == (byte)separator) 
                {
                    var currentValue = line[(lastIndex)..i].ToString();
                    indexes.Add(ParseValue<T>(currentValue));
                    lastIndex = i + 1;
                }
            }

            indexes.Add(ParseValue<T>(line[(lastIndex)..].ToString()));
            return indexes.ToArray();
        }

        //In the future update of .NET version, this will be totally redone by creating generic number

        private T ParseValue<T>(string value) where T : unmanaged 
        {
            return (T)(dynamic)float.Parse(value, CultureInfo.InvariantCulture); 
        }
    }
}
