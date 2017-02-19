using System;
using System.Collections.Generic;
using System.Linq;

using P3D.Legacy.Core.Extensions;
using P3D.Legacy.Core.Screens;
using P3D.Legacy.Core.World;

namespace P3D.Legacy.Core.Pokemon
{
    public class EvolutionCondition
    {
        public enum ConditionTypes
        {
            Level,
            Friendship,
            Item,
            HoldItem,
            Place,
            Trade,
            Gender,
            AtkDef,
            DefAtk,
            DefEqualsAtk,
            Move,
            DayTime,
            InParty,
            InPartyType,
            Weather
        }

        public enum EvolutionTrigger
        {
            None,
            LevelUp,
            Trading,
            ItemUse
        }

        public struct Condition
        {
            public ConditionTypes ConditionType;
            public string Argument;
            public EvolutionTrigger Trigger;
        }

        public int Evolution = 0;

        public List<Condition> Conditions = new List<Condition>();
        public void SetEvolution(int evolution)
        {
            this.Evolution = evolution;
        }

        public void AddCondition(string type, string arg, string trigger)
        {
            Condition c = new Condition();
            c.Argument = arg;

            switch (type.ToLower())
            {
                case "level":
                    c.ConditionType = ConditionTypes.Level;
                    break;
                case "item":
                    c.ConditionType = ConditionTypes.Item;
                    break;
                case "holditem":
                    c.ConditionType = ConditionTypes.HoldItem;
                    break;
                case "location":
                case "place":
                    c.ConditionType = ConditionTypes.Place;
                    break;
                case "friendship":
                    c.ConditionType = ConditionTypes.Friendship;
                    break;
                case "trade":
                    c.ConditionType = ConditionTypes.Trade;
                    break;
                case "move":
                    c.ConditionType = ConditionTypes.Move;
                    break;
                case "gender":
                    c.ConditionType = ConditionTypes.Gender;
                    break;
                case "atkdef":
                    c.ConditionType = ConditionTypes.AtkDef;
                    break;
                case "defatk":
                    c.ConditionType = ConditionTypes.DefAtk;
                    break;
                case "defequalsatk":
                    c.ConditionType = ConditionTypes.DefEqualsAtk;
                    break;
                case "daytime":
                    c.ConditionType = ConditionTypes.DayTime;
                    break;
                case "inparty":
                    c.ConditionType = ConditionTypes.InParty;
                    break;
                case "inpartytype":
                    c.ConditionType = ConditionTypes.InPartyType;
                    break;
                case "weather":
                    c.ConditionType = ConditionTypes.Weather;
                    break;
            }

            switch (trigger.ToLower())
            {
                case "none":
                case "":
                    c.Trigger = EvolutionTrigger.None;
                    break;
                case "level":
                case "levelup":
                    c.Trigger = EvolutionTrigger.LevelUp;
                    break;
                case "trade":
                case "trading":
                    c.Trigger = EvolutionTrigger.Trading;
                    break;
                case "item":
                case "itemuse":
                    c.Trigger = EvolutionTrigger.ItemUse;
                    break;
            }

            this.Conditions.Add(c);
        }

        public int Count
        {
            get { return Conditions.Count; }
        }

