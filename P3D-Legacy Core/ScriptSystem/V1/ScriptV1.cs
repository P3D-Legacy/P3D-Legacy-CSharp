using Microsoft.Xna.Framework;
using P3D.Legacy.Core.Extensions;
using P3D.Legacy.Core.Screens;
using System;
using System.Collections.Generic;
using System.Linq;

using P3D.Legacy.Core.Entities;
using P3D.Legacy.Core.Pokemon;
using P3D.Legacy.Core.Resources;
using P3D.Legacy.Core.Resources.Sound;
using P3D.Legacy.Core.World;

namespace P3D.Legacy.Core.ScriptSystem.V1
{
    public class ScriptV1
    {
        public enum ScriptTypes : int
        {
            Move = 0,
            MoveAsync = 1,
            MovePlayer = 2,
            Turn = 3,
            TurnPlayer = 4,
            Warp = 5,
            WarpPlayer = 6,
            Heal = 7,
            ViewPokemonImage = 8,
            GiveItem = 9,
            RemoveItem = 10,
            GetBadge = 11,

            Pokemon = 12,
            Npc = 13,
            Player = 14,
            Text = 15,
            Options = 16,
            SelectCase = 17,
            Wait = 18,
            Camera = 19,
            Battle = 20,
            Script = 21,
            Trainer = 22,
            Achievement = 23,
            Action = 24,
            Music = 25,
            Sound = 26,
            Register = 27,
            Unregister = 28,
            MessageBulb = 29,
            Entity = 30,
            Environment = 31,
            Level = 33,

            SwitchWhen = 34,
            SwitchEndWhen = 35,
            SwitchIf = 36,
            SwitchThen = 37,
            SwitchElse = 38,
            SwitchEndIf = 39,
            SwitchEnd = 40
        }


        public ScriptTypes ScriptType = ScriptTypes.Text;

        public string Value = "";
        public bool Started = false;
        public bool IsReady = false;

        public bool CanContinue = false;
        public void Initialize(string line)
        {
            if (line.StartsWith("@"))
            {
                line = line.Remove(0, 1);

                string script = line;
                string command = "";

                if (line.Contains(":"))
                {
                    script = line.Remove(line.IndexOf(":", StringComparison.Ordinal));
                    command = line.Remove(0, line.IndexOf(":", StringComparison.Ordinal) + 1);
                }

                switch (script)
                {
                    case "Move":
                        if (command.StartsWith("Async,"))
                        {
                            command = command.Remove(0, 6);
                            ScriptType = ScriptTypes.MoveAsync;
                        }
                        else if (command.StartsWith("Player,"))
                        {
                            command = command.Remove(0, 7);
                            ScriptType = ScriptTypes.MovePlayer;
                        }
                        else
                        {
                            ScriptType = ScriptTypes.Move;
                        }

                        Value = command;
                        break;
                    case "Turn":
                        if (command.StartsWith("Player,"))
                        {
                            command = command.Remove(0, 7);
                            ScriptType = ScriptTypes.TurnPlayer;
                        }
                        else
                        {
                            ScriptType = ScriptTypes.Turn;
                        }

                        Value = command;
                        break;
                    case "Warp":
                        if (command.StartsWith("Player,"))
                        {
                            command = command.Remove(0, 7);
                            ScriptType = ScriptTypes.WarpPlayer;
                        }
                        else
                        {
                            ScriptType = ScriptTypes.Warp;
                        }

                        Value = command;
                        break;
                    case "Heal":
                        ScriptType = ScriptTypes.Heal;
                        Value = command;
                        break;
                    case "ViewPokemonImage":
                        ScriptType = ScriptTypes.ViewPokemonImage;
                        Value = command;
                        break;
                    case "GiveItem":
                        ScriptType = ScriptTypes.GiveItem;
                        Value = command;
                        break;
                    case "RemoveItem":
                        ScriptType = ScriptTypes.RemoveItem;
                        Value = command;
                        break;
                    case "GetBadge":
                        ScriptType = ScriptTypes.GetBadge;
                        Value = command;

                        break;


                    case "Action":
                        ScriptType = ScriptTypes.Action;
                        Value = command;
                        break;
                    case "Music":
                        ScriptType = ScriptTypes.Music;
                        Value = command;
                        break;
                    case "Sound":
                        ScriptType = ScriptTypes.Sound;
                        Value = command;
                        break;
                    case "Text":
                        ScriptType = ScriptTypes.Text;
                        Value = command;
                        break;
                    case "Options":
                        ScriptType = ScriptTypes.Options;
                        Value = command;
                        break;
                    case "Wait":
                        ScriptType = ScriptTypes.Wait;
                        Value = command;
                        break;
                    case "Register":
                        ScriptType = ScriptTypes.Register;
                        Value = command;
                        break;
                    case "Unregister":
                        ScriptType = ScriptTypes.Unregister;
                        Value = command;
                        break;
                    case "NPC":
                        ScriptType = ScriptTypes.Npc;
                        Value = command;
                        break;
                    case "Achievement":
                        ScriptType = ScriptTypes.Achievement;
                        Value = command;
                        break;
                    case "Trainer":
                        ScriptType = ScriptTypes.Trainer;
                        Value = command;
                        break;
                    case "Battle":
                        ScriptType = ScriptTypes.Battle;
                        Value = command;
                        break;
                    case "Script":
                        ScriptType = ScriptTypes.Script;
                        Value = command;
                        break;
                    case "Bulb":
                    case "MessageBulb":
                        ScriptType = ScriptTypes.MessageBulb;
                        Value = command;
                        break;
                    case "Camera":
                        ScriptType = ScriptTypes.Camera;
                        Value = command;
                        break;
                    case "Pokemon":
                        ScriptType = ScriptTypes.Pokemon;
                        Value = command;
                        break;
                    case "Player":
                        ScriptType = ScriptTypes.Player;
                        Value = command;
                        break;
                    case "Entity":
                        ScriptType = ScriptTypes.Entity;
                        Value = command;
                        break;
                    case "Environment":
                        ScriptType = ScriptTypes.Environment;
                        Value = command;
                        break;
                    case "Level":
                        ScriptType = ScriptTypes.Level;
                        Value = command;
                        break;
                }
            }
            else if (line.StartsWith(":"))
            {
                line = line.Remove(0, 1);

                string script = "";
                string command = "";

                if (line.Contains(":"))
                {
                    script = line.Remove(line.IndexOf(":", StringComparison.Ordinal));
                    command = line.Remove(0, line.IndexOf(":", StringComparison.Ordinal) + 1);
                }
                else
                {
                    script = line;
                    command = "";
                }

                switch (script)
                {
                    case "if":
                        ScriptType = ScriptTypes.SwitchIf;
                        Value = command;
                        break;
                    case "when":
                        ScriptType = ScriptTypes.SwitchWhen;
                        Value = command;
                        break;
                    case "then":
                        ScriptType = ScriptTypes.SwitchThen;
                        break;
                    case "else":
                        ScriptType = ScriptTypes.SwitchElse;
                        break;
                    case "endif":
                        ScriptType = ScriptTypes.SwitchEndIf;
                        break;
                    case "endwhen":
                        ScriptType = ScriptTypes.SwitchEndWhen;
                        break;
                    case "end":
                        ScriptType = ScriptTypes.SwitchEnd;
                        break;
                    case "select":
                        ScriptType = ScriptTypes.SelectCase;
                        Value = command;
                        break;
                }
            }
        }

