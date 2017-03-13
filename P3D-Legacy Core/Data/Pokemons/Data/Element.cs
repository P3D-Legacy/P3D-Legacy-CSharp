using Microsoft.Xna.Framework;

namespace P3D.Legacy.Core.Pokemon
{
    public class Element
    {
        /// <summary>
        /// The Type an Element can be.
        /// </summary>
        public enum Types
        {
            Normal,
            Fighting,
            Flying,
            Poison,
            Ground,
            Rock,
            Bug,
            Ghost,
            Steel,
            Fire,
            Water,
            Grass,
            Electric,
            Psychic,
            Ice,
            Dragon,
            Dark,
            Fairy,
            Shadow,
            Blank
        }

        public static bool operator ==(Element element1, Element element2) => !ReferenceEquals(element1, null) && !ReferenceEquals(element2, null) && element1.Type == element2.Type;
        public static bool operator !=(Element element1, Element element2) => !ReferenceEquals(element1, null) && !ReferenceEquals(element2, null) && element1.Type != element2.Type;

        /// <summary>
        /// Returns a multiplier which represents the connection between an attacking and a defending element.
        /// </summary>
        /// <param name="attackElement">The attacking element.</param>
        /// <param name="defenseElement">The defending element.</param>
        public static float GetElementMultiplier(Element attackElement, Element defenseElement)
        {
            if (attackElement == null || defenseElement == null)
                return 1;

            if (defenseElement.Type == Element.Types.Blank || attackElement.Type == Element.Types.Blank)
                return 1;

            switch (attackElement.Type)
            {
                case Element.Types.Normal:
                    switch (defenseElement.Type)
                    {
                        case Element.Types.Normal:
                            return 1;
                        case Element.Types.Fighting:
                            return 1;
                        case Element.Types.Flying:
                            return 1;
                        case Element.Types.Poison:
                            return 1;
                        case Element.Types.Ground:
                            return 1;
                        case Element.Types.Rock:
                            return 0.5f;
                        case Element.Types.Bug:
                            return 1;
                        case Element.Types.Ghost:
                            return 0;
                        case Element.Types.Steel:
                            return 0.5f;
                        case Element.Types.Fire:
                            return 1;
                        case Element.Types.Water:
                            return 1;
                        case Element.Types.Grass:
                            return 1;
                        case Element.Types.Electric:
                            return 1;
                        case Element.Types.Psychic:
                            return 1;
                        case Element.Types.Ice:
                            return 1;
                        case Element.Types.Dragon:
                            return 1;
                        case Element.Types.Dark:
                            return 1;
                        default:
                            return 1;
                    }

                case Element.Types.Fighting:
                    switch (defenseElement.Type)
                    {
                        case Element.Types.Normal:
                            return 2;
                        case Element.Types.Fighting:
                            return 1;
                        case Element.Types.Flying:
                            return 0.5f;
                        case Element.Types.Poison:
                            return 0.5f;
                        case Element.Types.Ground:
                            return 1;
                        case Element.Types.Rock:
                            return 2;
                        case Element.Types.Bug:
                            return 0.5f;
                        case Element.Types.Ghost:
                            return 0;
                        case Element.Types.Steel:
                            return 2;
                        case Element.Types.Fire:
                            return 1;
                        case Element.Types.Water:
                            return 1;
                        case Element.Types.Grass:
                            return 1;
                        case Element.Types.Electric:
                            return 1;
                        case Element.Types.Psychic:
                            return 0.5f;
                        case Element.Types.Ice:
                            return 2;
                        case Element.Types.Dragon:
                            return 1;
                        case Element.Types.Dark:
                            return 2;
                        case Element.Types.Fairy:
                            return 0.5f;
                        default:
                            return 1;
                    }

                case Element.Types.Flying:
                    switch (defenseElement.Type)
                    {
                        case Element.Types.Normal:
                            return 1;
                        case Element.Types.Fighting:
                            return 2;
                        case Element.Types.Flying:
                            return 1;
                        case Element.Types.Poison:
                            return 1;
                        case Element.Types.Ground:
                            return 1;
                        case Element.Types.Rock:
                            return 0.5f;
                        case Element.Types.Bug:
                            return 2;
                        case Element.Types.Ghost:
                            return 1;
                        case Element.Types.Steel:
                            return 0.5f;
                        case Element.Types.Fire:
                            return 1;
                        case Element.Types.Water:
                            return 1;
                        case Element.Types.Grass:
                            return 2;
                        case Element.Types.Electric:
                            return 0.5f;
                        case Element.Types.Psychic:
                            return 1;
                        case Element.Types.Ice:
                            return 1;
                        case Element.Types.Dragon:
                            return 1;
                        case Element.Types.Dark:
                            return 1;
                        default:
                            return 1;
                    }

                case Element.Types.Poison:
                    switch (defenseElement.Type)
                    {
                        case Element.Types.Normal:
                            return 1;
                        case Element.Types.Fighting:
                            return 1;
                        case Element.Types.Flying:
                            return 1;
                        case Element.Types.Poison:
                            return 0.5f;
                        case Element.Types.Ground:
                            return 0.5f;
                        case Element.Types.Rock:
                            return 0.5f;
                        case Element.Types.Bug:
                            return 1;
                        case Element.Types.Ghost:
                            return 0.5f;
                        case Element.Types.Steel:
                            return 0;
                        case Element.Types.Fire:
                            return 1;
                        case Element.Types.Water:
                            return 1;
                        case Element.Types.Grass:
                            return 2;
                        case Element.Types.Electric:
                            return 1;
                        case Element.Types.Psychic:
                            return 1;
                        case Element.Types.Ice:
                            return 1;
                        case Element.Types.Dragon:
                            return 1;
                        case Element.Types.Dark:
                            return 1;
                        case Element.Types.Fairy:
                            return 2;
                        default:
                            return 1;
                    }

                case Element.Types.Ground:
                    switch (defenseElement.Type)
                    {
                        case Element.Types.Normal:
                            return 1;
                        case Element.Types.Fighting:
                            return 1;
                        case Element.Types.Flying:
                            return 0;
                        case Element.Types.Poison:
                            return 2;
                        case Element.Types.Ground:
                            return 1;
                        case Element.Types.Rock:
                            return 2;
                        case Element.Types.Bug:
                            return 0.5f;
                        case Element.Types.Ghost:
                            return 1;
                        case Element.Types.Steel:
                            return 2;
                        case Element.Types.Fire:
                            return 2;
                        case Element.Types.Water:
                            return 1;
                        case Element.Types.Grass:
                            return 0.5f;
                        case Element.Types.Electric:
                            return 2;
                        case Element.Types.Psychic:
                            return 1;
                        case Element.Types.Ice:
                            return 1;
                        case Element.Types.Dragon:
                            return 1;
                        case Element.Types.Dark:
                            return 1;
                        default:
                            return 1;
                    }

                case Element.Types.Rock:
                    switch (defenseElement.Type)
                    {
                        case Element.Types.Normal:
                            return 1;
                        case Element.Types.Fighting:
                            return 0.5f;
                        case Element.Types.Flying:
                            return 2;
                        case Element.Types.Poison:
                            return 1;
                        case Element.Types.Ground:
                            return 0.5f;
                        case Element.Types.Rock:
                            return 1;
                        case Element.Types.Bug:
                            return 2;
                        case Element.Types.Ghost:
                            return 1;
                        case Element.Types.Steel:
                            return 0.5f;
                        case Element.Types.Fire:
                            return 2;
                        case Element.Types.Water:
                            return 1;
                        case Element.Types.Grass:
                            return 1;
                        case Element.Types.Electric:
                            return 1;
                        case Element.Types.Psychic:
                            return 1;
                        case Element.Types.Ice:
                            return 2;
                        case Element.Types.Dragon:
                            return 1;
                        case Element.Types.Dark:
                            return 1;
                        default:
                            return 1;
                    }

                case Element.Types.Bug:
                    switch (defenseElement.Type)
                    {
                        case Element.Types.Normal:
                            return 1;
                        case Element.Types.Fighting:
                            return 0.5f;
                        case Element.Types.Flying:
                            return 0.5f;
                        case Element.Types.Poison:
                            return 0.5f;
                        case Element.Types.Ground:
                            return 1;
                        case Element.Types.Rock:
                            return 1;
                        case Element.Types.Bug:
                            return 1;
                        case Element.Types.Ghost:
                            return 0.5f;
                        case Element.Types.Steel:
                            return 0.5f;
                        case Element.Types.Fire:
                            return 0.5f;
                        case Element.Types.Water:
                            return 1;
                        case Element.Types.Grass:
                            return 2;
                        case Element.Types.Electric:
                            return 1;
                        case Element.Types.Psychic:
                            return 2;
                        case Element.Types.Ice:
                            return 1;
                        case Element.Types.Dragon:
                            return 1;
                        case Element.Types.Dark:
                            return 2;
                        case Element.Types.Fairy:
                            return 0.5f;
                        default:
                            return 1;
                    }

                case Element.Types.Ghost:
                    switch (defenseElement.Type)
                    {
                        case Element.Types.Normal:
                            return 0;
                        case Element.Types.Fighting:
                            return 1;
                        case Element.Types.Flying:
                            return 1;
                        case Element.Types.Poison:
                            return 1;
                        case Element.Types.Ground:
                            return 1;
                        case Element.Types.Rock:
                            return 1;
                        case Element.Types.Bug:
                            return 1;
                        case Element.Types.Ghost:
                            return 2;
                        case Element.Types.Steel:
                            return 1;
                        case Element.Types.Fire:
                            return 1;
                        case Element.Types.Water:
                            return 1;
                        case Element.Types.Grass:
                            return 1;
                        case Element.Types.Electric:
                            return 1;
                        case Element.Types.Psychic:
                            return 2;
                        case Element.Types.Ice:
                            return 1;
                        case Element.Types.Dragon:
                            return 1;
                        case Element.Types.Dark:
                            return 0.5f;
                        default:
                            return 1;
                    }

                case Element.Types.Steel:
                    switch (defenseElement.Type)
                    {
                        case Element.Types.Normal:
                            return 1;
                        case Element.Types.Fighting:
                            return 1;
                        case Element.Types.Flying:
                            return 1;
                        case Element.Types.Poison:
                            return 1;
                        case Element.Types.Ground:
                            return 1;
                        case Element.Types.Rock:
                            return 2;
                        case Element.Types.Bug:
                            return 1;
                        case Element.Types.Ghost:
                            return 1;
                        case Element.Types.Steel:
                            return 0.5f;
                        case Element.Types.Fire:
                            return 0.5f;
                        case Element.Types.Water:
                            return 0.5f;
                        case Element.Types.Grass:
                            return 1;
                        case Element.Types.Electric:
                            return 0.5f;
                        case Element.Types.Psychic:
                            return 1;
                        case Element.Types.Ice:
                            return 2;
                        case Element.Types.Dragon:
                            return 1;
                        case Element.Types.Dark:
                            return 1;
                        case Element.Types.Fairy:
                            return 2;
                        default:
                            return 1;
                    }

                case Element.Types.Fire:
                    switch (defenseElement.Type)
                    {
                        case Element.Types.Normal:
                            return 1;
                        case Element.Types.Fighting:
                            return 1;
                        case Element.Types.Flying:
                            return 1;
                        case Element.Types.Poison:
                            return 1;
                        case Element.Types.Ground:
                            return 1;
                        case Element.Types.Rock:
                            return 0.5f;
                        case Element.Types.Bug:
                            return 2;
                        case Element.Types.Ghost:
                            return 1;
                        case Element.Types.Steel:
                            return 2;
                        case Element.Types.Fire:
                            return 0.5f;
                        case Element.Types.Water:
                            return 0.5f;
                        case Element.Types.Grass:
                            return 2;
                        case Element.Types.Electric:
                            return 1;
                        case Element.Types.Psychic:
                            return 1;
                        case Element.Types.Ice:
                            return 2;
                        case Element.Types.Dragon:
                            return 0.5f;
                        case Element.Types.Dark:
                            return 1;
                        default:
                            return 1;
                    }

                case Element.Types.Water:
                    switch (defenseElement.Type)
                    {
                        case Element.Types.Normal:
                            return 1;
                        case Element.Types.Fighting:
                            return 1;
                        case Element.Types.Flying:
                            return 1;
                        case Element.Types.Poison:
                            return 1;
                        case Element.Types.Ground:
                            return 2;
                        case Element.Types.Rock:
                            return 2;
                        case Element.Types.Bug:
                            return 1;
                        case Element.Types.Ghost:
                            return 1;
                        case Element.Types.Steel:
                            return 1;
                        case Element.Types.Fire:
                            return 2;
                        case Element.Types.Water:
                            return 0.5f;
                        case Element.Types.Grass:
                            return 0.5f;
                        case Element.Types.Electric:
                            return 1;
                        case Element.Types.Psychic:
                            return 1;
                        case Element.Types.Ice:
                            return 1;
                        case Element.Types.Dragon:
                            return 0.5f;
                        case Element.Types.Dark:
                            return 1;
                        default:
                            return 1;
                    }

                case Element.Types.Grass:
                    switch (defenseElement.Type)
                    {
                        case Element.Types.Normal:
                            return 1;
                        case Element.Types.Fighting:
                            return 1;
                        case Element.Types.Flying:
                            return 0.5f;
                        case Element.Types.Poison:
                            return 0.5f;
                        case Element.Types.Ground:
                            return 2;
                        case Element.Types.Rock:
                            return 2;
                        case Element.Types.Bug:
                            return 0.5f;
                        case Element.Types.Ghost:
                            return 1;
                        case Element.Types.Steel:
                            return 0.5f;
                        case Element.Types.Fire:
                            return 0.5f;
                        case Element.Types.Water:
                            return 2;
                        case Element.Types.Grass:
                            return 0.5f;
                        case Element.Types.Electric:
                            return 1;
                        case Element.Types.Psychic:
                            return 1;
                        case Element.Types.Ice:
                            return 1;
                        case Element.Types.Dragon:
                            return 0.5f;
                        case Element.Types.Dark:
                            return 1;
                        default:
                            return 1;
                    }

                case Element.Types.Electric:
                    switch (defenseElement.Type)
                    {
                        case Element.Types.Normal:
                            return 1;
                        case Element.Types.Fighting:
                            return 1;
                        case Element.Types.Flying:
                            return 2;
                        case Element.Types.Poison:
                            return 1;
                        case Element.Types.Ground:
                            return 0;
                        case Element.Types.Rock:
                            return 1;
                        case Element.Types.Bug:
                            return 1;
                        case Element.Types.Ghost:
                            return 1;
                        case Element.Types.Steel:
                            return 1;
                        case Element.Types.Fire:
                            return 1;
                        case Element.Types.Water:
                            return 2;
                        case Element.Types.Grass:
                            return 0.5f;
                        case Element.Types.Electric:
                            return 0.5f;
                        case Element.Types.Psychic:
                            return 1;
                        case Element.Types.Ice:
                            return 1;
                        case Element.Types.Dragon:
                            return 0.5f;
                        case Element.Types.Dark:
                            return 1;
                        default:
                            return 1;
                    }

                case Element.Types.Psychic:
                    switch (defenseElement.Type)
                    {
                        case Element.Types.Normal:
                            return 1;
                        case Element.Types.Fighting:
                            return 2;
                        case Element.Types.Flying:
                            return 1;
                        case Element.Types.Poison:
                            return 2;
                        case Element.Types.Ground:
                            return 1;
                        case Element.Types.Rock:
                            return 1;
                        case Element.Types.Bug:
                            return 1;
                        case Element.Types.Ghost:
                            return 1;
                        case Element.Types.Steel:
                            return 0.5f;
                        case Element.Types.Fire:
                            return 1;
                        case Element.Types.Water:
                            return 1;
                        case Element.Types.Grass:
                            return 1;
                        case Element.Types.Electric:
                            return 1;
                        case Element.Types.Psychic:
                            return 0.5f;
                        case Element.Types.Ice:
                            return 1;
                        case Element.Types.Dragon:
                            return 1;
                        case Element.Types.Dark:
                            return 0;
                        default:
                            return 1;
                    }

                case Element.Types.Ice:
                    switch (defenseElement.Type)
                    {
                        case Element.Types.Normal:
                            return 1;
                        case Element.Types.Fighting:
                            return 1;
                        case Element.Types.Flying:
                            return 2;
                        case Element.Types.Poison:
                            return 1;
                        case Element.Types.Ground:
                            return 2;
                        case Element.Types.Rock:
                            return 1;
                        case Element.Types.Bug:
                            return 1;
                        case Element.Types.Ghost:
                            return 1;
                        case Element.Types.Steel:
                            return 0.5f;
                        case Element.Types.Fire:
                            return 0.5f;
                        case Element.Types.Water:
                            return 0.5f;
                        case Element.Types.Grass:
                            return 2;
                        case Element.Types.Electric:
                            return 1;
                        case Element.Types.Psychic:
                            return 1;
                        case Element.Types.Ice:
                            return 0.5f;
                        case Element.Types.Dragon:
                            return 2;
                        case Element.Types.Dark:
                            return 1;
                        default:
                            return 1;
                    }

                case Element.Types.Dragon:
                    switch (defenseElement.Type)
                    {
                        case Element.Types.Normal:
                            return 1;
                        case Element.Types.Fighting:
                            return 1;
                        case Element.Types.Flying:
                            return 1;
                        case Element.Types.Poison:
                            return 1;
                        case Element.Types.Ground:
                            return 1;
                        case Element.Types.Rock:
                            return 1;
                        case Element.Types.Bug:
                            return 1;
                        case Element.Types.Ghost:
                            return 1;
                        case Element.Types.Steel:
                            return 0.5f;
                        case Element.Types.Fire:
                            return 1;
                        case Element.Types.Water:
                            return 1;
                        case Element.Types.Grass:
                            return 1;
                        case Element.Types.Electric:
                            return 1;
                        case Element.Types.Psychic:
                            return 1;
                        case Element.Types.Ice:
                            return 1;
                        case Element.Types.Dragon:
                            return 2;
                        case Element.Types.Dark:
                            return 1;
                        case Element.Types.Fairy:
                            return 0;
                        default:
                            return 1;
                    }

                case Element.Types.Dark:
                    switch (defenseElement.Type)
                    {
                        case Element.Types.Normal:
                            return 1;
                        case Element.Types.Fighting:
                            return 0.5f;
                        case Element.Types.Flying:
                            return 1;
                        case Element.Types.Poison:
                            return 1;
                        case Element.Types.Ground:
                            return 1;
                        case Element.Types.Rock:
                            return 1;
                        case Element.Types.Bug:
                            return 1;
                        case Element.Types.Ghost:
                            return 2;
                        case Element.Types.Steel:
                            return 1;
                        case Element.Types.Fire:
                            return 1;
                        case Element.Types.Water:
                            return 1;
                        case Element.Types.Grass:
                            return 1;
                        case Element.Types.Electric:
                            return 1;
                        case Element.Types.Psychic:
                            return 2;
                        case Element.Types.Ice:
                            return 1;
                        case Element.Types.Dragon:
                            return 1;
                        case Element.Types.Dark:
                            return 0.5f;
                        case Element.Types.Fairy:
                            return 0.5f;
                        default:
                            return 1;
                    }

                case Element.Types.Fairy: // TODO: Not completed
                    switch (defenseElement.Type)
                    {
                        case Element.Types.Fire:
                            return 0.5f;
                        case Element.Types.Fighting:
                            return 2f;
                        case Element.Types.Poison:
                            return 0.5f;
                        case Element.Types.Dragon:
                            return 2f;
                        case Element.Types.Dark:
                            return 2f;
                        case Element.Types.Steel:
                            return 0.5f;
                    }
                    break;
                case Element.Types.Shadow:
                    switch (defenseElement.Type)
                    {
                        case Element.Types.Shadow:
                            return 0.5f;
                        default:
                            return 2;
                    }

                default:
                    return 1;
            }

            return 1;
        }


