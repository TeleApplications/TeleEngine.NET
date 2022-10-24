using System.Collections.Immutable;
using System.Numerics;
using System.Reflection;
using TeleEngine.NET.SharedObjects.Attributes;

namespace TeleEngine.NET.SharedObjects
{

    public static class Shared
    {
        public static ImmutableArray<SharedAttribute> SharedAttributes =>
            ImmutableArray.Create
            (
                AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(n => n.GetTypes()
                .Where(t => t.GetCustomAttributes<SharedAttribute>(true).Any()))
                .Select(n => n.GetCustomAttribute<SharedAttribute>()).ToArray()
            )!;

        private static ReadOnlyMemory<SharedInstanceHolder> sharedObjects;
        private static ReadOnlyMemory<int> objectsTypes =>
            SharedAttributes.Select(n => n.SharedId).ToArray();

        public static T? GetInstance<T>() 
        {
            var sharedAttribute = typeof(T).GetCustomAttribute<SharedAttribute>();
            if (sharedAttribute is null)
                throw new Exception($"Class {nameof(T)} didn't have any {nameof(SharedAttribute)}");

            if (sharedObjects.IsEmpty)
                sharedObjects = CreateSharedInstances();

            int index = FindInstanceIndex(sharedAttribute.SharedId);
            return index != objectsTypes.Length ? (T)sharedObjects.Span[index].Instance : default;
        }

        private static int FindInstanceIndex(int typeId) 
        {
            var typeVector = new Vector<int>(typeId);

            int vectorSize = Vector<int>.Count;
            int difference = objectsTypes.Length - vectorSize;

            var objectsArray = objectsTypes.Span.ToArray();
            for (int i = 0; i < difference; i+=vectorSize)
            {
                var currentVector = new Vector<int>(objectsArray, i);
                if (Vector.EqualsAll(typeVector, currentVector))
                    return i;
            }

            int defaultDifference = (difference + Math.Abs(difference)) / 2;
            for (int i = defaultDifference; i < objectsArray.Length; i++)
            {
                if (typeId == objectsArray[i])
                    return i;
            }
            return objectsTypes.Length;
        }

        private static ReadOnlyMemory<SharedInstanceHolder> CreateSharedInstances() 
        {
            SharedInstanceHolder[] instances = new SharedInstanceHolder[SharedAttributes.Length];
            for (int i = 0; i < SharedAttributes.Length; i++)
            {
                Type currentType = SharedAttributes[i].SharedType;
                instances[i] = new SharedInstanceHolder(currentType);
            }

            return instances;
        }
    }
}
