using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using P3D.Legacy.Core.Battle.BattleSystemV2;
using P3D.Legacy.Core.Pokemon;
using P3D.Legacy.Core.World;

namespace P3D.Legacy.Core.Screens
{
    public class BattleWeather
    {
        public enum WeatherTypes
        {
            Clear = 0,
            Rain = 1,
            Sunny = 2,
            Sandstorm = 3,
            Hailstorm = 4,
            Foggy = 5,
            Snow = 6,
            Underwater = 7
        }

        public static WeatherEnum GetWorldWeather(WeatherTypes FieldWeather)
        {
            switch (FieldWeather)
            {
                case WeatherTypes.Clear:
                    return WeatherEnum.Clear;
                case WeatherTypes.Foggy:
                    return WeatherEnum.Fog;
                case WeatherTypes.Hailstorm:
                    return WeatherEnum.Blizzard;
                case WeatherTypes.Rain:
                    return WeatherEnum.Rain;
                case WeatherTypes.Sandstorm:
                    return WeatherEnum.Sandstorm;
                case WeatherTypes.Sunny:
                    return WeatherEnum.Sunny;
                case WeatherTypes.Snow:
                    return WeatherEnum.Snow;
                case WeatherTypes.Underwater:
                    return WeatherEnum.Underwater;
                default:
                    return WeatherEnum.Clear;
            }
        }

        public static WeatherTypes GetBattleWeather(WeatherEnum WorldWeather)
        {
            switch (WorldWeather)
            {
                case WeatherEnum.Blizzard:
                    return WeatherTypes.Hailstorm;
                case WeatherEnum.Snow:
                    return WeatherTypes.Snow;
                case WeatherEnum.Clear:
                    return WeatherTypes.Clear;
                case WeatherEnum.Fog:
                    return WeatherTypes.Foggy;
                case WeatherEnum.Rain:
                case WeatherEnum.Thunderstorm:
                    return WeatherTypes.Rain;
                case WeatherEnum.Sandstorm:
                    return WeatherTypes.Sandstorm;
                case WeatherEnum.Sunny:
                    return WeatherTypes.Sunny;
                case WeatherEnum.Underwater:
                    return WeatherTypes.Underwater;
                default:
                    return WeatherTypes.Clear;
            }
        }

    }