        /// <summary>
        /// The Type of this Element.
        /// </summary>
        public Types Type { get; set; } = Element.Types.Blank;


        /// <summary>
        /// Creates a new instance of the Element class.
        /// </summary>
        /// <param name="TypeID">The ID of the type.</param>
        public Element(int typeID)
        {
            switch (typeID)
            {
                case 0:
                    Type = Element.Types.Normal;
                    break;
                case 1:
                    Type = Element.Types.Fighting;
                    break;
                case 2:
                    Type = Element.Types.Flying;
                    break;
                case 3:
                    Type = Element.Types.Poison;
                    break;
                case 4:
                    Type = Element.Types.Ground;
                    break;
                case 5:
                    Type = Element.Types.Rock;
                    break;
                case 6:
                    Type = Element.Types.Bug;
                    break;
                case 7:
                    Type = Element.Types.Ghost;
                    break;
                case 8:
                    Type = Element.Types.Steel;
                    break;
                case 9:
                    Type = Element.Types.Fire;
                    break;
                case 10:
                    Type = Element.Types.Water;
                    break;
                case 11:
                    Type = Element.Types.Grass;
                    break;
                case 12:
                    Type = Element.Types.Electric;
                    break;
                case 13:
                    Type = Element.Types.Psychic;
                    break;
                case 14:
                    Type = Element.Types.Ice;
                    break;
                case 15:
                    Type = Element.Types.Dragon;
                    break;
                case 16:
                    Type = Element.Types.Dark;
                    break;
                case 17:
                    Type = Element.Types.Blank;
                    break;
                case 18:
                    Type = Element.Types.Fairy;
                    break;
            }
        }

