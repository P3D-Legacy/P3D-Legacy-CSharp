using System.Collections.Generic;

using P3D.Legacy.Core.Pokemon;

namespace P3D.Legacy.Core.Battle.BattleSystemV2
{
    /// <summary>
    /// Represents a Pokémon in battle.
    /// </summary>
    public class PokemonProfile
    {
        /// <summary>
        /// Creates a new instance of the Pokémon Profile.
        /// </summary>
        /// <param name="Target">The Position on the battle field.</param>
        /// <param name="Pokemon">The reference to the Pokémon in the party.</param>
        public PokemonProfile(PokemonTarget Target, BasePokemon Pokemon)
        {
            this.FieldPosition = Target;
            this.Pokémon = Pokemon;
        }

        /// <summary>
        /// The Pokémon reference class instance that is associated with this profile.
        /// </summary>
        public BasePokemon Pokémon { get; } = null;

        /// <summary>
        /// The position of this Pokémon on the field.
        /// </summary>
        public PokemonTarget FieldPosition { get; } = PokemonTarget.OwnCenter;

        //Stuff that needs to be in FieldEffects cause its an effect for one side of the field or the entire field:
        //HealingWish: Heal next Pokémon switched in on the side.
        //Spikes, Toxic Spikes, Stealth Rocks, Mist, Guard Spec: Full side, not single Pokémon
        //Turn Count: Turn count for the entire battle, not just the Pokémon.
        //Reflect, Tailwind, HealBlock, Safeguard, and LightScreen: Affects whole team.
        //Wish: Need to store the target.
        //RemoteMoves (RemoteDamage,RemoteTurns,RemoteTarget,RemoteMoveID): per Target (Foresight)
        //TrickRoom, Gravity, MudSport, WaterSport, RoundCount, Weather, WeatherRounds, AmuletCoinUsed
        //PayDay: All uses add up.

        #region "ProfileElements"

        /// <summary>
        /// The amount of damage this Pokémon took in the last attack.
        /// </summary>
        public int LastDamageTaken = 0;
        /// <summary>
        /// The counter that indicates how many turns this Pokémon has been in battle.
        /// </summary>
        public int TurnsInBattle = 0;
        /// <summary>
        /// If this Pokémon directly damaged an opponent last turn.
        /// </summary>
        public bool DealtDamageThisTurn = false;
        /// <summary>
        /// List of used moves (IDs)
        /// </summary>
        public List<int> UsedMoves = new List<int>();
        /// <summary>
        /// An item this Pokémon possibly lost in battle.
        /// </summary>
        public Item LostItem = null;
        /// <summary>
        /// If this Pokémon Mega Evolved in this battle.
        /// </summary>
        public bool MegaEvolved = false;
        /// <summary>
        /// The last move this Pokémon used.
        /// </summary>
        public BaseAttack LastMove = null;
        /// <summary>
        /// The turn number this Pokémon last used a move.
        /// </summary>

        public int LastTurnMoved = 0;
        /// <summary>
        /// Trigger, if the Pokémon wants to switch out but another one used Pursuit on it.
        /// </summary>

        public bool Pursuit = false;
        /// <summary>
        /// The turns until the Pokémon wakes up.
        /// </summary>
        public int SleepTurns = 0;
        /// <summary>
        /// Round counter for toxic.
        /// </summary>
        public int ToxicRound = 0;
        /// <summary>
        /// Rounds until Yawn affects the Pokémon.
        /// </summary>
        public int Yawn = 0;
        /// <summary>
        /// Turns until confusion runs out.
        /// </summary>

        public int ConfusionTurns = 0;
        /// <summary>
        /// The Truant ability counter. When this is 1, the Pokémon won't move this round.
        /// </summary>
        //
        public int TruantRound = 0;

        /// <summary>
        /// Turns True if Imprison was used on this Pokémon.
        /// </summary>
        public bool Imprisoned = false;
        /// <summary>
        /// Taunt move counter, if true, Pokémon cannot use status moves.
        /// </summary>
        public int Taunted = 0;
        /// <summary>
        /// If the Pokémon is affected by Embargo.
        /// </summary>
        public int Embargo = 0;
        /// <summary>
        /// Rounds until Encore wears off.
        /// </summary>
        public int Encore = 0;
        /// <summary>
        /// The move reference that the encored Pokémon has to use.
        /// </summary>
        public BaseAttack EncoreMove = null;
        /// <summary>
        /// Rounds until Torment wears off.
        /// </summary>
        public int Torment = 0;
        /// <summary>
        /// The move that the Pokémon is forced to use.
        /// </summary>
        public int TormentMoveID = -1;
        /// <summary>
        /// The move chosen for Choice Items.
        /// </summary>
        public BaseAttack ChoiceMove = null;
        /// <summary>
        /// Turns true when the Pokémon is affected by Gastro Acid.
        /// </summary>

