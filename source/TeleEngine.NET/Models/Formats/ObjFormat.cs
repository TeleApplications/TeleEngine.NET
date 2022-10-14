using System.Collections.Immutable;
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
                    var data = format.SplitData(line, (char)32);
                    float x = (float)double.Parse(data.Span[0]);
                    float y = (float)double.Parse(data.Span[1]);
                    float z = (float)double.Parse(data.Span[2]);

                    return new float[] { x, y, z };
                })
            );

        public override string Name => nameof(ObjFormat);

        public ObjFormat(string path) : base(path) { }

        public override async Task<VertexModel> CreateModelAsync()
        {
            int dataLength = ObjectData.Length;
            Memory<float> vertices = new float[dataLength];

            for (int i = 0; i < dataLength; i++)
            {
                ReadOnlyMemory<char> lineCharacters = ObjectData.Span[i].ToCharArray();
                string symbol = lineCharacters[0..1].ToString();
                var currentFunction = FindFunction(symbol);

                if (currentFunction.Function is not null) 
                {
                    var verticesData = currentFunction.Function.Invoke(lineCharacters[2..], this);
                    verticesData.CopyTo(vertices);
                }
            }

            return new VertexModel(vertices, new float[] { 0 });
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

        private ReadOnlyMemory<string> SplitData(ReadOnlyMemory<char> line, char separator) 
        {
            var indexes = new List<string>();

            int lastIndex = -1;
            for (int i = 0; i < line.Length; i++)
            {
                if ((byte)line.Span[i] == (byte)separator) 
                {
                    var currentValue = line[(lastIndex + 1)..i].ToString();
                    indexes.Add(currentValue);
                    lastIndex = i;
                }
            }
            indexes.Add(line[lastIndex..].ToString());
            return indexes.ToArray();
        }
    }
}
