using System;

using Microsoft.Xna.Framework;

using P3D.Legacy.Core.Entities;
using P3D.Legacy.Core.Resources.Sound;
using P3D.Legacy.Core.Screens;

namespace P3D.Legacy.Core.Pokemon
{
    /// <summary>
    /// Represents a Medicine Item.
    /// </summary>
    public abstract class MedicineItem : Item
    {
        public override ItemTypes ItemType { get; }

        /// <summary>
        /// Tries to heal a Pokémon from the player's party. If this succeeds, the method returns True.
        /// </summary>
        /// <param name="pokeIndex">The index of the Pokémon in the player's party.</param>
        /// <param name="hp">The HP that should be healed.</param>
        public bool HealPokemon(int pokeIndex, int hp)
        {
            if (pokeIndex < 0 || pokeIndex > 5)
            {
                throw new ArgumentOutOfRangeException(nameof(pokeIndex), pokeIndex, "The index for a Pokémon in a player's party can only be between 0 and 5.");
            }

            var pokemon = Core.Player.Pokemons[pokeIndex];

            if (hp < 0)
            {
                hp = Convert.ToInt32(pokemon.MaxHP / (100 / (hp * (-1))));
            }

            if (pokemon.Status == BasePokemon.StatusProblems.Fainted)
            {
                Screen.TextBox.ReDelay = 0f;
                Screen.TextBox.Show(pokemon.GetDisplayName() + "~is fainted!", new Entity[] {  });

                return false;
            }
            else
            {
                if (pokemon.HP == pokemon.MaxHP)
                {
                    Screen.TextBox.ReDelay = 0f;
                    Screen.TextBox.Show(pokemon.GetDisplayName() + " has full~HP already.", new Entity[] { });

                    return false;
                }
                else
                {
                    int diff = pokemon.MaxHP - pokemon.HP;
                    diff = Convert.ToInt32(MathHelper.Clamp(diff, 1, hp));

                    pokemon.Heal(hp);

                    Screen.TextBox.ReDelay = 0f;

                    string t = "Restored " + pokemon.GetDisplayName() + "'s~HP by " + diff + ".";
                    t += RemoveItem();

                    SoundManager.PlaySound("single_heal", false);
                    Screen.TextBox.Show(t, new Entity[] { });
                    PlayerStatistics.Track("[17]Medicine Items used", 1);

                    return true;
                }
            }
        }

        /// <summary>
        /// Tries to cure a Pokémon from Poison.
        /// </summary>
        /// <param name="pokeIndex">The index of a Pokémon in the player's party.</param>
        public bool CurePoison(int pokeIndex)
        {
            if (pokeIndex < 0 || pokeIndex > 5)
            {
                throw new ArgumentOutOfRangeException(nameof(pokeIndex), pokeIndex, "The index for a Pokémon in a player's party can only be between 0 and 5.");
            }

            var pokemon = Core.Player.Pokemons[pokeIndex];

            if (pokemon.Status == BasePokemon.StatusProblems.Fainted)
            {
                Screen.TextBox.ReDelay = 0f;
                Screen.TextBox.Show(pokemon.GetDisplayName() + "~is fainted!", new Entity[] { });

                return false;
            }
            else if (pokemon.Status == BasePokemon.StatusProblems.Poison || pokemon.Status == BasePokemon.StatusProblems.BadPoison)
            {
                pokemon.Status = BasePokemon.StatusProblems.None;

                Screen.TextBox.ReDelay = 0f;

                string t = "Cures the poison of " + pokemon.GetDisplayName() + ".";
                t += RemoveItem();
                PlayerStatistics.Track("[17]Medicine Items used", 1);

                SoundManager.PlaySound("single_heal", false);
                Screen.TextBox.Show(t, new Entity[] { });

                return true;
            }
            else
            {
                Screen.TextBox.ReDelay = 0f;
                Screen.TextBox.Show(pokemon.GetDisplayName() + " is not poisoned.", new Entity[] { });

                return false;
            }
        }

        /// <summary>
        /// Tries to wake a Pokémon up from Sleep.
        /// </summary>
        /// <param name="pokeIndex">The index of a Pokémon in the player's party.</param>
        public bool WakeUp(int pokeIndex)
        {
            if (pokeIndex < 0 || pokeIndex > 5)
            {
                throw new ArgumentOutOfRangeException(nameof(pokeIndex), pokeIndex, "The index for a Pokémon in a player's party can only be between 0 and 5.");
            }

            var pokemon = Core.Player.Pokemons[pokeIndex];

            if (pokemon.Status == BasePokemon.StatusProblems.Fainted)
            {
                Screen.TextBox.ReDelay = 0f;
                Screen.TextBox.Show(pokemon.GetDisplayName() + "~is fainted!", new Entity[] { });

                return false;
            }
            else if (pokemon.Status == BasePokemon.StatusProblems.Sleep)
            {
                pokemon.Status = BasePokemon.StatusProblems.None;

                Screen.TextBox.ReDelay = 0f;

                string t = "Cures the sleep of " + pokemon.GetDisplayName() + ".";
                t += RemoveItem();

                SoundManager.PlaySound("single_heal", false);
                Screen.TextBox.Show(t, new Entity[] { });
                PlayerStatistics.Track("[17]Medicine Items used", 1);

                return true;
            }
            else
            {
                Screen.TextBox.ReDelay = 0f;
                Screen.TextBox.Show(pokemon.GetDisplayName() + " is not asleep.", new Entity[] { });

                return false;
            }
        }

