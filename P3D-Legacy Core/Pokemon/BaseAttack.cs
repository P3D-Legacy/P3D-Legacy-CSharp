using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using P3D.Legacy.Core.Resources;
using P3D.Legacy.Core.Screens;

namespace P3D.Legacy.Core.Pokemon
{
    public abstract class BaseAttack
    {
        public abstract class AttackManager
        {
            public abstract BaseAttack GetAttackByID(int ID);
        }

        private static AttackManager _am;
        protected static AttackManager AM
        {
            get
            {
                if (_am == null)
                {
                    var assembly = Assembly.GetEntryAssembly();
                    var type = assembly.GetTypes().SingleOrDefault(t => t.IsSubclassOf(typeof(AttackManager)));
                    if (type != null)
                        _am = Activator.CreateInstance(type) as AttackManager;
                }

                return _am;
            }
            set { _am = value; }
        }
        public static BaseAttack GetAttackByID(int ID) => AM.GetAttackByID(ID);



        #region "Fields"

        public const int MoveCount = 560;
        public enum Categories
        {
            Physical,
            Special,
            Status
        }

        public enum ContestCategories
        {
            Tough,
            Smart,
            Beauty,
            Cool,
            Cute
        }

        /// <summary>
        /// The target for an attack.
        /// </summary>
        public enum Targets
        {
            OneAdjacentTarget,
            //One adjacent target, excluding itself.
            OneAdjacentFoe,
            //One adjacent foe.
            OneAdjacentAlly,
            //One adjacent ally, excluding itself.

            OneTarget,
            //One target, excluding itself.
            OneFoe,
            //One Foe.
            OneAlly,
            //One ally, excluding itself.

            Self,
            //Only self

            AllAdjacentTargets,
            //All adjacent targets, exluding itself
            AllAdjacentFoes,
            //All adjacent foes
            AllAdjacentAllies,
            //All adjacent allies, excluding itself.

            AllTargets,
            //All Targets, excluding itself.
            AllFoes,
            //All Foes
            AllAllies,
            //All allies, excluding itself.

            All,
            //All Pokémon, including itself

            AllOwn
            //All allies, including itself.
        }

        public enum AIField
        {
            Nothing,

            Damage,

            Poison,
            Burn,
            Paralysis,
            Sleep,
            Freeze,
            Confusion,

            ConfuseOwn,

            CanPoison,
            CanBurn,
            CanParalyse,
            CanSleep,
            CanFreeze,
            CanConfuse,

            RaiseAttack,
            RaiseDefense,
            RaiseSpAttack,
            RaiseSpDefense,
            RaiseSpeed,
            RaiseAccuracy,
            RaiseEvasion,

            LowerAttack,
            LowerDefense,
            LowerSpAttack,
            LowerSpDefense,
            LowerSpeed,
            LowerAccuracy,
            LowerEvasion,

            CanRaiseAttack,
            CanRaiseDefense,
            CanRaiseSpAttack,
            CanRaiseSpDefense,
            CanRaiseSpeed,
            CanRaiseAccuracy,
            CanRauseEvasion,

            CanLowerAttack,
            CanLowerDefense,
            CanLowerSpAttack,
            CanLowerSpDefense,
            CanLowerSpeed,
            CanLowerAccuracy,
            CanLowerEvasion,

            Flinch,
            CanFlinch,

            Infatuation,

            Trap,
            Ohko,
            MultiTurn,
            Recoil,

            Healing,
            CureStatus,
            Support,
            Recharge,
            HighPriority,
            Absorbing,
            Selfdestruct,
            ThrawOut,
            CannotMiss,
            RemoveReflectLightscreen
        }

        //#Definitions

        public Element Type = new Element("Normal");
        public int Id { get; set; } = 1;

        public int Power { get; set; } = 40;

        public int Accuracy { get; set; } = 100;

        public string Name { get; set; } = "Pound";

        //Original MoveID, remove when not needed anymore. This stores the original move ID when the move isn't programmed yet.
        public int OriginalId = 1;
        //if Pound gets loaded instead of the correct move, this is true.
        public bool IsDefaultMove = false;

