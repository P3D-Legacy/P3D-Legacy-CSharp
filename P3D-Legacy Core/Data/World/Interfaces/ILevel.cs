using System.Collections.Generic;

using P3D.Legacy.Core.Entities;
using P3D.Legacy.Core.Entities.Other;
using P3D.Legacy.Core.Pokemon;

namespace P3D.Legacy.Core.World
{
    public interface ILevel
    {
        MapRenderer MapRenderer { get; }
        MapOffsetRenderer MapOffsetRenderer { get; }

        /// <summary>
        /// The Terrain of this level.
        /// </summary>
        ITerrain Terrain { get; }

        /// <summary>
        /// A RouteSign on the top left corner of the screen to display the map's name.
        /// </summary>
        RouteSign RouteSign { get; }

        /// <summary>
        /// Wether the map is dark, and needs to be lightened up by Flash.
        /// </summary>
        bool IsDark { get; set; }

        /// <summary>
        /// Indicates wether the player is surfing.
        /// </summary>
        bool IsSurfing { get; set; }

        /// <summary>
        /// Indicates wether the player is riding.
        /// </summary>
        bool IsRiding { get; set; }

        /// <summary>
        /// Indicates wether the player used Strength already.
        /// </summary>
        bool UsedStrength { get; set; }

        /// <summary>
        /// The reference to the active OwnPlayer instance.
        /// </summary>
        BaseOwnPlayer OwnPlayer { get; set; }

        /// <summary>
        /// The reference to the active OverworldPokemon instance.
        /// </summary>
        BaseOverworldPokemon OverworldPokemon { get; set; }

        /// <summary>
        /// The array of entities composing the map.
        /// </summary>
        List<Entity> Entities { get; }

        /// <summary>
        /// The array of floors the player can move on.
        /// </summary>
        List<Entity> Floors { get; }

        /// <summary>
        /// The array of shaders that add specific lighting to the map.
        /// </summary>
        List<IShader> Shaders { get; }
        
        /// <summary>
        /// The array of players on the server to render.
        /// </summary>
        List<BaseNetworkPlayer> NetworkPlayers { get; set; }

        /// <summary>
        /// The array of Pokémon on the server to render.
        /// </summary>
        List<BaseNetworkPokemon> NetworkPokemon { get; set; }

        /// <summary>
        /// The array of entities the offset maps are composed of.
        /// </summary>
        List<Entity> OffsetmapEntities { get; }

        /// <summary>
        /// The array of floors the offset maps are composed of.
        /// </summary>
        List<Entity> OffsetmapFloors { get; }

        /// <summary>
        /// The name of the current map.
        /// </summary>
        /// <remarks>This name gets displayed on the RouteSign.</remarks>
        string MapName { get; set; }

        /// <summary>
        /// The default background music for this level.
        /// </summary>
        /// <remarks>Doesn't play for surfing, riding and radio.</remarks>
        string MusicLoop { get; set; }

        /// <summary>
        /// The file this level got loaded from.
        /// </summary>
        /// <remarks>The path is relative to the \maps\ or \GameMode\[gamemode]\maps\ path.</remarks>
        string LevelFile { get; set; }

        /// <summary>
        /// Determines wether the player can use Ride on this map.
        /// </summary>
        bool CanRide { get; }

        /// <summary>
        /// Wether the player can move based on the entity around him.
        /// </summary>
        bool CanMove { get; }

        /// <summary>
        /// Wether the player can use the move Teleport.
        /// </summary>
        bool CanTeleport { get; set; }

        /// <summary>
        /// Wether the player can use the move Dig or an Escape Rope.
        /// </summary>
        bool CanDig { get; set; }

        /// <summary>
        /// Wether the player can use the move Fly.
        /// </summary>
        bool CanFly { get; set; }

        /// <summary>
        /// The type of Ride the player can use on this map.
        /// </summary>
        /// <remarks>0 = Depends on CanDig and CanFly, 1 = True, 2 = False</remarks>
        int RideType { get; set; }

        /// <summary>
        /// The Weather on this map.
        /// </summary>
        /// <remarks>For the weather, look at the WeatherTypes enumeration in World.vb</remarks>
        int WeatherType { get; set; }

        /// <summary>
        /// The environment type for this map.
        /// </summary>
        int EnvironmentType { get; set; }

        /// <summary>
        /// Wether the player can encounter wild Pokémon in the Grass entities.
        /// </summary>
        bool WildPokemonGrass { get; set; }

        /// <summary>
        /// Wether the player can encounter wild Pokémon on every floor tile.
        /// </summary>
        bool WildPokemonFloor { get; set; }

