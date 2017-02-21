using Microsoft.Xna.Framework;

namespace P3D.Legacy.Core.Server
{
    public abstract class BaseOnlinePlayer
    {
        public const int PLAYERDATAITEMSCOUNT = 15;

        public bool Moving { get; set; } = false;
        public string LevelFile { get; set; } = "";
        public int BusyType { get; set; } = 0;
        public int ServersID { get; set; } = 0;
        public string Name { get; set; } = "";
        public string GameJoltId { get; set; } = "";
        public bool Initialized { get; set; } = false;
        public Vector3 Position { get; set; } = new Vector3(0);
        public string Skin { get; set; } = "";
        public int Facing { get; set; } = 0;
        public Vector3 PokemonPosition { get; set; } = new Vector3(0);
        public int PokemonFacing { get; set; } = 0;
        public string PokemonSkin { get; set; } = "";
        public bool PokemonVisible { get; set; } = false;


        public abstract void ApplyNewData(IPackage package);
    }
}
