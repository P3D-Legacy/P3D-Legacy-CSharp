using System.Collections.Generic;
using System.Linq;

using P3D.Legacy.Core.Pokemon;
using P3D.Legacy.Core.Screens;

namespace P3D.Legacy.Core.Battle.BattleSystemV2
{
    /// <summary>
    /// Represents a position on the battle field by targeting the Pokémon.
    /// </summary>
    public class PokemonTarget
    {

        /// <summary>
        /// The positions on the battle field a Pokémon can get targeted on.
        /// </summary>
        public enum Targets
        {
            OwnLeft,
            OwnCenter,
            OwnRight,
            OppLeft,
            OppCenter,
            OppRight
        }


        private Targets _target = Targets.OwnCenter;
        /// <summary>
        /// Creates a new instance of a Pokémon Target.
        /// </summary>
        /// <param name="t">The Target type of this Pokémon Target.</param>
        public PokemonTarget(Targets t)
        {
            this._target = t;
        }

        /// <summary>
        /// Creates a new instance of a Pokémon Target.
        /// </summary>
        /// <param name="s">The Target type of this Pokémon Target.</param>
        public PokemonTarget(string s)
        {
            switch (s.ToLower())
            {
                case "ownleft":
                    this._target = Targets.OwnLeft;
                    break;
                case "ownright":
                    this._target = Targets.OwnRight;
                    break;
                case "owncenter":
                    this._target = Targets.OwnCenter;
                    break;
                case "oppleft":
                    this._target = Targets.OppLeft;
                    break;
                case "oppright":
                    this._target = Targets.OppRight;
                    break;
                case "oppcenter":
                    this._target = Targets.OppCenter;
                    break;
            }
        }

        /// <summary>
        /// The target of this PokémonTarget.
        /// </summary>
        public Targets Target
        {
            get { return this._target; }
        }

        public static bool operator ==(PokemonTarget FirstTarget, PokemonTarget SecondTarget)
        {
            return FirstTarget._target == SecondTarget._target;
        }

        public static bool operator !=(PokemonTarget FirstTarget, PokemonTarget SecondTarget)
        {
            return FirstTarget._target != SecondTarget._target;
        }

        /// <summary>
        /// Creates a target set to Own Left.
        /// </summary>
        public static PokemonTarget OwnLeft
        {
            get { return new PokemonTarget(Targets.OwnLeft); }
        }

        /// <summary>
        /// Creates a target set to Own Center.
        /// </summary>
        public static PokemonTarget OwnCenter
        {
            get { return new PokemonTarget(Targets.OwnCenter); }
        }

        /// <summary>
        /// Creates a target set to Own Right.
        /// </summary>
        public static PokemonTarget OwnRight
        {
            get { return new PokemonTarget(Targets.OwnRight); }
        }

        /// <summary>
        /// Creates a target set to Opp Left.
        /// </summary>
        public static PokemonTarget OppLeft
        {
            get { return new PokemonTarget(Targets.OppLeft); }
        }

        /// <summary>
        /// Creates a target set to Opp Center.
        /// </summary>
        public static PokemonTarget OppCenter
        {
            get { return new PokemonTarget(Targets.OppCenter); }
        }

        /// <summary>
        /// Creates a target set to Opp Right.
        /// </summary>
        public static PokemonTarget OppRight
        {
            get { return new PokemonTarget(Targets.OppRight); }
        }

        /// <summary>
        /// Reverses the sides of the target (Own => Opp; Opp => Own)
        /// </summary>
        public void Reverse()
        {
            switch (this._target)
            {
                case Targets.OwnLeft:
                    this._target = Targets.OppLeft;
                    break;
                case Targets.OwnCenter:
                    this._target = Targets.OppCenter;
                    break;
                case Targets.OwnRight:
                    this._target = Targets.OppRight;
                    break;
                case Targets.OppLeft:
                    this._target = Targets.OwnLeft;
                    break;
                case Targets.OppCenter:
                    this._target = Targets.OwnCenter;
                    break;
                case Targets.OppRight:
                    this._target = Targets.OwnRight;
                    break;
            }
        }

        /// <summary>
        /// Returns a string that represents the target.
        /// </summary>
        public override string ToString()
        {
            return this._target.ToString();
        }

