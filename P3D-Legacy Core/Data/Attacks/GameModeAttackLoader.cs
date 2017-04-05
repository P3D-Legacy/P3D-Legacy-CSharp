using System;
using System.Collections.Generic;
using System.Globalization;
using P3D.Legacy.Core.Resources;
using P3D.Legacy.Core.Resources.Managers;

namespace P3D.Legacy.Core.Pokemon
{
    /// <summary>
    /// Provides an interface to load additional GameMode moves.
    /// </summary>
    public class GameModeAttackLoader
    {

        //The default relative path to load moves from (Content folder).

        const string PATH = "Data\\Moves\\";
        //List of loaded moves.

        static List<BaseAttack> LoadedMoves = new List<BaseAttack>();
        /// <summary>
        /// Load the attack list for the loaded GameMode.
        /// </summary>
        /// <remarks>The game won't try to load the list if the default GameMode is selected.</remarks>
        public static void Load()
        {
            LoadedMoves.Clear();

            if (GameModeManager.ActiveGameMode.IsDefaultGamemode == false)
            {
                foreach (string file in System.IO.Directory.GetFiles(GameController.GamePath + "\\" + GameModeManager.ActiveGameMode.ContentFolder + "\\" + PATH, "*.dat"))
                {
                    LoadMove(file);
                }
            }

            if (LoadedMoves.Count > 0)
            {
                Logger.Debug("Loaded " + LoadedMoves.Count.ToString(NumberFormatInfo.InvariantInfo) + " GameMode move(s).");
            }
        }