        /// <summary>
        /// Tries to heal a Pokémon from Burn.
        /// </summary>
        /// <param name="pokeIndex">The index of a Pokémon in the player's party.</param>
        public bool HealBurn(int pokeIndex)
        {
            if (pokeIndex < 0 || pokeIndex > 5)
            {
                throw new ArgumentOutOfRangeException(nameof(pokeIndex), pokeIndex, "The index for a Pokémon in a player's party can only be between 0 and 5.");
            }

            var pokemon = Core.Player.Pokemons[pokeIndex];

            if (pokemon.Status == BasePokemon.StatusProblems.Fainted)
            {
                Screen.TextBox.ReDelay = 0f;
                Screen.TextBox.Show(pokemon.GetDisplayName() + "~is fainted!", new Entity[] { });

                return false;
            }
            else if (pokemon.Status == BasePokemon.StatusProblems.Burn)
            {
                pokemon.Status = BasePokemon.StatusProblems.None;

                Screen.TextBox.ReDelay = 0f;

                string t = "Cures the burn of " + pokemon.GetDisplayName() + ".";
                t += RemoveItem();

                SoundManager.PlaySound("single_heal", false);
                Screen.TextBox.Show(t, new Entity[] { });
                PlayerStatistics.Track("[17]Medicine Items used", 1);

                return true;
            }
            else
            {
                Screen.TextBox.ReDelay = 0f;
                Screen.TextBox.Show(pokemon.GetDisplayName() + " is not burned.", new Entity[] { });

                return false;
            }
        }

        /// <summary>
        /// Tries to heal a Pokémon from Ice.
        /// </summary>
        /// <param name="pokeIndex">The index of a Pokémon in the player's party.</param>
        public bool HealIce(int pokeIndex)
        {
            if (pokeIndex < 0 || pokeIndex > 5)
            {
                throw new ArgumentOutOfRangeException(nameof(pokeIndex), pokeIndex, "The index for a Pokémon in a player's party can only be between 0 and 5.");
            }

            var pokemon = Core.Player.Pokemons[pokeIndex];

            if (pokemon.Status == BasePokemon.StatusProblems.Fainted)
            {
                Screen.TextBox.ReDelay = 0f;
                Screen.TextBox.Show(pokemon.GetDisplayName() + "~is fainted!", new Entity[] { });

                return false;
            }
            else if (pokemon.Status == BasePokemon.StatusProblems.Freeze)
            {
                pokemon.Status = BasePokemon.StatusProblems.None;

                Core.Player.Inventory.RemoveItem(this.Id, 1);

                Screen.TextBox.ReDelay = 0f;

                string t = "Cures the ice of " + pokemon.GetDisplayName() + ".";
                t += RemoveItem();

                SoundManager.PlaySound("single_heal", false);
                Screen.TextBox.Show(t, new Entity[] { });
                PlayerStatistics.Track("[17]Medicine Items used", 1);

                return true;
            }
            else
            {
                Screen.TextBox.ReDelay = 0f;
                Screen.TextBox.Show(pokemon.GetDisplayName() + " is not frozen.", new Entity[] { });

                return false;
            }
        }

        /// <summary>
        /// Tries to heal a Pokémon from Paralysis.
        /// </summary>
        /// <param name="pokeIndex">The index of a Pokémon in the player's party.</param>
        public bool HealParalyze(int pokeIndex)
        {
            if (pokeIndex < 0 || pokeIndex > 5)
            {
                throw new ArgumentOutOfRangeException(nameof(pokeIndex), pokeIndex, "The index for a Pokémon in a player's party can only be between 0 and 5.");
            }

            var pokemon = Core.Player.Pokemons[pokeIndex];

            if (pokemon.Status == BasePokemon.StatusProblems.Fainted)
            {
                Screen.TextBox.ReDelay = 0f;
                Screen.TextBox.Show(pokemon.GetDisplayName() + "~is fainted!", new Entity[] { });

                return false;
            }
            else if (pokemon.Status == BasePokemon.StatusProblems.Paralyzed)
            {
                pokemon.Status = BasePokemon.StatusProblems.None;

                Core.Player.Inventory.RemoveItem(Id, 1);

                Screen.TextBox.ReDelay = 0f;

                string t = "Cures the paralyzis~of " + pokemon.GetDisplayName() + ".";
                t += RemoveItem();

                SoundManager.PlaySound("single_heal", false);
                Screen.TextBox.Show(t, new Entity[] { });
                PlayerStatistics.Track("[17]Medicine Items used", 1);

                return true;
            }
            else
            {
                Screen.TextBox.ReDelay = 0f;
                Screen.TextBox.Show(pokemon.GetDisplayName() + " is not~paralyzed.", new Entity[] { });

                return false;
            }
        }
    }
}