        /// <summary>
        /// This converts the Attack Target in an AttackV2 into a useable Pokemon Target on the field.
        /// </summary>
        /// <param name="AttackTarget">The Attack Target that should get converted.</param>
        /// <returns>A list of PokemonTargets that can be accessed with the Attack Target.</returns>
        /// <remarks>The list includes targets for which no Pokémon exist, so call the GetValidPokemonTargets function afterwards.</remarks>
        public List<PokemonTarget> ConvertAttackTarget(BaseAttack.Targets AttackTarget)
        {
            if (AttackTarget == BaseAttack.Targets.All)
            {
                return new [] { OwnLeft, OwnCenter, OwnRight, OppLeft, OppCenter, OppRight }.ToList();
            }
            if (AttackTarget == BaseAttack.Targets.Self)
            {
                return new[] { this }.ToList();
            }

            switch (this._target)
            {
                case Targets.OwnLeft:
                    switch (AttackTarget)
                    {
                        case BaseAttack.Targets.OneAdjacentTarget:
                        case BaseAttack.Targets.AllAdjacentTargets:
                            return new[] { OwnCenter, OppLeft, OppCenter }.ToList();
                        case BaseAttack.Targets.OneAdjacentFoe:
                        case BaseAttack.Targets.AllAdjacentFoes:
                            return new[] { OppLeft, OppCenter }.ToList();
                        case BaseAttack.Targets.OneAdjacentAlly:
                        case BaseAttack.Targets.AllAdjacentAllies:
                            return new[] { OwnCenter }.ToList();
                        case BaseAttack.Targets.OneTarget:
                        case BaseAttack.Targets.AllTargets:
                            return new[] { OwnCenter, OwnRight, OppLeft, OppCenter, OppRight }.ToList();
                        case BaseAttack.Targets.OneFoe:
                        case BaseAttack.Targets.AllFoes:
                            return new[] { OppLeft, OppCenter, OppRight }.ToList();
                        case BaseAttack.Targets.OneAlly:
                        case BaseAttack.Targets.AllAllies:
                            return new[] { OwnCenter, OwnRight }.ToList();
                    }
                    break;
                case Targets.OwnCenter:
                    switch (AttackTarget)
                    {
                        case BaseAttack.Targets.OneAdjacentTarget:
                        case BaseAttack.Targets.OneTarget:
                        case BaseAttack.Targets.AllAdjacentTargets:
                        case BaseAttack.Targets.AllTargets:
                            return new[] { OwnLeft, OwnRight, OppLeft, OppCenter, OppRight }.ToList();
                        case BaseAttack.Targets.OneAdjacentFoe:
                        case BaseAttack.Targets.OneFoe:
                        case BaseAttack.Targets.AllAdjacentFoes:
                        case BaseAttack.Targets.AllFoes:
                            return new[] { OppLeft, OppCenter, OppRight }.ToList();
                        case BaseAttack.Targets.OneAdjacentAlly:
                        case BaseAttack.Targets.OneAlly:
                        case BaseAttack.Targets.AllAdjacentAllies:
                        case BaseAttack.Targets.AllAllies:
                            return new[] { OwnLeft, OwnRight }.ToList();
                    }
                    break;
                case Targets.OwnRight:
                    switch (AttackTarget)
                    {
                        case BaseAttack.Targets.OneAdjacentTarget:
                        case BaseAttack.Targets.AllAdjacentTargets:
                            return new[] { OwnCenter, OppCenter, OppRight }.ToList();
                        case BaseAttack.Targets.OneAdjacentFoe:
                        case BaseAttack.Targets.AllAdjacentFoes:
                            return new[] { OppCenter, OppRight }.ToList();
                        case BaseAttack.Targets.OneAdjacentAlly:
                        case BaseAttack.Targets.AllAdjacentAllies:
                            return new[] { OwnCenter }.ToList();
                        case BaseAttack.Targets.OneTarget:
                        case BaseAttack.Targets.AllTargets:
                            return new[] { OwnLeft, OwnCenter, OppLeft, OppCenter, OppRight }.ToList();
                        case BaseAttack.Targets.OneFoe:
                        case BaseAttack.Targets.AllFoes:
                            return new[] { OppLeft, OppCenter, OppRight }.ToList();
                        case BaseAttack.Targets.OneAlly:
                        case BaseAttack.Targets.AllAllies:
                            return new[] { OwnLeft, OwnCenter }.ToList();
                    }
                    break;
                case Targets.OppLeft:
                    switch (AttackTarget)
                    {
                        case BaseAttack.Targets.OneAdjacentTarget:
                        case BaseAttack.Targets.AllAdjacentTargets:
                            return new[] { OwnLeft, OwnCenter, OppCenter }.ToList();
                        case BaseAttack.Targets.OneAdjacentFoe:
                        case BaseAttack.Targets.AllAdjacentFoes:
                            return new[] { OwnLeft, OwnCenter }.ToList();
                        case BaseAttack.Targets.OneAdjacentAlly:
                        case BaseAttack.Targets.AllAdjacentAllies:
                            return new[] { OppCenter }.ToList();
                        case BaseAttack.Targets.OneTarget:
                        case BaseAttack.Targets.AllTargets:
                            return new[] { OwnLeft, OwnCenter, OwnRight, OppCenter, OppRight }.ToList();
                        case BaseAttack.Targets.OneFoe:
                        case BaseAttack.Targets.AllFoes:
                            return new[] { OwnLeft, OwnCenter, OwnRight }.ToList();
                        case BaseAttack.Targets.OneAlly:
                        case BaseAttack.Targets.AllAllies:
                            return new[] { OppCenter, OppRight }.ToList();
                    }
                    break;
                case Targets.OppCenter:
                    switch (AttackTarget)
                    {
                        case BaseAttack.Targets.OneAdjacentTarget:
                        case BaseAttack.Targets.OneTarget:
                        case BaseAttack.Targets.AllAdjacentTargets:
                        case BaseAttack.Targets.AllTargets:
                            return new[] { OwnLeft, OwnCenter, OwnRight, OppLeft, OppRight }.ToList();
                        case BaseAttack.Targets.OneAdjacentFoe:
                        case BaseAttack.Targets.OneFoe:
                        case BaseAttack.Targets.AllAdjacentFoes:
                        case BaseAttack.Targets.AllFoes:
                            return new[] { OwnLeft, OwnCenter, OwnRight }.ToList();
                        case BaseAttack.Targets.OneAdjacentAlly:
                        case BaseAttack.Targets.OneAlly:
                        case BaseAttack.Targets.AllAdjacentAllies:
                        case BaseAttack.Targets.AllAllies:
                            return new[] { OppLeft, OppRight }.ToList();
                    }
                    break;
                case Targets.OppRight:
                    switch (AttackTarget)
                    {
                        case BaseAttack.Targets.OneAdjacentTarget:
                        case BaseAttack.Targets.AllAdjacentTargets:
                            return new[] { OwnCenter, OwnRight, OppCenter }.ToList();
                        case BaseAttack.Targets.OneAdjacentFoe:
                        case BaseAttack.Targets.AllAdjacentFoes:
                            return new[] { OwnCenter, OwnRight }.ToList();
                        case BaseAttack.Targets.OneAdjacentAlly:
                        case BaseAttack.Targets.AllAdjacentAllies:
                            return new[] { OppCenter }.ToList();
                        case BaseAttack.Targets.OneTarget:
                        case BaseAttack.Targets.AllTargets:
                            return new[] { OwnLeft, OwnCenter, OwnRight, OppRight, OppCenter }.ToList();
                        case BaseAttack.Targets.OneFoe:
                        case BaseAttack.Targets.AllFoes:
                            return new[] { OwnLeft, OwnCenter, OwnRight }.ToList();
                        case BaseAttack.Targets.OneAlly:
                        case BaseAttack.Targets.AllAllies:
                            return new[] { OppLeft, OppCenter }.ToList();
                    }
                    break;
            }

            return new List<PokemonTarget>();
        }

        /// <summary>
        /// Converts a list of Pokémon targets to a list of valid Pokémon targets that have a Pokémon profile assigned to them on the battle field.
        /// </summary>
        /// <param name="BattleScreen">The reference to the BattleScreen.</param>
        /// <param name="l">The list of Pokémon targets.</param>
        public static List<PokemonTarget> GetValidPokemonTargets(BaseBattleScreen BattleScreen, List<PokemonTarget> l)
        {
            List<PokemonTarget> returnList = new List<PokemonTarget>();

            foreach (PokemonTarget Target in l)
            {
                if ((BattleScreen.GetProfile(Target) != null))
                {
                    returnList.Add(Target);
                }
            }

            return returnList;
        }

        /// <summary>
        /// If the Pokémon target is on the own side of the field.
        /// </summary>
        public bool IsOwn
        {
            get
            {
                switch (this._target)
                {
                    case Targets.OwnCenter:
                    case Targets.OwnLeft:
                    case Targets.OwnRight:
                        return true;
                    case Targets.OppCenter:
                    case Targets.OppLeft:
                    case Targets.OppRight:
                        return false;
                }

                //Wont ever happen.
                return true;
            }
        }

    }

}
