namespace P3D.Legacy.Core.Pokemon
{
    public class Nature
    {
        private enum StatNames
        {
            Attack,
            Defense,
            SpAttack,
            SpDefense,
            Speed
        }

        public static float GetMultiplier(BasePokemon.Natures nature, string statName)
        {
            StatNames stat = StatNames.Attack;

            switch (statName.ToLower())
            {
                case "attack":
                case "atk":
                    stat = StatNames.Attack;
                    break;
                case "defense":
                case "def":
                    stat = StatNames.Defense;
                    break;
                case "spattack":
                case "spatk":
                case "specialattack":
                    stat = StatNames.SpAttack;
                    break;
                case "spdefense":
                case "spdef":
                case "specialdefense":
                    stat = StatNames.SpDefense;
                    break;
                case "speed":
                    stat = StatNames.Speed;
                    break;
            }

            switch (nature)
            {
                case BasePokemon.Natures.Hardy:
                    return 1;
                case BasePokemon.Natures.Lonely:
                    return CalcMulti(stat, StatNames.Attack, StatNames.Defense);
                case BasePokemon.Natures.Brave:
                    return CalcMulti(stat, StatNames.Attack, StatNames.Speed);
                case BasePokemon.Natures.Adamant:
                    return CalcMulti(stat, StatNames.Attack, StatNames.SpAttack);
                case BasePokemon.Natures.Naughty:
                    return CalcMulti(stat, StatNames.Attack, StatNames.SpDefense);
                case BasePokemon.Natures.Bold:
                    return CalcMulti(stat, StatNames.Defense, StatNames.Attack);
                case BasePokemon.Natures.Docile:
                    return 1;
                case BasePokemon.Natures.Relaxed:
                    return CalcMulti(stat, StatNames.Defense, StatNames.Speed);
                case BasePokemon.Natures.Impish:
                    return CalcMulti(stat, StatNames.Defense, StatNames.SpAttack);
                case BasePokemon.Natures.Lax:
                    return CalcMulti(stat, StatNames.Defense, StatNames.SpDefense);
                case BasePokemon.Natures.Timid:
                    return CalcMulti(stat, StatNames.Speed, StatNames.Attack);
                case BasePokemon.Natures.Hasty:
                    return CalcMulti(stat, StatNames.Speed, StatNames.Defense);
                case BasePokemon.Natures.Jolly:
                    return CalcMulti(stat, StatNames.Speed, StatNames.SpAttack);
                case BasePokemon.Natures.Naive:
                    return CalcMulti(stat, StatNames.Speed, StatNames.SpDefense);
                case BasePokemon.Natures.Modest:
                    return CalcMulti(stat, StatNames.SpAttack, StatNames.Attack);
                case BasePokemon.Natures.Mild:
                    return CalcMulti(stat, StatNames.SpAttack, StatNames.Defense);
                case BasePokemon.Natures.Quiet:
                    return CalcMulti(stat, StatNames.SpAttack, StatNames.Speed);
                case BasePokemon.Natures.Bashful:
                    return 1;
                case BasePokemon.Natures.Rash:
                    return CalcMulti(stat, StatNames.SpAttack, StatNames.SpDefense);
                case BasePokemon.Natures.Calm:
                    return CalcMulti(stat, StatNames.SpDefense, StatNames.Attack);
                case BasePokemon.Natures.Gentle:
                    return CalcMulti(stat, StatNames.SpDefense, StatNames.Defense);
                case BasePokemon.Natures.Sassy:
                    return CalcMulti(stat, StatNames.SpDefense, StatNames.Speed);
                case BasePokemon.Natures.Careful:
                    return CalcMulti(stat, StatNames.SpDefense, StatNames.SpAttack);
                case BasePokemon.Natures.Quirky:
                    return 1;
                case BasePokemon.Natures.Serious:
                    return 1;
                default:
                    return 1;
            }
        }

        private static float CalcMulti(StatNames calcStat, StatNames positiveStat, StatNames negativeStat)
        {
            if (calcStat == positiveStat)
            {
                return 1.1f;
            }
            else if (calcStat == negativeStat)
            {
                return 0.9f;
            }
            else
            {
                return 1;
            }
        }
    }
}