        public void Update()
        {
            switch (ScriptType)
            {
                case ScriptTypes.Text:
                    DoText();
                    break;
                case ScriptTypes.Wait:
                    DoWait();
                    break;
                case ScriptTypes.Move:
                    Move();
                    break;
                case ScriptTypes.MoveAsync:
                    MoveAsync();
                    break;
                case ScriptTypes.MovePlayer:
                    MovePlayer();
                    break;
                case ScriptTypes.Register:
                    Register();
                    break;
                case ScriptTypes.Unregister:
                    Unregister();
                    break;
                case ScriptTypes.Turn:
                    Turn();
                    break;
                case ScriptTypes.TurnPlayer:
                    TurnPlayer();
                    break;
                case ScriptTypes.Warp:
                    Warp();
                    break;
                case ScriptTypes.WarpPlayer:
                    WarpPlayer();
                    break;
                case ScriptTypes.Heal:
                    Heal();
                    break;
                case ScriptTypes.Action:
                    DoAction();
                    break;
                case ScriptTypes.Music:
                    DoMusic();
                    break;
                case ScriptTypes.Sound:
                    DoSound();
                    break;
                case ScriptTypes.ViewPokemonImage:
                    ViewPokemonImage();
                    break;
                case ScriptTypes.Npc:
                    DoNpc();
                    break;
                case ScriptTypes.Achievement:
                    GetAchievement();
                    break;
                case ScriptTypes.GiveItem:
                    GiveItem();
                    break;
                case ScriptTypes.RemoveItem:
                    RemoveItem();
                    break;
                case ScriptTypes.Trainer:
                    DoTrainerBattle();
                    break;
                case ScriptTypes.Battle:
                    DoBattle();
                    break;
                case ScriptTypes.Script:
                    DoScript();
                    break;
                case ScriptTypes.MessageBulb:
                    DoMessageBulb();
                    break;
                case ScriptTypes.Camera:
                    DoCamera();
                    break;
                case ScriptTypes.GetBadge:
                    GetBadge();
                    break;
                case ScriptTypes.Pokemon:
                    DoPokemon();
                    break;
                case ScriptTypes.Player:
                    DoPlayer();
                    break;
                case ScriptTypes.Entity:
                    DoEntity();
                    break;
                case ScriptTypes.Environment:
                    DoEnvironment();
                    break;
                case ScriptTypes.Level:
                    DoLevel();

                    break;
                case ScriptTypes.SwitchIf:
                    DoIf();
                    break;
                case ScriptTypes.SwitchThen:
                    IsReady = true;
                    break;
                case ScriptTypes.SwitchElse:
                {
                    var oS = (BaseOverworldScreen) Core.CurrentScreen;
                    oS.ActionScript.ChooseIf(true);

                    IsReady = true;
                }
                    break;
                case ScriptTypes.SwitchEndIf:
                {
                    var oS = (BaseOverworldScreen) Core.CurrentScreen;
                    oS.ActionScript.ChooseIf(true);

                    IsReady = true;
                }

                    break;
                case ScriptTypes.SwitchWhen:
                {
                    if (ActionScript.CSL().WaitingEndWhen[ActionScript.CSL().WhenIndex])
                    {
                        var oS = (BaseOverworldScreen) Core.CurrentScreen;
                        oS.ActionScript.Switch("");
                    }
                    IsReady = true;
                }
                    break;
                case ScriptTypes.SwitchEndWhen:
                {
                    var oS = (BaseOverworldScreen) Core.CurrentScreen;
                    oS.ActionScript.Switch("");
                    IsReady = true;
                }
                    break;
                case ScriptTypes.SwitchEnd:
                    EndScript();
                    break;
                case ScriptTypes.Options:
                    DoOptions();
                    break;
                case ScriptTypes.SelectCase:
                    DoSelect();
                    break;
            }
        }

        #region "NewScripts"

        public void EndScript()
        {
            ActionScript.ScriptLevelIndex -= 1;
            if (ActionScript.ScriptLevelIndex == -1)
            {
                var oS = (BaseOverworldScreen)Core.CurrentScreen;
                oS.ActionScript.Scripts.Clear();
                oS.ActionScript.reDelay = 1f;
                IsReady = true;
                Screen.TextBox.ReDelay = 1f;
                ActionScript.TempInputDirection = -1;
            }
        }

        private void Register()
        {
            ActionScript.RegisterID(Value);

            IsReady = true;
        }

        private void Unregister()
        {
            ActionScript.UnregisterID(Value);

            IsReady = true;
        }

        private void GetAchievement()
        {
            string indiciesData = Value.GetSplit(0, "|");
            indiciesData = indiciesData.Remove(0, 1);
            indiciesData = indiciesData.Remove(indiciesData.Length - 1, 1);
            string[] stringIndicies = indiciesData.Split(Convert.ToChar(","));
            List<int> indicies = new List<int>();
            for (var i = 0; i <= stringIndicies.Length - 1; i++)
            {
                indicies.Add(Convert.ToInt32(stringIndicies[i]));
            }

            string aInput = Value.GetSplit(1, "|");

            IsReady = true;
        }

        private void DoScript()
        {
            if (Core.CurrentScreen.Identification == Screen.Identifications.OverworldScreen)
            {
                ((BaseOverworldScreen)Core.CurrentScreen).ActionScript.StartScript(Value, 0);
            }
        }

        private void DoCamera()
        {
            var c = (BaseOverworldCamera) Screen.Camera;

            if (c.ThirdPerson == true)
            {
                string action = Value.GetSplit(0);
                switch (action.ToLower())
                {
                    case "set":
                        {
                            float x = Convert.ToSingle(Value.GetSplit(1).Replace(".", GameController.DecSeparator));
                            float y = Convert.ToSingle(Value.GetSplit(2).Replace(".", GameController.DecSeparator));
                            float z = Convert.ToSingle(Value.GetSplit(3).Replace(".", GameController.DecSeparator));
                            float yaw = Convert.ToSingle(Value.GetSplit(4).Replace(".", GameController.DecSeparator));
                            float pitch = Convert.ToSingle(Value.GetSplit(5).Replace(".", GameController.DecSeparator));

                            c.ThirdPersonOffset = new Vector3(x, y, z);
                            c.Yaw = yaw;
                            c.Pitch = pitch;
                        }
                        break;
                    case "reset":
                        {
                            c.ThirdPersonOffset = new Vector3(0f, 0.3f, 1.5f);
                            break;
                        }
                    case "yaw":
                        {
                            float yaw = Convert.ToSingle(Value.GetSplit(1).Replace(",", ".").Replace(".", GameController.DecSeparator));

                            c.Yaw = yaw;
                        }
                        break;
                    case "pitch":
                        {
                            float pitch = Convert.ToSingle(Value.GetSplit(1).Replace(",", ".").Replace(".", GameController.DecSeparator));

                            c.Pitch = pitch;
                        }
                        break;
                    case "position":
                        {
                            float x = Convert.ToSingle(Value.GetSplit(1).Replace(".", GameController.DecSeparator));
                            float y = Convert.ToSingle(Value.GetSplit(2).Replace(".", GameController.DecSeparator));
                            float z = Convert.ToSingle(Value.GetSplit(3).Replace(".", GameController.DecSeparator));

                            c.ThirdPersonOffset = new Vector3(x, y, z);
                            break;
                        }
                    case "x":
                        {
                            float x = Convert.ToSingle(Value.GetSplit(1).Replace(".", GameController.DecSeparator));

                            c.ThirdPersonOffset.X = x;
                        }
                        break;
                    case "y":
                        {
                            float y = Convert.ToSingle(Value.GetSplit(1).Replace(".", GameController.DecSeparator));

                            c.ThirdPersonOffset.Y = y;
                        }
                        break;
                    case "z":
                        {
                            float z = Convert.ToSingle(Value.GetSplit(1).Replace(".", GameController.DecSeparator));

                            c.ThirdPersonOffset.Z = z;
                        }
                        break;
                }

                c.UpdateThirdPersonCamera();
                c.UpdateFrustum();
                c.UpdateViewMatrix();
                Screen.Level.UpdateEntities();
                Screen.Level.Entities = Screen.Level.Entities.OrderByDescending(e => e.CameraDistance).ToList();
                Screen.Level.UpdateEntities();
            }

            IsReady = true;
        }

        private void DoText()
        {
            Screen.TextBox.ReDelay = 0f;
            Screen.TextBox.Show(Value, new BaseEntity[] { });
            IsReady = true;
        }

        private void DoSelect()
        {
            string condition = "";
            string check = Value;

            if (Value.Contains("(") & Value.Contains(")"))
            {
                condition = Value.Remove(0, Value.IndexOf("(", StringComparison.Ordinal) + 1);
                condition = condition.Remove(condition.LastIndexOf(")", StringComparison.Ordinal));
                check = Value.Remove(Value.IndexOf("(", StringComparison.Ordinal));
            }

            switch (check.ToLower())
            {
                case "random":
                    check = Convert.ToString(Core.Random.Next(1, Convert.ToInt32(condition) + 1));
                    break;
            }

            var oS = (BaseOverworldScreen)Core.CurrentScreen;

            ActionScript.CSL().WhenIndex += 1;

            oS.ActionScript.Switch(check);

            IsReady = true;
        }

        private void DoOptions()
        {
            Screen.TextBox.Showing = true;
            string[] options = Value.Split(Convert.ToChar(","));

            for (var i = 0; i <= options.Length - 1; i++)
            {
                if (i <= options.Length - 1)
                {
                    var flag = options[i];
                    bool removeFlag = false;

                    switch (flag)
                    {
                        case "[TEXT=FALSE]":
                            removeFlag = true;
                            Screen.TextBox.Showing = false;
                            break;
                    }

                    if (removeFlag)
                    {
                        List<string> l = options.ToList();
                        l.RemoveAt(i);
                        options = l.ToArray();
                        i -= 1;
                    }
                }
            }
            Screen.ChooseBox.Show(options, 0, true);

            ActionScript.CSL().WhenIndex += 1;

            IsReady = true;
        }