    public class FieldEffects
    {
        //Own stuff
        //Sleep turns
        public int OwnSleepTurns = 0;
        //Truant move counter
        public int OwnTruantRound = 0;
        //Imprison move counter
        public int OwnImprison = 0;
        //Taunt move counter
        public int OwnTaunt = 0;
        //Rage move counter
        public int OwnRageCounter = 0;
        //Uproar move counter
        public int OwnUproar = 0;
        //Focus energy move counter
        public int OwnFocusEnergy = 0;
        //Endure move counter
        public int OwnEndure = 0;
        //Protect move counter
        public int OwnProtectCounter = 0;
        //Kings Shield move counter
        public int OwnKingsShieldCounter = 0;
        //Detect move counter
        public int OwnDetectCounter = 0;
        //Ingrain move counter
        public int OwnIngrain = 0;
        //Substitute HP left
        public int OwnSubstitute = 0;
        //Lucky chant move counter
        public int OwnLuckyChant = 0;
        //magnetrise move counter
        public int OwnMagnetRise = 0;
        //Aqua ring move counter
        public int OwnAquaRing = 0;
        //Poison counter for bad poison
        public int OwnPoisonCounter = 0;
        //Nightmare move counter
        public int OwnNightmare = 0;
        //Curse move counter
        public int OwnCurse = 0;
        //Outrage move counter
        public int OwnOutrage = 0;
        //Thrash move counter
        public int OwnThrash = 0;
        //Petaldance move counter
        public int OwnPetalDance = 0;
        //Encore move counter
        public int OwnEncore = 0;
        //Encore move used
        public BaseAttack OwnEncoreMove = null;
        //Embargo move counter
        public int OwnEmbargo = 0;
        //Yawn move counter
        public int OwnYawn = 0;
        //Perishsong move counter
        public int OwnPerishSongCount = 0;
        //Turns until confusion runs out
        public int OwnConfusionTurns = 0;
        //Torment move counter
        public int OwnTorment = 0;
        //Torment move
        public BaseAttack OwnTormentMove = null;
        //The counter for the item Metronome
        public int OwnMetronomeItemCount = 0;
        //The move chosen to use for choice items
        public BaseAttack OwnChoiceMove = null;
        //Rollout move counter
        public int OwnRolloutCounter = 0;
        //IceBall move counter
        public int OwnIceBallCounter = 0;
        //Recharge counter for moves like Hyperbeam
        public int OwnRecharge = 0;
        //Checks if defense curl was used
        public int OwnDefenseCurl = 0;
        //Charge move counter
        public int OwnCharge = 0;
        //Counter for the payday move
        public int OwnPayDayCounter = 0;
        //Razor Wind move counter
        public int OwnRazorWindCounter = 0;
        //Skull Bash move counter
        public int OwnSkullBashCounter = 0;
        //Sky Attack move counter
        public int OwnSkyAttackCounter = 0;
        //Set if user used the move Minimize
        public int OwnMinimize = 0;
        //Last Damage the own Pokémon has done by moves.
        public int OwnLastDamage = 0;
        //The opponent used leech seed
        public int OwnLeechSeed = 0;
        //Charge counter for solar beam
        public int OwnSolarBeam = 0;
        //Counter for the moves lock-on and mind reader
        public int OwnLockOn = 0;
        //Counter for the Bide move
        public int OwnBideCounter = 0;
        //Half of the damage dealt by bide
        public int OwnBideDamage = 0;
        //Raise critical hit ration when Lansat got eaten
        public int OwnLansatBerry = 0;
        //Raises the attack speed once when Custap got eaten
        public int OwnCustapBerry = 0;
        //If the pokemon is trapped (for example by Spider Web), this is =1
        public int OwnTrappedCounter = 0;
        //Own Ghost Pokémon can be hit by Normal/Fighting attacks
        public int OwnForesight = 0;
        //Same as Foresight
        public int OwnOdorSleuth = 0;
        //Own Dark type Pokémon can be hit by Psychic type attacks
        public int OwnMiracleEye = 0;
        //Counts uses of protect moves
        public int OwnProtectMovesCount = 0;
        //Counter for the move fury cutter
        public int OwnFuryCutter = 0;
        //Turns for how long the own pokemon has been in battle
        public int OwnPokemonTurns = 0;
        //A counter for the stockpile moves used for Swallow and Spit Up
        public int OwnStockpileCount = 0;
        //Counter for the Ice Burn move.
        public int OwnIceBurnCounter = 0;
        public int OwnFreezeShockCounter = 0;
        //Doubles effect propability for four turns
        public int OwnWaterPledge = 0;
        //Halves the opponent's speed for four turns
        public int OwnGrassPledge = 0;
        //Deals damage of 1/8 HP at the end of turn for four turns
        public int OwnFirePledge = 0;
        public bool OwnPokemonDamagedThisTurn = false;
        public bool OwnPokemonDamagedLastTurn = false;
        public int OwnFlyCounter = 0;
        public int OwnDigCounter = 0;
        public int OwnBounceCounter = 0;
        public int OwnDiveCounter = 0;
        public int OwnShadowForceCounter = 0;
        public int OwnSkyDropCounter = 0;
        public int OwnWrap = 0;
        public int OwnWhirlpool = 0;
        public int OwnBind = 0;
        public int OwnClamp = 0;
        public int OwnFireSpin = 0;
        public int OwnMagmaStorm = 0;
        public int OwnSandTomb = 0;
        public int OwnInfestation = 0;
        public List<int> OwnUsedMoves = new List<int>();
        public int OwnMagicCoat = 0;
        public Item OwnLostItem = null;
        public bool OwnPursuit = false;
        public bool OwnMegaEvolved = false;
        //If roost got used, this is true and will get set false and revert types at the end of a turn.
        public bool OwnRoostUsed = false;
        //If own Pokémon used destiny bond, this is true. If the opponent knocks the own Pokémon out, it will faint as well.
        public bool OwnDestinyBond = false;
        //True, if healing wish got used. Heals the next switched in Pokémon.
        public bool OwnHealingWish = false;
        //If own Pokémon is affected by Gastro Acid
        public bool OwnGastroAcid = false;

