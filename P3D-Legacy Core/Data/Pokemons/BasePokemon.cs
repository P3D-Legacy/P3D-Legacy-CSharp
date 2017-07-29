using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

using P3D.Legacy.Core.Extensions;
using P3D.Legacy.Core.Pokemon.Resource;
using P3D.Legacy.Core.Resources;
using P3D.Legacy.Core.Resources.Managers;
using P3D.Legacy.Core.Resources.Managers.Sound;
using P3D.Legacy.Core.Screens;

namespace P3D.Legacy.Core.Pokemon
{
    public abstract class BasePokemon
    {
        public abstract class PokemonManager
        {
            public abstract BasePokemon GetPokemonByID(int ID);
            public abstract BasePokemon GetPokemonByID(int ID, string additionalData);
            public abstract BasePokemon GetPokemonByData(string inputData);
        }

        private static PokemonManager _pm;
        protected static PokemonManager PM
        {
            get
            {
                if (_pm == null)
                {
                    var assembly = Assembly.GetEntryAssembly();
                    var type = assembly.GetTypes().SingleOrDefault(t => t.IsSubclassOf(typeof(PokemonManager)));
                    if (type != null)
                        _pm = Activator.CreateInstance(type) as PokemonManager;
                }

                return _pm;
            }
            set { _pm = value; }
        }



        /// <summary>
        /// Returns a new Pokémon class instance.
        /// </summary>
        /// <param name="Number">The number of the Pokémon in the national Pokédex.</param>
        public static BasePokemon GetPokemonByID(int ID) => PM.GetPokemonByID(ID);
        public static BasePokemon GetPokemonByID(int ID, string additionalData) => PM.GetPokemonByID(ID, additionalData);
        /// <summary>
        /// Returns a new Pokémon class instance defined by data.
        /// </summary>
        /// <param name="InputData">The data that defines the Pokémon.</param>
        public static BasePokemon GetPokemonByData(string inputData) => PM.GetPokemonByData(inputData);



        /// <summary>
        /// Defines which Pokémon in the default GameMode are considered "legendary".
        /// </summary>
        public static readonly int[] Legendaries =
        {
            144, 145, 146, 150, 151,
            243, 244, 245, 249, 250, 251,
            377, 378, 379, 380, 381, 382, 383, 384, 385, 386,
            480, 481, 482, 483, 484, 485, 486, 487, 488, 489, 490, 491, 492, 493, 494,
            638, 639, 640, 641, 642, 643, 644, 645, 646, 647, 648, 649,
            716, 717, 718, 719, 720, 721
        };

        #region "Events"

        public event TexturesClearedEVentHandler TexturesCleared;
        public delegate void TexturesClearedEVentHandler(object sender, EventArgs e);

        #endregion

        #region "Enums"

        /// <summary>
        /// The different experience types a Pokémon can have.
        /// </summary>
        public enum ExperienceTypes
        {
            Fast,
            MediumFast,
            MediumSlow,
            Slow
        }

        /// <summary>
        /// EggGroups a Pokémon can have to define its breeding compatibility.
        /// </summary>
        public enum EggGroups
        {
            Monster,
            Water1,
            Water2,
            Water3,
            Bug,
            Flying,
            Field,
            Fairy,
            Grass,
            Undiscovered,
            HumanLike,
            Mineral,
            Amorphous,
            Ditto,
            Dragon,
            GenderUnknown,
            None
        }

        /// <summary>
        /// Genders of a Pokémon.
        /// </summary>
        public enum Genders
        {
            Male,
            Female,
            Genderless
        }

        /// <summary>
        /// The status problems a Pokémon can have.
        /// </summary>
        public enum StatusProblems
        {
            None,
            Burn,
            Freeze,
            Paralyzed,
            Poison,
            BadPoison,
            Sleep,
            Fainted
        }

        /// <summary>
        /// The volatile status a Pokémon can have.
        /// </summary>
        public enum VolatileStatus
        {
            Confusion,
            Flinch,
            Infatuation,
            Trapped
        }

        /// <summary>
        /// Different natures of a Pokémon.
        /// </summary>
        public enum Natures
        {
            Hardy,
            Lonely,
            Brave,
            Adamant,
            Naughty,
            Bold,
            Docile,
            Relaxed,
            Impish,
            Lax,
            Timid,
            Hasty,
            Serious,
            Jolly,
            Naive,
            Modest,
            Mild,
            Quiet,
            Bashful,
            Rash,
            Calm,
            Gentle,
            Sassy,
            Careful,
            Quirky
        }

        /// <summary>
        /// Ways to change the Friendship value of a Pokémon.
        /// </summary>
        public enum FriendShipCauses
        {
            Walking,
            LevelUp,
            Fainting,
            EnergyPowder,
            HealPowder,
            EnergyRoot,
            RevivalHerb,
            Trading,
            Vitamin,
            EVBerry
        }

        #endregion

        #region "Properties"

        /// <summary>
        /// Returns the name to reference to the animation/model of this Pokémon.
        /// </summary>
        public List<int> AbilityTag = new List<int>();

        public string AnimationName => BasePokemonForms.GetAnimationName(this);

        public string AdditionalData
        {
            get { return _additionalData; }
            set
            {
                _additionalData = value;

                ClearTextures();
            }
        }

        public int Number { get; set; }

        public ExperienceTypes ExperienceType { get; set; }

        public int BaseExperience { get; set; }

        protected string Name { get; set; }

        public int CatchRate { get; set; }

        public int BaseFriendship { get; set; }

        public int BaseEggSteps { get; set; }

        public EggGroups EggGroup1 { get; set; }

        public EggGroups EggGroup2 { get; set; }

        public decimal IsMale { get; set; }

        public bool IsGenderless { get; set; }

        public int Devolution { get; set; } = 0;

        public bool CanLearnAllMachines { get; set; } = false;

        public bool CanSwim { get; set; }

        public bool CanFly { get; set; }

        public int EggPokemon { get; set; } = 0;

        public int TradeValue { get; set; } = 10;

