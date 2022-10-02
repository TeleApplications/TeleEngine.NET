
namespace TeleEngine.NET.SharedObjects.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public sealed class SharedAttribute : Attribute
    {
        private static int sharedValues = 0;
        private static List<SharedAttribute> sharedTypes = new();

        public Type SharedType { get; }
        public int SharedId  { get; }

        public SharedAttribute(Type sharedType) 
        {
            SharedType = sharedType;

            int index = sharedTypes.FindIndex(n => n.SharedType == sharedType);
            if (index == -1)
            {
                SharedId = sharedValues;
                Interlocked.Increment(ref sharedValues);
                sharedTypes.Add(this);
            }
            else
                SharedId = sharedTypes[index].SharedId;
        }
    }
}
