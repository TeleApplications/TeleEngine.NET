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
            int rotationValue = 0;
            if (keyboardState.GetCurrentKeyState(Silk.NET.GLFW.Keys.Q)) 
                rotationValue = 1;
            if (keyboardState.GetCurrentKeyState(Silk.NET.GLFW.Keys.E))
                rotationValue = -1;

            if (keyboardState.GetCurrentKeyState(Silk.NET.GLFW.Keys.W))
                camera.Transform.Position += camera.VectorData.Front;
            if (keyboardState.GetCurrentKeyState(Silk.NET.GLFW.Keys.S))
                camera.Transform.Position -= camera.VectorData.Front;

            if (keyboardState.GetCurrentKeyState(Silk.NET.GLFW.Keys.A))
                camera.Transform.Position += (Vector3D)Vector3.Normalize(Vector3.Cross(camera.VectorData.Front, camera.VectorData.Up));
            if (keyboardState.GetCurrentKeyState(Silk.NET.GLFW.Keys.D))
                camera.Transform.Position -= (Vector3D)Vector3.Normalize(Vector3.Cross(camera.VectorData.Front, camera.VectorData.Up));

            camera.Yaw += rotationValue;
            camera.Pitch -= rotationValue;
            return base.UpdateAsync(openGL, camera);
        }
    }
}