        //Last move used
        public BaseAttack OwnLastMove = null;
        //Trap move counter
        public int OwnSpikes = 0;
        //Trap move counter
        public int OwnStealthRock = 0;
        //Trap move counter
        public int OwnStickyWeb = 0;
        //Trap move counter
        public int OwnToxicSpikes = 0;
        //Mist move counter
        public int OwnMist = 0;
        //Guard spec item counter
        public int OwnGuardSpec = 0;
        //Own turns
        public int OwnTurnCounts = 0;
        //Lightscreen move counter
        public int OwnLightScreen = 0;
        //Reflect move counter
        public int OwnReflect = 0;
        //Tailwind move counter
        public int OwnTailWind = 0;
        //Healblock move counter
        public int OwnHealBlock = 0;
        //Safeguard move counter 
        public int OwnSafeguard = 0;
        //Wish move counter 
        public int OwnWish = 0;
        //stored Futuresight move damage
        public int OwnFutureSightDamage = 0;
        //Turns until Futuresight hits
        public int OwnFutureSightTurns = 0;
        //Move ID for the Futuresight move
        public int OwnFutureSightID = 0;

        //Opp stuff
        public int OppSpikes = 0;
        public int OppStealthRock = 0;
        public int OppStickyWeb = 0;
        public int OppToxicSpikes = 0;
        public int OppMist = 0;
        public int OppGuardSpec = 0;
        public int OppTurnCounts = 0;
        public BaseAttack OppLastMove = null;
        public int OppLightScreen = 0;
        public int OppReflect = 0;
        public int OppTailWind = 0;
        public int OppSleepTurns = 0;
        public int OppTruantRound = 0;
        public int OppImprison = 0;
        public int OppHealBlock = 0;
        public int OppTaunt = 0;
        public int OppRageCounter = 0;
        public int OppUproar = 0;
        public int OppFocusEnergy = 0;
        public int OppEndure = 0;
        public int OppProtectCounter = 0;
        public int OppKingsShieldCounter = 0;
        public int OppDetectCounter = 0;
        public int OppIngrain = 0;
        public int OppSubstitute = 0;
        public int OppSafeguard = 0;
        public int OppLuckyChant = 0;
        public int OppWish = 0;
        public int OppMagnetRise = 0;
        public int OppAquaRing = 0;
        public int OppPoisonCounter = 0;
        public int OppNightmare = 0;
        public int OppCurse = 0;
        public int OppOutrage = 0;
        public int OppThrash = 0;
        public int OppPetalDance = 0;
        public int OppEncore = 0;
        public BaseAttack OppEncoreMove;
        public int OppEmbargo = 0;
        public int OppYawn = 0;
        public int OppFutureSightDamage = 0;
        public int OppFutureSightTurns = 0;
        public int OppFutureSightID = 0;
        public int OppPerishSongCount = 0;
        public int OppConfusionTurns = 0;
        public int OppTorment = 0;
        public BaseAttack OppTormentMove = null;
        public int OppMetronomeItemCount = 0;
        public BaseAttack OppChoiceMove = null;
        public int OppRolloutCounter = 0;
        public int OppIceBallCounter = 0;
        public int OppRecharge = 0;
        public int OppDefenseCurl = 0;
        public int OppCharge = 0;
        public int OppPayDayCounter = 0;
        public int OppRazorWindCounter = 0;
        public int OppSkullBashCounter = 0;
        public int OppSkyAttackCounter = 0;
        public int OppMinimize = 0;
        public int OppLastDamage = 0;
        public int OppLeechSeed = 0;
        public int OppSolarBeam = 0;
        public int OppLockOn = 0;
        public int OppBideCounter = 0;
        public int OppBideDamage = 0;
        public int OppLansatBerry = 0;
        public int OppCustapBerry = 0;
        public int OppTrappedCounter = 0;
        public int OppForesight = 0;
        public int OppOdorSleuth = 0;
        public int OppMiracleEye = 0;
        public int OppProtectMovesCount = 0;
        public int OppFuryCutter = 0;
        public int OppPokemonTurns = 0;
        public int OppStockpileCount = 0;
        public int OppIceBurnCounter = 0;
        public int OppFreezeShockCounter = 0;
        public int OppWaterPledge = 0;
        public int OppGrassPledge = 0;
        public int OppFirePledge = 0;
        public bool OppPokemonDamagedThisTurn = false;
        public bool OppPokemonDamagedLastTurn = false;
        public int OppMagicCoat = 0;
        public Item OppLostItem = null;
        public bool OppPursuit = false;
        public bool OppMegaEvolved = false;
        public bool OppRoostUsed = false;
        public bool OppDestinyBond = false;
        public bool OppHealingWish = false;

