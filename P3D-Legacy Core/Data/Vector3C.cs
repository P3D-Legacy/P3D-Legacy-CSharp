using Microsoft.Xna.Framework;

namespace P3D.Legacy.Core.Data
{
    /// <summary>
    /// A Vector3 class, because fuck properties
    /// </summary>
    public class Vector3C
    {
        public static implicit operator Vector3(Vector3C vector) => new Vector3(vector.X, vector.Y, vector.Z);
        public static implicit operator Vector3C(Vector3 vector) => new Vector3C(vector.X, vector.Y, vector.Z);

        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public Vector3C(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static bool operator ==(Vector3C vector1, Vector3C vector2) => vector1.X == vector2.X && vector1.Y == vector2.Y && vector1.Z == vector2.Z;
        public static bool operator !=(Vector3C vector1, Vector3C vector2) => !(vector1 == vector2);

        public static bool operator ==(Vector3C vector1, Vector3 vector2) => vector1.X == vector2.X && vector1.Y == vector2.Y && vector1.Z == vector2.Z;
        public static bool operator !=(Vector3C vector1, Vector3 vector2) => !(vector1 == vector2);

        public static bool operator ==(Vector3 vector1, Vector3C vector2) => vector1.X == vector2.X && vector1.Y == vector2.Y && vector1.Z == vector2.Z;
        public static bool operator !=(Vector3 vector1, Vector3C vector2) => !(vector1 == vector2);
    }
}