        private void DoIf()
        {
            string condition = "";
            string check = Value;

            if (Value.Contains("(") & Value.Contains(")"))
            {
                condition = Value.Remove(0, Value.IndexOf("(", StringComparison.Ordinal) + 1);
                condition = condition.Remove(condition.LastIndexOf(")", StringComparison.Ordinal));
                check = Value.Remove(Value.IndexOf("(", StringComparison.Ordinal));
            }

            BaseOverworldScreen oS = (BaseOverworldScreen)Core.CurrentScreen;

            bool T = false;
            bool inverse = false;

            if (check.StartsWith("not "))
            {
                check = check.Remove(0, 4);
                inverse = true;
            }

            switch (check.ToLower())
            {
                case "register":
                    T = ActionScript.IsRegistered(condition);
                    break;
                case "daytime":
                    if (Convert.ToInt32(condition) == (int) BaseWorld.GetTime)
                    {
                        T = true;
                    }
                    break;
                case "freeplaceinparty":
                    if (Core.Player.Pokemons.Count < 6)
                    {
                        T = true;
                    }
                    break;
                case "season":
                    if (Convert.ToInt32(condition) == Convert.ToInt32(BaseWorld.CurrentSeason))
                    {
                        T = true;
                    }
                    break;
                case "nopokemon":
                    if (Core.Player.Pokemons.Count == 0)
                    {
                        T = true;
                    }
                    break;
                case "countpokemon":
                    if (Core.Player.Pokemons.Count == Convert.ToInt32(condition))
                    {
                        T = true;
                    }
                    break;
                case "day":
                    if (Convert.ToInt32(condition) == (int) DateTime.Now.DayOfWeek)
                    {
                        T = true;
                    }
                    break;
                case "aurora":
                    if (condition == "0")
                    {
                        T = !BaseWorld.IsAurora;
                    }
                    else
                    {
                        T = BaseWorld.IsAurora;
                    }
                    break;
                case "random":
                    if (Core.Random.Next(0, Convert.ToInt32(condition)) == 0)
                    {
                        T = true;
                    }
                    break;
                case "position":
                    string[] positionValues = condition.Split(Convert.ToChar(","));
                    Vector3 checkPosition = new Vector3(Convert.ToInt32(Screen.Camera.Position.X), Convert.ToInt32(Screen.Camera.Position.Y), Convert.ToInt32(Screen.Camera.Position.Z));

                    if (positionValues[0].ToLower() != "player")
                    {
                        int targetId = Convert.ToInt32(positionValues[0]);
                        checkPosition = Screen.Level.GetNPC(targetId).Position;
                    }

                    Vector3 p = new Vector3(Convert.ToSingle(positionValues[1]), Convert.ToSingle(positionValues[2]), Convert.ToSingle(positionValues[3]));

                    if (p == checkPosition)
                    {
                        T = true;
                    }
                    else
                    {
                        T = false;
                    }
                    break;
                case "weather":
                    if (Convert.ToInt32(condition) == (int) Screen.Level.World.CurrentWeather)
                    {
                        T = true;
                    }
                    else
                    {
                        T = false;
                    }
                    break;
                case "regionweather":
                    if (Convert.ToInt32(condition) == (int) BaseWorld.GetCurrentRegionWeather())
                    {
                        T = true;
                    }
                    else
                    {
                        T = false;
                    }
                    break;
                case "hasbadge":
                    if (Core.Player.Badges.Contains(Convert.ToInt32(condition)))
                    {
                        T = true;
                    }
                    else
                    {
                        T = false;
                    }
                    break;
                case "hasitem":
                    if (Core.Player.Inventory.GetItemAmount(Convert.ToInt32(condition)) > 0)
                    {
                        T = true;
                    }
                    else
                    {
                        T = false;
                    }
                    break;
                case "haspokemon":
                    foreach (IPokemon p in Core.Player.Pokemons)
                    {
                        if (p.Number == Convert.ToInt32(condition))
                        {
                            T = true;
                            break; // TODO: might not be correct. Was : Exit For
                        }
                    }

                    break;
            }

            if (inverse)
            {
                T = !T;
            }

            ActionScript.CSL().WaitingEndIf[ActionScript.CSL().IfIndex + 1] = false;
            ActionScript.CSL().CanTriggerElse[ActionScript.CSL().IfIndex + 1] = false;

            oS.ActionScript.ChooseIf(T);

            IsReady = true;
        }

        private void DoWait()
        {
            if (Convert.ToInt32(Value) > 0)
            {
                Value = Convert.ToString(Convert.ToInt32(Value) - 1);
            }
            if (Convert.ToInt32(Value) <= 0)
            {
                IsReady = true;
            }
        }

