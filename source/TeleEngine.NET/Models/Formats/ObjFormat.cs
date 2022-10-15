using System.Collections.Immutable;
using System.Globalization;

namespace TeleEngine.NET.Models.Formats
{
    //In the future it's going to have a proper implementation
    //of textures and overall more abstract pattern
    public sealed class ObjFormat : ObjectFormat<ObjFormat>
    {
        public override string Name => nameof(ObjFormat);
        public override ImmutableArray<ElementFunction<ObjFormat, ReadOnlyMemory<float>>> ElementFunctions =>
            ImmutableArray.Create
            (
                new ElementFunction<ObjFormat, ReadOnlyMemory<float>>("v", (ReadOnlyMemory<char> line, ObjFormat format) => 
                {
                    return format.SeparateData<float>(line, (char)32);
                })
            );

        public ObjFormat(string path) : base(path) { }
        public ObjFormat() { }

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
