using System.Collections.Immutable;
using System.Globalization;

namespace TeleEngine.NET.Models.Formats
{
    //In the future it's going to have a proper implementation
    //of textures and overall more abstract pattern
    public sealed class ObjFormat : ObjectFormat<ObjFormat>
    {
        public override string Name => nameof(ObjFormat);
        public override ImmutableArray<ElementFunction<ObjFormat, float>> ElementFunctions =>
            ImmutableArray.Create
            (
                new ElementFunction<ObjFormat, float>("v", (ReadOnlyMemory<char> line, ObjFormat format) => 
                {
                    int count = format.FormatData.Vertices.Count;
                    var data = format.SeparateData<float>(line, (char)32); 
                    data.CopyTo(format.FormatData.Vertices.Data[count..]);

                    format.FormatData.Vertices.Count += data.Length;
                }),
                new ElementFunction<ObjFormat, float>("f", (ReadOnlyMemory<char> line, ObjFormat format) => 
                {
                    var faceLines = format.SeparateData<string>(line, (char)32);

                    int lastIndex = 0;
                    for (int i = 0; i < faceLines.Length; i++)
                    {
                        var currentFaces = format.SeparateData<uint>(faceLines.Span[i].ToCharArray(), (char)47);
                        int currentIndex = format.FormatData.Faces.Count + lastIndex;

                        currentFaces.CopyTo(format.FormatData.Faces.Data[currentIndex..]);
                        lastIndex += currentFaces.Length;
                    }

                    format.FormatData.Faces.Count += lastIndex;
                })
            );

        public ObjFormat(string path) : base(path) { }
        public ObjFormat() { }

        private ReadOnlyMemory<T> SeparateData<T>(ReadOnlyMemory<char> line, char separator)
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
        private T ParseValue<T>(string value)
        {
            return (typeof(T) == typeof(string) ? (T)(dynamic)value : (T)(dynamic)float.Parse(value, CultureInfo.InvariantCulture));
        }
    }
}
