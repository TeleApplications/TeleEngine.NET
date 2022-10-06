using Silk.NET.OpenGL;
using System.Numerics;
using TeleEngine.NET.Components;
using TeleEngine.NET.Components.CameraComponenets.Interfaces;
using TeleEngine.NET.Components.Vertices;
using TeleEngine.NET.Components.Vertices.DefaultModels.Models;
using TeleEngine.NET.InputManager.Inputs;
using TeleEngine.NET.SharedObjects;
using TeleEngine.NET.Views;

namespace TeleEngine.NET.Examples.GettingStarted.Components
{
    internal sealed class TestComponent : VertexComponent
    {
        private static KeyboardInput keyboardState = Shared.GetInstance<KeyboardInput>()!;
        private static MouseInput mouseState = Shared.GetInstance<MouseInput>()!;

        public override Transform Transform { get; set; } =
            new()
            {
                Position = new(0, 0, -2),
                Rotation = System.Numerics.Quaternion.Identity,
                Scale = 1.25f
            };
        public override VertexModel Model => TriangleModel.Shared.Model;

        public TestComponent() 
        {
        }

        public override async Task UpdateAsync(GL openGL, ICamera camera)
        {
            var mousePosition = mouseState.CalculateRelativeMousePosition();
            MainScene.CurrentViewWindow.Title = $"X: {mousePosition.X} Y: {mousePosition.Y}";
            //Transform = new() { Position = new Vector3(-mousePosition.X, mousePosition.Y, Transform.Position.Z), Rotation = Transform.Rotation, Scale = Transform.Scale };

            int rotationValue = 0;
            if (keyboardState.GetCurrentKeyState(Silk.NET.GLFW.Keys.Q)) 
                rotationValue = 1;
            if (keyboardState.GetCurrentKeyState(Silk.NET.GLFW.Keys.E))
                rotationValue = -1;

            if (keyboardState.GetCurrentKeyState(Silk.NET.GLFW.Keys.I))
                Transform = new() { Position = new(Transform.Position.X + 0.01f, Transform.Position.Y, Transform.Position.Z), Rotation = Transform.Rotation, Scale = Transform.Scale };
            if (keyboardState.GetCurrentKeyState(Silk.NET.GLFW.Keys.L))
                Transform = new() { Position = new(Transform.Position.X - 0.01f, Transform.Position.Y, Transform.Position.Z), Rotation = Transform.Rotation, Scale = Transform.Scale };
            if (keyboardState.GetCurrentKeyState(Silk.NET.GLFW.Keys.J))
                Transform = new() { Position = new(Transform.Position.X, Transform.Position.Y, Transform.Position.Z + 0.01f), Rotation = Transform.Rotation, Scale = Transform.Scale };
            if (keyboardState.GetCurrentKeyState(Silk.NET.GLFW.Keys.K))
                Transform = new() { Position = new(Transform.Position.X, Transform.Position.Y, Transform.Position.Z - 0.01f), Rotation = Transform.Rotation, Scale = Transform.Scale };

            var normalizePosition = Vector3.Normalize(Vector3.Cross(camera.VectorData.Front, camera.VectorData.Up)) * 0.1f;
            if (keyboardState.GetCurrentKeyState(Silk.NET.GLFW.Keys.D))
                camera.Transform = new() { Position = new(camera.Transform.Position.X + normalizePosition.X, camera.Transform.Position.Y + normalizePosition.Y, camera.Transform.Position.Z + normalizePosition.Z), Rotation = camera.Transform.Rotation, Scale = camera.Transform.Scale };
            if (keyboardState.GetCurrentKeyState(Silk.NET.GLFW.Keys.A))
                camera.Transform = new() { Position = new(camera.Transform.Position.X - normalizePosition.X, camera.Transform.Position.Y - normalizePosition.Y, camera.Transform.Position.Z - normalizePosition.Z), Rotation = camera.Transform.Rotation, Scale = camera.Transform.Scale };
            if (keyboardState.GetCurrentKeyState(Silk.NET.GLFW.Keys.W))
                camera.Transform = new() { Position = new(camera.Transform.Position.X, camera.Transform.Position.Y, camera.Transform.Position.Z + 0.01f), Rotation = camera.Transform.Rotation, Scale = camera.Transform.Scale };
            if (keyboardState.GetCurrentKeyState(Silk.NET.GLFW.Keys.S))
                camera.Transform = new() { Position = new(camera.Transform.Position.X, camera.Transform.Position.Y, camera.Transform.Position.Z - 0.01f), Rotation = camera.Transform.Rotation, Scale = camera.Transform.Scale };

            camera.Yaw += rotationValue;
            camera.Pitch -= rotationValue;
        }
    }
}
