using System.Collections.Immutable;
using System.Net.Http.Headers;
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
                    (ICamera camera) => MatrixHelper.CalculateViewMatrix(camera)),
                new ShaderResult<ICamera, Matrix4x4>("uProjection",
                    (ICamera camera) => MatrixHelper.CalculateProjectionMatrix(camera.FieldOfView, camera.AspectRatio))
            );

        public CameraVectorData VectorData =>
            CalculateVectorData();
        public Transform Transform { get; set; } = new()
        {
            Position = Vector3.Zero,
            Rotation = Quaternion.Identity,
            Scale = 1f
        };

        public int FieldOfView { get; set; } = 90;
        public float AspectRatio { get; set; } = 1.25f;

        public int Pitch { get; set; } = -45;
        public int Yaw { get; set; } = 45;

        private CameraVectorData CalculateVectorData() 
        {
            float pitchRadians = MatrixHelper.CalculateDegreesToRadians(Pitch);
            float yawRadians = MatrixHelper.CalculateDegreesToRadians(Yaw);

            Vector3 frontVector = new Vector3()
            {
                X = (MathF.Cos(pitchRadians) * MathF.Cos(yawRadians)),
                Y = (MathF.Sin(pitchRadians)),
                Z = (MathF.Cos(pitchRadians) * MathF.Sin(yawRadians)),
            };
            return new CameraVectorData(Vector3.UnitY, Vector3.Normalize(frontVector));
        }
    }
}