        /// <summary>
        /// Returns the evolution of a Pokémon. Returns 0 if not successful
        /// </summary>
        /// <param name="p">The Pokémon to get the evolution from.</param>
        /// <param name="trigger">The trigger that triggered the evolution.</param>
        /// <param name="arg">An argument (for example Item ID)</param>
        public static int EvolutionNumber(BasePokemon p, EvolutionTrigger trigger, string arg)
        {
            if (trigger == EvolutionTrigger.LevelUp || trigger == EvolutionTrigger.Trading)
            {
                if ((p.Item != null))
                {
                    if (p.Item.Id == 112)
                    {
                        return 0;
                    }
                }
            }

            List<int> possibleEvolutions = new List<int>();

            foreach (EvolutionCondition e in p.EvolutionConditions)
            {
                bool canEvolve = true;

                foreach (Condition c in e.Conditions)
                {
                    if (c.Trigger != trigger)
                    {
                        canEvolve = false;
                    }
                }

                if (canEvolve)
                {
                    foreach (Condition c in e.Conditions)
                    {
                        switch (c.ConditionType)
                        {
                            case ConditionTypes.AtkDef:
                                if (p.Attack <= p.Defense)
                                {
                                    canEvolve = false;
                                }
                                break;
                            case ConditionTypes.DayTime:
                                List<string> daytimes = c.Argument.Split(Convert.ToChar(";")).ToList();

                                if (!daytimes.Contains(Convert.ToString(Convert.ToInt32(BaseWorld.GetTime))))
                                {
                                    canEvolve = false;
                                }
                                break;
                            case ConditionTypes.DefAtk:
                                if (p.Defense <= p.Attack)
                                {
                                    canEvolve = false;
                                }
                                break;
                            case ConditionTypes.DefEqualsAtk:
                                if (p.Attack != p.Defense)
                                {
                                    canEvolve = false;
                                }
                                break;
                            case ConditionTypes.Friendship:
                                if (p.Friendship < Convert.ToInt32(c.Argument))
                                {
                                    canEvolve = false;
                                }
                                break;
                            case ConditionTypes.Gender:
                                if ((int) p.Gender != Convert.ToInt32(c.Argument))
                                {
                                    canEvolve = false;
                                }
                                break;
                            case ConditionTypes.HoldItem:
                                if (p.Item == null)
                                {
                                    canEvolve = false;
                                }
                                else
                                {
                                    if (p.Item.Id != Convert.ToInt32(c.Argument))
                                    {
                                        canEvolve = false;
                                    }
                                }
                                break;
                            case ConditionTypes.InParty:
                            {
                                bool isInParty = false;
                                foreach (var pokemon in Core.Player.Pokemons)
                                {
                                    if (pokemon.Number == Convert.ToInt32(c.Argument))
                                    {
                                        isInParty = true;
                                        break; // TODO: might not be correct. Was : Exit For
                                    }
                                }

                                if (isInParty == false)
                                {
                                    canEvolve = false;
                                }
                            }
                                break;
                            case ConditionTypes.InPartyType:
                            {
                                bool isInParty = false;
                                foreach (var pokemon in Core.Player.Pokemons)
                                {
                                    if (pokemon.IsType(new Element(c.Argument).Type) == true)
                                    {
                                        isInParty = true;
                                        break; // TODO: might not be correct. Was : Exit For
                                    }
                                }

                                if (isInParty == false)
                                {
                                    canEvolve = false;
                                }
                            }
                                break;
                            case ConditionTypes.Item:
                                if (Convert.ToInt32(arg) != Convert.ToInt32(c.Argument))
                                {
                                    canEvolve = false;
                                }
                                break;
                            case ConditionTypes.Level:
                                if (p.Level < Convert.ToInt32(c.Argument))
                                {
                                    canEvolve = false;
                                }
                                break;
                            case ConditionTypes.Move:
                                bool hasattack = false;
                                foreach (var a in p.Attacks)
                                {
                                    if (a.Id == Convert.ToInt32(c.Argument))
                                    {
                                        hasattack = true;
                                        break; // TODO: might not be correct. Was : Exit For
                                    }
                                }

                                if (hasattack == false)
                                {
                                    canEvolve = false;
                                }
                                break;
                            case ConditionTypes.Place:
                                if (Screen.Level.MapName.ToLower() != c.Argument.ToLower())
                                {
                                    canEvolve = false;
                                }
                                break;
                            case ConditionTypes.Trade:
                                if (c.Argument.IsNumeric())
                                {
                                    if (Convert.ToInt32(c.Argument) > 0)
                                    {
                                        if (Convert.ToInt32(c.Argument) != Convert.ToInt32(arg))
                                        {
                                            canEvolve = false;
                                        }
                                    }
                                }
                                break;
                            case ConditionTypes.Weather:
                                if (BaseWorld.GetCurrentRegionWeather().ToString().ToLower() != c.Argument.ToLower())
                                {
                                    canEvolve = false;
                                }
                                break;
                        }
                    }
                }

                if (canEvolve == true)
                {
                    possibleEvolutions.Add(e.Evolution);
                }
            }

            if (possibleEvolutions.Count > 0)
            {
                return possibleEvolutions[Core.Random.Next(0, possibleEvolutions.Count)];
            }

            return 0;
        }

    }
}
