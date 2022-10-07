using System.Numerics;
using TeleEngine.NET.MathComponents.Vectors;

namespace TeleEngine.NET.Components
{
    public sealed class Transform
    {
        public Vector3D Position { get; set; } = (Vector3D)Vector3.Zero;
        public Quaternion Rotation { get; set; } = Quaternion.Identity;
        public float Scale { get; set; } = 1.25f;

        public Matrix4x4 CalculateMatrixTransform() 
        {
            var rotationX = Matrix4x4.CreateRotationX(Rotation.X);
            var rotationY = Matrix4x4.CreateRotationX(Rotation.Y);
            var rotationZ = Matrix4x4.CreateRotationX(Rotation.Z);

            Matrix4x4 rotationValue = rotationX * rotationY * rotationZ;
            return (rotationValue) * Matrix4x4.CreateScale(Scale) * Matrix4x4.CreateTranslation(Position);
        }

        public Transform() { }
    }
}