        /// <summary>
        /// Loads a move from a file.
        /// </summary>
        /// <param name="file">The file to load the move from.</param>
        private static void LoadMove(string file)
        {
            BaseAttack move = default(BaseAttack);
            //Load the default Pound move.
            move.IsGameModeMove = true;

            string[] content = System.IO.File.ReadAllLines(file);

            string key = "";
            string value = "";

            bool setID = false;
            //Controls if the move sets its ID.

            try
            {
                //Go through lines of the file and set the properties depending on the content.
                //Lines starting with # are comments.
                foreach (string l in content)
                {
                    if (l.Contains("|") == true && l.StartsWith("#") == false)
                    {
                        key = l.Remove(l.IndexOf("|"));
                        value = l.Remove(0, l.IndexOf("|") + 1);

                        switch (key.ToLower())
                        {
                            case "id":
                                move.Id = Convert.ToInt32(value);
                                move.OriginalId = Convert.ToInt32(value);
                                setID = true;
                                break;
                            case "pp":
                                move.CurrentPp = Convert.ToInt32(value);
                                move.MaxPp = Convert.ToInt32(value);
                                move.OriginalPp = Convert.ToInt32(value);
                                break;
                            case "function":
                                move.GameModeFunction = value;
                                break;
                            case "power":
                            case "basepower":
                                move.Power = Convert.ToInt32(value);
                                break;
                            case "accuracy":
                            case "acc":
                                move.Accuracy = Convert.ToInt32(value);
                                break;
                            case "type":
                                move.Type = new Element(value);
                                break;
                            case "category":
                                switch (value.ToLower())
                                {
                                    case "physical":
                                        move.Category = BaseAttack.Categories.Physical;
                                        break;
                                    case "special":
                                        move.Category = BaseAttack.Categories.Special;
                                        break;
                                    case "status":
                                        move.Category = BaseAttack.Categories.Status;
                                        break;
                                }
                                break;
                            case "contestcategory":
                                switch (value.ToLower())
                                {
                                    case "tough":
                                        move.ContestCategory = BaseAttack.ContestCategories.Tough;
                                        break;
                                    case "smart":
                                        move.ContestCategory = BaseAttack.ContestCategories.Smart;
                                        break;
                                    case "beauty":
                                        move.ContestCategory = BaseAttack.ContestCategories.Beauty;
                                        break;
                                    case "cool":
                                        move.ContestCategory = BaseAttack.ContestCategories.Cool;
                                        break;
                                    case "cute":
                                        move.ContestCategory = BaseAttack.ContestCategories.Cute;
                                        break;
                                }
                                break;
                            case "name":
                                move.Name = value;
                                break;
                            case "description":
                                move.Description = value;
                                break;
                            case "criticalchance":
                            case "critical":
                                move.CriticalChance = Convert.ToInt32(value);
                                break;
                            case "hmmove":
                            case "ishmmove":
                                move.IsHmMove = Convert.ToBoolean(value);
                                break;
                            case "priority":
                                move.Priority = Convert.ToInt32(value);
                                break;
                            case "timestoattack":
                            case "tta":
                                move.TimesToAttack = Convert.ToInt32(value);

                                break;
                            case "makescontact":
                            case "contact":
                                move.MakesContact = Convert.ToBoolean(value);
                                break;
                            case "protectaffected":
                                move.ProtectAffected = Convert.ToBoolean(value);
                                break;
                            case "magiccoataffected":
                                move.MagicCoatAffected = Convert.ToBoolean(value);
                                break;
                            case "snatchaffected":
                                move.SnatchAffected = Convert.ToBoolean(value);
                                break;
                            case "mirrormoveaffected":
                                move.MirrorMoveAffected = Convert.ToBoolean(value);
                                break;
                            case "kingsrockaffected":
                                move.KingsrockAffected = Convert.ToBoolean(value);
                                break;
                            case "counteraffected":
                                move.CounterAffected = Convert.ToBoolean(value);
                                break;
                            case "disabledduringgravity":
                            case "disabledwhilegravity":
                                move.DisabledWhileGravity = Convert.ToBoolean(value);
                                break;
                            case "useeffectiveness":
                                move.UseEffectiveness = Convert.ToBoolean(value);
                                break;
                            case "ishealingmove":
                                move.IsHealingMove = Convert.ToBoolean(value);
                                break;
                            case "removesfrozen":
                                move.RemovesFrozen = Convert.ToBoolean(value);
                                break;
                            case "isrecoilmove":
                                move.IsRecoilMove = Convert.ToBoolean(value);
                                break;
                            case "ispunchingmove":
                                move.IsPunchingMove = Convert.ToBoolean(value);
                                break;
                            case "immunityaffected":
                                move.ImmunityAffected = Convert.ToBoolean(value);
                                break;
                            case "isdamagingmove":
                                move.IsDamagingMove = Convert.ToBoolean(value);
                                break;
                            case "isprotectmove":
                                move.IsProtectMove = Convert.ToBoolean(value);
                                break;
                            case "issoundmove":
                                move.IsSoundMove = Convert.ToBoolean(value);
                                break;
                            case "isaffectedbysubstitute":
                                move.IsAffectedBySubstitute = Convert.ToBoolean(value);
                                break;
                            case "isonehitkomove":
                                move.IsOneHitKoMove = Convert.ToBoolean(value);
                                break;
                            case "iswonderguardaffected":
                                move.IsWonderGuardAffected = Convert.ToBoolean(value);
                                break;
                            case "useaccevasion":
                                move.UseAccEvasion = Convert.ToBoolean(value);
                                break;
                            case "canhitinmidair":
                                move.CanHitInMidAir = Convert.ToBoolean(value);
                                break;
                            case "canhitunderground":
                                move.CanHitUnderground = Convert.ToBoolean(value);
                                break;
                            case "canhitunderwater":
                                move.CanHitUnderwater = Convert.ToBoolean(value);
                                break;
                            case "canhitsleeping":
                                move.CanHitSleeping = Convert.ToBoolean(value);
                                break;
                            case "cangainstab":
                                move.CanGainStab = Convert.ToBoolean(value);
                                break;
                            case "ispowdermove":
                                move.IsPowderMove = Convert.ToBoolean(value);
                                break;
                            case "istrappingmove":
                                move.IsTrappingMove = Convert.ToBoolean(value);
                                break;
                            case "ispulsemove":
                                move.IsPulseMove = Convert.ToBoolean(value);
                                break;
                            case "isbulletmove":
                                move.IsBulletMove = Convert.ToBoolean(value);
                                break;
                            case "isjawmove":
                                move.IsJawMove = Convert.ToBoolean(value);
                                break;
                            case "useoppdefense":
                                move.UseOppDefense = Convert.ToBoolean(value);
                                break;
                            case "useoppevasion":
                                move.UseOppEvasion = Convert.ToBoolean(value);
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //If an error occurs loading a move, log the error.
                Logger.Log(Logger.LogTypes.ErrorMessage, "GameModeAttackLoader.vb: Error loading GameMode move from file \"" + file + "\": " + ex.Message + "; Last Key/Value pair successfully loaded: " + key + "|" + value);
            }

            if (setID == true)
            {
                if (move.Id >= 1000)
                {
                    var testMove = BaseAttack.GetAttackByID(move.Id);
                    if (testMove.IsDefaultMove == true)
                    {
                        LoadedMoves.Add(move);
                        //Add the move.
                    }
                    else
                    {
                        Logger.Log(Logger.LogTypes.ErrorMessage, "GameModeAttackLoader.vb: User defined moves are not allowed to have an ID of an already existing move or an ID below 1000. The ID for the move loaded from \"" + file + "\" has the ID " + move.Id.ToString(NumberFormatInfo.InvariantInfo) + ", which is the ID of an already existing move (" + testMove.Name + ").");
                    }
                }
                else
                {
                    Logger.Log(Logger.LogTypes.ErrorMessage, "GameModeAttackLoader.vb: User defined moves are not allowed to have an ID of an already existing move or an ID below 1000. The ID for the move loaded from \"" + file + "\" has the ID " + move.Id.ToString(NumberFormatInfo.InvariantInfo) + ", which is smaller than 1000.");
                }
            }
            else
            {
                Logger.Log(Logger.LogTypes.ErrorMessage, "GameModeAttackLoader.vb: User defined moves must set their ID through the \"ID\" property, however the move loaded from \"" + file + "\" has no ID set so it will be ignored.");
            }
        }

        /// <summary>
        /// Returns a custom move based on its ID.
        /// </summary>
        /// <param name="ID">The ID of the custom move.</param>
        /// <returns>Returns a move or nothing.</returns>
        public static BaseAttack GetAttackByID(int ID)
        {
            foreach (var m in LoadedMoves)
            {
                if (m.Id == ID)
                {
                    return m;
                }
            }
            return null;
        }

    }
}