        //A GameMode can specify a pre defined function for a move.
        public string GameModeFunction = "";

        public bool IsGameModeMove = false;

        public int OriginalPp = 35;
        public Categories Category = Categories.Physical;
        public ContestCategories ContestCategory = ContestCategories.Tough;
        public string Description = "Pounds with forelegs or tail.";
        public int CriticalChance = 1;
        public bool IsHmMove = false;
        public Targets Target = Targets.OneAdjacentTarget;
        public int Priority = 0;
        public int TimesToAttack = 1;
        public List<int> EffectChances = new List<int>();
        //#End

        //#SpecialDefinitions
        public bool MakesContact = true;
        public bool ProtectAffected = true;
        public bool MagicCoatAffected = false;
        public bool SnatchAffected = false;
        public bool MirrorMoveAffected = true;
        public bool KingsrockAffected = true;
        public bool CounterAffected = true;
        public bool DisabledWhileGravity = false;
        public bool UseEffectiveness = true;
        public bool IsHealingMove = false;
        public bool RemovesFrozen = false;
        public bool IsRecoilMove = false;
        public bool IsPunchingMove = false;
        public bool ImmunityAffected = true;
        public bool IsDamagingMove = true;
        public bool IsProtectMove = false;
        public bool IsSoundMove = false;
        public bool HasSecondaryEffect = false;
        public bool IsAffectedBySubstitute = true;
        public bool IsOneHitKoMove = false;
        public bool IsWonderGuardAffected = true;
        public bool UseAccEvasion = true;
        public bool CanHitInMidAir = false;
        public bool CanHitUnderground = false;
        public bool CanHitUnderwater = false;
        public bool CanHitSleeping = true;
        public bool CanGainStab = true;
        public bool IsPowderMove = false;
        public bool IsTrappingMove = false;
        public bool IsPulseMove = false;
        public bool IsBulletMove = false;
        public bool IsJawMove = false;
        public bool UseOppDefense = true;

        public bool UseOppEvasion = true;
        public bool FocusOppPokemon = true;
        //#End

        public int CurrentPp = 0;
        public int MaxPp = 0;

        public int Disabled = 0;
        public AIField AiField1 = AIField.Damage;
        public AIField AiField2 = AIField.Nothing;

        public AIField AiField3 = AIField.Nothing;
        #endregion

        public abstract int GetEffectChance(int i, bool own, Screen battleScreen);

        /// <summary>
        /// Gets called prior to using the attack.
        /// </summary>
        /// <param name="own">If the own Pokémon used the move.</param>
        /// <param name="battleScreen">Reference to the BattleScreen.</param>
        public virtual void PreAttack(bool own, Screen battleScreen) { /* DO NOTHING HERE */ }

        /// <summary>
        /// If the move fails prior to using it. Return True for failing.
        /// </summary>
        /// <param name="own">If the own Pokémon used the move.</param>
        /// <param name="battleScreen">Reference to the BattleScreen.</param>
        public virtual bool MoveFailBeforeAttack(bool own, Screen battleScreen) =>  /* DO NOTHING HERE */ false;

        /// <summary>
        /// Returns the BasePower of this move. Defaults to Power.
        /// </summary>
        /// <param name="Own">If the own Pokémon used the move.</param>
        /// <param name="battleScreen">Reference to the BattleScreen.</param>
        public virtual int GetBasePower(bool own, Screen battleScreen) => Power;

        /// <summary>
        /// Returns the calculated damage of this move.
        /// </summary>
        /// <param name="own">If the own Pokémon used the move.</param>
        /// <param name="battleScreen">Reference to the BattleScreen.</param>
        public abstract int GetDamage(bool critical, bool own, bool targetPokemon, Screen battleScreen);

        /// <summary>
        /// Returns how many times this move is getting used in a row.
        /// </summary>
        /// <param name="Own">If the own Pokémon used the move.</param>
        /// <param name="battleScreen">Reference to the BattleScreen.</param>
        public virtual int GetTimesToAttack(bool own, Screen battleScreen) => TimesToAttack;

