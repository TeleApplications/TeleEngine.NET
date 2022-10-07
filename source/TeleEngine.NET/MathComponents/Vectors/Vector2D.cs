using TeleEngine.NET.MathComponents.Interfaces;

namespace TeleEngine.NET.MathComponents.Vectors
{
    public sealed class Vector2D : IVector
    {
        public float X { get; set; }
        public float Y { get; set; }

        public Vector2D(float x, float y) 
        {
            X = x;
            Y = y;
        }

        public static Vector2D operator +(Vector2D firstVector, IVector secondVector) =>
            new(firstVector.X + secondVector.X, firstVector.Y + secondVector.Y);
        public static Vector2D operator -(Vector2D firstVector, IVector secondVector) =>
            new(firstVector.X - secondVector.X, firstVector.Y - secondVector.Y);

        public static implicit operator System.Numerics.Vector2(Vector2D vector) =>
            new(vector.X, vector.Y);
        public static explicit operator Vector2D(System.Numerics.Vector2 vector) =>
            new(vector.X, vector.Y);
    }
}
