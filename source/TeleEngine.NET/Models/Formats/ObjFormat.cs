using System.Collections.Immutable;
using TeleEngine.NET.MathComponents.Vectors;

namespace TeleEngine.NET.Models.Formats
{
    internal readonly struct ParameterFunction<TSource, T>
    {
        public string ParameterName { get; }
        public Func<string, TSource, T> Function { get; }

        public ParameterFunction(string name, Func<string, TSource, T> function) 
        {
            ParameterName = name;
            Function = function;
        }
    }

    internal sealed class ObjFormat : ObjectFormat
    {
        private ImmutableArray<ParameterFunction<ObjFormat, Vector3D>> verticesFuntions =
            ImmutableArray.Create
            (
                new ParameterFunction<ObjFormat, Vector3D>("v", (string line, ObjFormat format) => 
                {
                    var data = format.SplitData(line, (char)0);
                    float x = float.Parse(data.Span[0]);
                    float y = float.Parse(data.Span[1]);
                    float z = float.Parse(data.Span[2]);

                    return new(x, y, z);
                })
            );

        public override string Name => nameof(ObjFormat);

        //In the future it's going to have a proper implementation
        //of textures
        public ObjFormat(string path) : base(path) { }

        public override async Task<ModelData> CreateModelAsync()
        {
            return default;
        }

        private ReadOnlyMemory<string> SplitData(string line, char separator) 
        {
            ReadOnlySpan<char> lineSpan = line.ToCharArray();
            var indexes = new List<string>();

            int lastIndex = 0;
            for (int i = 0; i < lineSpan.Length; i++)
            {
                if ((byte)lineSpan[i] == (byte)separator) 
                {
                    var currentValue = lineSpan[lastIndex..i].ToString();
                    indexes.Add(currentValue);
                    lastIndex = i;
                }
            }
            return indexes.ToArray();
        }
    }
}