        /// <summary>
        /// Event that occurs when the move connects.
        /// </summary>
        /// <param name="Own">If the own Pokémon used the move.</param>
        /// <param name="battleScreen">Reference to the BattleScreen.</param>
        public abstract void MoveHits(bool own, Screen battleScreen);

        public virtual void MoveRecoil(bool own, Screen battleScreen) { /* DO NOTHING HERE (will do recoil if moves overrides it) */ }

        public virtual void MoveRecharge(bool own, Screen battleScreen) { /* DO NOTHING HERE (will do recoil if moves overrides it) */ }

        public virtual void MoveSwitch(bool own, Screen battleScreen) { /* DO NOTHING HERE (will do recoil if moves overrides it) */ }

        /// <summary>
        /// Event that occurs when the move misses its target.
        /// </summary>
        /// <param name="Own">If the own Pokémon used the move.</param>
        /// <param name="battleScreen">Reference to the BattleScreen.</param>
        public virtual void MoveMisses(bool own, Screen battleScreen) { /* DO NOTHING HERE */ }

        /// <summary>
        /// If the move gets blocked by a protection move.
        /// </summary>
        /// <param name="Own">If the own Pokémon used the move.</param>
        /// <param name="battleScreen">Reference to the BattleScreen.</param>
        public virtual void MoveProtectedDetected(bool own, Screen battleScreen) { /* DO NOTHING HERE */ }

        /// <summary>
        /// Event that occurs when the move has no effect on the target.
        /// </summary>
        /// <param name="Own">If the own Pokémon used the move.</param>
        /// <param name="battleScreen">Reference to the BattleScreen.</param>
        public virtual void MoveHasNoEffect(bool own, Screen battleScreen) { /* DO NOTHING HERE */ }

        /// <summary>
        /// Returns the type of the move. Defaults to the Type field.
        /// </summary>
        /// <param name="Own">If the own Pokémon used the move.</param>
        /// <param name="BattleScreen">Reference to the BattleScreen.</param>
        public virtual Element GetAttackType(bool own, Screen battleScreen)
        {
            BaseBattleScreen screen = battleScreen as BaseBattleScreen;
            var p = screen.OwnPokemon;
            if (own == false)
            {
                p = screen.OppPokemon;
            }

            if (p.Ability.Name.ToLower() == "normalize")
            {
                return new Element(Element.Types.Normal);
            }

            if (Type.Type == Element.Types.Normal)
            {
                if (p.Ability.Name.ToLower() == "pixilate")
                {
                    return new Element(Element.Types.Fairy);
                }
                if (p.Ability.Name.ToLower() == "refrigerate")
                {
                    return new Element(Element.Types.Ice);
                }
                if (p.Ability.Name.ToLower() == "aerilate")
                {
                    return new Element(Element.Types.Flying);
                }
            }

            return Type;
        }

        /// <summary>
        /// Returns the accuracy of this move.
        /// </summary>
        /// <param name="Own">If the own Pokémon used the move.</param>
        /// <param name="battleScreen">Reference to the BattleScreen.</param>
        public virtual int GetAccuracy(bool own, Screen battleScreen) => Accuracy;

        /// <summary>
        /// If the PP of this move should get deducted when using it. Default is True.
        /// </summary>
        /// <param name="Own">If the own Pokémon used the move.</param>
        /// <param name="battleScreen">Reference to the BattleScreen.</param>
        public virtual bool DeductPp(bool own, Screen battleScreen) => true;

        /// <summary>
        /// If the Accuracy and Evasion parameters of Pokémon and moves should get used for this attack.
        /// </summary>
        /// <param name="Own">If the own Pokémon used the move.</param>
        /// <param name="battleScreen">Reference to the BattleScreen.</param>
        public virtual bool GetUseAccEvasion(bool own, Screen battleScreen) => UseAccEvasion;

        /// <summary>
        /// Event that occurs when the move gets selected in the menu.
        /// </summary>
        /// <param name="Own">If the own Pokémon used the move.</param>
        /// <param name="battleScreen">Reference to the BattleScreen.</param>
        public virtual void MoveSelected(bool own, Screen battleScreen) { /* DO NOTHING */ }

