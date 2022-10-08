using System.Collections.Immutable;
using System.Numerics;
using TeleEngine.NET.Components.CameraComponenets.Interfaces;
using TeleEngine.NET.MathComponents.Vectors;
using TeleEngine.NET.Shaders;

namespace TeleEngine.NET.Components.CameraComponenets.Cameras
{
    public class PerespectiveCamera : ICamera
    {
        private static readonly Vector3D DefaultVector = new(0, 0, 0);

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
            Position = (Vector3D)Vector3.Zero,
            Rotation = Quaternion.Identity,
            Scale = 1f
        };

        public int FieldOfView { get; set; } = 60;
        public float AspectRatio { get; set; } = 1f;

        public int Pitch { get; set; } = 0;
        public int Yaw { get; set; } = -90;

        private CameraVectorData CalculateVectorData() 
        {
            float pitchRadians = MatrixHelper.CalculateDegreesToRadians(Pitch);
            float yawRadians = MatrixHelper.CalculateDegreesToRadians(Yaw);

            var frontVector = new Vector3()
            {
                X = (MathF.Cos(yawRadians) * MathF.Cos(pitchRadians)),
                Y = (MathF.Sin(pitchRadians)),
                Z = (MathF.Sin(yawRadians) * MathF.Cos(pitchRadians)),
            };
            var upVector = CalculateUpDirection();
            return new CameraVectorData((Vector3D)upVector, (Vector3D)frontVector);
        }

        private Vector3D CalculateUpDirection() 
        {
            var normalizedPosition = Vector3.Normalize(Vector3.Zero - Transform.Position);
            var upDirection = Vector3.UnitY;

            var vectorCross = Vector3.Cross(upDirection, normalizedPosition);
            Vector3 rightDirection = Vector3.Normalize(vectorCross);
            var result = Vector3.Cross(normalizedPosition, rightDirection);
            return (Vector3D)Vector3.Cross(normalizedPosition, rightDirection);
        }
    }
}
