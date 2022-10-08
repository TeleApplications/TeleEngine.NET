using Silk.NET.OpenGL;
using System.Numerics;
using TeleEngine.NET.Components;
using TeleEngine.NET.Components.CameraComponenets.Cameras;
using TeleEngine.NET.Components.CameraComponenets.Interfaces;
using TeleEngine.NET.Components.Vertices;
using TeleEngine.NET.Components.Vertices.DefaultModels.Models;
using TeleEngine.NET.InputManager.Inputs;
using TeleEngine.NET.MathComponents.Vectors;
using TeleEngine.NET.SharedObjects;

namespace TeleEngine.NET.Examples.GettingStarted.Components
{
    internal sealed class CameraComponent : VertexComponent
    {
        private static KeyboardInput keyboardState = Shared.GetInstance<KeyboardInput>()!;

        public override VertexModel Model => TriangleModel.Shared.Model;
        public override Transform Transform { get; set; } =
            new()
            {
                Position = (Vector3D)Vector3.Zero,
                Rotation = System.Numerics.Quaternion.Identity,
                Scale = 1.25f
            };

        public override Task UpdateAsync(GL openGL, ICamera camera)
        {
            return base.UpdateAsync(openGL, camera);
        }
    }
}
