using System.IO;

namespace P3D.Legacy.Core.Pokemon
{
    /// <summary>
    /// The base class for all Mega Stone items.
    /// </summary>
    public abstract class MegaStone : Item
    {
        public int MegaPokemonNumber { get; }

        public override string Description { get; }
        public override bool CanBeTossed { get; }
        public override bool CanBeTraded { get; }
        public override bool CanBeUsed { get; }
        public override bool CanBeUsedInBattle { get; }

        public MegaStone(string megaPokemonName, int megaPokemonNumber)
        {
            Description = "One variety of the mysterious Mega Stones. Have " + megaPokemonName + " hold it, and this stone will enable it to Mega Evolve during battle.";
            TextureSource = Path.Combine("Items", "MegaStones");

            MegaPokemonNumber = megaPokemonNumber;
        }

    }
}