        private void DoAction()
        {
            switch (true)
            {
                // Not Used?
                /*
                case Value.ToLower() == "storagesystem":
                    Core.SetScreen(new TransitionScreen(Core.CurrentScreen, new StorageSystemScreen(Core.CurrentScreen), Color.Black, false));
                    break;
                case Value.ToLower() == "apricornkurt":
                    Core.SetScreen(new ApricornScreen(Core.CurrentScreen, "Kurt"));
                    break;
                case Value.ToLower().StartsWith("trade("):
                    Value = Value.Remove(0, Value.IndexOf("(") + 1);
                    Value = Value.Remove(Value.Length - 1, 1);

                    string storeData = Convert.ToString(Value.GetSplit(0));
                    bool canBuy = Convert.ToBoolean(Value.GetSplit(1));
                    bool canSell = Convert.ToBoolean(Value.GetSplit(2));

                    string currencyIndicator = "P";

                    if (Value.CountSplits() > 3)
                    {
                        currencyIndicator = Value.GetSplit(3);
                    }

                    Core.SetScreen(new TransitionScreen(Core.CurrentScreen, new TradeScreen(Core.CurrentScreen, storeData, canBuy, canSell, currencyIndicator), Color.Black, false));
                    break;
                case Value.ToLower().StartsWith("getpokemon("):
                    Value = Value.Remove(0, Value.IndexOf("(") + 1);
                    Value = Value.Remove(Value.Length - 1, 1);

                    int commas = 0;
                    foreach (char c in Value)
                    {
                        if (c == ",")
                        {
                            commas += 1;
                        }
                    }


                    int pokemonId = Convert.ToInt32(Value.GetSplit(0));
                    int level = Convert.ToInt32(Value.GetSplit(1));

                    string catchMethod = "random reason";
                    if (commas > 1)
                    {
                        catchMethod = Value.GetSplit(2);
                    }

                    Item catchBall = Item.GetItemByID(1);
                    if (commas > 2)
                    {
                        catchBall = Item.GetItemByID(Convert.ToInt32(Value.GetSplit(3)));
                    }

                    string catchLocation = Screen.Level.MapName;
                    if (commas > 3)
                    {
                        catchLocation = Value.GetSplit(4);
                    }

                    bool isEgg = false;
                    if (commas > 4)
                    {
                        isEgg = Convert.ToBoolean(Value.GetSplit(5));
                    }

                    string catchTrainer = Core.Player.Name;
                    if (commas > 5 & Value.GetSplit(6) != "<playername>")
                    {
                        catchTrainer = Value.GetSplit(6);
                    }

                    Pokemon pokemon = pokemon.GetPokemonByID(pokemonId);
                    pokemon.Generate(level, true);

                    pokemon.CatchTrainerName = catchTrainer;
                    pokemon.OT = Core.Player.OT;

                    pokemon.CatchLocation = catchLocation;
                    pokemon.CatchBall = catchBall;
                    pokemon.CatchMethod = catchMethod;

                    if (isEgg)
                    {
                        pokemon.EggSteps = 1;
                        pokemon.SetCatchInfos(Item.GetItemByID(5), "obtained at");
                    }
                    else
                    {
                        pokemon.EggSteps = 0;
                    }

                    Core.Player.Pokemons.Add(pokemon);

                    int pokedexType = 2;
                    if (pokemon.IsShiny == true)
                    {
                        pokedexType = 3;
                    }

                    Core.Player.PokedexData = Pokedex.ChangeEntry(Core.Player.PokedexData, pokemon.Number, pokedexType);
                    break;
                case Value.ToLower().StartsWith("townmap,"):
                    string startRegion = Value.GetSplit(1);
                    Core.SetScreen(new TransitionScreen(Core.CurrentScreen, new MapScreen(Core.CurrentScreen, startRegion, { "view" }), Color.Black, false));
                    break;
                case Value.ToLower() == "opendonation":
                    Core.SetScreen(new DonationScreen(Core.CurrentScreen));
                    break;
                case Value.ToLower() == "receivepokedex":
                    Core.Player.hasPokedex = true;
                    foreach (Pokemon p in Core.Player.Pokemons)
                    {
                        int i = 2;
                        if (p.IsShiny == true)
                        {
                            i = 3;
                        }
                        Core.Player.PokedexData = Pokedex.ChangeEntry(Core.Player.PokedexData, p.Number, i);
                    }

                    break;
                case Value.ToLower() == "receivepokegear":
                    Core.Player.hasPokegear = true;
                    break;
                case Value.ToLower().StartsWith("renamepokemon("):
                    Value = Value.Remove(0, Value.IndexOf("(") + 1);
                    Value = Value.Remove(Value.Length - 1, 1);

                    string index = Value;
                    bool renameOTcheck = false;
                    bool canRename = true;

                    if (Value.Contains(","))
                    {
                        index = Value.GetSplit(0);
                        renameOTcheck = Convert.ToBoolean(Value.GetSplit(1));
                    }

                    int pokemonIndex = 0;
                    if (Information.IsNumeric(index) == true)
                    {
                        pokemonIndex = Convert.ToInt32(index);
                    }
                    else
                    {
                        if (index.ToLower() == "last")
                        {
                            pokemonIndex = Core.Player.Pokemons.Count - 1;
                        }
                    }

                    if (renameOTcheck)
                    {
                        if (Core.Player.Pokemons(pokemonIndex).OT != Core.Player.OT)
                        {
                            canRename = false;
                        }
                    }

                    if (canRename)
                    {
                        Core.SetScreen(new NameObjectScreen(Core.CurrentScreen, Core.Player.Pokemons(pokemonIndex)));
                    }
                    else
                    {
                        Screen.TextBox.Show("I cannot rename this~Pokémon because the~OT is different!*Did you receive it in~a trade or something?", {

                        }, true, false);
                    }
                    break;
                case Value.ToLower() == "renamerival":
                    Core.SetScreen(new NameObjectScreen(Core.CurrentScreen, TextureManager.GetTexture("NPC\\4", new Rectangle(0, 64, 32, 32)), false, false, "rival", "Silver", Script.NameRival));
                    break;
                case Value.ToLower().StartsWith("playcry("):
                    Value = Value.Remove(0, Value.IndexOf("(") + 1);
                    Value = Value.Remove(Value.Length - 1, 1);

                    Pokemon p = pokemon.GetPokemonByID(Convert.ToInt32(Value));
                    p.PlayCry();
                    break;
                case Value.ToLower().StartsWith("showOpokemon("):
                    Value = Value.Remove(0, Value.IndexOf("(") + 1);
                    Value = Value.Remove(Value.Length - 1, 1);

                    Screen.Level.OverworldPokemon.Visible = Convert.ToBoolean(Value);
                    break;
                case Value.ToLower() == "togglethirdperson":
                    if (Core.CurrentScreen.Identification == Screen.Identifications.OverworldScreen)
                    {
                        OverworldCamera c = (OverworldCamera)Screen.Camera;

                        c.SetThirdPerson(!c.ThirdPerson, false);
                        c.UpdateFrustum();
                        c.UpdateViewMatrix();
                        Screen.Level.UpdateEntities();
                        Screen.Level.Entities = (from e in Screen.Level.Entitiesorderby e.CameraDistance descending).ToList();
                        Screen.Level.UpdateEntities();
                    }
                    break;
                case Value.ToLower() == "activatethirdperson":
                    if (Core.CurrentScreen.Identification == Screen.Identifications.OverworldScreen)
                    {
                        OverworldCamera c = (OverworldCamera)Screen.Camera;

                        c.SetThirdPerson(true, false);
                        c.UpdateFrustum();
                        c.UpdateViewMatrix();
                        Screen.Level.UpdateEntities();
                        Screen.Level.Entities = (from e in Screen.Level.Entitiesorderby e.CameraDistance descending).ToList();
                        Screen.Level.UpdateEntities();
                    }
                    break;
                case Value.ToLower() == "deactivatethirdperson":
                    if (Core.CurrentScreen.Identification == Screen.Identifications.OverworldScreen)
                    {
                        OverworldCamera c = (OverworldCamera)Screen.Camera;

                        c.SetThirdPerson(false, false);
                        c.UpdateFrustum();
                        c.UpdateViewMatrix();
                        Screen.Level.UpdateEntities();
                        Screen.Level.Entities = (from e in Screen.Level.Entitiesorderby e.CameraDistance descending).ToList();
                        Screen.Level.UpdateEntities();
                    }
                    break;
                case Value.ToLower().StartsWith("setfont("):
                    Value = Value.Remove(0, Value.IndexOf("(") + 1);
                    Value = Value.Remove(Value.Length - 1, 1);

                    switch (Value.ToLower())
                    {
                        case "standard":
                            Screen.TextBox.TextFont = FontManager.GetFontContainer("textfont");
                            break;
                        case "unown":
                            Screen.TextBox.TextFont = FontManager.GetFontContainer("unown");
                            break;
                    }
                    break;
                case Value.ToLower().StartsWith("setrenderdistance("):
                    Value = Value.Remove(0, Value.IndexOf("(") + 1);
                    Value = Value.Remove(Value.Length - 1, 1);

                    switch (Value.ToLower())
                    {
                        case "0":
                        case "tiny":
                            Core.GameOptions.RenderDistance = 0;
                            break;
                        case "1":
                        case "small":
                            Core.GameOptions.RenderDistance = 1;
                            break;
                        case "2":
                        case "normal":
                            Core.GameOptions.RenderDistance = 2;
                            break;
                        case "3":
                        case "far":
                            Core.GameOptions.RenderDistance = 3;
                            break;
                        case "4":
                        case "extreme":
                            Core.GameOptions.RenderDistance = 4;
                            break;
                    }

                    Screen.Level.World = new World(Screen.Level.EnvironmentType, Screen.Level.WeatherType);
                    break;
                case Value.ToLower().StartsWith("wearskin("):
                    Value = Value.Remove(0, Value.IndexOf("(") + 1);
                    Value = Value.Remove(Value.Length - 1, 1);

                    var with1 = Screen.Level.OwnPlayer;
                    string textureId = Value;

                    Logger.Debug(textureId);

                    with1.Texture = TextureManager.GetTexture("Textures\\NPC\\" + textureId);

                    with1.SkinName = textureId;

                    with1.UpdateEntity();
                    break;
                case Value.ToLower() == "toggledarkness":
                    Screen.Level.IsDark = !Screen.Level.IsDark;
                    break;
                case Value.ToLower().StartsWith("globalhub"):
                case Value.ToLower().StartsWith("friendhub"):
                    if (GameJolt.API.LoggedIn == true & Core.Player.IsGamejoltSave | GameController.IS_DEBUG_ACTIVE)
                    {
                        if (GameJolt.LogInScreen.UserBanned(Core.GameJoltSave.GameJoltID) == false)
                        {
                            Core.SetScreen(new TransitionScreen(Core.CurrentScreen, new GameJolt.GTSMainScreen(Core.CurrentScreen), Color.Black, false));
                        }
                        else
                        {
                            Screen.TextBox.Show("This GameJolt account~(" + Core.GameJoltSave.GameJoltID + ") is banned~from the GTS!", {

                            }, false, false, Color.Red);
                        }
                    }
                    else
                    {
                        Screen.TextBox.Show("You are not using~your GameJolt profile.*Please connect to GameJolt~and switch to the GameJolt~profile to enable the GTS.*You can do this by going~back to the main menu~and choosing \"Play online\".", {

                        }, false, false, Color.Red);
                    }
                    break;
                case Value.ToLower().StartsWith("gamejoltlogin"):
                    Core.SetScreen(new GameJolt.LogInScreen(Core.CurrentScreen));
                    break;
                case Value.ToLower().StartsWith("readpokemon("):
                    Value = Value.Remove(0, Value.IndexOf("(") + 1);
                    Value = Value.Remove(Value.Length - 1, 1);

                    Pokemon p = Core.Player.Pokemons(Convert.ToInt32(Value));

                    string message = "Hm... I see your~" + p.GetDisplayName();
                    string addmessage = "~is very stable with~";

                    if (p.EVAttack > p.EVDefense & p.EVAttack > p.EVHP & p.EVAttack > p.EVSpAttack & p.EVAttack > p.EVSpDefense & p.EVAttack > p.EVSpeed)
                    {
                        addmessage += "performing physical moves.";
                    }
                    if (p.EVDefense > p.EVAttack & p.EVDefense > p.EVHP & p.EVDefense > p.EVSpAttack & p.EVDefense > p.EVSpDefense & p.EVDefense > p.EVSpeed)
                    {
                        addmessage += "taking hits.";
                    }
                    if (p.EVHP > p.EVAttack & p.EVHP > p.EVDefense & p.EVHP > p.EVSpAttack & p.EVHP > p.EVSpDefense & p.EVHP > p.EVSpeed)
                    {
                        addmessage += "taking damage.";
                    }
                    if (p.EVSpAttack > p.EVAttack & p.EVSpAttack > p.EVDefense & p.EVSpAttack > p.EVHP & p.EVSpAttack > p.EVSpDefense & p.EVSpAttack > p.EVSpeed)
                    {
                        addmessage += "performing complex strategies.";
                    }
                    if (p.EVSpDefense > p.EVAttack & p.EVSpDefense > p.EVDefense & p.EVSpDefense > p.EVHP & p.EVSpDefense > p.EVSpAttack & p.EVSpDefense > p.EVSpeed)
                    {
                        addmessage += "breaking strategies.";
                    }
                    if (p.EVSpeed > p.EVAttack & p.EVSpeed > p.EVDefense & p.EVSpeed > p.EVHP & p.EVSpeed > p.EVSpAttack & p.EVSpeed > p.EVSpDefense)
                    {
                        addmessage += "speeding the others out.";
                    }

                    if (addmessage == "~is very stable with~")
                    {
                        addmessage = "~is very well balanced.";
                    }

                    message += addmessage;

                    message += "*...~...*What that means?~I am not sure...";

                    Screen.TextBox.Show(message, {

                    }, false, false);
                    break;
                case Value.ToLower.StartsWith("achieveemblem(") == true:
                    Value = Value.Remove(0, Value.IndexOf("(") + 1);
                    Value = Value.Remove(Value.Length - 1, 1);

                    GameJolt.Emblem.AchieveEmblem(Value);
                    break;
                    */
            }

            IsReady = true;
        }