        public bool OppGastroAcid = false;
        public int OppFlyCounter = 0;
        public int OppDigCounter = 0;
        public int OppBounceCounter = 0;
        public int OppDiveCounter = 0;
        public int OppShadowForceCounter = 0;

        public int OppSkyDropCounter = 0;
        public int OppWrap = 0;
        public int OppWhirlpool = 0;
        public int OppBind = 0;
        public int OppClamp = 0;
        public int OppFireSpin = 0;
        public int OppMagmaStorm = 0;
        public int OppSandTomb = 0;

        public int OppInfestation = 0;

        public List<int> OppUsedMoves = new List<int>();
        //Weather
        private BattleWeather.WeatherTypes _weather = BattleWeather.WeatherTypes.Clear;

        public int WeatherRounds = 0;
        public BattleWeather.WeatherTypes Weather
        {
            get { return this._weather; }
            set
            {
                this._weather = value;
                Screen.Level.World.CurrentWeather = BattleWeather.GetWorldWeather(value);
            }
        }

        //Field stuff
        public int TrickRoom = 0;
        public int Gravity = 0;
        public int MudSport = 0;
        public int WaterSport = 0;
        public int Rounds = 0;

        public int AmuletCoin = 0;
        //Special stuff
        public int RunTries = 0;
        public List<int> UsedPokemon = new List<int>();
        public List<int> StolenItemIDs = new List<int>();
        public bool DefeatedTrainerPokemon = false;

        public bool RoamingFled = false;
        //BatonPassTemp:
        public bool OwnUsedBatonPass = false;
        public List<int> OwnBatonPassStats;

        public bool OwnBatonPassConfusion = false;
        public bool OppUsedBatonPass = false;
        public List<int> OppBatonPassStats;

        public bool OppBatonPassConfusion = false;
        public bool CanUseItem(bool own)
        {
            int embargo = OwnEmbargo;
            if (own == false)
            {
                embargo = OppEmbargo;
            }
            if (embargo > 0)
            {
                return false;
            }
            return true;
        }