        public bool CanBreed { get; set; } = true;

        public int Experience { get; set; }

        public Genders Gender { get; set; }

        public int EggSteps { get; set; }

        public string NickName { get; set; }

        public int Level { get; set; }

        public string OT { get; set; } = "00000";

        public StatusProblems Status { get; set; } = StatusProblems.None;

        public Natures Nature { get; set; }

        public string CatchLocation { get; set; } = "at unknown place";

        public string CatchTrainerName { get; set; } = "???";

        public string CatchMethod { get; set; } = "somehow obtained";

        public int Friendship { get; set; }

        public bool IsShiny { get; set; }

        public string IndividualValue { get; set; } = "";

        #endregion

        #region "Definition"

        #region "Base Stats"

        public int BaseHP { get; set; }
        public int BaseAttack { get; set; }
        public int BaseDefense { get; set; }
        public int BaseSpAttack { get; set; }
        public int BaseSpDefense { get; set; }
        public int BaseSpeed { get; set; }

        #endregion

        #region "GiveEVStats"

        public int GiveEVHP { get; set; }
        public int GiveEVAttack { get; set; }
        public int GiveEVDefense { get; set; }
        public int GiveEVSpAttack { get; set; }
        public int GiveEVSpDefense { get; set; }
        public int GiveEVSpeed { get; set; }

        #endregion

        public Element Type1;
        public Element Type2;
        public Dictionary<Item, int> StartItems = new Dictionary<Item, int>();
        public Dictionary<int, BaseAttack> AttackLearns = new Dictionary<int, BaseAttack>();
        public List<int> EggMoves = new List<int>();
        public List<BaseAttack> TutorAttacks = new List<BaseAttack>();
        public List<EvolutionCondition> EvolutionConditions = new List<EvolutionCondition>();
        public List<BaseAbility> NewAbilities = new List<BaseAbility>();
        public BaseAbility HiddenAbility = null;
        public List<int> Machines = new List<int>();
        public PokedexEntry PokedexEntry;
        public SoundEffect Cry;

        public Dictionary<int, int> WildItems = new Dictionary<int, int>();

        #endregion

        #region "SavedStats"

        #region "Stats"

        /// <summary>
        /// The HP of this Pokémon.
        /// </summary>
        public int HP { get; set; }

        /// <summary>
        /// The maximal HP of this Pokémon.
        /// </summary>
        public int MaxHP { get; set; }

        /// <summary>
        /// The Attack of this Pokémon.
        /// </summary>
        public int Attack { get; set; }

        /// <summary>
        /// The Defense of this Pokémon.
        /// </summary>
        public int Defense { get; set; }

        /// <summary>
        /// The Special Attack of this Pokémon.
        /// </summary>
        public int SpAttack { get; set; }

        /// <summary>
        /// The Special Defense of this Pokémon.
        /// </summary>
        public int SpDefense { get; set; }

        /// <summary>
        /// The Speed of this Pokémon.
        /// </summary>
        public int Speed { get; set; }

        #endregion

        #region "EVStats"

        private int _EVHP;
        /// <summary>
        /// The HP EV this Pokémon got.
        /// </summary>
        public int EVHP
        {
            get { return _EVHP; }
            set
            {
                _EVHP = value;

                CalculateStats();
            }
        }

        private int _EVAttack;
        /// <summary>
        /// The Attack EV this Pokémon got.
        /// </summary>
        public int EVAttack
        {
            get { return _EVAttack; }
            set
            {
                _EVAttack = value;

                CalculateStats();
            }
        }

        private int _EVDefense;
        /// <summary>
        /// The Defense EV this Pokémon got.
        /// </summary>
        public int EVDefense
        {
            get { return _EVDefense; }
            set
            {
                _EVDefense = value;

                CalculateStats();
            }
        }

        private int _EVSpAttack;
        /// <summary>
        /// The Special Attack EV this Pokémon got.
        /// </summary>
        public int EVSpAttack
        {
            get { return _EVSpAttack; }
            set
            {
                _EVSpAttack = value;

                CalculateStats();
            }
        }

        private int _EVSpDefense;
        /// <summary>
        /// The Special Defense EV this Pokémon got.
        /// </summary>
        public int EVSpDefense
        {
            get { return _EVSpDefense; }
            set
            {
                _EVSpDefense = value;

                CalculateStats();
            }
        }

        private int _EVSpeed;
        /// <summary>
        /// The Speed EV this Pokémon got.
        /// </summary>
        public int EVSpeed
        {
            get { return _EVSpeed; }
            set
            {
                _EVSpeed = value;

                CalculateStats();
            }
        }

        #endregion

        #region "IVStats"

        /// <summary>
        /// The HP IV this Pokémon got.
        /// </summary>
        public int IVHP { get; set; }

        /// <summary>
        /// The Attack IV this Pokémon got.
        /// </summary>
        public int IVAttack { get; set; }

        /// <summary>
        /// The Defense IV this Pokémon got.
        /// </summary>
        public int IVDefense { get; set; }

        /// <summary>
        /// The Special Attack IV this Pokémon got.
        /// </summary>
        public int IVSpAttack { get; set; }

        /// <summary>
        /// The Special Defense IV this Pokémon got.
        /// </summary>
        public int IVSpDefense { get; set; }

        /// <summary>
        /// The Speed IV this Pokémon got.
        /// </summary>
        public int IVSpeed { get; set; }

        #endregion

        private string _additionalData = "";

        private Item _item;
        public Item Item
        {
            get { return _item; }
            set
            {
                _item = value;

                ClearTextures();
            }
        }

        public List<BaseAttack> Attacks = new List<BaseAttack>();
        public BaseAbility Ability;

        public Item CatchBall { get; set; } = Item.GetItemByID(5);

        #endregion

        #region "Temp"

        protected List<VolatileStatus> Volatiles { get; } = new List<VolatileStatus>();

