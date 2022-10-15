using System.Collections.Immutable;
using TeleEngine.NET.Components.Vertices;
using TeleEngine.NET.Models.Interfaces;

namespace TeleEngine.NET.Models
{
    public abstract class ObjectFormat<T> : IFormat where T : ObjectFormat<T>, new()
    {
        private ReadOnlyMemory<string> objectData;
        private T? currentFormat;

        public abstract ImmutableArray<ElementFunction<T, ReadOnlyMemory<float>>> ElementFunctions { get; }
        public abstract string Name { get; }

        public ObjectFormat() { }
        public ObjectFormat(string path) 
        {
            objectData = File.ReadAllLines(path);
            currentFormat = new T();
        }

        public VertexModel CreateModel() 
        {
            int dataLength = objectData.Length;
            Memory<float> vertices = new float[dataLength * 5];

            int verticesCount = 0;
            for (int i = 0; i < dataLength; i++)
            {
                ReadOnlyMemory<char> lineCharacters = objectData.Span[i].ToCharArray();
                int charactersCount = CalculateFirstSeparateLength(lineCharacters, (char)32);

                int index = lineCharacters.Length - (Math.Abs((lineCharacters.Length / 1) - 1)); 
                int currentLength = (index + Math.Abs(index)) / 2;

                var symbols = lineCharacters[0..currentLength];
                var currentFunction = symbols.Length > 0 && (byte)symbols.Span[0] != 32 ? FindFunction(symbols.Span.ToString()) : default;

                if (currentFunction.Function is not null)
                {
                    var verticesData = currentFunction.Function.Invoke(lineCharacters[2..], currentFormat!);
                    verticesData.CopyTo(vertices[verticesCount..]);
                    verticesCount += verticesData.Length;
                }
            }
            return new VertexModel(vertices[0..verticesCount], new float[] { 0 }); 
        }

        protected virtual ElementFunction<T, ReadOnlyMemory<float>> FindFunction(string symbol) 
        {
            for (int i = 0; i < ElementFunctions.Length; i++)
            {
                var currentFunction = ElementFunctions[i];
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

        public virtual bool ValideteFormat() => objectData.Length > 0;
    }
}