        private void DoMusic()
        {
            MusicManager.PlayMusic(Value, true);

            if (Core.CurrentScreen.Identification == Screen.Identifications.OverworldScreen)
            {
                Screen.Level.MusicLoop = Value;
            }

            IsReady = true;
        }

        private void DoSound()
        {
            string sound = Value;
            bool stopMusic = false;

            if (Value.Contains(","))
            {
                sound = Value.GetSplit(0);
                stopMusic = Convert.ToBoolean(Value.GetSplit(1));
            }

            SoundManager.PlaySound(sound, stopMusic);

            IsReady = true;
        }

        private void DoMessageBulb()
        {
            if (Started == false)
            {
                Started = true;
                string[] data = Value.Split(Convert.ToChar("|"));

                int id = Convert.ToInt32(data[0]);
                Vector3 position = new Vector3(Convert.ToSingle(data[1].Replace(".", GameController.DecSeparator)), Convert.ToSingle(data[2].Replace(".", GameController.DecSeparator)), Convert.ToSingle(data[3].Replace(".", GameController.DecSeparator)));

                BaseMessageBulb.NotifcationTypes noType = BaseMessageBulb.NotifcationTypes.Waiting;
                switch (id)
                {
                    case 0:
                        noType = BaseMessageBulb.NotifcationTypes.Waiting;
                        break;
                    case 1:
                        noType = BaseMessageBulb.NotifcationTypes.Exclamation;
                        break;
                    case 2:
                        noType = BaseMessageBulb.NotifcationTypes.Shouting;
                        break;
                    case 3:
                        noType = BaseMessageBulb.NotifcationTypes.Question;
                        break;
                    case 4:
                        noType = BaseMessageBulb.NotifcationTypes.Note;
                        break;
                    case 5:
                        noType = BaseMessageBulb.NotifcationTypes.Heart;
                        break;
                    case 6:
                        noType = BaseMessageBulb.NotifcationTypes.Unhappy;
                        break;
                    case 7:
                        noType = BaseMessageBulb.NotifcationTypes.Happy;
                        break;
                    case 8:
                        noType = BaseMessageBulb.NotifcationTypes.Friendly;
                        break;
                    case 9:
                        noType = BaseMessageBulb.NotifcationTypes.Poisoned;
                        break;
                    default:
                        noType = BaseMessageBulb.NotifcationTypes.Exclamation;
                        break;
                }

                Screen.Level.Entities.Add(new MessageBulb(position, noType));
            }

            bool contains = false;
            Screen.Level.Entities = Screen.Level.Entities.OrderByDescending(e => e.CameraDistance).ToList();
            foreach (IEntity e in Screen.Level.Entities)
            {
                if (e.EntityID == "MessageBulb")
                {
                    e.Update();
                    contains = true;
                }
            }
            if (contains == false)
            {
                IsReady = true;
            }
            else
            {
                for (var i = 0; i <= Screen.Level.Entities.Count - 1; i++)
                {
                    if (i <= Screen.Level.Entities.Count - 1)
                    {
                        if (Screen.Level.Entities[i].CanBeRemoved == true)
                        {
                            Screen.Level.Entities.RemoveAt(i);
                            i -= 1;
                        }
                    }
                    else
                    {
                        break; // TODO: might not be correct. Was : Exit For
                    }
                }
            }
        }

        private void DoBattle()
        {
            string actionValue = Value.GetSplit(0);
            switch (actionValue.ToLower())
            {
                case "trainer":
                    string id = Value.GetSplit(1);
                    Trainer t = new Trainer(id);

                    if (Value.CountSeperators(",") > 1)
                    {
                        foreach (string v in Value.Split(Convert.ToChar(",")))
                        {
                            switch (v)
                            {
                                case "generate_pokemon_tower":
                                    int level = 0;
                                    foreach (IPokemon p in Core.Player.Pokemons)
                                    {
                                        if (p.Level > level)
                                        {
                                            level = p.Level;
                                        }
                                    }


                                    while (Convert.ToString(level)(Convert.ToString(level).Length - 1) != "0")
                                    {
                                        level += 1;
                                    }
                                    break;
                            }
                        }
                    }

                    BattleSystem.BattleScreen b = new BattleSystem.BattleScreen(t, Core.CurrentScreen, 0);
                    Core.SetScreen(new BattleIntroScreen(Core.CurrentScreen, b, t, t.GetIniMusicName(), t.IntroType));
                    break;
                case "wild":
                    int id = Convert.ToInt32(Value.GetSplit(1));
                    int Level = Convert.ToInt32(Value.GetSplit(2));

                    Pokemon p = Pokemon.GetPokemonByID(id);
                    p.Generate(Level, true);

                    Core.Player.PokedexData = Pokedex.ChangeEntry(Core.Player.PokedexData, p.Number, 1);

                    BattleSystem.BattleScreen b = new BattleSystem.BattleScreen(p, Core.CurrentScreen, 0);
                    Core.SetScreen(new BattleIntroScreen(Core.CurrentScreen, b, Core.Random.Next(0, 10)));
                    break;
            }
            IsReady = true;
        }

        private void DoTrainerBattle()
        {
            Trainer t = new Trainer(Value);
            if (t.IsBeaten() == false)
            {
                if (Started == false)
                {
                    ((BaseOverworldScreen)Core.CurrentScreen).TrainerEncountered = true;
                    MusicManager.PlayMusic(t.GetInSightMusic(), true);
                    if (!string.IsNullOrEmpty(t.IntroMessage))
                    {
                        Screen.TextBox.reDelay = 0f;
                        Screen.TextBox.Show(t.IntroMessage, {

                        });
                    }
                    Started = true;
                }

                if (Screen.TextBox.Showing == false)
                {
                    ((BaseOverworldScreen)Core.CurrentScreen).TrainerEncountered = false;

                    BattleSystem.BattleScreen b = new BattleSystem.BattleScreen(new Trainer(Value), Core.CurrentScreen, 0);
                    Core.SetScreen(new BattleIntroScreen(Core.CurrentScreen, b, t, t.GetIniMusicName(), t.IntroType));
                }
            }
            else
            {
                Screen.TextBox.ReDelay = 0f;
                Screen.TextBox.Show(t.DefeatMessage, new BaseEntity[] { });

                IsReady = true;
            }

            if (Screen.TextBox.Showing == false)
            {
                IsReady = true;
            }
        }

