using Microsoft.Xna.Framework;
using P3D.Legacy.Core.Entities;
using P3D.Legacy.Core.Resources;
using P3D.Legacy.Core.Screens;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using Microsoft.Xna.Framework.Graphics;
using P3D.Legacy.Core.Entities.Other;
using P3D.Legacy.Core.Resources.Models;

namespace P3D.Legacy.Core.World
{
    /*
    public class LevelLoader
    {
        const bool Multithread = false;
        public static List<Vector3> LoadedOffsetMapOffsets = new List<Vector3>();

        public static List<string> LoadedOffsetMapNames = new List<string>();

        private enum TagTypes
        {
            Entity,
            Floor,
            EntityField,
            Level,
            LevelActions,
            Npc,
            Shader,
            OffsetMap,
            Structure,
            Backdrop,
            None
        }

        Vector3 _offset;
        bool _loadOffsetMap = true;
        int _offsetMapLevel = 0;
        string _mapOrigin = "";
        //Prevents infinite loops when loading more than one offset map level.
        List<string> _sessionMapsLoaded = new List<string>();

        //Store these so other classes can get them.
        private List<Entity> _entities = new List<Entity>();

        private List<Entity> _floors = new List<Entity>();
        //A counter across all LevelLoader instances to count how many instances across the program are active.

        static int _busy = 0;

        public static bool IsBusy => _busy > 0;

        #region "File Loading"


        object[] _tempParams;

        /// <summary>
        /// Loads the level.
        /// </summary>
        /// <param name="Params">Params contruction: String LevelFile, bool IsOffsetMap, Vector3 Offset, int Offsetmaplevel, Str() InstanceLoadedOffsetMaps</param>
        public void LoadLevel(object[] Params)
        {
            _busy += 1;
            _tempParams = Params;

            if (Multithread)
            {
                new Thread(InternalLoad) { IsBackground = true }.Start();
            }
            else
            {
                InternalLoad();
            }
        }

        private void InternalLoad()
        {
            string levelPath = Convert.ToString(_tempParams[0]);
            bool loadOffsetMap = Convert.ToBoolean(_tempParams[1]);
            Vector3 offset = (Vector3) _tempParams[2];
            _offsetMapLevel = Convert.ToInt32(_tempParams[3]);
            _sessionMapsLoaded = (List<string>) _tempParams[4];

            Stopwatch timer = new Stopwatch();
            timer.Start();

            this._loadOffsetMap = loadOffsetMap;
            _mapOrigin = levelPath;

            if (loadOffsetMap == false)
            {
                Screen.Level.LevelFile = levelPath;

                Core.Player.LastSavePlace = Screen.Level.LevelFile;
                Core.Player.LastSavePlacePosition = Player.Temp.LastPosition.X + "," + Player.Temp.LastPosition.Y.ToString().Replace(GameController.DecSeparator, ".") + "," + Player.Temp.LastPosition.Z;

                Screen.Level.Entities.Clear();
                Screen.Level.Floors.Clear();
                Screen.Level.Shaders.Clear();
                Screen.Level.BackdropRenderer.Clear();

                Screen.Level.OffsetmapFloors.Clear();
                Screen.Level.OffsetmapEntities.Clear();

                Screen.Level.WildPokemonFloor = false;
                Screen.Level.WalkedSteps = 0;

                LoadedOffsetMapNames.Clear();
                LoadedOffsetMapOffsets.Clear();
                Floor.ClearFloorTemp();

                Player.Temp.MapSteps = 0;

                _sessionMapsLoaded.Add(levelPath);
            }

            levelPath = GameModeManager.GetMapPath(levelPath);
            Logger.Debug("Loading map: " + levelPath.Remove(0, GameController.GamePath.Length));
            Security.FileValidation.CheckFileValid(levelPath, false, "LevelLoader.vb");

            if (File.Exists(levelPath) == false)
            {
                Logger.Log(Logger.LogTypes.ErrorMessage,
                    "LevelLoader.vb: Error accessing map file \"" + levelPath + "\". File not found.");
                _busy -= 1;

                if (Core.CurrentScreen.Identification == Screen.Identifications.OverworldScreen && loadOffsetMap == false)
                {
                    ((BaseOverworldScreen) Core.CurrentScreen).Titles.Add(new OverworldScreen.Title("Couldn't find map file!", 20f, Microsoft.Xna.Framework.Color.White, 6f, Vector2.Zero, true));
                }

                return;
            }

            List<string> data = File.ReadAllLines(levelPath).ToList();
            Dictionary<string, object> tags = new Dictionary<string, object>();

            _offset = offset;

            for (var i = 0; i < data.Count; i++)
            {
                if (data[i].Contains("{"))
                {
                    data[i] = data[i].Remove(0, data[i].IndexOf("{"));

                    if (data[i].StartsWith("{\"Comment\"{COM"))
                    {
                        data[i] = data[i].Remove(0, data[i].IndexOf("[", StringComparison.Ordinal) + 1);
                        data[i] = data[i].Remove(data[i].IndexOf("]", StringComparison.Ordinal));

                        Logger.Log(Logger.LogTypes.Debug, data[i]);
                    }
                }
            }

            int countLines = 0;

            for (var i = 0;; i++)
            {
                if (i > data.Count - 1)
                {
                    break; // TODO: might not be correct. Was : Exit For
                }

                tags.Clear();
                if (data[i].Contains("{") && data[i].Contains("}"))
                {
                    try
                    {
                        var tagType = TagTypes.None;
                        data[i] = data[i].Remove(0, data[i].IndexOf("{", StringComparison.Ordinal) + 2);

                        if (data[i].ToLower().StartsWith("structure\""))
                            tagType = TagTypes.Structure;

                        if (tagType == TagTypes.Structure)
                        {
                            data[i] = data[i].Remove(0, data[i].IndexOf("[", StringComparison.Ordinal) + 1);
                            data[i] = data[i].Remove(data[i].Length - 3, 3);

                            tags = GetTags(data[i]);

                            var newLines = AddStructure(tags);

                            data.InsertRange(i + 1, newLines);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Log(Logger.LogTypes.Warning, "LevelLoader.vb: Failed to load map object! (Index: " + countLines + ") from mapfile: " + levelPath + "; Error message: " + ex.Message);
                    }
                }
            }

            for (var i = 0; i < data.Count; i++)
            {
                tags.Clear();
                var orgLine = data[i];
                countLines += 1;

                if (data[i].Contains("{") && data[i].Contains("}"))
                {
                    try
                    {
                        var tagType = TagTypes.None;
                        data[i] = data[i].Remove(0, data[i].IndexOf("{", StringComparison.Ordinal) + 2);

                        if (data[i].ToLower().StartsWith("entity\""))
                            tagType = TagTypes.Entity;
                        else if (data[i].ToLower().StartsWith("floor\""))
                            tagType = TagTypes.Floor;
                        else if (data[i].ToLower().StartsWith("entityfield\""))
                            tagType = TagTypes.EntityField;
                        else if (data[i].ToLower().StartsWith("level\""))
                            tagType = TagTypes.Level;
                        else if (data[i].ToLower().StartsWith("actions\""))
                            tagType = TagTypes.LevelActions;
                        else if (data[i].ToLower().StartsWith("npc\""))
                            tagType = TagTypes.Npc;
                        else if (data[i].ToLower().StartsWith("shader\""))
                            tagType = TagTypes.Shader;
                        else if (data[i].ToLower().StartsWith("offsetmap\""))
                            tagType = TagTypes.OffsetMap;
                        else if (data[i].ToLower().StartsWith("backdrop\""))
                            tagType = TagTypes.Backdrop;

                        if (tagType != TagTypes.None)
                        {
                            data[i] = data[i].Remove(0, data[i].IndexOf("[", StringComparison.Ordinal) + 1);
                            data[i] = data[i].Remove(data[i].Length - 3, 3);

                            tags = GetTags(data[i]);

                            switch (tagType)
                            {
                                case TagTypes.EntityField:
                                    EntityField(tags);
                                    break;
                                case TagTypes.Entity:
                                    AddEntity(tags, new System.Drawing.Size(1, 1), 1, true, new Vector3(1, 1, 1));
                                    break;
                                case TagTypes.Floor:
                                    AddFloor(tags);
                                    break;
                                case TagTypes.Level:
                                    if (!loadOffsetMap)
                                        SetupLevel(tags);
                                    break;
                                case TagTypes.LevelActions:
                                    if (!loadOffsetMap)
                                        SetupActions(tags);
                                    break;
                                case TagTypes.Npc:
                                    AddNpc(tags);
                                    break;
                                case TagTypes.Shader:
                                    if (!loadOffsetMap)
                                        AddShader(tags);
                                    break;
                                case TagTypes.OffsetMap:
                                    if (!loadOffsetMap || _offsetMapLevel <= Core.GameOptions.MaxOffsetLevel)
                                        AddOffsetMap(tags);
                                    break;
                                case TagTypes.Backdrop:
                                    if (!loadOffsetMap)
                                        AddBackdrop(tags);
                                    break;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Log(Logger.LogTypes.Warning, "LevelLoader.vb: Failed to load map object! (Index: " + countLines + ") (Line: " + orgLine + ") from mapfile: " + levelPath + "; Error message: " + ex.Message);
                    }
                }
            }

            if (!loadOffsetMap)
                LoadBerries();

            foreach (var s in Screen.Level.Shaders)
            {
                if (!s.HasBeenApplied)
                {
                    s.ApplyShader(Screen.Level.Entities.ToArray());
                    s.ApplyShader(Screen.Level.Floors.ToArray());
                }
            }

            Logger.Debug("Map loading finished: " + levelPath.Remove(0, GameController.GamePath.Length));
            Logger.Debug("Loaded textures: " + TextureManager.TextureList.Count);
            timer.Stop();
            Logger.Debug("Map loading time: " + timer.ElapsedTicks + " Ticks; " + timer.ElapsedMilliseconds + " Milliseconds.");

            //Dim xmlLevelLoader As New XmlLevelLoader
            //xmlLevelLoader.Load(My.Computer.FileSystem.SpecialDirectories.Desktop && "\t.xml", _5DHero.XmlLevelLoader.LevelElement.Types.Default, Vector3.Zero)

            _busy -= 1;

            if (_busy == 0)
            {
                Screen.Level.StartOffsetMapUpdate();
            }
        }

        private Dictionary<string, object> GetTags(string line)
        {
            var tags = new Dictionary<string, object>();

            var tagList = line.Split( new [] { "}{" }, StringSplitOptions.RemoveEmptyEntries);
            for (var i = 0; i <= tagList.Length - 1; i++)
            {
                var currentTag = tagList[i];

                if (!currentTag.EndsWith("}}"))
                    currentTag += "}";

                if (!currentTag.StartsWith("{"))
                    currentTag = "{" + currentTag;

                ProcessTag(ref tags, currentTag);
            }

            return tags;
        }

        private void ProcessTag(ref Dictionary<string, object> dictionary, string tag)
        {
            string tagName = "";
            string tagContent = "";

            tag = tag.Remove(0, 1);
            tag = tag.Remove(tag.Length - 1, 1);

            tagName = tag.Remove(tag.IndexOf("{", StringComparison.Ordinal) - 1).Remove(0, 1);
            tagContent = tag.Remove(0, tag.IndexOf("{", StringComparison.Ordinal));

            string[] contentRows = tagContent.Split(Convert.ToChar("}"));
            for (var i = 0; i < contentRows.Length; i++)
            {
                if (contentRows[i].Length > 0)
                {
                    contentRows[i] = contentRows[i].Remove(0, 1);

                    string subTagType = contentRows[i].Remove(contentRows[i].IndexOf("[", StringComparison.Ordinal));
                    string subTagValue = contentRows[i].Remove(0, contentRows[i].IndexOf("[", StringComparison.Ordinal) + 1);
                    subTagValue = subTagValue.Remove(subTagValue.Length - 1, 1);

                    switch (subTagType.ToLower())
                    {
                        case "int":
                            dictionary.Add(tagName, Convert.ToInt32(subTagValue));
                            break;
                        case "str":
                            dictionary.Add(tagName, Convert.ToString(subTagValue));
                            break;
                        case "sng":
                            subTagValue = subTagValue.Replace(".", GameController.DecSeparator);
                            dictionary.Add(tagName, Convert.ToSingle(subTagValue));
                            break;
                        case "bool":
                            dictionary.Add(tagName, Convert.ToBoolean(subTagValue));
                            break;
                        case "intarr":
                        {
                            string[] values = subTagValue.Split(Convert.ToChar(","));
                            List<int> arr = new List<int>();
                            foreach (string value in values)
                                arr.Add(Convert.ToInt32(value));

                            dictionary.Add(tagName, arr);
                        }
                            break;
                        case "rec":
                        {
                            string[] content = subTagValue.Split(Convert.ToChar(","));
                            dictionary.Add(tagName,
                                new Rectangle(Convert.ToInt32(content[0]),
                                    Convert.ToInt32(content[1]), Convert.ToInt32(content[2]),
                                    Convert.ToInt32(content[3])));
                        }
                            break;
                        case "recarr":
                        {
                            var values = subTagValue.Split(Convert.ToChar("]"));
                            var arr = (values.Where(value => value.Length > 0)
                                .Select(value => value.Remove(0, 1))
                                .Select(value => value.Split(Convert.ToChar(",")))
                                .Select(
                                    content =>
                                        new Rectangle(Convert.ToInt32(content[0]),
                                            Convert.ToInt32(content[1]), Convert.ToInt32(content[2]),
                                            Convert.ToInt32(content[3])))).ToList();

                            dictionary.Add(tagName, arr);
                        }
                            break;
                        case "sngarr":
                        {
                            var values = subTagValue.Split(Convert.ToChar(","));
                            var arr = values.Select(value => value.Replace(".", GameController.DecSeparator)).Select(value => Convert.ToSingle(value)).ToList();

                            dictionary.Add(tagName, arr);
                        }
                            break;
                    }
                }
            }
        }

        private object GetTag(Dictionary<string, object> tags, string tagName)
        {
            if (tags.ContainsKey(tagName))
            {
                return tags[tagName];
            }

            for (var i = 0; i <= tags.Count - 1; i++)
            {
                if (tags.Keys[i].ToLower() == tagName.ToLower())
                {
                    return tags.Values[i];
                }
            }

            return null;
        }

        private bool TagExists(Dictionary<string, object> tags, string tagName)
        {
            if (tags.ContainsKey(tagName))
            {
                return true;
            }

            for (var i = 0; i <= tags.Count - 1; i++)
            {
                if (tags.Keys[i].ToLower() == tagName.ToLower())
                {
                    return true;
                }
            }

            return false;
        }

        #endregion

        private void AddOffsetMap(Dictionary<string, object> tags)
        {
            if (Core.GameOptions.LoadOffsetMaps > 0)
            {
                List<int> offsetList = (List<int>) GetTag(tags, "Offset");
                Vector3 mapOffset = new Vector3(offsetList[0], 0, offsetList[1]);
                if (offsetList.Count >= 3)
                {
                    mapOffset = new Vector3(offsetList[0], offsetList[1], offsetList[2]);
                }

                string mapName = Convert.ToString(GetTag(tags, "Map"));

                if (_loadOffsetMap)
                {
                    if (_sessionMapsLoaded.Contains(mapName))
                    {
                        return;
                    }
                }
                _sessionMapsLoaded.Add(mapName);

                LoadedOffsetMapNames.Add(mapName);
                LoadedOffsetMapOffsets.Add(mapOffset);

                string listName = Screen.Level.LevelFile + "|" + mapName + "|" + Screen.Level.World.CurrentWeather + "|" +
                                  BaseWorld.GetCurrentRegionWeather() + "|" + BaseWorld.GetTime + "|" + BaseWorld.CurrentSeason;
                if (Core.OffsetMaps.ContainsKey(listName) == false)
                {
                    List<List<Entity>> mapList = new List<List<Entity>>();

                    List<object> @params = new List<object>();
                    @params.AddRange( new object [] { mapName, true, mapOffset + _offset, _offsetMapLevel + 1, _sessionMapsLoaded } );

                    int offsetEntityCount = Screen.Level.OffsetmapEntities.Count;
                    int offsetFloorCount = Screen.Level.OffsetmapFloors.Count;

                    LevelLoader levelLoader = new LevelLoader();
                    levelLoader.LoadLevel(@params.ToArray());
                    List<Entity> entList = new List<Entity>();
                    List<Entity> floorList = new List<Entity>();

                    for (var i = offsetEntityCount; i <= Screen.Level.OffsetmapEntities.Count - 1; i++)
                    {
                        entList.Add(Screen.Level.OffsetmapEntities[i]);
                    }
                    for (var i = offsetFloorCount; i <= Screen.Level.OffsetmapFloors.Count - 1; i++)
                    {
                        floorList.Add(Screen.Level.OffsetmapFloors[i]);
                    }
                    mapList.AddRange( new [] { entList, floorList } );

                    Core.OffsetMaps.Add(listName, mapList);
                }
                else
                {
                    Logger.Debug("Loaded Offsetmap from store: " + mapName);

                    foreach (var e in Core.OffsetMaps[listName][0])
                    {
                        if (e.MapOrigin == mapName)
                        {
                            e.IsOffsetMapContent = true;
                            Screen.Level.OffsetmapEntities.Add(e);
                        }
                    }
                    foreach (var e in Core.OffsetMaps[listName][1])
                    {
                        if (e.MapOrigin == mapName)
                        {
                            e.IsOffsetMapContent = true;
                            Screen.Level.OffsetmapFloors.Add(e);
                        }
                    }
                }
                Logger.Debug("Offset maps in store: " + Core.OffsetMaps.Count);

                Screen.Level.OffsetmapEntities = Screen.Level.OffsetmapEntities.OrderByDescending(e => e.CameraDistance).ToList();

                foreach (var entity in Screen.Level.OffsetmapEntities)
                {
                    entity.UpdateEntity();
                }
                foreach (var floor in Screen.Level.OffsetmapFloors)
                {
                    floor.UpdateEntity();
                }
            }
        }

        #region "AddElements"


        static Dictionary<string, List<string>> _tempStructureList = new Dictionary<string, List<string>>();

        public static void ClearTempStructures()
        {
            _tempStructureList.Clear();
        }

        private string[] AddStructure(Dictionary<string, object> tags)
        {
            List<float> offsetList = (List<float>) GetTag(tags, "Offset");
            Vector3 mapOffset = new Vector3(offsetList[0], 0, offsetList[1]);
            if (offsetList.Count >= 3)
            {
                mapOffset = new Vector3(offsetList[0], offsetList[1], offsetList[2]);
            }

            int mapRotation = -1;
            if (TagExists(tags, "Rotation"))
            {
                mapRotation = Convert.ToInt32(GetTag(tags, "Rotation"));
            }

            string mapName = Convert.ToString(GetTag(tags, "Map"));
            if (mapName.EndsWith(".dat") == false)
            {
                mapName = mapName + ".dat";
            }

            bool addNpc = false;
            if (TagExists(tags, "AddNPC"))
            {
                addNpc = Convert.ToBoolean(GetTag(tags, "AddNPC"));
            }

            string structureKey = mapOffset.X.ToString() + "|" + mapOffset.Y.ToString() + "|" + mapOffset.Z.ToString() +
                                  "|" + mapName;

            if (_tempStructureList.ContainsKey(structureKey) == false)
            {
                string filepath = GameModeManager.GetMapPath(mapName);
                Security.FileValidation.CheckFileValid(filepath, false, "LevelLoader.vb/StructureSpawner");

                if (File.Exists(filepath) == false)
                {
                    Logger.Log(Logger.LogTypes.ErrorMessage,
                        "LevelLoader.vb: Error loading structure from \"" + filepath + "\". File not found.");

                    return new string[] { } ;
                }

                string[] mapContent = File.ReadAllLines(filepath);
                List<string> structureList = new List<string>();

                for (var i = 0; i < mapContent.Length; i++)
                {
                    string line = mapContent[i];
                    if (line.EndsWith("}"))
                    {
                        bool addLine = false;
                        switch (true)
                        {
                            case line.Trim(' ', Strings.Chr(9)).StartsWith("{\"Entity\"{ENT["):
                                addLine = true;
                                break;
                            case line.Trim(' ', Strings.Chr(9)).StartsWith("{\"Floor\"{ENT["):
                                addLine = true;
                                break;
                            case line.Trim(' ', Strings.Chr(9)).StartsWith("{\"EntityField\"{ENT["):
                                addLine = true;
                                break;
                            case line.Trim(' ', Strings.Chr(9)).StartsWith("{\"NPC\"{NPC["):
                                if (addNpc)
                                {
                                    addLine = true;
                                }
                                break;
                            case line.Trim(' ', Strings.Chr(9)).StartsWith("{\"Shader\"{SHA["):
                                addLine = true;
                                break;
                        }

                        if (addLine)
                        {
                            line = ReplaceStructurePosition(line, mapOffset);

                            if (mapRotation > -1)
                            {
                                line = ReplaceStructureRotation(line, mapRotation);
                            }

                            structureList.Add(line);
                        }
                    }
                }

                _tempStructureList.Add(structureKey, structureList);
            }

            return _tempStructureList[structureKey].ToArray();
        }

        private string ReplaceStructureRotation(string line, int mapRotation)
        {
            var replaceString = "";

            if (line.ToLower().Contains("{\"rotation\"{int["))
                replaceString = "{\"rotation\"{int[";

            if (!string.IsNullOrEmpty(replaceString))
            {
                var rotationString = line.Remove(0, line.ToLower().IndexOf(replaceString, StringComparison.Ordinal));
                rotationString = rotationString.Remove(rotationString.IndexOf("]}}", StringComparison.Ordinal) + 3);

                var rotationData = rotationString.Remove(0, rotationString.IndexOf("[", StringComparison.Ordinal) + 1);
                rotationData = rotationData.Remove(rotationData.IndexOf("]", StringComparison.Ordinal));

                var newRotation = Convert.ToInt32(rotationData) + mapRotation;
                while (newRotation > 3)
                {
                    newRotation -= 4;
                }

                line = line.Replace(rotationString, "{\"rotation\"{int[" + newRotation + "]}}");
            }

            return line;
        }

        private string ReplaceStructurePosition(string line, Vector3 mapOffset)
        {
            string replaceString = "";

            if (line.ToLower().Contains("{\"position\"{sngarr["))
                replaceString = "{\"position\"{sngarr[";
            else if (line.ToLower().Contains("{\"position\"{intarr["))
                replaceString = "{\"position\"{intarr[";

            if (!string.IsNullOrEmpty(replaceString))
            {
                var positionString = line.Remove(0, line.ToLower().IndexOf(replaceString, StringComparison.Ordinal));
                positionString = positionString.Remove(positionString.IndexOf("]}}", StringComparison.Ordinal) + 3);

                var positionData = positionString.Remove(0, positionString.IndexOf("[", StringComparison.Ordinal) + 1);
                positionData = positionData.Remove(positionData.IndexOf("]", StringComparison.Ordinal));

                var posArr = positionData.Split(Convert.ToChar(","));
                Vector3 newPosition =
                    new Vector3(
                        ScriptConversion.ToSingle(posArr[0].Replace(".", GameController.DecSeparator)) + mapOffset.X,
                        ScriptConversion.ToSingle(posArr[1].Replace(".", GameController.DecSeparator)) + mapOffset.Y,
                        Convert.ToSingle(posArr[2].Replace(".", GameController.DecSeparator)) + mapOffset.Z);

                if (line.ToLower().Contains("{\"position\"{sngarr["))
                {
                    line = line.Replace(positionString,
                        "{\"position\"{sngarr[" + newPosition.X.ToString(CultureInfo.InvariantCulture).Replace(GameController.DecSeparator, ".") +
                        "," + newPosition.Y.ToString(CultureInfo.InvariantCulture).Replace(GameController.DecSeparator, ".") + "," +
                        newPosition.Z.ToString(CultureInfo.InvariantCulture).Replace(GameController.DecSeparator, ".") + "]}}");
                }
                else
                {
                    line = line.Replace(positionString,
                        "{\"position\"{intarr[" +
                        Convert.ToInt32(newPosition.X).ToString().Replace(GameController.DecSeparator, ".") + "," +
                        Convert.ToInt32(newPosition.Y).ToString().Replace(GameController.DecSeparator, ".") + "," +
                        Convert.ToInt32(newPosition.Z).ToString().Replace(GameController.DecSeparator, ".") + "]}}");
                }
            }

            return line;
        }

        private void EntityField(Dictionary<string, object> tags)
        {
            var sizeList = (List<int>) GetTag(tags, "Size");
            var fill = true;
            if (TagExists(tags, "Fill"))
            {
                fill = Convert.ToBoolean(GetTag(tags, "Fill"));
            }
            var steps = new Vector3(1, 1, 1);
            if (TagExists(tags, "Steps"))
            {
                var stepList = (List<float>) GetTag(tags, "Steps");
                steps = stepList.Count == 3 ? new Vector3(stepList[0], stepList[1], stepList[2]) : new Vector3(stepList[0], 1, stepList[1]);
            }

            if (sizeList.Count == 3)
                AddEntity(tags, new System.Drawing.Size(sizeList[0], sizeList[2]), sizeList[1], fill, steps);
            else
                AddEntity(tags, new System.Drawing.Size(sizeList[0], sizeList[1]), 1, fill, steps);
        }

        private void AddNpc(Dictionary<string, object> tags)
        {
            var posList = (List<float>) GetTag(tags, "Position");
            var position = new Vector3(posList[0] + _offset.X, posList[1] + _offset.Y, posList[2] + _offset.Z);

            var scale = new Vector3(1);
            if (TagExists(tags, "Scale"))
            {
                var scaleList = (List<float>) GetTag(tags, "Scale");
                scale = new Vector3(scaleList[0], scaleList[1], scaleList[2]);
            }

            var textureId = Convert.ToString(GetTag(tags, "TextureID"));
            var rotation = Convert.ToInt32(GetTag(tags, "Rotation"));
            var actionValue = Convert.ToInt32(GetTag(tags, "Action"));
            var additionalValue = Convert.ToString(GetTag(tags, "AdditionalValue"));
            var name = Convert.ToString(GetTag(tags, "Name"));
            var id = Convert.ToInt32(GetTag(tags, "ID"));

            var movement = Convert.ToString(GetTag(tags, "Movement"));
            var moveRectangles = (List<Rectangle>) GetTag(tags, "MoveRectangles");

            var shader = new Vector3(1f);
            if (TagExists(tags, "Shader"))
            {
                var shaderList = (List<float>) GetTag(tags, "Shader");
                shader = new Vector3(shaderList[0], shaderList[1], shaderList[2]);
            }

            var animateIdle = false;
            if (TagExists(tags, "AnimateIdle"))
                animateIdle = Convert.ToBoolean(GetTag(tags, "AnimateIdle"));
            
            var npc = (BaseNPC) Entity.GetNewEntity("NPC", position, 
            new Texture2D[] { null }, new [] { 0, 0 }, true, new Vector3(0), scale, BaseModel.BillModel, actionValue, additionalValue, true,
            shader, -1, _mapOrigin, "", _offset, new object[] { textureId, rotation, name, id, animateIdle, movement, moveRectangles });

            if (_loadOffsetMap == false)
                Screen.Level.Entities.Add(npc);
            else
                Screen.Level.OffsetmapEntities.Add(npc);
        }

        private void AddFloor(Dictionary<string, object> tags)
        {
            var sizeList = (List<int>) GetTag(tags, "Size");
            var size = new System.Drawing.Size(sizeList[0], sizeList[1]);

            var posList = (List<int>) GetTag(tags, "Position");
            var position = new Vector3(posList[0] + _offset.X, posList[1] + _offset.Y, posList[2] + _offset.Z);

            var texturePath = Convert.ToString(GetTag(tags, "TexturePath"));
            var textureRectangle = (Rectangle) GetTag(tags, "Texture");
            var texture = TextureManager.GetTexture(texturePath, textureRectangle);

            var visible = true;
            if (TagExists(tags, "Visible"))
                visible = Convert.ToBoolean(GetTag(tags, "Visible"));

            var shader = new Vector3(1f);
            if (TagExists(tags, "Shader"))
            {
                var shaderList = (List<float>) GetTag(tags, "Shader");
                shader = new Vector3(shaderList[0], shaderList[1], shaderList[2]);
            }

            var removeFloor = false;
            if (TagExists(tags, "Remove"))
                removeFloor = Convert.ToBoolean(GetTag(tags, "Remove"));

            var hasSnow = true;
            if (TagExists(tags, "hasSnow"))
                hasSnow = Convert.ToBoolean(GetTag(tags, "hasSnow"));

            var hasSand = true;
            if (TagExists(tags, "hasSand"))
                hasSand = Convert.ToBoolean(GetTag(tags, "hasSand"));

            var hasIce = false;
            if (TagExists(tags, "isIce"))
                hasIce = Convert.ToBoolean(GetTag(tags, "isIce"));

            var rotation = 0;
            if (TagExists(tags, "Rotation"))
                rotation = Convert.ToInt32(GetTag(tags, "Rotation"));

            var seasonTexture = "";
            if (TagExists(tags, "SeasonTexture"))
                seasonTexture = Convert.ToString(GetTag(tags, "SeasonTexture"));

            var floorList = Screen.Level.Floors;
            if (_loadOffsetMap)
                floorList = Screen.Level.OffsetmapFloors;

            if (!removeFloor)
            {
                for (var x = 0; x <= size.Width - 1; x++)
                {
                    for (var z = 0; z <= size.Height - 1; z++)
                    {
                        bool exists = false;

                        int iZ = z;
                        int iX = x;

                        var ent = _loadOffsetMap ? Screen.Level.OffsetmapFloors.Find(e => e.Position == new Vector3(position.X + iX, position.Y, position.Z + iZ)) : Screen.Level.Floors.Find(e => e.Position == new Vector3(position.X + iX, position.Y, position.Z + iZ));

                        if ((ent != null))
                        {
                            ent.Textures = new [] { texture };
                            ent.Visible = visible;
                            ent.SeasonColorTexture = seasonTexture;
                            ent.LoadSeasonTextures();
                            ((Floor) ent).SetRotation(rotation);
                            ((Floor) ent).hasSnow = hasSnow;
                            ((Floor) ent).IsIce = hasIce;
                            ((Floor) ent).hasSand = hasSand;
                            exists = true;
                        }

                        if (exists == false)
                        {
                            //Floor f = new Floor(Position.X + x, Position.Y, Position.Z + z, { TextureManager.GetTexture(TexturePath, TextureRectangle) }, { 0, 0 }, false, rotation, new Vector3(1f), BaseModel.FloorModel, 0, "", Visible, Shader, hasSnow, hasIce, hasSand);
                            //f.MapOrigin = MapOrigin;
                            //f.SeasonColorTexture = SeasonTexture;
                            //f.LoadSeasonTextures();
                            //f.IsOffsetMapContent = loadOffsetMap;
                            //floorList.Add(f);
                        }
                    }
                }
            }
            else
            {
                for (var x = 0; x <= size.Width - 1; x++)
                {
                    for (var z = 0; z <= size.Height - 1; z++)
                    {
                        for (var i = 0; i <= floorList.Count; i++)
                        {
                            if (i < floorList.Count)
                            {
                                Entity floor = floorList[i];
                                if (floor.Position.X == position.X + x && floor.Position.Y == position.Y && floor.Position.Z == position.Z + z)
                                {
                                    floorList.RemoveAt(i);
                                    i -= 1;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void AddEntity(Dictionary<string, object> tags, System.Drawing.Size size, int sizeY, bool fill, Vector3 steps)
        {
            string entityId = Convert.ToString(GetTag(tags, "EntityID"));

            int id = -1;
            if (TagExists(tags, "ID"))
            {
                id = Convert.ToInt32(GetTag(tags, "ID"));
            }

            List<float> posList = (List<float>) GetTag(tags, "Position");
            Vector3 position = new Vector3(posList[0] + _offset.X, posList[1] + _offset.Y, posList[2] + _offset.Z);

            List<Rectangle> texList =
                (List<Rectangle>) GetTag(tags, "Textures");
            List<Texture2D> textureList = new List<Texture2D>();
            string texturePath = Convert.ToString(GetTag(tags, "TexturePath"));
            foreach (Rectangle textureRectangle in texList)
            {
                textureList.Add(TextureManager.GetTexture(texturePath, textureRectangle));
            }
            Texture2D[] textureArray = textureList.ToArray();

            List<int> textureIndexList = (List<int>) GetTag(tags, "TextureIndex");
            int[] textureIndex = textureIndexList.ToArray();

            List<float> scaleList = default(List<float>);
            Vector3 scale = new Vector3(1);
            if (TagExists(tags, "Scale"))
            {
                scaleList = (List<float>) GetTag(tags, "Scale");
                scale = new Vector3(scaleList[0], scaleList[1], scaleList[2]);
            }

            bool collision = Convert.ToBoolean(GetTag(tags, "Collision"));

            int modelId = Convert.ToInt32(GetTag(tags, "ModelID"));

            int actionValue = Convert.ToInt32(GetTag(tags, "Action"));

            string additionalValue = "";
            if (TagExists(tags, "AdditionalValue"))
            {
                additionalValue = Convert.ToString(GetTag(tags, "AdditionalValue"));
            }

            Vector3 rotation = Entity.GetRotationFromInteger(Convert.ToInt32(GetTag(tags, "Rotation")));

            bool visible = true;
            if (TagExists(tags, "Visible"))
                visible = Convert.ToBoolean(GetTag(tags, "Visible"));

            var shader = new Vector3(1f);
            if (TagExists(tags, "Shader"))
            {
                var shaderList = (List<float>) GetTag(tags, "Shader");
                shader = new Vector3(shaderList[0], shaderList[1], shaderList[2]);
            }

            var rotationXyz = new Vector3();
            if (TagExists(tags, "RotationXYZ"))
            {
                var rotationList = (List<float>) GetTag(tags, "RotationXYZ");
                rotation = new Vector3(rotationList[0], rotationList[1], rotationList[2]);
            }

            var seasonTexture = "";
            if (TagExists(tags, "SeasonTexture"))
                seasonTexture = Convert.ToString(GetTag(tags, "SeasonTexture"));

            var seasonToggle = "";
            if (TagExists(tags, "SeasonToggle"))
                seasonToggle = Convert.ToString(GetTag(tags, "SeasonToggle"));

            var opacity = 1f;
            if (TagExists(tags, "Opacity"))
                opacity = Convert.ToSingle(GetTag(tags, "Opacity"));

            for (var x = 0; x <= size.Width - 1; x += steps.X)
            {
                for (var z = 0; z <= size.Height - 1; z += steps.Z)
                {
                    for (var y = 0; y <= sizeY - 1; y += steps.Y)
                    {
                        bool doAdd = false;
                        if (fill == false)
                        {
                            if (x == 0 || z == 0 || z == size.Height - 1 || x == size.Width - 1)
                            {
                                doAdd = true;
                            }
                        }
                        else
                        {
                            doAdd = true;
                        }

                        if (!string.IsNullOrEmpty(seasonToggle))
                        {
                            if (seasonToggle.Contains(",") == false)
                            {
                                if (seasonToggle.ToLower() != BaseWorld.CurrentSeason.ToString().ToLower())
                                {
                                    doAdd = false;
                                }
                            }
                            else
                            {
                                string[] seasons = seasonToggle.ToLower().Split(Convert.ToChar(","));
                                if (seasons.Contains(BaseWorld.CurrentSeason.ToString().ToLower()) == false)
                                {
                                    doAdd = false;
                                }
                            }
                        }

                        if (doAdd)
                        {
                            var newEnt = Entity.GetNewEntity(EntityID, new Vector3(Position.X + X, Position.Y + Y, Position.Z + Z), TextureArray, TextureIndex, Collision, Rotation, Scale, BaseModel.getModelbyID(ModelID), ActionValue, AdditionalValue, Visible, Shader, ID, MapOrigin, SeasonTexture, Offset, { }, Opacity);
                            newEnt.IsOffsetMapContent = loadOffsetMap;

                            if ((newEnt != null))
                            {
                                if (_loadOffsetMap == false)
                                {
                                    Screen.Level.Entities.Add(newEnt);
                                }
                                else
                                {
                                    Screen.Level.OffsetmapEntities.Add(newEnt);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void SetupLevel(Dictionary<string, object> tags)
        {
            var name = Convert.ToString(GetTag(tags, "Name"));
            var musicLoop = Convert.ToString(GetTag(tags, "MusicLoop"));

            Screen.Level.WildPokemonFloor = TagExists(tags, "WildPokemon") && Convert.ToBoolean(GetTag(tags, "WildPokemon"));

            Screen.Level.ShowOverworldPokemon = !TagExists(tags, "OverworldPokemon") || Convert.ToBoolean(GetTag(tags, "OverworldPokemon"));

            Screen.Level.CurrentRegion = TagExists(tags, "CurrentRegion") ? Convert.ToString(GetTag(tags, "CurrentRegion")) : "Johto";

            Screen.Level.HiddenAbilityChance = TagExists(tags, "HiddenAbility") ? Convert.ToInt32(GetTag(tags, "HiddenAbility")) : 0;
            Screen.Level.MapName = name;
            Screen.Level.MusicLoop = musicLoop;
        }


        public static string MapScript = "";

        private void SetupActions(Dictionary<string, object> tags)
        {
            Screen.Level.CanTeleport = TagExists(tags, "CanTeleport") && Convert.ToBoolean(GetTag(tags, "CanTeleport"));

            Screen.Level.CanDig = TagExists(tags, "CanDig") && Convert.ToBoolean(GetTag(tags, "CanDig"));

            Screen.Level.CanFly = TagExists(tags, "CanFly") && Convert.ToBoolean(GetTag(tags, "CanFly"));

            Screen.Level.RideType = TagExists(tags, "RideType") ? Convert.ToInt32(GetTag(tags, "RideType")) : 0;

            Screen.Level.EnvironmentType = TagExists(tags, "EnviromentType") ? Convert.ToInt32(GetTag(tags, "EnviromentType")) : 0;

            Screen.Level.WeatherType = TagExists(tags, "Weather") ? Convert.ToInt32(GetTag(tags, "Weather")) : 0;

            //It's not my fault I swear. The keyboard was slippy, I was partly sick and there was fog on the road and I couldnt see.
            var lightningExists = TagExists(tags, "Lightning");
            var lightingExists = TagExists(tags, "Lighting");

            if (lightningExists && lightingExists)
            {
                Screen.Level.LightingType = Convert.ToInt32(GetTag(tags, "Lighting"));
            }
            else if (lightingExists)
            {
                Screen.Level.LightingType = Convert.ToInt32(GetTag(tags, "Lighting"));
            }
            else if (lightningExists)
            {
                Screen.Level.LightingType = Convert.ToInt32(GetTag(tags, "Lightning"));
            }
            else
            {
                Screen.Level.LightingType = 1;
            }

            Screen.Level.IsDark = TagExists(tags, "IsDark") && Convert.ToBoolean(GetTag(tags, "IsDark"));

            if (TagExists(tags, "Terrain"))
            {
                Screen.Level.Terrain.TerrainType = Terrain.FromString(Convert.ToString(GetTag(tags, "Terrain")));
            }
            else
            {
                Screen.Level.Terrain.TerrainType = TerrainTypeEnums.Plain;
            }

            Screen.Level.IsSafariZone = TagExists(tags, "IsSafariZone") && Convert.ToBoolean(GetTag(tags, "IsSafariZone"));

            if (TagExists(tags, "BugCatchingContest"))
            {
                Screen.Level.IsBugCatchingContest = true;
                Screen.Level.BugCatchingContestData = Convert.ToString(GetTag(tags, "BugCatchingContest"));
            }
            else
            {
                Screen.Level.IsBugCatchingContest = false;
                Screen.Level.BugCatchingContestData = "";
            }

            if (TagExists(tags, "MapScript"))
            {
                var scriptName = Convert.ToString(GetTag(tags, "MapScript"));
                if (Core.CurrentScreen.Identification == Screen.Identifications.OverworldScreen)
                {
                    if (((BaseOverworldScreen) Core.CurrentScreen).ActionScript.IsReady == true)
                    {
                        ((BaseOverworldScreen) Core.CurrentScreen).ActionScript.reDelay = 0f;
                        ((BaseOverworldScreen) Core.CurrentScreen).ActionScript.StartScript(scriptName, 0);
                        //A script intro is playing (fly)
                    }
                    else
                    {
                        MapScript = scriptName;
                    }
                    //Must be a direct save load from the main menu.
                }
                else
                {
                    MapScript = scriptName;
                }
            }
            else
            {
                MapScript = "";
            }

            if (TagExists(tags, "RadioChannels"))
                foreach (var c in Convert.ToString(GetTag(tags, "RadioChannels")).Split(Convert.ToChar(",")))
                    Screen.Level.AllowedRadioChannels.Add(Convert.ToDecimal(c.Replace(".", GameController.DecSeparator)));
            else
                Screen.Level.AllowedRadioChannels.Clear();

            Screen.Level.BattleMapData = TagExists(tags, "BattleMap") ? Convert.ToString(GetTag(tags, "BattleMap")) : "";

            Screen.Level.SurfingBattleMapData = TagExists(tags, "SurfingBattleMap") ? Convert.ToString(GetTag(tags, "SurfingBattleMap")) : "";

            Screen.Level.World = new World(Screen.Level.EnvironmentType, Screen.Level.WeatherType);
        }

        private void AddShader(Dictionary<string, object> tags)
        {
            var sizeList = (List<int>) GetTag(tags, "Size");
            var size = new Vector3(sizeList[0], 1, sizeList[1]);
            if (sizeList.Count == 3)
                size = new Vector3(sizeList[0], sizeList[1], sizeList[2]);

            var shaderList = (List<float>) GetTag(tags, "Shader");
            var shader = new Vector3(shaderList[0], shaderList[1], shaderList[2]);

            var stopOnContact = Convert.ToBoolean(GetTag(tags, "StopOnContact"));

            var posList = (List<int>) GetTag(tags, "Position");
            var position = new Vector3(posList[0] + _offset.X, posList[1] + _offset.Y, posList[2] + _offset.Z);

            var objectSizeList = (List<int>) GetTag(tags, "Size");
            var objectSize = new System.Drawing.Size(objectSizeList[0], objectSizeList[1]);

            var dayTime = new List<int>();
            if (TagExists(tags, "DayTime"))
                dayTime = (List<int>) GetTag(tags, "DayTime");

            if (dayTime.Contains((int) World.GetTime) || dayTime.Contains(-1) || dayTime.Count == 0)
            {
                var newShader = new Shader(position, size, shader, stopOnContact);
                Screen.Level.Shaders.Add(newShader);
            }
        }

        private void AddBackdrop(Dictionary<string, object> tags)
        {
            var sizeList = (List<int>) GetTag(tags, "Size");
            var width = sizeList[0];
            var height = sizeList[1];

            var posList = (List<float>) GetTag(tags, "Position");
            var position = new Vector3(posList[0] + _offset.X, posList[1] + _offset.Y, posList[2] + _offset.Z);

            var rotation = Vector3.Zero;
            if (TagExists(tags, "Rotation"))
            {
                var rotationList = (List<float>) GetTag(tags, "Rotation");
                rotation = new Vector3(rotationList[0], rotationList[1], rotationList[2]);
            }

            var backdropType = Convert.ToString(GetTag(tags, "Type"));

            var texturePath = Convert.ToString(GetTag(tags, "TexturePath"));
            var textureRectangle = (Rectangle) GetTag(tags, "Texture");
            var texture = TextureManager.GetTexture(texturePath, textureRectangle);

            var trigger = "";
            var isTriggered = true;

            if (TagExists(tags, "Trigger"))
                trigger = Convert.ToString(GetTag(tags, "Trigger"));

            switch (trigger.ToLower())
            {
                case "offset":
                    if (Core.GameOptions.LoadOffsetMaps == 0)
                        isTriggered = false;
                    break;
                case "notoffset":
                    if (Core.GameOptions.LoadOffsetMaps > 0)
                        isTriggered = false;
                    break;
            }

            if (isTriggered)
                Screen.Level.BackdropRenderer.AddBackdrop(new BackdropRenderer.Backdrop(backdropType, position, rotation, width, height, texture));
        }

        #endregion

        private void LoadBerries()
        {
            string[] data = Core.Player.BerryData.Replace("}" + Constants.vbNewLine, "}").Split(Convert.ToChar("}"));
            for (var i = 0; i < data.Length; i++)
            {
                if (data[i].Contains("{"))
                {
                    data[i] = data[i].Remove(0, data[i].IndexOf("{"));
                    data[i] = data[i].Remove(0, 1);

                    List<string> bData = data[i].Split(Convert.ToChar("|")).ToList();
                    string[] pData = bData[1].Split(Convert.ToChar(","));

                    if (bData.Count == 6)
                    {
                        bData.Add("0");
                    }

                    if (bData[0].ToLower() == Screen.Level.LevelFile.ToLower())
                    {
                        //BaseEntity newEnt = Entity.GetNewEntity("BerryPlant", new Vector3(Convert.ToSingle(PData(0)), Convert.ToSingle(PData(1)), Convert.ToSingle(PData(2))), { null }, { 0, 0 }, true, new Vector3(0), new Vector3(1), BaseModel.BillModel, 0, "", true, new Vector3(1f), -1, MapOrigin, "", Offset);
                        //((BerryPlant)newEnt).Initialize(Convert.ToInt32(BData(2)), Convert.ToInt32(BData(3)), Convert.ToString(BData(4)), BData(5), Convert.ToBoolean(BData(6)));

                        Screen.Level.Entities.Add(newEnt);
                    }
                }
            }
        }

    }
    */
}
