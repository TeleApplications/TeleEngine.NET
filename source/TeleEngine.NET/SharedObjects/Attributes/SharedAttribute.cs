
namespace TeleEngine.NET.SharedObjects.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public sealed class SharedAttribute : Attribute
    {
        public Type SharedType { get; }

        public SharedAttribute(Type sharedType) 
        {
            SharedType = sharedType;
        }
    }
}
