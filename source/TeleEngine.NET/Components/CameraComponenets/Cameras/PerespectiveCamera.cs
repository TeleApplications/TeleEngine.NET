using System.Collections.Immutable;
using System.Numerics;
using TeleEngine.NET.Components.CameraComponenets.Interfaces;
using TeleEngine.NET.Shaders;

namespace TeleEngine.NET.Components.CameraComponenets.Cameras
{
    public class PerespectiveCamera : ICamera
    {
        public ImmutableArray<ShaderResult<ICamera, Matrix4x4>> ShaderResults =>
            ImmutableArray.Create
            (
                new ShaderResult<ICamera, Matrix4x4>("uView",
                    (ICamera camera) => MatrixHelper.CalculateViewMatrix(camera.Transform)),
                new ShaderResult<ICamera, Matrix4x4>("uProjection",
                    (ICamera camera) => MatrixHelper.CalculateProjectionMatrix(camera.FieldOfView, camera.AspectRatio))
            );

        public Transform Transform { get; set; } = new()
        {
            Position = Vector3.Zero,
            Rotation = Quaternion.Identity,
            Scale = 1f
        };

        public int FieldOfView { get; set; } = 90;
        public float AspectRatio { get; set; } = 1.25f;
    }
}
