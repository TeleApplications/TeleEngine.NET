
namespace TeleEngine.NET.SharedObjects.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public sealed class SharedAttribute : Attribute
    {
        private int sharedValues = 0;

        public Type SharedType { get; }
        public int SharedId  { get; }

        public SharedAttribute(Type sharedType) 
        {
            SharedType = sharedType;
            SharedId = sharedValues;

            Interlocked.Increment(ref sharedValues);
        }
    }
}