        /// <summary>
        /// Wether the player can encounter wild Pokémon while surfing.
        /// </summary>
        bool WildPokemonWater { get; set; }

        /// <summary>
        /// Wether the Overworld Pokémon is visible.
        /// </summary>
        bool ShowOverworldPokemon { get; set; }

        /// <summary>
        /// The amount of walked steps on this map.
        /// </summary>
        int WalkedSteps { get; set; }

        /// <summary>
        /// The region this map is assigned to.
        /// </summary>
        /// <remarks>The default is "Johto".</remarks>
        string CurrentRegion { get; set; }

        /// <summary>
        /// Chance of a Hidden Ability being on a wild pokemon.
        /// </summary>
        int HiddenAbilityChance { get; set; }

        /// <summary>
        /// The LightingType of this map. More information in the Level\UpdateLighting.
        /// </summary>
        int LightingType { get; set; }

        /// <summary>
        /// Wether the map is a part of the SafariZone. This changes the Battle Menu and the MenuScreen.
        /// </summary>
        bool IsSafariZone { get; set; }

        /// <summary>
        /// Wether the map is a part of the BugCatchingContest. This changes the Battle Menu and the MenuScreen.
        /// </summary>
        bool IsBugCatchingContest { get; set; }

        /// <summary>
        /// Holds data for the Bug Catching Contest.
        /// </summary>
        /// <remarks>Composed of 3 values, separated by ",": 0 = script location for ending the contest, 1 = script location for selecting the remaining balls item, 2 = Menu Item name for the remaining balls item.</remarks>
        string BugCatchingContestData { get; set; }

        /// <summary>
        /// Used to modify the Battle Map camera position.
        /// </summary>
        /// <remarks>Data: MapName,x,y,z OR Mapname OR x,y,z OR empty</remarks>
        string BattleMapData { get; set; }

        /// <summary>
        /// Used to modify the Battle Map.
        /// </summary>
        /// <remarks>Data: MapName,x,y,z OR Mapname OR empty</remarks>
        string SurfingBattleMapData { get; set; }

        /// <summary>
        /// The instance of the World class, handling time, season and weather based operations.
        /// </summary>
        BaseWorld World { get; set; }

        /// <summary>
        /// Wether the radio is currently activated.
        /// </summary>
        bool IsRadioOn { get; set; }

        /// <summary>
        /// The currently selected radio station. If possible, this will replace the MusicLoop.
        /// </summary>
        IRadioStation SelectedRadioStation { get; set; }

        /// <summary>
        /// Allowed radio channels on this map.
        /// </summary>
        List<decimal> AllowedRadioChannels { get; set; }

        /// <summary>
        /// Handles wild Pokémon encounters.
        /// </summary>
        IPokemonEncounter PokemonEncounter { get; }

        /// <summary>
        /// The backdrop renderer of this level.
        /// </summary>
        IBackdropRenderer BackdropRenderer { get; }

        PokemonEcounterDataStruct PokemonEncounterData { get; set; }
        WarpDataStruct WarpData { get; set; }


        /// <summary>
        /// Loads a level from a levelfile.
        /// </summary>
        /// <param name="Levelpath">The path to load the level from. Start with "|" to prevent loading a levelfile.</param>
        void Load(string Levelpath);

        /// <summary>
        /// Renders the level.
        /// </summary>
        void Draw();

        /// <summary>
        /// Updates the level's logic.
        /// </summary>
        void Update();

        /// <summary>
        /// Updates all entities on the map and offset map and sorts the enumarations.
        /// </summary>
        void UpdateEntities();

        /// <summary>
        /// Sorts the entity enumerations.
        /// </summary>
        void SortEntities();

        /// <summary>
        /// Sorts and updates offset map entities.
        /// </summary>
        void UpdateOffsetMap();

        /// <summary>
        /// Sorts the offset map entity enumerations.
        /// </summary>
        void SortOffsetEntities();

        /// <summary>
        /// Returns a list of all NPCs on the map.
        /// </summary>
        List<BaseNPC> GetNPCs();

        /// <summary>
        /// Returns an NPC based on their ID.
        /// </summary>
        /// <param name="ID">The ID of the NPC to return from the level.</param>
        /// <returns>Returns either a matching NPC or Nothing.</returns>
        BaseNPC GetNPC(int ID);

        /// <summary>
        /// Returns an NPC based on the entity ID.
        /// </summary>
        Entity GetEntity(int ID);

        /// <summary>
        /// Checks all NPCs on the map for if the player is in their line of sight.
        /// </summary>
        void CheckTrainerSights();
    }
}
