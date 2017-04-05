using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using P3D.Legacy.Core.Pokemon;
using P3D.Legacy.Core.Resources;
using P3D.Legacy.Core.Resources.Managers;

namespace P3D.Legacy.Core.Battle
{
    public class BattleStats
    {
        public static Texture2D GetStatImage(BasePokemon.StatusProblems status)
        {
            Rectangle r = new Rectangle(0, 0, 0, 0);

            switch (status)
            {
                case BasePokemon.StatusProblems.BadPoison:
                case BasePokemon.StatusProblems.Poison:
                    r = new Rectangle(96, 18, 19, 6);
                    break;
                case BasePokemon.StatusProblems.Burn:
                    r = new Rectangle(96, 0, 19, 6);
                    break;
                case BasePokemon.StatusProblems.Fainted:
                    r = new Rectangle(96, 30, 19, 6);
                    break;
                case BasePokemon.StatusProblems.Freeze:
                    r = new Rectangle(96, 12, 19, 6);
                    break;
                case BasePokemon.StatusProblems.Paralyzed:
                    r = new Rectangle(96, 6, 19, 6);
                    break;
                case BasePokemon.StatusProblems.Sleep:
                    r = new Rectangle(96, 24, 19, 6);
                    break;
                case BasePokemon.StatusProblems.None:
                    return null;
            }

            return TextureManager.GetTexture("GUI|Menus|Types", r, "");
        }

        public static Color GetStatColor(BasePokemon.StatusProblems Status)
        {
            switch (Status)
            {
                case BasePokemon.StatusProblems.BadPoison:
                case BasePokemon.StatusProblems.Poison:
                    return new Color(214, 49, 222);
                case BasePokemon.StatusProblems.Burn:
                    return new Color(231, 90, 74);
                case BasePokemon.StatusProblems.Paralyzed:
                    return new Color(239, 173, 0);
                case BasePokemon.StatusProblems.Freeze:
                    return new Color(33, 140, 247);
                case BasePokemon.StatusProblems.Sleep:
                    return new Color(132, 132, 132);
                case BasePokemon.StatusProblems.Fainted:
                    return new Color(181, 0, 0);
            }

            return Color.White;
        }
    }
}
