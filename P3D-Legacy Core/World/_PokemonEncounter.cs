using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using P3D.Legacy.Core.Input;
using P3D.Legacy.Core.Pokemon;
using P3D.Legacy.Core.Resources;
using P3D.Legacy.Core.Screens;

namespace P3D.Legacy.Core.World
{
    /* After BattleSystem
    /// <summary>
    /// A class to handle wild Pokémon encounters.
    /// </summary>
    public class PokemonEncounter
    {
        #region "Fields and Constants"

        //Stores a reference to the level instance.
        private ILevel _levelReference;

        #endregion

        #region "Constructors"

        /// <summary>
        /// Creates a new instance of the PokemonEncounter class.
        /// </summary>
        /// <param name="levelReference">The reference to the level instance.</param>
        public PokemonEncounter(ILevel levelReference)
        {
            _levelReference = levelReference;
        }

        #endregion

        #region "Methods"

        /// <summary>
        /// Checks if the player should encounter a wild Pokémon.
        /// </summary>
        /// <param name="Position">The position the encounter should happen.</param>
        /// <param name="Method">The method of the encounter.</param>
        /// <param name="pokeFile">The source .poke file. If left empty, the game will assume the levelfile as source .poke file.</param>
        public void TryEncounterWildPokemon(Vector3 Position, EncounterMethods Method, string pokeFile)
        {
            var _with1 = this._levelReference;

            //Only after walking 3 steps, try to encounter a wild Pokémon.
            if (_with1.WalkedSteps > 3)
            {
                //Compose the correct .poke file from the levelfile, if the pokeFile parameter is empty.
                if (string.IsNullOrEmpty(pokeFile))
                {
                    pokeFile = _with1.LevelFile.Remove(_with1.LevelFile.Length - 4, 4) + ".poke";
                }

                //Only try to register a wild battle if the .poke file exists.
                if (System.IO.File.Exists(GameModeManager.GetPokeFilePath(pokeFile)) == true)
                {
                    int startRandomValue = 12;
                    int minRandomValue = 5;

                    if (Core.Player.Pokemons.Count > 0)
                    {
                        var p = Core.Player.Pokemons[0];

                        //Arena Trap/Illuminate/No Guard/Swarm ability:
                        if (p.Ability.Name.ToLower() == "arena trap" || p.Ability.Name.ToLower() == "illuminate" || p.Ability.Name.ToLower() == "no guard" || p.Ability.Name.ToLower() == "swarm")
                        {
                            startRandomValue = 6;
                            minRandomValue = 3;
                        }

                        //Intimidate/Keen Eye/Quick Feet/Stench/White Smoke ability:
                        if (p.Ability.Name.ToLower() == "intimidate" || p.Ability.Name.ToLower() == "keen eye" || p.Ability.Name.ToLower() == "quick feet" || p.Ability.Name.ToLower() == "stench" || p.Ability.Name.ToLower() == "white smoke")
                        {
                            startRandomValue = 24;
                            minRandomValue = 10;
                        }

                        //Sand Veil ability:
                        if (_with1.WeatherType == 7 && p.Ability.Name.ToLower() == "sand veil")
                        {
                            if (Core.Random.Next(0, 100) < 50)
                            {
                                return;
                            }
                        }

                        //Snow Cloak ability:
                        if (p.Ability.Name.ToLower() == "snow cloak")
                        {
                            if (_with1.WeatherType == 2 || _with1.WeatherType == 9)
                            {
                                if (Core.Random.Next(0, 100) < 50)
                                {
                                    return;
                                }
                            }
                        }
                    }

                    //Do some shit to determine if the wild Pokémon will be met or not:
                    int randomValue = startRandomValue - _with1.WalkedSteps;
                    randomValue = Convert.ToInt32(MathHelper.Clamp(randomValue, minRandomValue, startRandomValue));

                    if (Core.Random.Next(0, randomValue * 2) == 0)
                    {
                        //Don't encounter a Pokémon if the left ctrl key is held down.
                        if (GameController.IS_DEBUG_ACTIVE == true || Core.Player.SandBoxMode == true)
                        {
                            if (KeyBoardHandler.KeyDown(Keys.LeftControl) == true)
                            {
                                return;
                            }
                        }

                        //Reset walked steps and set the wild Pokémon data:
                        _with1.WalkedSteps = 0;

                        _with1.PokemonEncounterData.Position = Position;
                        _with1.PokemonEncounterData.EncounteredPokemon = true;
                        _with1.PokemonEncounterData.Method = Method;
                        _with1.PokemonEncounterData.PokeFile = pokeFile;
                    }
                }
            }
        }

        /// <summary>
        /// Triggers a battle with a wild Pokémon if the requirements are met.
        /// </summary>
        public void TriggerBattle()
        {
            //If the encounter check is true.
            if (this._levelReference.PokemonEncounterData.EncounteredPokemon == true && Core.CurrentScreen.Identification == Screen.Identifications.OverworldScreen)
            {
                //If the player met the set position:
                if (Screen.Camera.Position.X == this._levelReference.PokemonEncounterData.Position.X && Screen.Camera.Position.Z == this._levelReference.PokemonEncounterData.Position.Z)
                {
                    //Make the player stop and set encounter check to false.
                    this._levelReference.PokemonEncounterData.EncounteredPokemon = false;
                    Screen.Camera.StopMovement();

                    //Generate new wild Pokémon:
                    var Pokemon = Spawner.GetPokemon(Screen.Level.LevelFile, this._levelReference.PokemonEncounterData.Method, true, this._levelReference.PokemonEncounterData.PokeFile);

                    if ((Pokemon != null) && ((BaseOverworldScreen)Core.CurrentScreen).TrainerEncountered == false && ((BaseOverworldScreen)Core.CurrentScreen).ActionScript.IsReady == true)
                    {
                        Screen.Level.RouteSign.Hide();
                        //When a battle starts, hide the Route sign.

                        //If the player has a repel going and the first Pokémon's level is greater than the wild Pokémon's level, don't start the battle.
                        if (Core.Player.RepelSteps > 0)
                        {
                            var p = Core.Player.GetWalkPokemon();
                            if ((p != null))
                            {
                                if (p.Level >= Pokemon.Level)
                                {
                                    return;
                                }
                            }
                        }

                        //Cleanse Tag prevents wild Pokémon if held by first Pokémon in party.
                        if (Core.Player.Pokemons[0].Level >= Pokemon.Level)
                        {
                            if ((Core.Player.Pokemons[0].Item != null))
                            {
                                if (Core.Player.Pokemons[0].Item.Id == 94)
                                {
                                    if (Core.Random.Next(0, 3) == 0)
                                    {
                                        return;
                                    }
                                }
                            }
                        }

                        //Pure Incense Lowers the chance of encountering wild Pokémon if held by first Pokémon in party.
                        if (Core.Player.Pokemons[0].Level >= Pokemon.Level)
                        {
                            if ((Core.Player.Pokemons[0].Item != null))
                            {
                                if (Core.Player.Pokemons[0].Item.Id == 291)
                                {
                                    if (Core.Random.Next(0, 3) == 0)
                                    {
                                        return;
                                    }
                                }
                            }
                        }

                        //Register wild Pokémon in the Pokédex.
                        Core.Player.PokedexData = BasePokedex.ChangeEntry(Core.Player.PokedexData, Pokemon.Number, 1);

                        //Determine wild intro type. If it's a roaming battle, set 12.
                        int introType = Core.Random.Next(0, 10);
                        if (BattleSystem.BattleScreen.RoamingBattle == true)
                        {
                            introType = 12;
                        }

                        BattleSystem.BattleScreen b = new BattleSystem.BattleScreen(Pokemon, Core.CurrentScreen, this._levelReference.PokemonEncounterData.Method);
                        Core.SetScreen(new BattleIntroScreen(Core.CurrentScreen, b, introType));
                    }
                }
            }
        }

        #endregion

    }
    */
}