        private void DoPokemon()
        {
            string command = Value;
            string argument = "";

            if (command.Contains("(") & command.EndsWith(")"))
            {
                argument = command.Remove(0, command.IndexOf("(", StringComparison.Ordinal) + 1);
                argument = argument.Remove(argument.Length - 1, 1);
                command = command.Remove(command.IndexOf("(", StringComparison.Ordinal));
            }

            switch (command.ToLower())
            {
                case "cry":
                {
                    int pokemonId = Convert.ToInt32(argument);

                    IPokemon p = Pokemon.GetPokemonByID(pokemonId);
                    p.PlayCry();
                }
                    break;
                case "remove":
                {
                    int index = Convert.ToInt32(argument);
                    if (Core.Player.Pokemons.Count - 1 >= index)
                    {
                        Logger.Debug("Remove Pokémon (" + Core.Player.Pokemons[index].GetDisplayName() + ") at index " +
                                     index);
                        Core.Player.Pokemons.RemoveAt(index);
                    }
                }
                    break;
                case "add":
                {
                    int commas = 0;
                    foreach (char c in argument)
                    {
                        if (c == ',')
                        {
                            commas += 1;
                        }
                    }


                    int pokemonId = Convert.ToInt32(argument.GetSplit(0));
                    int level = Convert.ToInt32(argument.GetSplit(1));

                    string catchMethod = "random reason";
                    if (commas > 1)
                    {
                        catchMethod = argument.GetSplit(2);
                    }

                    IItem catchBall = Item.GetItemByID(1);
                    if (commas > 2)
                    {
                        catchBall = Item.GetItemByID(Convert.ToInt32(argument.GetSplit(3)));
                    }

                    string catchLocation = Screen.Level.MapName;
                    if (commas > 3)
                    {
                        catchLocation = argument.GetSplit(4);
                    }

                    bool isEgg = false;
                    if (commas > 4)
                    {
                        isEgg = Convert.ToBoolean(argument.GetSplit(5));
                    }

                    string catchTrainer = Core.Player.Name;
                    if (commas > 5 & argument.GetSplit(6) != "<playername>")
                    {
                        catchTrainer = argument.GetSplit(6);
                    }

                    IPokemon pokemon = Pokemon.GetPokemonByID(pokemonId);
                    pokemon.Generate(level, true);

                    pokemon.CatchTrainerName = catchTrainer;
                    pokemon.OT = Core.Player.OT;

                    pokemon.CatchLocation = catchLocation;
                    pokemon.CatchBall = catchBall;
                    pokemon.CatchMethod = catchMethod;

                    if (isEgg)
                    {
                        pokemon.EggSteps = 1;
                        pokemon.SetCatchInfos(Item.GetItemByID(5), "obtained at");
                    }
                    else
                    {
                        pokemon.EggSteps = 0;
                    }

                    Core.Player.Pokemons.Add(pokemon);

                    int pokedexType = 2;
                    if (pokemon.IsShiny == true)
                    {
                        pokedexType = 3;
                    }

                    Core.Player.PokedexData = Pokedex.ChangeEntry(Core.Player.PokedexData, pokemon.Number, pokedexType);
                }
                    break;
                case "setadditionalvalue":
                {
                    int Index = Convert.ToInt32(argument.GetSplit(0, ","));
                    string additionalValue = argument.GetSplit(1, ",");

                    if (Core.Player.Pokemons.Count - 1 >= Index)
                    {
                        Core.Player.Pokemons[Index].AdditionalData = additionalValue;
                    }
                }
                    break;
                case "setnickname":
                {
                    int Index = Convert.ToInt32(argument.GetSplit(0, ","));
                    string nickName = argument.GetSplit(1, ",");

                    if (Core.Player.Pokemons.Count - 1 >= Index)
                    {
                        Core.Player.Pokemons[Index].NickName = nickName;
                    }
                }
                    break;
                case "setstat":
                {
                    int Index = Convert.ToInt32(argument.GetSplit(0, ","));
                    string stat = argument.GetSplit(1, ",");
                    int statValue = Convert.ToInt32(argument.GetSplit(2, ","));

                    if (Core.Player.Pokemons.Count - 1 >= Index)
                    {
                        var with2 = Core.Player.Pokemons[Index];
                        switch (stat.ToLower())
                        {
                            case "maxhp":
                            case "hp":
                                with2.MaxHP = statValue;
                                break;
                            case "chp":
                                with2.HP = statValue;
                                break;
                            case "atk":
                            case "attack":
                                with2.Attack = statValue;
                                break;
                            case "def":
                            case "defense":
                                with2.Defense = statValue;
                                break;
                            case "spatk":
                            case "specialattack":
                            case "spattack":
                                with2.SpAttack = statValue;
                                break;
                            case "spdef":
                            case "specialdefense":
                            case "spdefense":
                                with2.SpDefense = statValue;
                                break;
                            case "speed":
                                with2.Speed = statValue;
                                break;
                        }
                    }
                }
                    break;
                case "clear":
                {
                    Core.Player.Pokemons.Clear();
                }
                    break;
                case "removeattack":
                {
                    int Index = Convert.ToInt32(argument.GetSplit(0, ","));
                    int attackIndex = Convert.ToInt32(argument.GetSplit(1, ","));

                    if (Core.Player.Pokemons.Count - 1 >= Index)
                    {
                        var p = Core.Player.Pokemons[Index];

                        if (p.Attacks.Count - 1 >= attackIndex)
                        {
                            p.Attacks.RemoveAt(attackIndex);
                        }
                    }
                }
                    break;
                case "clearattacks":
                {
                    int Index = Convert.ToInt32(argument);

                    if (Core.Player.Pokemons.Count - 1 >= Index)
                    {
                        Core.Player.Pokemons[Index].Attacks.Clear();
                    }
                }
                    break;
                case "addattack":
                {
                    int Index = Convert.ToInt32(argument.GetSplit(0, ","));
                    int attackId = Convert.ToInt32(argument.GetSplit(1, ","));

                    if (Core.Player.Pokemons.Count - 1 >= Index)
                    {
                        var p = Core.Player.Pokemons[Index];

                        if (p.Attacks.Count < 4)
                        {
                            BattleSystem.Attack newAttack = BattleSystem.Attack.GetAttackByID(attackId);
                            p.Attacks.Add(newAttack);
                        }
                    }
                }
                    break;
                case "removeattack":
                {
                    int Index = Convert.ToInt32(argument.GetSplit(0, ","));
                    int attackIndex = Convert.ToInt32(argument.GetSplit(1, ","));
                        
                    if (Core.Player.Pokemons.Count - 1 >= Index)
                    {
                        var p = Core.Player.Pokemons[Index];

                        if (p.Attacks.Count - 1 >= attackIndex)
                        {
                            p.Attacks.RemoveAt(attackIndex);
                        }
                    }
                }
                    break;
                case "setshiny":
                {
                    int Index = Convert.ToInt32(argument.GetSplit(0, ","));
                    bool isShiny = Convert.ToBoolean(argument.GetSplit(1, ","));

                    if (Core.Player.Pokemons.Count - 1 >= Index)
                    {
                        Core.Player.Pokemons[Index].IsShiny = isShiny;
                    }
                }
                    break;
                case "changelevel":
                {
                    int Index = Convert.ToInt32(argument.GetSplit(0, ","));
                    int newLevel = Convert.ToInt32(argument.GetSplit(1, ","));

                    if (Core.Player.Pokemons.Count - 1 >= Index)
                    {
                        Core.Player.Pokemons[Index].Level = newLevel;
                    }
                }
                    break;
                case "gainexp":
                {
                    int Index = Convert.ToInt32(argument.GetSplit(0, ","));
                    int exp = Convert.ToInt32(argument.GetSplit(1, ","));

                    if (Core.Player.Pokemons.Count - 1 >= Index)
                    {
                        Core.Player.Pokemons[Index].Experience += exp;
                    }
                }
                    break;
                case "setnature":
                {
                    int Index = Convert.ToInt32(argument.GetSplit(0, ","));
                    BasePokemon.Natures nature = BasePokemon.ConvertIDToNature(Convert.ToInt32(argument.GetSplit(1, ",")));

                    if (Core.Player.Pokemons.Count - 1 >= Index)
                    {
                        Core.Player.Pokemons[Index].Nature = nature;
                    }
                }
                    break;
                case "npctrade":
                    string[] splits = argument.Split(Convert.ToChar("|"));
                    Script.SaveNPCTrade = splits;

                    Core.SetScreen(new ChoosePokemonScreen(Core.CurrentScreen, BaseItem.GetItemByID(5), Script.DoNPCTrade, "Choose trade Pokémon", true));
                    ((ChoosePokemonScreen)Core.CurrentScreen).ExitedSub = Script.ExitedNPCTrade;
                    break;
                case "hide":
                    Screen.Level.OverworldPokemon.Visible = false;
                    break;
            }

            IsReady = true;
        }

