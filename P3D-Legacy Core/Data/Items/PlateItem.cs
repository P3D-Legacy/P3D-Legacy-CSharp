using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;

namespace P3D.Legacy.Core.Pokemon
{
    /// <summary>
    /// The base item for all Arceus plates.
    /// </summary>
    public abstract class PlateItem : Item
    {
        public override bool CanBeUsed { get; }
        public override bool CanBeUsedInBattle { get; }
        public override ItemTypes ItemType { get; }
        public override string Description { get; }
        public override int PokeDollarPrice { get; }

        public PlateItem(Element.Types type)
        {
            Description = "An item to be held by a Pokémon. It's a stone tablet that boosts the power of " + type + "-type moves.";
            TextureSource = Path.Combine("Items", "Plates");
            TextureRectangle = GetTextureRectangle(type);
        }

        private Rectangle GetTextureRectangle(Element.Types type)
        {
            var typeArray = new List<Element.Types> {
                Element.Types.Bug,
                Element.Types.Dark,
                Element.Types.Dragon,
                Element.Types.Electric,
                Element.Types.Fairy,
                Element.Types.Fighting,
                Element.Types.Fire,
                Element.Types.Flying,
                Element.Types.Ghost,
                Element.Types.Grass,
                Element.Types.Ground,
                Element.Types.Ice,
                Element.Types.Poison,
                Element.Types.Psychic,
                Element.Types.Rock,
                Element.Types.Steel,
                Element.Types.Water
            };
            int i = typeArray.IndexOf(type);

            int x = i;
            int y = 0;
            while (x > 4)
            {
                x -= 5;
                y += 1;
            }

            return new Rectangle(x * 24, y * 24, 24, 24);
        }

    }
}
