using Microsoft.Xna.Framework;

namespace P3D.Legacy.Core.Extensions
{
    public static class BoundingFrustumExtension
    {
        public static bool FastIntersect(this BoundingFrustum frustum, BoundingBox box)
        {
            Vector3 normal;
            Vector3 vector2;

            normal = frustum.Left.Normal;
            vector2.X = normal.X >= 0f ? box.Min.X : box.Max.X;
            vector2.Y = normal.Y >= 0f ? box.Min.Y : box.Max.Y;
            vector2.Z = normal.Z >= 0f ? box.Min.Z : box.Max.Z;
            if (frustum.Left.D + normal.X * vector2.X + normal.Y * vector2.Y + normal.Z * vector2.Z > 0f)
                return false;

            normal = frustum.Right.Normal;
            vector2.X = (normal.X >= 0f) ? box.Min.X : box.Max.X;
            vector2.Y = (normal.Y >= 0f) ? box.Min.Y : box.Max.Y;
            vector2.Z = (normal.Z >= 0f) ? box.Min.Z : box.Max.Z;
            if (frustum.Right.D + normal.X * vector2.X + normal.Y * vector2.Y + normal.Z * vector2.Z > 0f)
                return false;

            normal = frustum.Bottom.Normal;
            vector2.X = normal.X >= 0f ? box.Min.X : box.Max.X;
            vector2.Y = normal.Y >= 0f ? box.Min.Y : box.Max.Y;
            vector2.Z = normal.Z >= 0f ? box.Min.Z : box.Max.Z;
            if (frustum.Bottom.D + normal.X * vector2.X + normal.Y * vector2.Y + normal.Z * vector2.Z > 0f)
                return false;

            normal = frustum.Top.Normal;
            vector2.X = normal.X >= 0f ? box.Min.X : box.Max.X;
            vector2.Y = normal.Y >= 0f ? box.Min.Y : box.Max.Y;
            vector2.Z = normal.Z >= 0f ? box.Min.Z : box.Max.Z;
            if (frustum.Top.D + normal.X * vector2.X + normal.Y * vector2.Y + normal.Z * vector2.Z > 0f)
                return false;

            return true;
        }
    }
}