        /// <summary>
        /// Returns if this Pokémon is affected by a Volatile Status effect.
        /// </summary>
        /// <param name="VolatileStatus">The Volatile Status effect to test for.</param>
        public bool HasVolatileStatus(VolatileStatus VolatileStatus) => Volatiles.Contains(VolatileStatus);

        /// <summary>
        /// Affects this Pokémon with a Volatile Status.
        /// </summary>
        /// <param name="VolatileStatus">The Volatile Status to affect this Pokémon with.</param>
        public void AddVolatileStatus(VolatileStatus VolatileStatus)
        {
            if (!Volatiles.Contains(VolatileStatus))
                Volatiles.Add(VolatileStatus);
        }

        /// <summary>
        /// Removes a Volatile Status effect this Pokémon is affected by.
        /// </summary>
        /// <param name="VolatileStatus">The Volatile Status effect to remove.</param>
        public void RemoveVolatileStatus(VolatileStatus VolatileStatus)
        {
            if (Volatiles.Contains(VolatileStatus))
                Volatiles.Remove(VolatileStatus);
        }

        /// <summary>
        /// Clears all Volatile Status effects affecting this Pokémon.
        /// </summary>
        public void ClearAllVolatiles() => Volatiles.Clear();

        public int StatAttack = 0;
        public int StatDefense = 0;
        public int StatSpAttack = 0;
        public int StatSpDefense = 0;
        public int StatSpeed = 0;

        public int Accuracy = 0;
        public int Evasion = 0;

        public bool HasLeveledUp = false;

        public int SleepTurns = -1;
        public int ConfusionTurns = -1;

        public BaseAttack LastHitByMove;
        public int LastDamageReceived = 0;
        public int LastHitPhysical = -1;

        #endregion

        #region "OriginalStats"

        //Original Stats store the stats that the Pokémon has by default. When they get overwritten (for example by Dittos Transform move), the original values get stored in the "Original_X" value.
        //All these properties ensure that no part of the original Pokémon gets overwritten once the original value got set into place.

        /// <summary>
        /// The Pokémon's original primary type.
        /// </summary>
        public Element OriginalType1
        {
            get { return _originalType1; }
            set
            {
                if (_originalType1 == null)
                    _originalType1 = value;
            }
        }

        /// <summary>
        /// The Pokémon's original secondary type.
        /// </summary>
        public Element OriginalType2
        {
            get { return _originalType2; }
            set
            {
                if (_originalType2 == null)
                    _originalType2 = value;
            }
        }

        /// <summary>
        /// The Pokémon's original national Pokédex number.
        /// </summary>
        public int OriginalNumber
        {
            get { return _originalNumber; }
            set
            {
                if (_originalNumber == -1)
                    _originalNumber = value;
            }
        }

        /// <summary>
        /// The Pokémon's original shiny state.
        /// </summary>
        public int OriginalShiny
        {
            get { return _originalShiny; }
            set
            {
                if (_originalShiny == -1)
                    _originalShiny = value;
            }
        }

        /// <summary>
        /// The Pokémon's original stats.
        /// </summary>
        public int[] OriginalStats
        {
            get { return _originalStats; }
            set
            {
                if (ReferenceEquals(_originalStats, new [] { -1, -1, -1, -1, -1 })) // TODO: Dafuq
                    _originalStats = value;
            }
        }

        /// <summary>
        /// The Pokémon's original ability.
        /// </summary>
        public BaseAbility OriginalAbility
        {
            get { return _originalAbility; }
            set
            {
                if (_originalAbility == null)
                    _originalAbility = value;
            }
        }

        /// <summary>
        /// The Pokémon's original hold item.
        /// </summary>
        public Item OriginalItem
        {
            get { return _originalItem; }
            set
            {
                if (_originalItem == null)
                    _originalItem = value;
            }
        }

        /// <summary>
        /// The Pokémon's original moveset.
        /// </summary>
        public List<BaseAttack> OriginalMoves
        {
            get { return _originalMoves; }
            set
            {
                if (_originalMoves == null)
                    _originalMoves = value;
            }
        }

        /// <summary>
        /// If this Pokémon has been using the Transform move (or any other move/ability that causes similar effects).
        /// </summary>
        public bool IsTransformed { get; set; } = false;

        protected Element _originalType1;
        protected Element _originalType2;

        protected int _originalNumber = -1;
        protected int[] _originalStats = { -1, -1, -1, -1, -1 };
        protected int _originalShiny = -1;
        protected List<BaseAttack> _originalMoves;

        protected BaseAbility _originalAbility;

        protected Item _originalItem;

        #endregion

        protected List<Texture2D> Textures = new List<Texture2D>();
        protected Vector3 Scale = new Vector3(1);

        /// <summary>
        /// Empties the cached textures.
        /// </summary>
        protected void ClearTextures()
        {
            Textures.Clear();
            Textures.AddRange(new Texture2D[]{ null, null, null, null, null, null, null, null, null, null });
            TexturesCleared?.Invoke(this, new EventArgs());
        }
        
        /// <summary>
        /// Resets the temp storages of the Pokémon.
        /// </summary>
        public abstract void ResetTemp();

        public abstract void LoadAltAbility();

        public abstract void RestoreAbility();

        /// <summary>
        /// Loads definition data from the data files and empties the temp textures.
        /// </summary>
        public abstract void ReloadDefinitions();

        /// <summary>
        /// Loads definition data from the data file.
        /// </summary>
        /// <param name="number">The number of the Pokémon in the national Pokédex.</param>
        /// <param name="additionalData">The additional data.</param>
        public abstract void LoadDefinitions(int number, string additionalData);

        /// <summary>
        /// Applies data to the Pokémon.
        /// </summary>
        /// <param name="inputData">The input data.</param>
        public abstract void LoadData(string inputData);

        /// <summary>
        /// Returns the save data from the Pokémon.
        /// </summary>
        public abstract string GetSaveData();

        /// <summary>
        /// Generates a Pokémon's initial values.
        /// </summary>
        /// <param name="newLevel">The level to set the Pokémon's level to.</param>
        /// <param name="setParameters">If the parameters like Nature and Ability should be set. Otherwise, it just loads the attacks and sets the level.</param>
        public abstract void Generate(int newLevel, bool setParameters);