        /// <summary>
        /// Creates a new instance of the Element class.
        /// </summary>
        /// <param name="Type">The Type as string.</param>
        public Element(string type)
        {
            switch (type.ToLower())
            {
                case "normal":
                    Type = Element.Types.Normal;
                    break;
                case "fighting":
                    Type = Element.Types.Fighting;
                    break;
                case "flying":
                    Type = Element.Types.Flying;
                    break;
                case "poison":
                    Type = Element.Types.Poison;
                    break;
                case "ground":
                    Type = Element.Types.Ground;
                    break;
                case "rock":
                    Type = Element.Types.Rock;
                    break;
                case "bug":
                    Type = Element.Types.Bug;
                    break;
                case "ghost":
                    Type = Element.Types.Ghost;
                    break;
                case "steel":
                    Type = Element.Types.Steel;
                    break;
                case "fire":
                    Type = Element.Types.Fire;
                    break;
                case "water":
                    Type = Element.Types.Water;
                    break;
                case "grass":
                    Type = Element.Types.Grass;
                    break;
                case "electric":
                    Type = Element.Types.Electric;
                    break;
                case "psychic":
                    Type = Element.Types.Psychic;
                    break;
                case "ice":
                    Type = Element.Types.Ice;
                    break;
                case "dragon":
                    Type = Element.Types.Dragon;
                    break;
                case "dark":
                    Type = Element.Types.Dark;
                    break;
                case "fairy":
                    Type = Element.Types.Fairy;
                    break;
                case "shadow":
                    Type = Element.Types.Shadow;
                    break;
                case "blank":
                    Type = Element.Types.Blank;
                    break;
                default:
                    Type = Element.Types.Blank;
                    break;
            }
        }

