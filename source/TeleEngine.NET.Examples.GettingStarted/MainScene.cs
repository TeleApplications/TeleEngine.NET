using Silk.NET.Windowing;
using System.Numerics;
using TeleEngine.NET.Components;
using TeleEngine.NET.Components.CameraComponenets.Cameras;
using TeleEngine.NET.Components.CameraComponenets.Interfaces;
using TeleEngine.NET.Components.Vertices.SimpleShapeVertices;
using TeleEngine.NET.Examples.GettingStarted.Components;
using TeleEngine.NET.MathComponents.Vectors;
using TeleEngine.NET.Views.CustomViews.Scene;

namespace TeleEngine.NET.Examples.GettingStarted
{
    internal sealed class MainScene : SceneView
    {
        public override ICamera Camera { get; set; } =
            new PerespectiveCamera();

        public MainScene(WindowOptions options) : base(options)
        {
            AddComponent(new CameraComponent());

            float aspectRatio = options.Size.X / options.Size.Y;
            Task.Run(async () => await SpawnObjects<TestComponent>(1, (int index) =>
            new Transform()
            {
                Position = new Vector3D(-aspectRatio + ((int)(index / (MathF.Round(index / 7) + 1))), ((int)(index / (MathF.Round(index / 7) + 1))), 10),
                Rotation = Quaternion.Identity,
                Scale = 1f
            }, 10));
        }

        private async Task SpawnObjects<T>(int count, Func<int, Transform> transformFunction, int delay = 0) where T : Intefaces.IComponent, new()
        {
            for (int i = 0; i < count; i++)
            {
                await Task.Delay(delay);

                Transform currentTransform = transformFunction.Invoke(i);
                var component = new T() { Transform = currentTransform };
                await AddComponent(component);
            }
        }
    }
}