        #region "Converters"

        /// <summary>
        /// Converts an EggGroup ID string to the EggGroup enum item.
        /// </summary>
        /// <param name="ID">The ID string.</param>
        public static EggGroups ConvertIDToEggGroup(string ID)
        {
            switch (ID.ToLower())
            {
                case "monster":
                    return EggGroups.Monster;
                case "water1":
                    return EggGroups.Water1;
                case "water2":
                    return EggGroups.Water2;
                case "water3":
                    return EggGroups.Water3;
                case "bug":
                    return EggGroups.Bug;
                case "flying":
                    return EggGroups.Flying;
                case "field":
                    return EggGroups.Field;
                case "fairy":
                    return EggGroups.Fairy;
                case "grass":
                    return EggGroups.Grass;
                case "undiscovered":
                    return EggGroups.Undiscovered;
                case "humanlike":
                    return EggGroups.HumanLike;
                case "mineral":
                    return EggGroups.Mineral;
                case "amorphous":
                    return EggGroups.Amorphous;
                case "ditto":
                    return EggGroups.Ditto;
                case "dragon":
                    return EggGroups.Dragon;
                case "genderunknown":
                    return EggGroups.GenderUnknown;
                case "none":
                case "":
                case "0":
                case "nothing":
                    return EggGroups.None;
            }

            return EggGroups.None;
        }

        /// <summary>
        /// Converts a Nature ID to a Nature enum item.
        /// </summary>
        /// <param name="ID">The nature ID.</param>
        public static Natures ConvertIDToNature(int ID)
        {
            switch (ID)
            {
                case 0:
                    return Natures.Hardy;
                case 1:
                    return Natures.Lonely;
                case 2:
                    return Natures.Brave;
                case 3:
                    return Natures.Adamant;
                case 4:
                    return Natures.Naughty;
                case 5:
                    return Natures.Bold;
                case 6:
                    return Natures.Docile;
                case 7:
                    return Natures.Relaxed;
                case 8:
                    return Natures.Impish;
                case 9:
                    return Natures.Lax;
                case 10:
                    return Natures.Timid;
                case 11:
                    return Natures.Hasty;
                case 12:
                    return Natures.Serious;
                case 13:
                    return Natures.Jolly;
                case 14:
                    return Natures.Naive;
                case 15:
                    return Natures.Modest;
                case 16:
                    return Natures.Mild;
                case 17:
                    return Natures.Quiet;
                case 18:
                    return Natures.Bashful;
                case 19:
                    return Natures.Rash;
                case 20:
                    return Natures.Calm;
                case 21:
                    return Natures.Gentle;
                case 22:
                    return Natures.Sassy;
                case 23:
                    return Natures.Careful;
                case 24:
                    return Natures.Quirky;
                default:
                    return Natures.Hardy;
            }
        }

        #endregion
        
        /// <summary>
        /// Generates a new individual value for this Pokémon.
        /// </summary>
        protected void GenerateIndividualValue()
        {
            string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

            string s = "";
            for (var x = 0; x <= 10; x++)
                s += chars[Core.Random.Next(0, chars.Length)].ToString(NumberFormatInfo.InvariantInfo);

            IndividualValue = s;
        }

        /// <summary>
        /// Returns the Display Name of this Pokémon.
        /// </summary>
        /// <remarks>Returns "Egg" when the Pokémon is in an egg. Returns the properly translated name if it exists. Returns the nickname if set.</remarks>
        public string GetDisplayName()
        {
            if (EggSteps > 0)
            {
                return "Egg";
            }
            else
            {
                if (string.IsNullOrEmpty(NickName))
                {
                    if (Localization.TokenExists("pokemon_name_" + Name))
                    {
                        return Localization.GetString("pokemon_name_" + Name);
                    }
                    else
                    {
                        return Name;
                    }
                }
                else
                {
                    return NickName;
                }
            }
        }

        /// <summary>
        /// Returns the properly translated name of a Pokémon if defined in the language files.
        /// </summary>
        public string GetName() => Localization.TokenExists("pokemon_name_" + Name) ? Localization.GetString("pokemon_name_" + Name) : Name;

        /// <summary>
        /// Returns the English name of the Pokémon.
        /// </summary>
        public string OriginalName
        {
            get { return Name; }
            set { Name = value; }
        }

        #region "Experience, Level Up and Stats"

        /// <summary>
        /// Gives the Pokémon experience points and levels it up.
        /// </summary>
        /// <param name="Exp">The amount of EXP.</param>
        /// <param name="LearnRandomAttack">If the Pokémon should learn an attack if it could learn one at level up.</param>
        public void GetExperience(int Exp, bool LearnRandomAttack)
        {
            Experience += Exp;
            while (Experience >= NeedExperience(Level + 1))
            {
                LevelUp(LearnRandomAttack);
            }
            Level = Level.Clamp(1, GameModeManager.GetActiveGameRuleValueOrDefault("MaxLevel", 100));
        }

        /// <summary>
        /// Rasies the Pokémon's level by one.
        /// </summary>
        /// <param name="LearnRandomAttack">If one attack of the Pokémon should be replaced by an attack potentially learned on the new level.</param>
        public void LevelUp(bool LearnRandomAttack)
        {
            Level += 1;

            int currentMaxHP = MaxHP;

            CalculateStats();

            //Heals the Pokémon by the HP difference.
            int HPDifference = MaxHP - currentMaxHP;
            if (HPDifference > 0)
            {
                Heal(HPDifference);
            }

            if (LearnRandomAttack)
            {
                LearnAttack(Level);
            }
        }