        public bool CanUseOwnItem(bool own, BaseBattleScreen BattleScreen)
        {
            BasePokemon p = BattleScreen.OwnPokemon;
            if (own == false)
            {
                p = BattleScreen.OppPokemon;
            }
            if (p.Ability.Name.ToLower() == "klutz")
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Checks if a Pokémon can use its ability.
        /// </summary>
        /// <param name="own">Which Pokémon?</param>
        /// <param name="BattleScreen">The BattleScreen reference.</param>
        /// <param name="CheckType">0: Supression abilities, 1: GastroAcid, 2: both</param>
        public bool CanUseAbility(bool own, BaseBattleScreen BattleScreen, int CheckType = 0)
        {
            if (CheckType == 0 || CheckType == 2)
            {
                BasePokemon p = BattleScreen.OppPokemon;
                if (own == false)
                {
                    p = BattleScreen.OwnPokemon;
                }

                string[] supressAbilities = {
                "mold breaker",
                "turboblaze",
                "teravolt"
            };

                if (supressAbilities.Contains(p.Ability.Name.ToLower()) == true)
                {
                    return false;
                }
            }
            if (CheckType == 1 || CheckType == 2)
            {
                if (own == true)
                {
                    if (OwnGastroAcid == true)
                    {
                        return false;
                    }
                }
                else
                {
                    if (OppGastroAcid == true)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public float GetPokemonWeight(bool own, BaseBattleScreen BattleScreen)
        {
            BasePokemon p = BattleScreen.OwnPokemon;
            BasePokemon op = BattleScreen.OppPokemon;
            if (own == false)
            {
                p = BattleScreen.OppPokemon;
                op = BattleScreen.OwnPokemon;
            }

            float weigth = p.PokedexEntry.Weight;

            if (p.Ability.Name.ToLower() == "light metal" && CanUseAbility(own, BattleScreen) == true)
            {
                weigth /= 2;
            }

            if (p.Ability.Name.ToLower() == "heavy metal" && CanUseAbility(own, BattleScreen) == true)
            {
                weigth *= 2;
            }

            return weigth;
        }

        public bool MovesFirst(bool own)
        {
            if (own == true)
            {
                if (OwnTurnCounts > OppTurnCounts)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (OppTurnCounts > OwnTurnCounts)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

    }

    public enum EndBattleReasons
    {
        WinWild,
        LooseWild,
        WinTrainer,
        LooseTrainer,
        WinPvP,
        LoosePvP
    }

    public struct RoundConst
    {
        public enum StepTypes
        {
            Move,
            Item,
            Switch,
            Text,
            Flee,
            FreeSwitch
        }

        public StepTypes StepType;
        public object Argument;
    }

    public interface IBattle
    {
        /// <summary>
        /// If the battle is a remote battle, then start this function once client input is received.
        /// </summary>

        void StartMultiTurnAction(BaseBattleScreen BattleScreen);
        void StartRound(BaseBattleScreen BattleScreen);
        RoundConst GetOppStep(BaseBattleScreen BattleScreen, RoundConst OwnStep);
        void AI_MegaEvolve(BaseBattleScreen BattleScreen);
        void DoMegaEvolution(BaseBattleScreen BattleScreen, bool own);
        void MegaEvolCheck(BaseBattleScreen BattleScreen);
        void InitializeRound(BaseBattleScreen BattleScreen, RoundConst OwnStep);
        bool IsChargingTurn(BaseBattleScreen BattleScreen, bool own, BaseAttack moveUsed);

        void DoAttackRound(BaseBattleScreen BattleScreen, bool own, BaseAttack moveUsed);
        /// <summary>
        /// Faints a Pokémon.
        /// </summary>
        /// <param name="own">true: faints own, false: faints opp</param>

        void FaintPokemon(bool own, BaseBattleScreen BattleScreen, string message);
        bool CureStatusProblem(bool own, bool @from, BaseBattleScreen BattleScreen, string message, string cause);
        bool InflictFlinch(bool own, bool @from, BaseBattleScreen BattleScreen, string message, string cause);
        bool InflictBurn(bool own, bool @from, BaseBattleScreen BattleScreen, string message, string cause);
        bool InflictFreeze(bool own, bool @from, BaseBattleScreen BattleScreen, string message, string cause);
        bool InflictParalysis(bool own, bool @from, BaseBattleScreen BattleScreen, string message, string cause);
        bool InflictSleep(bool own, bool @from, BaseBattleScreen BattleScreen, int turnsPreset, string message, string cause);
        bool InflictPoison(bool own, bool @from, BaseBattleScreen BattleScreen, bool bad, string message, string cause);
        bool InflictConfusion(bool own, bool @from, BaseBattleScreen BattleScreen, string message, string cause);
        bool RaiseStat(bool own, bool @from, BaseBattleScreen BattleScreen, string Stat, int val, string message, string cause);
        bool LowerStat(bool own, bool @from, BaseBattleScreen BattleScreen, string Stat, int val, string message, string cause);
        bool InflictInfatuate(bool own, bool @from, BaseBattleScreen BattleScreen, string message, string cause);
        void InflictRecoil(bool own, bool @from, BaseBattleScreen BattleScreen, BaseAttack MoveUsed, int Damage, string message, string cause);
        void GainHP(int HPAmount, bool own, bool @from, BaseBattleScreen BattleScreen, string message, string cause);
        void ReduceHP(int HPAmount, bool own, bool @from, BaseBattleScreen BattleScreen, string message, string cause);
        void ReduceHP(int HPAmount, bool own, bool @from, BaseBattleScreen BattleScreen, string message, string cause, string sound);
        void UseBerry(bool own, bool @from, Item BerryItem, BaseBattleScreen BattleScreen, string message, string cause);
        bool RemoveHeldItem(bool own, bool @from, BaseBattleScreen BattleScreen, string message, string cause, bool TestFor = false);
        void ChangeWeather(bool own, bool @from, BattleWeather.WeatherTypes newWeather, int turns, BaseBattleScreen BattleScreen, string message, string cause);

        void TriggerAbilityEffect(BaseBattleScreen BattleScreen, bool own);
        /// <summary>
        /// Switches camera to angel
        /// </summary>
        /// <param name="direction">0=main battle/1=own pokemon/2=opp pokemon</param>
        /// <param name="own">If the code comes from the own player or not.</param>
        /// <param name="BattleScreen">Battlescreen reference</param>
        /// <param name="AddPVP">If the call should get added the PVP list or the own queue.</param>

        void ChangeCameraAngel(int direction, bool own, BaseBattleScreen BattleScreen, bool AddPVP = false);
        /// <summary>
        /// Ends a round (or a complete round)
        /// </summary>
        /// <param name="BattleScreen">Battlescreen</param>
        /// <param name="type">0=complete;1=own;2=opp</param>

        void EndRound(BaseBattleScreen BattleScreen, int type);
        void SwitchOutOwn(BaseBattleScreen BattleScreen, int SwitchInIndex, int InsertIndex, string message = "");
        void ApplyOwnBatonPass(BaseBattleScreen BattleScreen);
        void SwitchInOwn(BaseBattleScreen BattleScreen, int NewPokemonIndex, bool FirstTime, int InsertIndex, string message = "");
        void SwitchOutOpp(BaseBattleScreen BattleScreen, int index, string message = "");
        void ApplyOppBatonPass(BaseBattleScreen BattleScreen);
        void SwitchInOpp(BaseBattleScreen BattleScreen, bool FirstTime, int index);
        void EndBattle(EndBattleReasons reason, BaseBattleScreen BattleScreen, bool AddPVP);
        bool Won { get; set; }
        bool Fled { get; set; }
    }

    public abstract class BaseBattleScreen : Screen
    {
        //Remove when new system gets put in place:
        public BasePokemon OwnPokemon { get; set; }
        public BasePokemon OppPokemon { get; set; }

        //public IBattle Battle { get; set; }

        public FieldEffects FieldEffects { get; set; }

        
        //Used for after fainting switching
        public static bool OwnFaint { get; set; }
        public static bool OppFaint { get; set; }

        public bool IsRemoteBattle { get; set; }

        public bool IsHost { get; set; }
        public Vector3 BattleMapOffset { get; set; }

        internal object GetProfile(PokemonTarget target)
        {
            throw new NotImplementedException();
        }
    }
}