        /// <summary>
        /// Event that occurs before this move deals damage.
        /// </summary>
        /// <param name="Own">If the own Pokémon used the move.</param>
        /// <param name="battleScreen">Reference to the BattleScreen.</param>
        public virtual void BeforeDealingDamage(bool own, Screen battleScreen) { /* DO NOTHING */ }

        /// <summary>
        /// Event that occurs when this move's damage gets absorbed by a substitute.
        /// </summary>
        /// <param name="Own">If the own Pokémon used the move.</param>
        /// <param name="battleScreen">Reference to the BattleScreen.</param>
        public virtual void AbsorbedBySubstitute(bool own, Screen battleScreen) { /* DO NOTHING */ }

        /// <summary>
        /// Event that occurs when the Soundproof ability blocks this move.
        /// </summary>
        /// <param name="Own">If the own Pokémon used the move.</param>
        /// <param name="battleScreen">Reference to the BattleScreen.</param>
        public virtual void MoveFailsSoundproof(bool own, Screen battleScreen) { /* DO NOTHING */ }

        /// <summary>
        /// Event that occurs when a flinch has been inflicted.
        /// </summary>
        /// <param name="Own">If the own Pokémon used the move.</param>
        /// <param name="battleScreen">Reference to the BattleScreen.</param>
        public virtual void InflictedFlinch(bool own, Screen battleScreen) { /* DO NOTHING */ }

        /// <summary>
        /// Event that occurs when the pokemon has hurt itself in confusion.
        /// </summary>
        /// <param name="Own">If the own Pokémon is confused.</param>
        /// <param name="battleScreen">Reference to the BattleScreen.</param>
        public virtual void HurtItselfInConfusion(bool own, Screen battleScreen) { /* DO NOTHING */ }

        /// <summary>
        /// Event that occurs when the pokemon has falls in love with the opponent.
        /// </summary>
        /// <param name="Own">If the own Pokémon is in love.</param>
        /// <param name="battleScreen">Reference to the BattleScreen.</param>
        public virtual void IsAttracted(bool own, Screen battleScreen) { /* DO NOTHING */ }

        /// <summary>
        /// Event that occurs when a the pokemon has been put to sleep.
        /// </summary>
        /// <param name="Own">If the own Pokémon used the move.</param>
        /// <param name="battleScreen">Reference to the BattleScreen.</param>
        public virtual void IsSleeping(bool own, Screen battleScreen) { /* DO NOTHING */ }

        /// <summary>
        /// Returns the attack stat of a Pokémon (Physical or Special).
        /// </summary>
        /// <param name="p">The Pokémon that used the move.</param>
        public virtual int GetUseAttackStat(BasePokemon p) => Category == Categories.Physical ? p.Attack : p.SpAttack;

        /// <summary>
        /// Returns the defense stat of a Pokémon (Physical or Special).
        /// </summary>
        /// <param name="p">The Pokémon that used the move.</param>
        public virtual int GetUseDefenseStat(BasePokemon p) => Category == Categories.Physical ? p.Defense : p.SpDefense;

        /// <summary>
        /// If the AI is allowed to use this move.
        /// </summary>
        /// <param name="battleScreen">Reference to the BattleScreen.</param>
        public virtual bool AiUseMove(Screen battleScreen) => true;

        #region "Animation"

        public void UserPokemonMoveAnimation(Screen battleScreen)
        {
            if (Core.Player.ShowBattleAnimations == 1)
                InternalUserPokemonMoveAnimation(battleScreen);
        }

        /// <summary>
        /// Override this method in the attack class to insert the move animation query objects into the queue.
        /// </summary>
        /// <param name="battleScreen"></param>
        public virtual void InternalUserPokemonMoveAnimation(Screen battleScreen) { }

        public void OpponentPokemonMoveAnimation(Screen battleScreen)
        {
            if (Core.Player.ShowBattleAnimations == 1)
                InternalOpponentPokemonMoveAnimation(battleScreen);
        }