        /// <summary>
        /// Recalculates all stats for this Pokémon using its current EVs, IVs and level.
        /// </summary>
        /// 
        public void CalculateStatsBarSpeed()
        {
            MaxHP = CalcStatus(Level, true, BaseHP, EVHP, IVHP, "HP");
            Attack = CalcStatus(Level, false, BaseAttack, EVAttack, IVAttack, "Attack");
            Defense = CalcStatus(Level, false, BaseDefense, EVDefense, IVDefense, "Defense");
            SpAttack = CalcStatus(Level, false, BaseSpAttack, EVSpAttack, IVSpAttack, "SpAttack");
            SpDefense = CalcStatus(Level, false, BaseSpDefense, EVSpDefense, IVSpDefense, "SpDefense");
        }

        public void CalculateStats()
        {
            CalculateStatsBarSpeed();
            Speed = CalcStatus(Level, false, BaseSpeed, EVSpeed, IVSpeed, "Speed");
        }

        /// <summary>
        /// Gets the value of a status.
        /// </summary>
        /// <param name="calcLevel">The level of the Pokémon.</param>
        /// <param name="DoHP">If the requested stat is HP.</param>
        /// <param name="baseStat">The base stat of the Pokémon.</param>
        /// <param name="EVStat">The EV stat of the Pokémon.</param>
        /// <param name="IVStat">The IV stat of the Pokémon.</param>
        /// <param name="StatName">The name of the stat.</param>
        private int CalcStatus(int calcLevel, bool DoHP, int baseStat, int EVStat, int IVStat, string StatName)
        {
            if (DoHP)
            {
                if (Number == 292)
                {
                    return 1;
                }
                else
                {
                    return Convert.ToInt32(Math.Floor((double) (((IVStat + (2 * baseStat) + (EVStat / 4) + 100) * calcLevel) / 100) + 10));
                }
            }
            else
            {
                return Convert.ToInt32(Math.Floor(((((IVStat + (2 * baseStat) + (EVStat / 4)) * calcLevel) / 100) + 5) * Pokemon.Nature.GetMultiplier(Nature, StatName)));
            }
        }

        /// <summary>
        /// Replaces a random move of the Pokémon by one that it learns on a given level.
        /// </summary>
        /// <param name="learnLevel">The level the Pokémon learns the desired move on.</param>
        public void LearnAttack(int learnLevel)
        {
            if (AttackLearns.ContainsKey(learnLevel))
            {
                var a = AttackLearns[learnLevel];

                if (Attacks.Any(la => la.Id == a.Id))
                    return; //Pokémon already knows that attack.

                Attacks.Add(a);

                if (Attacks.Count == 5)
                    Attacks.RemoveAt(Core.Random.Next(0, 5));
            }
        }

        /// <summary>
        /// Returns the EXP needed for the given level.
        /// </summary>
        /// <param name="EXPLevel">The level this function should return the exp amount for.</param>
        public int NeedExperience(int EXPLevel)
        {
            int n = EXPLevel;
            int i = 0;

            switch (ExperienceType)
            {
                case ExperienceTypes.Fast:
                    i = Convert.ToInt32((4 * n * n * n) / 5);
                    break;
                case ExperienceTypes.MediumFast:
                    i = Convert.ToInt32(n * n * n);
                    break;
                case ExperienceTypes.MediumSlow:
                    i = Convert.ToInt32(((6 * n * n * n) / 5) - (15 * n * n) + (100 * n) - 140);
                    break;
                case ExperienceTypes.Slow:
                    i = Convert.ToInt32((5 * n * n * n) / 4);
                    break;
                default:
                    i = Convert.ToInt32(n * n * n);
                    break;
            }

            if (i < 0)
            {
                i = 0;
            }

            return i;
        }

        /// <summary>
        /// Returns the cummulative PP count of all moves.
        /// </summary>
        public int CountPp() => Attacks.Sum(attack => attack.CurrentPp);

        /// <summary>
        /// Fully heals this Pokémon.
        /// </summary>
        public void FullRestore()
        {
            Status = StatusProblems.None;
            Heal(MaxHP);
            Volatiles.Clear();
            if (Attacks.Count > 0)
            {
                for (var d = 0; d <= Attacks.Count - 1; d++)
                {
                    Attacks[d].CurrentPp = Attacks[d].MaxPp;
                }
            }
        }

        /// <summary>
        /// Heals the Pokémon.
        /// </summary>
        /// <param name="HealHP">The amount of HP to heal the Pokémon by.</param>
        public void Heal(int HealHP) => HP = (HP + HealHP).Clamp(0, MaxHP);

        /// <summary>
        /// Changes the Friendship value of this Pokémon.
        /// </summary>
        /// <param name="cause">The cause as to why the Friendship value should change.</param>
        public void ChangeFriendShip(FriendShipCauses cause)
        {
            int @add = 0;

            switch (cause)
            {
                case FriendShipCauses.Walking:
                    @add = 1;
                    break;
                case FriendShipCauses.LevelUp:
                    if (Friendship <= 99)
                    {
                        @add = 5;
                    }
                    else if (Friendship > 99 && Friendship <= 199)
                    {
                        @add = 3;
                    }
                    else
                    {
                        @add = 2;
                    }
                    break;
                case FriendShipCauses.Fainting:
                    Friendship -= 1;
                    break;
                case FriendShipCauses.EnergyPowder:
                case FriendShipCauses.HealPowder:
                    if (Friendship <= 99)
                    {
                        @add = -5;
                    }
                    else if (Friendship > 99 && Friendship <= 199)
                    {
                        @add = -5;
                    }
                    else
                    {
                        @add = -10;
                    }
                    break;
                case FriendShipCauses.EnergyRoot:
                    if (Friendship <= 99)
                    {
                        @add = -10;
                    }
                    else if (Friendship > 99 && Friendship <= 199)
                    {
                        @add = -10;
                    }
                    else
                    {
                        @add = -15;
                    }
                    break;
                case FriendShipCauses.RevivalHerb:
                    if (Friendship <= 99)
                    {
                        @add = -15;
                    }
                    else if (Friendship > 99 && Friendship <= 199)
                    {
                        @add = -15;
                    }
                    else
                    {
                        @add = -20;
                    }
                    break;
                case FriendShipCauses.Trading:
                    Friendship = BaseFriendship;
                    break;
                case FriendShipCauses.Vitamin:
                    if (Friendship <= 99)
                    {
                        @add = 5;
                    }
                    else if (Friendship > 99 && Friendship <= 199)
                    {
                        @add = 3;
                    }
                    else
                    {
                        @add = 2;
                    }
                    break;
                case FriendShipCauses.EVBerry:
                    if (Friendship <= 99)
                    {
                        @add = 10;
                    }
                    else if (Friendship > 99 && Friendship <= 199)
                    {
                        @add = 5;
                    }
                    else
                    {
                        @add = 2;
                    }
                    break;
            }

            if (@add > 0)
            {
                if (CatchBall.Id == 174)
                {
                    @add += 1;
                }
                if ((Item != null))
                {
                    if (Item.Name.ToLower() == "soothe bell")
                    {
                        @add *= 2;
                    }
                }
            }

            if (@add != 0)
            {
                Friendship += @add;
            }

            Friendship = Convert.ToInt32(MathHelper.Clamp(Friendship, 0, 255));
        }