        public int GastroAcid = 0;
        /// <summary>
        /// The counter for the rage move.
        /// </summary>
        public int Rage = 0;
        /// <summary>
        /// Checks if Defense Curl was used.
        /// </summary>
        public int DefenseCurl = 0;
        /// <summary>
        /// Charge move counter.
        /// </summary>
        public int Charge = 0;
        /// <summary>
        /// Indicates if minimize got used.
        /// </summary>
        public int Minimize = 0;
        /// <summary>
        /// Counter for Bide
        /// </summary>
        public int Bide = 0;
        /// <summary>
        /// Stores all the damage that the Pokémon will deal with Bide.
        /// </summary>
        public int BideDamage = 0;
        /// <summary>
        /// Doubles effect probability for four turns.
        /// </summary>

        public int WaterPledge = 0;
        /// <summary>
        /// The counter for the Uproar move.
        /// </summary>
        public int Uproar = 0;
        /// <summary>
        /// The Outrage move counter.
        /// </summary>
        public int Outrage = 0;
        /// <summary>
        /// The Thrash move counter.
        /// </summary>
        public int Thrash = 0;
        /// <summary>
        /// The Petal Dance move counter.
        /// </summary>
        public int PetalDance = 0;
        /// <summary>
        /// Rollout move counter
        /// </summary>
        public int Rollout = 0;
        /// <summary>
        /// Iceball move counter
        /// </summary>
        public int IceBall = 0;
        /// <summary>
        /// Recharge counter for moves like Hyperbeam.
        /// </summary>
        public int Recharge = 0;
        /// <summary>
        /// Razor Wind move counter.
        /// </summary>
        public int RazorWind = 0;
        /// <summary>
        /// Skull bash Move counter.
        /// </summary>
        public int SkullBash = 0;
        /// <summary>
        /// Sky Attack move counter.
        /// </summary>
        public int SkyAttack = 0;
        /// <summary>
        /// Solar beam move counter.
        /// </summary>
        public int SolarBeam = 0;
        /// <summary>
        /// Ice Burn move counter.
        /// </summary>
        public int IceBurn = 0;
        /// <summary>
        /// Freeze Shock move counter.
        /// </summary>
        public int FreezeShock = 0;
        /// <summary>
        /// Fly move counter.
        /// </summary>
        public int Fly = 0;
        /// <summary>
        /// Dig move counter.
        /// </summary>
        public int Dig = 0;
        /// <summary>
        /// Bounce move counter.
        /// </summary>
        public int Bounce = 0;
        /// <summary>
        /// Dive move counter.
        /// </summary>
        public int Dive = 0;
        /// <summary>
        /// Shadow Force move counter.
        /// </summary>
        public int ShadowForce = 0;
        /// <summary>
        /// Sky Drop move counter.
        /// </summary>

        public int SkyDrop = 0;
        /// <summary>
        /// Turns this Pokémon is trapped in Wrap.
        /// </summary>
        public int Wrap = 0;
        /// <summary>
        /// Turns this Pokémon is trapped in Whirlpool.
        /// </summary>
        public int Whirlpool = 0;
        /// <summary>
        /// Turns this Pokémon is trapped in Bind.
        /// </summary>
        public int Bind = 0;
        /// <summary>
        /// Turns this Pokémon is trapped in Clamp.
        /// </summary>
        public int Clamp = 0;
        /// <summary>
        /// Turns this Pokémon is trapped in FireSpin.
        /// </summary>
        public int FireSpin = 0;
        /// <summary>
        /// Turns this Pokémon is trapped in MagmaStorm.
        /// </summary>
        public int MagmaStorm = 0;
        /// <summary>
        /// Turns this Pokémon is trapped in SandTomb.
        /// </summary>

        public int SandTomb = 0;
        /// <summary>
        /// Focus Energy move counter.
        /// </summary>
        public int FocusEnergy = 0;
        /// <summary>
        /// Lucky Chant move counter.
        /// </summary>
        public int LuckyChant = 0;
        /// <summary>
        /// Counter for the Lock On and Mind Reader move.
        /// </summary>
        public int LockedOn = 0;
        /// <summary>
        /// Counter for the Fury Cutter move.
        /// </summary>
        public int FuryCutter = 0;
        /// <summary>
        /// Counter for the Stockpile move.
        /// </summary>
        public int StockPile = 0;
        /// <summary>
        /// Magic Coat move counter.
        /// </summary>
        public int MagicCoat = 0;
        /// <summary>
        /// Roost counter, set to flying type.
        /// </summary>
        public int Roost = 0;
        /// <summary>
        /// Destiny Bond move counter.
        /// </summary>

        public int DestinyBond = 0;
        /// <summary>
        /// Endure move counter.
        /// </summary>
        public int Endure = 0;
        /// <summary>
        /// Protect move counter
        /// </summary>
        public int Protect = 0;
        /// <summary>
        /// Detect move counter
        /// </summary>
        public int Detect = 0;
        /// <summary>
        /// Indicates how much HP the substitute has left.
        /// </summary>
        public int Substitute = 0;
        /// <summary>
        /// Counts the consecutive uses of Protect like moves.
        /// </summary>
        public int ProtectMoveCounter = 0;
        /// <summary>
        /// King's Shield move counter.
        /// </summary>

