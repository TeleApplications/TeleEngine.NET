using System.Numerics;

namespace TeleEngine.NET.Components
{
    public struct Transform
    {
        public Vector3 Position { get; set; } = Vector3.Zero;
        public Quaternion Rotation { get; set; } = Quaternion.Identity;

        public float Scale { get; set; } = 1.25f;
        public Matrix4x4 MatrixTransform => Matrix4x4.Identity * Matrix4x4.CreateTranslation(Position) * Matrix4x4.CreateFromQuaternion(Rotation) * Matrix4x4.CreateScale(Scale);

        public Transform() { }
    }
}