        #endregion

        #region "Textures/Models"

        /// <summary>
        /// Returns a texture for this Pokémon.
        /// </summary>
        /// <param name="index">0=normal,front
        /// 1=normal,back
        /// 2=shiny,front
        /// 3=shiny,back
        /// 4=menu sprite
        /// 5=egg menu sprite
        /// 6=Egg front sprite
        /// 7=Egg back sprite
        /// 8=normal overworld
        /// 9=shiny overworld
        /// 10=normal,front,animation</param>
        private Texture2D GetTexture(int index)
        {
            if (Textures[index] == null)
            {
                switch (index)
                {
                    case 0:
                        Textures[index] = TextureManager.GetTexture("Pokemon|Sprites|" + AnimationName, new Microsoft.Xna.Framework.Rectangle(0, 0, 128, 128), "");
                        break;
                    case 1:
                        Textures[index] = TextureManager.GetTexture("Pokemon|Sprites|" + AnimationName, new Microsoft.Xna.Framework.Rectangle(128, 0, 128, 128), "");
                        break;
                    case 2:
                        Textures[index] = TextureManager.GetTexture("Pokemon|Sprites|" + AnimationName, new Microsoft.Xna.Framework.Rectangle(0, 128, 128, 128), "");
                        break;
                    case 3:
                        Textures[index] = TextureManager.GetTexture("Pokemon|Sprites|" + AnimationName, new Microsoft.Xna.Framework.Rectangle(128, 128, 128, 128), "");
                        break;
                    case 4:
                        Vector2 v = BasePokemonForms.GetMenuImagePosition(this);
                        Size s = BasePokemonForms.GetMenuImageSize(this);

                        string shiny = "";
                        if (IsShiny)
                        {
                            shiny = "Shiny";
                        }

                        Textures[index] = TextureManager.GetTexture("GUI|PokemonMenu" + shiny, new Microsoft.Xna.Framework.Rectangle(Convert.ToInt32(v.X) * 32, Convert.ToInt32(v.Y) * 32, s.Width, s.Height), "");
                        break;
                    case 5:
                        if (Number == 490)
                        {
                            Textures[index] = TextureManager.GetTexture("GUI|PokemonMenu", new Microsoft.Xna.Framework.Rectangle(928, 992, 32, 32), "");
                        }
                        else
                        {
                            Textures[index] = EggCreator.CreateEggSprite(this, TextureManager.GetTexture("GUI|PokemonMenu", new Microsoft.Xna.Framework.Rectangle(992, 992, 32, 32), ""), TextureManager.GetTexture("Pokemon|Egg|Templates|Menu"));
                        }
                        break;
                    case 6:
                        if (Number == 490)
                        {
                            Textures[index] = TextureManager.GetTexture("Pokemon|Egg|Egg_manaphy_front");
                        }
                        else
                        {
                            Textures[index] = EggCreator.CreateEggSprite(this, TextureManager.GetTexture("Pokemon|Egg|Egg_front"), TextureManager.GetTexture("Pokemon|Egg|Templates|Front"));
                        }
                        break;
                    case 7:
                        if (Number == 490)
                        {
                            Textures[index] = TextureManager.GetTexture("Pokemon|Egg|Egg_manaphy_back");
                        }
                        else
                        {
                            Textures[index] = EggCreator.CreateEggSprite(this, TextureManager.GetTexture("Pokemon|Egg|Egg_back"), TextureManager.GetTexture("Pokemon|Egg|Templates|Back"));
                        }
                        break;
                    case 8:
                    {
                        string addition = BasePokemonForms.GetOverworldAddition(this);
                        Textures[index] = TextureManager.GetTexture("Pokemon|Overworld|Normal|" + Number + addition);
                    }
                        break;
                    case 9:
                    {
                        string addition = BasePokemonForms.GetOverworldAddition(this);
                        Textures[index] = TextureManager.GetTexture("Pokemon|Overworld|Shiny|" + Number + addition);
                    }
                        break;
                }
            }

            return Textures[index];
        }

        /// <summary>
        /// Returns the Overworld texture of this Pokémon.
        /// </summary>
        public Texture2D GetOverworldTexture() => GetTexture(!IsShiny ? 8 : 9);

        /// <summary>
        /// Returns the Menu Texture of this Pokémon.
        /// </summary>
        /// <param name="CanGetEgg">If the texture returned can represent the Pokémon in its egg.</param>
        public Texture2D GetMenuTexture(bool CanGetEgg) => EggSteps > 0 && CanGetEgg ? GetTexture(5) : GetTexture(4);

        /// <summary>
        /// Returns the Menu Texture of this Pokémon.
        /// </summary>
        public Texture2D GetMenuTexture() => GetMenuTexture(true);

        /// <summary>
        /// Returns the display texture of this Pokémon.
        /// </summary>
        /// <param name="FrontView">If this Pokémon should be viewed from the front.</param>
        public Texture2D GetTexture(bool FrontView) => FrontView ? (IsEgg ? GetTexture(6) : GetTexture(IsShiny ? 2 : 0)) : (IsEgg ? GetTexture(7) : GetTexture(IsShiny ? 3 : 1));

