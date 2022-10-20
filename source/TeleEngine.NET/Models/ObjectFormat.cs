using System.Collections.Immutable;
using TeleEngine.NET.Components.Vertices;
using TeleEngine.NET.Models.Interfaces;

namespace TeleEngine.NET.Models
{
    public abstract class ObjectFormat<T> : IFormat where T : ObjectFormat<T>, new()
    {
        private ReadOnlyMemory<string> fileLines;
        private T? currentFormat;

        public ObjectData<float> FormatData { get; set; }
        public abstract ImmutableArray<ElementFunction<T, float>> ElementFunctions { get; }
        public abstract string Name { get; }

        public ObjectFormat() { }
        public ObjectFormat(string path) 
        {
            fileLines = File.ReadAllLines(path);
            currentFormat = new T();
        }

        public VertexModel CreateModel() 
        {
            var data = CreateObjectData();
            Memory<float> finalVertices = new float[data.Faces.Count];

            int verticesIndex = 0;
            for (int i = 0; i < data.Faces.Count; i+=3) 
            {
                int faceIndex = (int)((data.Faces.Data.Span[i]) - 1) * 3;

                for (int j = 0; j < 3; j++)
                {
                    int currentIndex = faceIndex + j;
                    var currentValue = data.Vertices.Data.Span[currentIndex];

                    int index = (verticesIndex * 3) + j;
                    finalVertices.Span[index] = currentValue;
                }


                verticesIndex++;
            }
            return new VertexModel(finalVertices, data.Faces.Data);
        }

        private ObjectData<float> CreateObjectData()
        {
            int linesCount = fileLines.Length;
            currentFormat!.FormatData = ObjectData<float>.Create(linesCount * 6);

            for (int i = 0; i < fileLines.Length; i++)
            {
                ReadOnlyMemory<char> lineCharacters = fileLines.Span[i].ToCharArray();
                int charactersCount = CalculateFirstSeparateLength(lineCharacters, (char)32);

                int index = lineCharacters.Length - (Math.Abs((lineCharacters.Length / 1) - 1)); 
                int currentLength = (index + Math.Abs(index)) / 2;

                var symbols = lineCharacters[0..currentLength];
                var currentFunction = symbols.Length > 0 && (byte)symbols.Span[0] != 32 ? FindFunction(symbols.Span.ToString()) : default;

                if (currentFunction.Function is not null) 
                {
                    currentFunction.Function.Invoke(lineCharacters[2..], currentFormat!);
                }
            }

            return currentFormat!.FormatData;
        }

        protected virtual ElementFunction<T, float> FindFunction(string symbol) 
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

        public virtual bool ValideteFormat() => fileLines.Length > 0;
    }
}
