using Microsoft.Xna.Framework;

namespace P3D.Legacy.Core.World
{
    public interface IPokemonEncounter
    {
        /// <summary>
        /// Checks if the player should encounter a wild Pokémon.
        /// </summary>
        /// <param name="position">The position the encounter should happen.</param>
        /// <param name="method">The method of the encounter.</param>
        /// <param name="pokeFile">The source .poke file. If left empty, the game will assume the levelfile as source .poke file.</param>

        void TryEncounterWildPokemon(Vector3 position, EncounterMethods method, string pokeFile);
        /// <summary>
        /// Triggers a battle with a wild Pokémon if the requirements are met.
        /// </summary>
        void TriggerBattle();
    }
}