        /// <summary>
        /// Returns properties to display models on a 2D GUI. Data structure: scale sng,posX sng,posY sng,posZ sng,roll sng
        /// </summary>
        public Tuple<float, float, float, float, float> GetModelProperties()
        {
            float scale = Convert.ToSingle(0.6f / PokedexEntry.Height);
            float x = 0f;
            float y = 0f;
            float z = 0f;

            float roll = 0.3f;

            switch (Number)
            {
                case 6:
                    scale = 0.55f;
                    break;
                case 9:
                    scale = 0.7f;
                    break;
                case 15:
                    z = 4f;
                    break;
                case 19:
                    scale = 1.1f;
                    break;
                case 20:
                    scale = 1.3f;
                    break;
                case 23:
                    scale = 1;
                    break;
                case 24:
                    scale = 0.5f;
                    break;
                case 41:
                    z = 5f;
                    break;
                case 55:
                    scale = 0.7f;
                    break;
                case 63:
                    z = -4f;
                    break;
                case 74:
                    scale = 0.75f;
                    break;
                case 81:
                    z = 6f;
                    break;
                case 82:
                    scale = 0.9f;
                    z = 6f;
                    break;
                case 95:
                    x = -6;
                    scale = 0.3f;
                    break;
                case 98:
                    scale = 1;
                    break;
                case 102:
                    scale = 0.9f;
                    break;
                case 103:
                    scale = 0.45f;
                    break;
                case 115:
                    scale = 0.45f;
                    break;
                case 129:
                    z = -4f;
                    break;
                case 130:
                    scale = 0.25f;
                    break;
                case 131:
                    z = -8;
                    break;
                case 143:
                    scale = 0.5f;
                    roll = 1.2f;
                    break;
                case 144:
                    z = -9;
                    scale = 0.35f;
                    break;
                case 147:
                    scale = 0.7f;
                    break;
                case 148:
                    x = 5f;
                    scale = 0.4f;
                    break;
                case 149:
                case 150:
                    scale = 0.42f;
                    break;
                case 151:
                    z = 5;
                    break;
                case 157:
                    scale = 0.6f;
                    break;
                case 160:
                    scale = 0.5f;
                    break;
                case 162:
                    scale = 0.8f;
                    break;
                case 164:
                    z = -3;
                    break;
                case 168:
                    scale = 0.8f;
                    break;
                case 180:
                    scale = 0.5f;
                    break;
                case 181:
                    scale = 0.75f;
                    break;
                case 184:
                case 185:
                    scale = 0.8f;
                    break;
                case 187:
                    scale = 0.65f;
                    break;
                case 196:
                case 197:
                    scale = 0.9f;
                    break;
                case 206:
                    scale = 0.9f;
                    break;
                case 208:
                    scale = 0.4f;
                    break;
                case 211:
                    z = 5;
                    break;
                case 212:
                    scale = 0.7f;
                    break;
                case 214:
                    scale = 1.2f;
                    break;
                case 217:
                    scale = 0.55f;
                    break;
                case 223:
                    z = -5;
                    break;
                case 229:
                    scale = 0.8f;
                    break;
                case 230:
                    scale = 0.6f;
                    z = 3;
                    break;
                case 235:
                    scale = 0.7f;
                    break;
                case 241:
                    scale = 0.7f;
                    break;
                case 247:
                    scale = 0.7f;
                    break;
                case 248:
                    scale = 0.6f;
                    break;
                case 249:
                    scale = 0.3f;
                    break;
                case 250:
                    scale = 0.2f;
                    break;
                case 336:
                    scale = 0.8f;
                    break;
            }

            return new Tuple<float, float, float, float, float>(scale, x, y, z, roll);
        }

        #endregion

        /// <summary>
        /// Checks if this Pokémon can evolve by a given EVolutionTrigger and EVolutionArgument.
        /// </summary>
        /// <param name="trigger">The trigger of the evolution.</param>
        /// <param name="argument">An argument that specifies the evolution.</param>
        public bool CanEVolve(EvolutionCondition.EvolutionTrigger trigger, string argument) => EvolutionCondition.EvolutionNumber(this, trigger, argument) != 0;

        /// <summary>
        /// Returns the evolution ID of this Pokémon.
        /// </summary>
        /// <param name="trigger">The trigger of the evolution.</param>
        /// <param name="argument">An argument that specifies the evolution.</param>
        public int GetEVolutionId(EvolutionCondition.EvolutionTrigger trigger, string argument) => EvolutionCondition.EvolutionNumber(this, trigger, argument);

        /// <summary>
        /// Sets the catch infos of the Pokémon. Uses the current map name and player name + OT.
        /// </summary>
        /// <param name="Ball">The Pokéball this Pokémon got captured in.</param>
        /// <param name="Method">The capture method.</param>
        public void SetCatchInfos(Item Ball, string Method)
        {
            CatchLocation = Screen.Level.MapName;
            CatchTrainerName = Core.Player.Name;
            OT = Core.Player.OT;

            CatchMethod = Method;
            CatchBall = Ball;
        }

        /// <summary>
        /// Checks if the Pokémon is of a certain type.
        /// </summary>
        /// <param name="CheckType">The type to check.</param>
        public bool IsType(Element.Types CheckType) => Type1.Type == CheckType || Type2.Type == CheckType;

        /// <summary>
        /// Plays the cry of this Pokémon.
        /// </summary>
        public void PlayCry()
        {
            float Pitch = 0f;
            int percent = 100;
            if (HP >= 0 && MaxHP > 0)
            {
                percent = Convert.ToInt32(Math.Ceiling((double) (HP * 100 / MaxHP)));
            }

            if (percent <= 50)
            {
                Pitch = -0.1f;
            }
            if (percent <= 15 || this.Status != StatusProblems.None)
            {
                Pitch = -0.2f;
            }
            if (percent == 0)
            {
                Pitch = -0.3f;
            }

            SoundEffectManager.PlayPokemonCry(Number, Pitch, 0f);
        }

