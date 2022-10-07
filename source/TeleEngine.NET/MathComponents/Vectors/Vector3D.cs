using TeleEngine.NET.MathComponents.Interfaces;

namespace TeleEngine.NET.MathComponents.Vectors
{
    public sealed class Vector3D : IVector
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public Vector3D(float x, float y, float z) 
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static Vector3D operator +(Vector3D firstVector, Vector3D secondVector) =>
            new(firstVector.X + secondVector.X, firstVector.Y + secondVector.Y, firstVector.Z + secondVector.Z);
        public static Vector3D operator -(Vector3D firstVector, Vector3D secondVector) =>
            new(firstVector.X - secondVector.X, firstVector.Y - secondVector.Y, firstVector.Z - secondVector.Z);

        public static implicit operator System.Numerics.Vector3(Vector3D vector) =>
            new(vector.X, vector.Y, vector.Z);
        public static explicit operator Vector3D(System.Numerics.Vector3 vector) =>
            new(vector.X, vector.Y, vector.Z);
    } 
}
