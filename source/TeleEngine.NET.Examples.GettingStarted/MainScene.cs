using Silk.NET.Windowing;
using System.Numerics;
using TeleEngine.NET.Components;
using TeleEngine.NET.Components.Vertices.SimpleShapeVertices;
using TeleEngine.NET.Views.CustomViews.Scene;

namespace TeleEngine.NET.Examples.GettingStarted
{
    internal sealed class MainScene : SceneView
    {
        public MainScene(WindowOptions options) : base(options)
        {
            float aspectRatio = options.Size.X / options.Size.Y;
            Task.Run(async () => await SpawnObjects<TriangleComponent>(70, (int index) =>
            new Transform()
            {
                Position = new Vector3(-aspectRatio + ((int)(index / ((index / 5) + 1))), -3f + (MathF.Round(index / 5)), 10),
                Rotation = Quaternion.Identity,
                Scale = 1f
            }));
        }

        private async Task SpawnObjects<T>(int count, Func<int, Transform> transformFunction) where T : Intefaces.IComponent, new()
        {
            for (int i = 0; i < count; i++)
            {
                Transform currentTransform = transformFunction.Invoke(i);
                var component = new T() { Transform = currentTransform };

                await AddComponent(component);
            }
        }
    }
}
