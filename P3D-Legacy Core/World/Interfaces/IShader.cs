using P3D.Legacy.Core.Entities;

namespace P3D.Legacy.Core.World
{
    public interface IShader
    {
        void ApplyShader(Entity[] entities);
        bool HasBeenApplied { get; set; }
    }
}