        public int KingsShield = 0;
        /// <summary>
        /// Ingrain move counter.
        /// </summary>
        public int Ingrain = 0;
        /// <summary>
        /// Counter for the Magnet Rise move.
        /// </summary>
        public int MagnetRise = 0;
        /// <summary>
        /// Counter for the Aqua Ring move.
        /// </summary>
        public int AquaRing = 0;
        /// <summary>
        /// If the Pokémon is affected by Nightmare.
        /// </summary>
        public int Nightmare = 0;
        /// <summary>
        /// If the Pokémon is affected by Curse.
        /// </summary>
        public int Cursed = 0;
        /// <summary>
        /// Turns until Perish Song faints Pokémon.
        /// </summary>
        public int PerishSong = 0;
        /// <summary>
        /// Counter for moves like Spider Web.
        /// </summary>
        public int Trapped = 0;
        /// <summary>
        /// Counter for the Foresight move.
        /// </summary>
        public int Foresight = 0;
        /// <summary>
        /// Counter for the Odor Sleught move.
        /// </summary>
        public int OdorSleught = 0;
        /// <summary>
        /// Counter for the Miracle Eye move.
        /// </summary>
        public int MiracleEye = 0;
        /// <summary>
        /// Halves this Pokémon's speed for four turns.
        /// </summary>
        public int GrassPledge = 0;
        /// <summary>
        /// Deals damage of 1/8 HP at the end of turn for four turns.
        /// </summary>

        public int FirePledge = 0;
        /// <summary>
        /// If Leech Seed got used on this Pokémon
        /// </summary>
        public int LeechSeed = 0;
        /// <summary>
        /// The target the leech seed HP gets sent to.
        /// </summary>

        public PokemonTarget LeechSeedTarget = null;
        /// <summary>
        /// Counter for the Metronome item.
        /// </summary>

        public int MetronomeItemCount = 0;
        /// <summary>
        /// Raises critical Hit ratio, Lansat Berry trigger
        /// </summary>
        public int LansatBerry = 0;
        /// <summary>
        /// Raises attack speed when Custap berry got eaten.
        /// </summary>

        public int CustapBerry = 0;
        #endregion

        /// <summary>
        /// Resets the fields to their default values when a new Pokémon gets switched in.
        /// </summary>
        /// <param name="BatonPassed">If the Pokémon got switched in using Baton Pass.</param>
        public void ResetFields(bool BatonPassed)
        {
            this.SleepTurns = 0;
            this.TruantRound = 0;
            this.Taunted = 0;
            this.Rage = 0;
            this.Uproar = 0;
            this.Endure = 0;
            this.Protect = 0;
            this.Detect = 0;
            this.KingsShield = 0;
            this.ProtectMoveCounter = 0;
            this.ToxicRound = 0;
            this.Nightmare = 0;
            this.Outrage = 0;
            this.Thrash = 0;
            this.PetalDance = 0;
            this.Encore = 0;
            this.EncoreMove = null;
            this.Yawn = 0;
            this.ConfusionTurns = 0;
            this.Torment = 0;
            this.TormentMoveID = 0;
            this.ChoiceMove = null;
            this.Recharge = 0;
            this.Rollout = 0;
            this.IceBall = 0;
            this.DefenseCurl = 0;
            this.Charge = 0;
            this.SolarBeam = 0;
            this.LansatBerry = 0;
            this.CustapBerry = 0;
            this.Trapped = 0;
            this.FuryCutter = 0;
            this.TurnsInBattle = 0;
            this.StockPile = 0;
            this.DestinyBond = 0;
            this.GastroAcid = 0;
            this.Foresight = 0;
            this.OdorSleught = 0;
            this.MiracleEye = 0;
            this.Fly = 0;
            this.Dig = 0;
            this.Bounce = 0;
            this.Dive = 0;
            this.ShadowForce = 0;
            this.SkyDrop = 0;
            this.SkyAttack = 0;
            this.RazorWind = 0;
            this.SkullBash = 0;
            this.Wrap = 0;
            this.Whirlpool = 0;
            this.Bind = 0;
            this.Clamp = 0;
            this.FireSpin = 0;
            this.MagmaStorm = 0;
            this.SandTomb = 0;
            this.Bide = 0;
            this.BideDamage = 0;
            this.Roost = 0;

            //If Baton Pass is not used to switch, also reset these variables:
            if (BatonPassed == false)
            {
                this.FocusEnergy = 0;
                this.Ingrain = 0;
                this.Substitute = 0;
                this.MagnetRise = 0;
                this.AquaRing = 0;
                this.Cursed = 0;
                this.Embargo = 0;
                this.PerishSong = 0;
                this.LeechSeed = 0;
                this.LeechSeedTarget = null;
            }
        }

    }
}