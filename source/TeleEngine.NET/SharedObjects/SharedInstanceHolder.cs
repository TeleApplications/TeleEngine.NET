
namespace TeleEngine.NET.SharedObjects
{
    internal readonly struct SharedInstanceHolder
    {
        public Type InstanceType { get; }
        public object Instance { get; }

        public SharedInstanceHolder(Type instanceType) 
        {
            InstanceType = instanceType;
            Instance = Activator.CreateInstance(instanceType)!;
        }
    }
}
