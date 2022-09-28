using System.Collections.Immutable;
using System.Numerics;
using System.Reflection;
using TeleEngine.NET.SharedObjects.Attributes;

namespace TeleEngine.NET.SharedObjects
{

    public static class Shared
    {
        public static ImmutableArray<SharedAttribute> SharedAttributes =
            ImmutableArray.Create
            (
                Assembly.GetExecutingAssembly().GetCustomAttributes<SharedAttribute>().ToArray()
            );
        private static ReadOnlyMemory<SharedInstanceHolder> sharedObjects;
        private static ReadOnlyMemory<byte[]> objectsTypes =>
            SharedAttributes.Select(n => n.SharedType.GUID.ToByteArray()).ToArray();

        public static T? GetInstance<T>() 
        {
            if (sharedObjects.IsEmpty)
                sharedObjects = CreateSharedInstances();

            var typeId = typeof(T).GUID.ToByteArray().AsMemory();
            int index = FindInstanceIndex(typeId);
            return index != objectsTypes.Length ? (T)sharedObjects.Span[index].Instance : default;
        }

        private static int FindInstanceIndex(ReadOnlyMemory<byte> typeId) 
        {
            var typeVector = new Vector<byte>(typeId.Span);

            int vectorSize = Vector<byte>.Count;
            int difference = objectsTypes.Length - vectorSize;

            for (int i = 0; i < difference; i+=vectorSize)
            {
                var currentVector = new Vector<byte>(objectsTypes.Span[i]);
                if (Vector.EqualsAll(typeVector, currentVector))
                    return i;
            }

            for (int i = difference; i < vectorSize; i++)
            {
                if (Array.Equals(typeId, objectsTypes.Span[i]))
                    return i;
            }
            return objectsTypes.Length;
        }

        private static ReadOnlyMemory<SharedInstanceHolder> CreateSharedInstances() 
        {
            Memory<SharedInstanceHolder> instances = new SharedInstanceHolder[SharedAttributes.Length];
            for (int i = 0; i < SharedAttributes.Length; i++)
            {
                Type currentType = SharedAttributes[i].SharedType;
                instances.Span[i] = new SharedInstanceHolder(currentType);
            }

            return instances;
        }
    }
}