        private void DoNpc()
        {
            string command = Value;
            string argument = "";

            if (command.Contains("(") & command.EndsWith(")"))
            {
                argument = command.Remove(0, command.IndexOf("(") + 1);
                argument = argument.Remove(argument.Length - 1, 1);
                command = command.Remove(command.IndexOf("("));
            }

            switch (command.ToLower())
            {
                case "remove":
                {
                    IEntity targetNpc = Screen.Level.GetNPC(Convert.ToInt32(argument));
                    Screen.Level.Entities.Remove(targetNpc);
                    IsReady = true;
                }
                    break;
                case "position":
                case "warp":
                {
                    INPC targetNpc = Screen.Level.GetNPC(Convert.ToInt32(argument.GetSplit(0)));

                    string[] positionData = argument.Split(Convert.ToChar(","));
                    targetNpc.Position =
                        new Vector3(Convert.ToSingle(positionData[1].Replace(".", GameController.DecSeparator)), Convert.ToSingle(positionData[2].Replace(".", GameController.DecSeparator)), Convert.ToSingle(positionData[3].Replace(".", GameController.DecSeparator)));
                    targetNpc.CreatedWorld = false;
                    IsReady = true;
                }
                    break;
                case "register":
                {
                    NPC.AddNPCData(argument);
                    IsReady = true;
                }
                    break;
                case "unregister":
                {
                    NPC.RemoveNPCData(argument);
                    IsReady = true;
                }
                    break;
                case "wearskin":
                {
                    string textureId = argument.GetSplit(1);
                    INPC targetNpc = Screen.Level.GetNPC(Convert.ToInt32(argument.GetSplit(0)));

                    targetNpc.SetupSprite(textureId, "", false);
                    IsReady = true;
                }
                    break;
                case "move":
                {
                    INPC targetNpc = Screen.Level.GetNPC(Convert.ToInt32(argument.GetSplit(0)));
                    int steps = Convert.ToInt32(argument.GetSplit(1));

                    Screen.Level.UpdateEntities();
                    if (Started == false)
                    {
                        targetNpc.Moved += steps;
                        Started = true;
                    }
                    else
                    {
                        if (targetNpc.Moved <= 0f)
                        {
                            IsReady = true;
                        }
                    }
                }
                    break;
                case "turn":
                {
                    INPC targetNpc = Screen.Level.GetNPC(Convert.ToInt32(argument.GetSplit(0)));

                    targetNpc.FaceRotation = Convert.ToInt32(argument.GetSplit(1));
                    targetNpc.Update();
                    targetNpc.UpdateEntity();
                    IsReady = true;
                }
                    break;
                default:
                    IsReady = true;
                    break;
            }
        }

        private void DoPlayer()
        {
            string command = Value;
            string argument = "";

            if (command.Contains("(") & command.EndsWith(")"))
            {
                argument = command.Remove(0, command.IndexOf("(", StringComparison.Ordinal) + 1);
                argument = argument.Remove(argument.Length - 1, 1);
                command = command.Remove(command.IndexOf("(", StringComparison.Ordinal));
            }

            switch (command.ToLower())
            {
                case "wearskin":
                    var with3 = Screen.Level.OwnPlayer;
                    string textureId = argument;
                    with3.SetTexture(textureId, false);

                    with3.UpdateEntity();

                    IsReady = true;
                    break;
                case "move":
                    if (Started == false)
                    {
                        Screen.Camera.Move(Convert.ToSingle(argument));
                        Started = true;
                        Screen.Level.OverworldPokemon.Visible = false;
                    }
                    else
                    {
                        Screen.Level.UpdateEntities();
                        Screen.Camera.Update();
                        if (Screen.Camera.IsMoving == false)
                        {
                            IsReady = true;
                        }
                    }
                    break;
                case "turn":
                    if (Started == false)
                    {
                        Screen.Camera.Turn(Convert.ToInt32(argument));
                        Started = true;
                        Screen.Level.OverworldPokemon.Visible = false;
                    }
                    else
                    {
                        Screen.Camera.Update();
                        Screen.Level.UpdateEntities();
                        if (Screen.Camera.Turning == false)
                        {
                            IsReady = true;
                        }
                    }
                    break;
                case "turnto":
                    if (Started == false)
                    {
                        int turns = Convert.ToInt32(argument) - Screen.Camera.GetPlayerFacingDirection();
                        if (turns < 0)
                        {
                            turns = turns + 4;
                        }

                        if (turns > 0)
                        {
                            Screen.Camera.Turn(turns);
                            Started = true;
                            Screen.Level.OverworldPokemon.Visible = false;
                        }
                        else
                        {
                            IsReady = true;
                        }
                    }
                    else
                    {
                        Screen.Camera.Update();
                        Screen.Level.UpdateEntities();
                        if (Screen.Camera.Turning == false)
                        {
                            IsReady = true;
                        }
                    }
                    break;
                case "warp":
                    int commas = 0;
                    foreach (char c in argument)
                    {
                        if (c == ',')
                        {
                            commas += 1;
                        }
                    }


                    switch (commas)
                    {
                        case 4:
                            Screen.Level.WarpData.WarpDestination = argument.GetSplit(0);
                            Screen.Level.WarpData.WarpPosition = new Vector3(Convert.ToSingle(argument.GetSplit(1)), Convert.ToSingle(argument.GetSplit(2).Replace(".", GameController.DecSeparator)), Convert.ToSingle(argument.GetSplit(3)));
                            Screen.Level.WarpData.WarpRotations = Convert.ToInt32(argument.GetSplit(4));
                            Screen.Level.WarpData.DoWarpInNextTick = true;
                            Screen.Level.WarpData.CorrectCameraYaw = Screen.Camera.Yaw;
                            break;
                        case 2:
                            Screen.Camera.Position = new Vector3(Convert.ToSingle(argument.GetSplit(0)), Convert.ToSingle(argument.GetSplit(1).Replace(".", GameController.DecSeparator)), Convert.ToSingle(argument.GetSplit(2)));
                            break;
                    }

                    Screen.Level.OverworldPokemon.warped = true;
                    Screen.Level.OverworldPokemon.Visible = false;

                    IsReady = true;
                    break;
                case "stopmovement":
                    Screen.Camera.StopMovement();

                    IsReady = true;
                    break;
                case "money":
                    Core.Player.Money += Convert.ToInt32(argument);

                    IsReady = true;
                    break;
                case "setmovement":
                    string[] movements = argument.Split(Convert.ToChar(","));

                    Screen.Camera.PlannedMovement = new Vector3(Convert.ToInt32(movements[0]), Convert.ToInt32(movements[1]), Convert.ToInt32(movements[2]));
                    IsReady = true;
                    break;
                default:
                    IsReady = true;
                    break;
            }
        }

        private void DoEntity()
        {
            string command = Value;
            string argument = "";

            if (command.Contains("(") & command.EndsWith(")"))
            {
                argument = command.Remove(0, command.IndexOf("(", StringComparison.Ordinal) + 1);
                argument = argument.Remove(argument.Length - 1, 1);
                command = command.Remove(command.IndexOf("(", StringComparison.Ordinal));
            }

            int entId = Convert.ToInt32(argument.GetSplit(0));
            IEntity ent = Screen.Level.GetEntity(entId);

            if ((ent != null))
            {
                switch (command.ToLower())
                {
                    case "warp":
                        List<string> positionList = argument.Split(Convert.ToChar(",")).ToList();
                        Vector3 newPosition = new Vector3(Convert.ToSingle(positionList[1].Replace(".", GameController.DecSeparator)), Convert.ToSingle(positionList[2].Replace(".", GameController.DecSeparator)), Convert.ToSingle(positionList[3].Replace(".", GameController.DecSeparator)));

                        ent.Position = newPosition;
                        ent.CreatedWorld = false;
                        break;
                    case "scale":
                        List<string> scaleList = argument.Split(Convert.ToChar(",")).ToList();
                        Vector3 newScale = new Vector3(Convert.ToSingle(scaleList[1].Replace(".", GameController.DecSeparator)), Convert.ToSingle(scaleList[2].Replace(".", GameController.DecSeparator)), Convert.ToSingle(scaleList[3].Replace(".", GameController.DecSeparator)));

                        ent.Scale = newScale;
                        ent.CreatedWorld = false;
                        break;
                    case "remove":
                        ent.CanBeRemoved = true;
                        break;
                    case "setid":
                        ent.ID = Convert.ToInt32(argument.GetSplit(1));
                        break;
                    case "opacity":
                        ent.NormalOpacity = Convert.ToSingle(Convert.ToInt32(argument.GetSplit(1)) / 100);
                        break;
                    case "visible":
                        ent.Visible = Convert.ToBoolean(argument.GetSplit(1));
                        break;
                    //Case "move"
                    case "setadditionalvalue":
                        ent.AdditionalValue = argument.GetSplit(1);
                        break;
                    case "collision":
                        ent.Collision = Convert.ToBoolean(argument.GetSplit(1));

                        break;
                }
            }

            IsReady = true;
        }