        /// <summary>
        /// Checks if this Pokémon knows a certain move.
        /// </summary>
        /// <param name="Move">The move to check for.</param>
        public bool KnowsMove(BaseAttack Move) => Attacks.Any(a => a.Id == Move.Id);

        /// <summary>
        /// Checks if this Pokémon is inside an Egg.
        /// </summary>
        public bool IsEgg => EggSteps > 0;

        /// <summary>
        /// Adds Effort values (EV) to this Pokémon after defeated another Pokémon, if possible.
        /// </summary>
        /// <param name="DefeatedPokemon">The defeated Pokémon.</param>
        public void GainEffort(BasePokemon DefeatedPokemon)
        {
            int allEV = EVHP + EVAttack + EVDefense + EVSpAttack + EVSpDefense + EVSpeed;
            if (allEV >= 510)
                return;
            int maxEVgain = 510 - allEV;
            int totalEVgain = 0;
            
            //EV gains
            int gainEVHP = DefeatedPokemon.GiveEVHP;
            int gainEVAttack = DefeatedPokemon.GiveEVAttack;
            int gainEVDefense = DefeatedPokemon.GiveEVDefense;
            int gainEVSpAttack = DefeatedPokemon.GiveEVSpAttack;
            int gainEVSpDefense = DefeatedPokemon.GiveEVSpDefense;
            int gainEVSpeed = DefeatedPokemon.GiveEVSpeed;

            int EVfactor = 1;

            int itemNumber = 0;
            if (Item != null)
                itemNumber = Item.Id;

            switch (itemNumber)
            {
                //Macho Brace
                case 581: EVfactor *= 2; break; 
                //Power Items
                case 582: gainEVHP += 4; break;
                case 583: gainEVAttack += 4; break;
                case 584: gainEVDefense += 4; break;
                case 585: gainEVSpAttack += 4; break;
                case 586: gainEVSpDefense += 4; break;
                case 587: gainEVSpeed += 4; break;
            }
            
            //HP gain
            if (gainEVHP > 0 && EVHP < 252 && maxEVgain - totalEVgain > 0)
            {
                gainEVHP *= EVfactor;
                gainEVHP = MathHelper.Clamp(gainEVHP, 0, 252 - EVHP);
                gainEVHP = MathHelper.Clamp(gainEVHP, 0, maxEVgain - totalEVgain);
                EVHP += gainEVHP;
                totalEVgain += gainEVHP;
            }
            //Attack gain
            if (gainEVAttack > 0 && EVAttack < 252 && maxEVgain - totalEVgain > 0)
            {
                gainEVAttack *= EVfactor;
                gainEVAttack = MathHelper.Clamp(gainEVAttack, 0, 252 - EVAttack);
                gainEVAttack = MathHelper.Clamp(gainEVAttack, 0, maxEVgain - totalEVgain);
                EVAttack += gainEVAttack;
                totalEVgain += gainEVAttack;
            }
            //Defense gain
            if (gainEVDefense > 0 && EVDefense < 252 && maxEVgain - totalEVgain > 0)
            {
                gainEVDefense *= EVfactor;
                gainEVDefense = MathHelper.Clamp(gainEVDefense, 0, 252 - EVDefense);
                gainEVDefense = MathHelper.Clamp(gainEVDefense, 0, maxEVgain - totalEVgain);
                EVDefense += gainEVDefense;
                totalEVgain += gainEVDefense;
            }
            //SpAttack gain
            if (gainEVSpAttack > 0 && EVSpAttack < 252 && maxEVgain - totalEVgain > 0)
            {
                gainEVSpAttack *= EVfactor;
                gainEVSpAttack = MathHelper.Clamp(gainEVSpAttack, 0, 252 - EVSpAttack);
                gainEVSpAttack = MathHelper.Clamp(gainEVSpAttack, 0, maxEVgain - totalEVgain);
                EVSpAttack += gainEVSpAttack;
                totalEVgain += gainEVSpAttack;
            }
            //SpDefense gain
            if (gainEVSpDefense > 0 && EVSpDefense < 252 && maxEVgain - totalEVgain > 0)
            {
                gainEVSpDefense *= EVfactor;
                gainEVSpDefense = MathHelper.Clamp(gainEVSpDefense, 0, 252 - EVSpDefense);
                gainEVSpDefense = MathHelper.Clamp(gainEVSpDefense, 0, maxEVgain - totalEVgain);
                EVSpDefense += gainEVSpDefense;
                totalEVgain += gainEVSpDefense;
            }
            //Speed gain
            if (gainEVSpeed > 0 && EVSpeed < 252 && maxEVgain - totalEVgain > 0)
            {
                gainEVSpeed *= EVfactor;
                gainEVSpeed = MathHelper.Clamp(gainEVSpeed, 0, 252 - EVSpeed);
                gainEVSpeed = MathHelper.Clamp(gainEVSpeed, 0, maxEVgain - totalEVgain);
                EVSpeed += gainEVSpeed;
                totalEVgain += gainEVSpeed;
            }
        }

        /// <summary>
        /// Returns if this Pokémon knows an HM move.
        /// </summary>
        public bool HasHmMove => Attacks.Any(m => m.IsHmMove);

        /// <summary>
        /// Returns if this Pokémon is fully evolved.
        /// </summary>
        public bool IsFullyEVolved => EvolutionConditions.Count == 0;

        /// <summary>
        /// Checks if this Pokémon has a Hidden Ability assigend to it.
        /// </summary>
        public bool HasHiddenAbility => HiddenAbility != null;

        /// <summary>
        /// Checks if the Pokémon has its Hidden Ability set as its current ability.
        /// </summary>
        public bool IsUsingHiddenAbility => HasHiddenAbility && HiddenAbility.Id == Ability.Id;


        public override string ToString() => GetSaveData();
    }
}
