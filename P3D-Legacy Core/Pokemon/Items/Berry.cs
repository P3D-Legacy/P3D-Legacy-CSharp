using System.IO;
using Microsoft.Xna.Framework;

namespace P3D.Legacy.Core.Pokemon
{
    public abstract class Berry : MedicineItem
    {
        public enum Flavours
        {
            Spicy,
            Dry,
            Sweet,
            Bitter,
            Sour
        }


        public int PhaseTime;
        public string Size;
        public string Firmness;
        public int BerryIndex;
        public int MinBerries;

        public int MaxBerries;
        public int Spicy = 0;
        public int Dry = 0;
        public int Sweet = 0;
        public int Bitter = 0;

        public int Sour = 0;
        public int WinterGrow = 0;
        public int SpringGrow = 3;
        public int SummerGrow = 2;

        public int FallGrow = 1;
        public Element.Types Type;

        public int Power = 60;
        public override int PokeDollarPrice { get; }
        public override int FlingDamage { get; }
        public override ItemTypes ItemType { get; }
        public override int SortValue { get; }
        public override string Description { get; }

        public Berry(int phaseTime, string description, string size, string firmness, int minBerries, int maxBerries)
        {
            SortValue = Id - 1999;

            PhaseTime = phaseTime;
            Size = size;
            Firmness = firmness;
            BerryIndex = Id - 2000;
            MinBerries = minBerries;
            MaxBerries = maxBerries;

            int x = BerryIndex * 128;
            int y = 0;
            while (x >= 512)
            {
                x -= 512;
                y += 32;
            }

            Description = description;
            TextureSource = Path.Combine("Textures", "Berries");
            TextureRectangle = new Rectangle(x, y, 32, 32);
        }

        public Flavours Flavour
        {
            get
            {
                Flavours returnFlavour = Flavours.Spicy;
                int highestFlavour = Spicy;

                if (Dry > highestFlavour)
                {
                    highestFlavour = Dry;
                    returnFlavour = Flavours.Dry;
                }
                if (Sweet > highestFlavour)
                {
                    highestFlavour = Sweet;
                    returnFlavour = Flavours.Sweet;
                }
                if (Bitter > highestFlavour)
                {
                    highestFlavour = Bitter;
                    returnFlavour = Flavours.Bitter;
                }
                if (Sour > highestFlavour)
                {
                    highestFlavour = Sour;
                    returnFlavour = Flavours.Sour;
                }

                return returnFlavour;
            }
        }

        /// <summary>
        /// Returns if a Pokémon likes this berry based on its flavour.
        /// </summary>
        /// <param name="p">The Pokémon to test this berry for.</param>
        public bool PokemonLikes(BasePokemon p)
        {
            switch (p.Nature)
            {
                case BasePokemon.Natures.Lonely:
                    if (Flavour == Flavours.Spicy)
                    {
                        return true;
                    }
                    else if (Flavour == Flavours.Sour)
                    {
                        return false;
                    }
                    break;
                case BasePokemon.Natures.Adamant:
                    if (Flavour == Flavours.Spicy)
                    {
                        return true;
                    }
                    else if (Flavour == Flavours.Dry)
                    {
                        return false;
                    }
                    break;
                case BasePokemon.Natures.Naughty:
                    if (Flavour == Flavours.Spicy)
                    {
                        return true;
                    }
                    else if (Flavour == Flavours.Bitter)
                    {
                        return false;
                    }
                    break;
                case BasePokemon.Natures.Brave:
                    if (Flavour == Flavours.Spicy)
                    {
                        return true;
                    }
                    else if (Flavour == Flavours.Sweet)
                    {
                        return false;
                    }
                    break;
                case BasePokemon.Natures.Bold:
                    if (Flavour == Flavours.Sour)
                    {
                        return true;
                    }
                    else if (Flavour == Flavours.Spicy)
                    {
                        return false;
                    }
                    break;
                case BasePokemon.Natures.Impish:
                    if (Flavour == Flavours.Sour)
                    {
                        return true;
                    }
                    else if (Flavour == Flavours.Dry)
                    {
                        return false;
                    }
                    break;
                case BasePokemon.Natures.Lax:
                    if (Flavour == Flavours.Sour)
                    {
                        return true;
                    }
                    else if (Flavour == Flavours.Bitter)
                    {
                        return false;
                    }
                    break;
                case BasePokemon.Natures.Relaxed:
                    if (Flavour == Flavours.Sour)
                    {
                        return true;
                    }
                    else if (Flavour == Flavours.Sweet)
                    {
                        return false;
                    }
                    break;
                case BasePokemon.Natures.Modest:
                    if (Flavour == Flavours.Dry)
                    {
                        return true;
                    }
                    else if (Flavour == Flavours.Spicy)
                    {
                        return false;
                    }
                    break;
                case BasePokemon.Natures.Mild:
                    if (Flavour == Flavours.Dry)
                    {
                        return true;
                    }
                    else if (Flavour == Flavours.Sour)
                    {
                        return false;
                    }
                    break;
                case BasePokemon.Natures.Rash:
                    if (Flavour == Flavours.Dry)
                    {
                        return true;
                    }
                    else if (Flavour == Flavours.Bitter)
                    {
                        return false;
                    }
                    break;
                case BasePokemon.Natures.Quiet:
                    if (Flavour == Flavours.Dry)
                    {
                        return true;
                    }
                    else if (Flavour == Flavours.Sweet)
                    {
                        return false;
                    }
                    break;
                case BasePokemon.Natures.Calm:
                    if (Flavour == Flavours.Bitter)
                    {
                        return true;
                    }
                    else if (Flavour == Flavours.Spicy)
                    {
                        return false;
                    }
                    break;
                case BasePokemon.Natures.Gentle:
                    if (Flavour == Flavours.Bitter)
                    {
                        return true;
                    }
                    else if (Flavour == Flavours.Sour)
                    {
                        return false;
                    }
                    break;
                case BasePokemon.Natures.Careful:
                    if (Flavour == Flavours.Bitter)
                    {
                        return true;
                    }
                    else if (Flavour == Flavours.Dry)
                    {
                        return false;
                    }
                    break;
                case BasePokemon.Natures.Sassy:
                    if (Flavour == Flavours.Bitter)
                    {
                        return true;
                    }
                    else if (Flavour == Flavours.Sweet)
                    {
                        return false;
                    }
                    break;
                case BasePokemon.Natures.Timid:
                    if (Flavour == Flavours.Sweet)
                    {
                        return true;
                    }
                    else if (Flavour == Flavours.Spicy)
                    {
                        return false;
                    }
                    break;
                case BasePokemon.Natures.Hasty:
                    if (Flavour == Flavours.Sweet)
                    {
                        return true;
                    }
                    else if (Flavour == Flavours.Sour)
                    {
                        return false;
                    }
                    break;
                case BasePokemon.Natures.Jolly:
                    if (Flavour == Flavours.Sweet)
                    {
                        return true;
                    }
                    else if (Flavour == Flavours.Dry)
                    {
                        return false;
                    }
                    break;
                case BasePokemon.Natures.Naive:
                    if (Flavour == Flavours.Sweet)
                    {
                        return true;
                    }
                    else if (Flavour == Flavours.Bitter)
                    {
                        return false;
                    }
                    break;
                default:
                    return true;
            }
            return true;
        }

    }
}