        private void DoEnvironment()
        {
            string command = Value;
            string argument = "";

            if (command.Contains("(") & command.EndsWith(")"))
            {
                argument = command.Remove(0, command.IndexOf("(", StringComparison.Ordinal) + 1);
                argument = argument.Remove(argument.Length - 1, 1);
                command = command.Remove(command.IndexOf("(", StringComparison.Ordinal));
            }

            switch (argument.ToLower())
            {
                case "changeweathertype":
                    Screen.Level.WeatherType = Convert.ToInt32(argument);
                    Screen.Level.World = new World(Screen.Level.EnvironmentType, Screen.Level.WeatherType);
                    break;
                case "changeenvironmenttype":
                    Screen.Level.EnvironmentType = Convert.ToInt32(argument);
                    Screen.Level.World = new World(Screen.Level.EnvironmentType, Screen.Level.WeatherType);
                    break;
                case "canfly":
                    Screen.Level.CanFly = Convert.ToBoolean(argument);
                    break;
                case "candig":
                    Screen.Level.CanDig = Convert.ToBoolean(argument);
                    break;
                case "canteleport":
                    Screen.Level.CanTeleport = Convert.ToBoolean(argument);
                    break;
                case "wildpokemongrass":
                    Screen.Level.WildPokemonGrass = Convert.ToBoolean(argument);
                    break;
                case "wildpokemonwater":
                    Screen.Level.WildPokemonWater = Convert.ToBoolean(argument);
                    break;
                case "wildpokemoneverywhere":
                    Screen.Level.WildPokemonFloor = Convert.ToBoolean(argument);
                    break;
                case "isdark":
                    Screen.Level.IsDark = Convert.ToBoolean(argument);
                    break;
                case "resetwalkedsteps":
                    Screen.Level.WalkedSteps = 0;
                    break;
            }

            IsReady = true;
        }

        private void DoLevel()
        {
            string command = Value;
            string argument = "";

            if (command.Contains("(") & command.EndsWith(")"))
            {
                argument = command.Remove(0, command.IndexOf("(", StringComparison.Ordinal) + 1);
                argument = argument.Remove(argument.Length - 1, 1);
                command = command.Remove(command.IndexOf("(", StringComparison.Ordinal));
            }

            switch (command.ToLower())
            {
                case "update":
                    Screen.Level.Update();
                    Screen.Level.UpdateEntities();
                    Screen.Camera.Update();
                    break;
            }

            IsReady = true;
        }

        #endregion

        private void ViewPokemonImage()
        {
            int pokemonId = Convert.ToInt32(Value.GetSplit(0));
            bool shiny = Convert.ToBoolean(Value.GetSplit(1));
            bool front = Convert.ToBoolean(Value.GetSplit(2));

            Screen.PokemonImageView.Show(pokemonId, shiny, front);
            IsReady = true;
        }

        private void Move()
        {
            int targetId = Convert.ToInt32(Value.GetSplit(0));
            INPC targetNpc = Screen.Level.GetNPC(targetId);
            int moved = Convert.ToInt32(Value.GetSplit(1));

            Screen.Level.UpdateEntities();
            if (Started == false)
            {
                targetNpc.Moved += moved;
                Started = true;
            }
            else
            {
                if (targetNpc.Moved <= 0f)
                {
                    IsReady = true;
                }
            }
        }

        private void MoveAsync()
        {
            int targetId = Convert.ToInt32(Value.GetSplit(0));
            INPC targetNpc = Screen.Level.GetNPC(targetId);
            int moved = Convert.ToInt32(Value.GetSplit(1));

            targetNpc.Moved += moved;
            Started = true;
            IsReady = true;
        }

        private void MovePlayer()
        {
            if (Started == false)
            {
                Screen.Camera.Move(Convert.ToSingle(Value));
                Started = true;
                Screen.Level.OverworldPokemon.Visible = false;
            }
            else
            {
                Screen.Level.UpdateEntities();
                Screen.Camera.Update();
                if (Screen.Camera.IsMoving == false)
                {
                    IsReady = true;
                }
            }
        }

        private void Turn()
        {
            int targetId = Convert.ToInt32(Value.GetSplit(0));
            INPC targetNpc = Screen.Level.GetNPC(targetId);

            targetNpc.FaceRotation = Convert.ToInt32(Value.GetSplit(1));
            targetNpc.Update();
            targetNpc.UpdateEntity();
            IsReady = true;
        }

        private void Warp()
        {
            int targetId = Convert.ToInt32(Value.GetSplit(0));
            INPC targetNpc = Screen.Level.GetNPC(targetId);

            Vector3 targetPosition = new Vector3(Convert.ToInt32(Value.GetSplit(1)), Convert.ToInt32(Value.GetSplit(2)), Convert.ToInt32(Value.GetSplit(3)));
            targetNpc.Position = targetPosition;
            Logger.Debug(targetNpc.Position.ToString());
            targetNpc.Update();

            IsReady = true;
        }

        private void WarpPlayer()
        {
            int commas = 0;
            foreach (char c in Value)
            {
                if (c == ',')
                {
                    commas += 1;
                }
            }

            switch (commas)
            {
                case 4:
                    Screen.Level.WarpData.WarpDestination = Value.GetSplit(0);
                    Screen.Level.WarpData.WarpPosition = new Vector3(Convert.ToSingle(Value.GetSplit(1)), Convert.ToSingle(Value.GetSplit(2).Replace(".", GameController.DecSeparator)), Convert.ToSingle(Value.GetSplit(3)));
                    Screen.Level.WarpData.WarpRotations = Convert.ToInt32(Value.GetSplit(4));
                    Screen.Level.WarpData.DoWarpInNextTick = true;
                    Screen.Level.WarpData.CorrectCameraYaw = Screen.Camera.Yaw;
                    break;
                case 2:
                    Screen.Camera.Position = new Vector3(Convert.ToSingle(Value.GetSplit(0)), Convert.ToSingle(Value.GetSplit(1).Replace(".", GameController.DecSeparator)), Convert.ToSingle(Value.GetSplit(2)));
                    break;
            }

            Screen.Level.OverworldPokemon.Visible = false;

            IsReady = true;
        }

        private void Heal()
        {
            if (string.IsNullOrEmpty(Value))
            {
                Core.Player.HealParty();
            }
            else
            {
                string[] data = Value.Split(Convert.ToChar(","));
                List<int> members = new List<int>();
                foreach (string member in data)
                {
                    members.Add(Convert.ToInt32(member));
                }
                Core.Player.HealParty(members.ToArray());
            }

            IsReady = true;
        }

        private void TurnPlayer()
        {
            if (Started == false)
            {
                Screen.Camera.Turn(Convert.ToInt32(Value));
                Started = true;
                Screen.Level.OverworldPokemon.Visible = false;
            }
            else
            {
                Screen.Camera.Update();
                Screen.Level.UpdateEntities();
                if (Screen.Camera.Turning == false)
                {
                    IsReady = true;
                }
            }
        }

        private void GiveItem()
        {
            int itemId = Convert.ToInt32(Value.GetSplit(0));
            IItem item = Item.GetItemByID(itemId);

            int amount = Convert.ToInt32(Value.GetSplit(1));

            string message = "";
            if (amount == 1)
            {
                message = "Received the~" + item.Name + ".*" + Core.Player.Name + " stored it in the~" + item.ItemType + " pocket.";
            }
            else
            {
                message = "Received " + amount + "~" + item.PluralName + ".*" + Core.Player.Name + " stored them~in the " + item.ItemType + " pocket.";
            }

            Core.Player.Inventory.AddItem(itemId, amount);
            SoundManager.PlaySound("item_found", true);

            Screen.TextBox.ReDelay = 0f;
            Screen.TextBox.Show(message, new BaseEntity[] { });

            IsReady = true;
        }

        private void RemoveItem()
        {
            int itemId = Convert.ToInt32(Value.GetSplit(0));
            IItem item = Item.GetItemByID(itemId);

            int amount = Convert.ToInt32(Value.GetSplit(1));

            string message = "";
            if (amount == 1)
            {
                message = "<playername> handed over the~" + item.Name + "!";
            }
            else
            {
                message = "<playername> handed over the~" + item.PluralName + "!";
            }

            Core.Player.Inventory.RemoveItem(itemId, amount);

            Screen.TextBox.ReDelay = 0f;
            Screen.TextBox.Show(message, new BaseEntity[] { });

            IsReady = true;
        }

        private void GetBadge()
        {
            if (Value.IsNumeric())
            {
                if (Core.Player.Badges.Contains(Convert.ToInt32(Value)) == false)
                {
                    Core.Player.Badges.Add(Convert.ToInt32(Value));
                    SoundManager.PlaySound("badge_acquired", true);
                    Screen.TextBox.Show(Core.Player.Name + " received the~" + Badge.GetBadgeName(Convert.ToInt32(Value)) + "badge.", new BaseEntity[] { }, false, false);

                    Core.Player.AddPoints(10, "Got a badge (V1 script!).");
                }
            }
            else
            {
                throw new Exception("Invalid argument exception");
            }

            IsReady = true;
        }
    }
}
