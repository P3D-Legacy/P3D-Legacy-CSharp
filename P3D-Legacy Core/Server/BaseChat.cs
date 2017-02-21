using Microsoft.Xna.Framework;

namespace P3D.Legacy.Core.Server
{
    public abstract class BaseChat
    {
        public static Color OwnColor { get; set; } = new Color(39, 206, 249);
        public static Color FriendColor { get; set; } = Color.LightGreen;
        public static Color ServerColor { get; set; } = Color.Orange;
    }
}