        /// <summary>
        /// Override this method in the attack class to insert the move animation query objects into the queue.
        /// </summary>
        /// <param name="battleScreen"></param>
        public virtual void InternalOpponentPokemonMoveAnimation(Screen battleScreen) { }

        #endregion

        /// <summary>
        /// Returns a copy of this move.
        /// </summary>
        public BaseAttack Copy()
        {
            var m = IsGameModeMove ? GameModeAttackLoader.GetAttackByID(Id) : GetAttackByID(Id);

            //Set definition properties:
            m.OriginalPp = OriginalPp;
            m.CurrentPp = CurrentPp;
            m.MaxPp = MaxPp;
            m.OriginalId = OriginalId;

            return m;
        }

        /// <summary>
        /// Builds an instance of AttackV2 with PP and MaxPP set.
        /// </summary>
        /// <param name="InputData">Data in the format "ID,MaxPP,CurrentPP"</param>
        public static BaseAttack ConvertStringToAttack(string InputData)
        {
            if (!string.IsNullOrEmpty(InputData))
            {
                string[] Data = InputData.Split(Convert.ToChar(","));
                var a = GetAttackByID(Convert.ToInt32(Data[0]));

                if (a != null)
                {
                    a.MaxPp = Convert.ToInt32(Data[1]);
                    a.CurrentPp = Convert.ToInt32(Data[2]);
                }

                return a;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Raises the PP of the move by one stage.
        /// </summary>
        public bool RaisePp()
        {
            switch (OriginalPp)
            {
                case 5:
                    switch (MaxPp)
                    {
                        case 5:
                        case 6:
                        case 7:
                            CurrentPp += 1;
                            MaxPp += 1;
                            return true;
                    }
                    break;

                case 10:
                    switch (MaxPp)
                    {
                        case 10:
                        case 12:
                        case 14:
                            CurrentPp += 2;
                            MaxPp += 2;
                            return true;
                    }
                    break;

                case 15:
                    switch (MaxPp)
                    {
                        case 15:
                        case 18:
                        case 21:
                            CurrentPp += 3;
                            MaxPp += 3;
                            return true;
                    }
                    break;

                case 20:
                    switch (MaxPp)
                    {
                        case 20:
                        case 24:
                        case 28:
                            CurrentPp += 4;
                            MaxPp += 4;
                            return true;
                    }
                    break;

                case 25:
                    switch (MaxPp)
                    {
                        case 25:
                        case 30:
                        case 35:
                            CurrentPp += 5;
                            MaxPp += 5;
                            return true;
                    }
                    break;

                case 30:
                    switch (MaxPp)
                    {
                        case 30:
                        case 36:
                        case 42:
                            CurrentPp += 6;
                            MaxPp += 6;
                            return true;
                    }
                    break;

                case 35:
                    switch (MaxPp)
                    {
                        case 35:
                        case 42:
                        case 49:
                            CurrentPp += 7;
                            MaxPp += 7;
                            return true;
                    }
                    break;

                case 40:
                    switch (MaxPp)
                    {
                        case 40:
                        case 48:
                        case 56:
                            CurrentPp += 8;
                            MaxPp += 8;
                            return true;
                    }
                    break;
            }

            CurrentPp = Convert.ToInt32(MathHelper.Clamp(CurrentPp, 0, MaxPp));

            return false;
        }

        /// <summary>
        /// Returns the texture representing the category of this move.
        /// </summary>
        public Texture2D GetDamageCategoryImage()
        {
            var r = new Rectangle(0, 0, 0, 0);

            switch (Category)
            {
                case Categories.Physical:
                    r = new Rectangle(115, 0, 28, 14);
                    break;
                case Categories.Special:
                    r = new Rectangle(115, 14, 28, 14);
                    break;
                case Categories.Status:
                    r = new Rectangle(115, 28, 28, 14);
                    break;
            }

            return TextureManager.GetTexture(Path.Combine("GUI", "Menus", "Types"), r, "");
        }

        /// <summary>
        /// Returns a saveable string.
        /// </summary>
        public override string ToString() => $"{OriginalId},{MaxPp},{CurrentPp}";
    }
}
