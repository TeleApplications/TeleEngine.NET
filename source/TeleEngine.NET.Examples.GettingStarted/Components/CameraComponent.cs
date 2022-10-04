using Silk.NET.OpenGL;
using System.Numerics;
using TeleEngine.NET.Components;
using TeleEngine.NET.Components.CameraComponenets.Interfaces;
using TeleEngine.NET.Components.Vertices;
using TeleEngine.NET.Components.Vertices.DefaultModels.Models;
using TeleEngine.NET.InputManager.Inputs;
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
                Position = new(0, 0, 2),
                Rotation = System.Numerics.Quaternion.Identity,
                Scale = 1.25f
            };

        public override Task UpdateAsync(GL openGL, ICamera camera)
        {

            if (keyboardState.GetCurrentKeyState(Silk.NET.GLFW.Keys.D))
                camera.Transform = new() { Position = new(camera.Transform.Position.X + 0.01f, camera.Transform.Position.Y, camera.Transform.Position.Z), Rotation = camera.Transform.Rotation, Scale = camera.Transform.Scale };
            if (keyboardState.GetCurrentKeyState(Silk.NET.GLFW.Keys.A))
                camera.Transform = new() { Position = new(camera.Transform.Position.X - 0.01f, camera.Transform.Position.Y, camera.Transform.Position.Z), Rotation = camera.Transform.Rotation, Scale = camera.Transform.Scale };
            if (keyboardState.GetCurrentKeyState(Silk.NET.GLFW.Keys.W))
                camera.Transform = new() { Position = new(camera.Transform.Position.X, camera.Transform.Position.Y, camera.Transform.Position.Z + 0.01f), Rotation = camera.Transform.Rotation, Scale = camera.Transform.Scale };
            if (keyboardState.GetCurrentKeyState(Silk.NET.GLFW.Keys.S))
                camera.Transform = new() { Position = new(camera.Transform.Position.X, camera.Transform.Position.Y, camera.Transform.Position.Z - 0.01f), Rotation = camera.Transform.Rotation, Scale = camera.Transform.Scale };

            return base.UpdateAsync(openGL, camera);
        }
    }
}
