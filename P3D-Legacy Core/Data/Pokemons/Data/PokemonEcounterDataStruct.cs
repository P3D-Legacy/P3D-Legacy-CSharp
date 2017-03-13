using Microsoft.Xna.Framework;

using P3D.Legacy.Core.World;

namespace P3D.Legacy.Core.Pokemon
{
    /// <summary>
    /// A structure to store wild pokémon encounter data in.
    /// </summary>
    public class PokemonEcounterDataStruct
    {
        /// <summary>
        /// The assumed position the player will be in when encounterning the Pokémon.
        /// </summary>

        public Vector3 Position;
        /// <summary>
        /// Wether the player encountered a Pokémon.
        /// </summary>

        public bool EncounteredPokemon;
        /// <summary>
        /// The encounter method.
        /// </summary>

        public EncounterMethods Method;
        /// <summary>
        /// The link to the .poke file used to spawn the Pokémon in.
        /// </summary>
        public string PokeFile;
    }
}