        /// <summary>
        /// Creates a new instance of the Element class.
        /// </summary>
        /// <param name="Type">The Type to set this Element to.</param>
        public Element(Types type)
        {
            Type = type;
        }

        /// <summary>
        /// Returns the rectangle from the texture "GUI\Menus\Types" that represents the Type of this Element.
        /// </summary>
        public Rectangle GetElementImage()
        {
            var r = new Rectangle(0, 0, 0, 0);

            switch (Type)
            {
                case Element.Types.Normal:
                    r = new Rectangle(0, 0, 48, 16);
                    break;
                case Element.Types.Grass:
                    r = new Rectangle(0, 16, 48, 16);
                    break;
                case Element.Types.Fire:
                    r = new Rectangle(0, 32, 48, 16);
                    break;
                case Element.Types.Water:
                    r = new Rectangle(0, 48, 48, 16);
                    break;
                case Element.Types.Electric:
                    r = new Rectangle(0, 64, 48, 16);
                    break;
                case Element.Types.Ground:
                    r = new Rectangle(0, 80, 48, 16);
                    break;
                case Element.Types.Rock:
                    r = new Rectangle(0, 96, 48, 16);
                    break;
                case Element.Types.Ice:
                    r = new Rectangle(0, 112, 48, 16);
                    break;
                case Element.Types.Steel:
                    r = new Rectangle(0, 128, 48, 16);
                    break;
                case Element.Types.Bug:
                    r = new Rectangle(48, 0, 48, 16);
                    break;
                case Element.Types.Fighting:
                    r = new Rectangle(48, 16, 48, 16);
                    break;
                case Element.Types.Flying:
                    r = new Rectangle(48, 32, 48, 16);
                    break;
                case Element.Types.Poison:
                    r = new Rectangle(48, 48, 48, 16);
                    break;
                case Element.Types.Ghost:
                    r = new Rectangle(48, 64, 48, 16);
                    break;
                case Element.Types.Dark:
                    r = new Rectangle(48, 80, 48, 16);
                    break;
                case Element.Types.Psychic:
                    r = new Rectangle(48, 96, 48, 16);
                    break;
                case Element.Types.Dragon:
                    r = new Rectangle(48, 128, 48, 16);
                    break;
                case Element.Types.Fairy:
                    r = new Rectangle(96, 48, 48, 16);
                    break;
                case Element.Types.Shadow:
                    r = new Rectangle(96, 64, 48, 16);
                    break;
                case Element.Types.Blank:
                    r = new Rectangle(48, 112, 48, 16);
                    break;
                default:
                    r = new Rectangle(48, 112, 48, 16);
                    break;
            }

            return r;
        }
        

        public override string ToString()
        {
            switch (Type)
            {
                case Element.Types.Blank:
                    return "Blank";
                case Element.Types.Bug:
                    return "Bug";
                case Element.Types.Dark:
                    return "Dark";
                case Element.Types.Dragon:
                    return "Dragon";
                case Element.Types.Electric:
                    return "Electric";
                case Element.Types.Fairy:
                    return "Fairy";
                case Element.Types.Fighting:
                    return "Fighting";
                case Element.Types.Fire:
                    return "Fire";
                case Element.Types.Flying:
                    return "Flying";
                case Element.Types.Ghost:
                    return "Ghost";
                case Element.Types.Grass:
                    return "Grass";
                case Element.Types.Ground:
                    return "Ground";
                case Element.Types.Ice:
                    return "Ice";
                case Element.Types.Normal:
                    return "Normal";
                case Element.Types.Poison:
                    return "Poison";
                case Element.Types.Psychic:
                    return "Psychic";
                case Element.Types.Rock:
                    return "Rock";
                case Element.Types.Shadow:
                    return "Shadow";
                case Element.Types.Steel:
                    return "Steel";
                case Element.Types.Water:
                    return "Water";
                default:
                    return "Blank";
            }
        }
    }
}
