﻿using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using Microsoft.Xna.Framework;

using P3D.Legacy.Core.Resources;
using P3D.Legacy.Core.Resources.Managers.Music;
using P3D.Legacy.Core.Resources.Managers.Sound;
using P3D.Legacy.Core.Settings.YamlConverters;
using P3D.Legacy.Core.Storage.Folders;

using PCLExt.FileStorage;
using PCLExt.FileStorage.Extensions;

using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace P3D.Legacy.Core.Settings
{
    public class Options
    {
        public static SerializerBuilder SerializerBuilder { get; } = new SerializerBuilder().EmitDefaults().WithTypeConverter(new CultureInfoConverter()).WithTypeConverter(new Vector2Converter());
        public static DeserializerBuilder DeserializerBuilder { get; } = new DeserializerBuilder().IgnoreUnmatchedProperties().WithTypeConverter(new CultureInfoConverter()).WithTypeConverter(new Vector2Converter());

        public static Options Default => new Options
        {
            Music = 50.0f, Sound = 50.0f, Muted = false, ForceMusic = false,
            RenderDistance = 3, LoadOffsetMaps = 0, MaxOffsetLevel = 0,
            ViewBobbing = true,  LightingEnabled = true, GamePadEnabled = true, StartedOfflineGame = false, PreferMultiSampling = false,
            ContentPackNames = { },
            ShowDebug = false, ShowDebugConsole = false, ShowGUI = true, DrawViewBox = false,
            GraphicStyle = 1,
            Language = new CultureInfo("en"),
            WindowSize = new Vector2(1200, 680)
        };

        public static void SaveOptions(Options options)
        {
            var serializer = SerializerBuilder.Build();
            new SaveFolder().OptionsFile.WriteAllText(serializer.Serialize(options));
        }
        public static Options LoadOptions()
        {
            var deserializer = DeserializerBuilder.Build();
            try
            {
                var deserialized = deserializer.Deserialize<Options>(new SaveFolder().OptionsFile.ReadAllText());
                if (deserialized == null)
                {
                    SaveOptions(Default);
                    deserialized = Default;
                }
                deserialized.Parse();
                return deserialized;
            }
            catch (YamlException)
            {
                SaveOptions(Default);
                var deserialized = deserializer.Deserialize<Options>(new SaveFolder().OptionsFile.ReadAllText());
                deserialized.Parse();
                return deserialized;
            }
        }



        public float Music { get; set; }
        public float Sound { get; set; }
        public bool Muted { get; set; }
        public bool ForceMusic { get; set; }

        public int RenderDistance { get; set; }
        public int LoadOffsetMaps { get; set; }
        public int MaxOffsetLevel { get; set; }

        public bool ViewBobbing { get; set; }
        public bool LightingEnabled { get; set; }
        public bool GamePadEnabled { get; set; }
        public bool StartedOfflineGame { get; set; }
        public bool PreferMultiSampling { get; set; }

        public string[] ContentPackNames { get; set; } = { };

        public bool ShowDebug { get; set; }
        public bool ShowDebugConsole { get; set; }
        public bool ShowGUI { get; set; }
        public bool DrawViewBox { get; set; }

        public int GraphicStyle { get; set; }

        public CultureInfo Language { get; set; } = new CultureInfo("en");
        
        public Vector2 WindowSize { get; set; }



        public void Parse()
        {
            if (WindowSize.X == 0 && WindowSize.Y == 0)
                WindowSize = new Vector2(1200, 680);


            MusicManager.Volume = Music / 100.0f;

            SoundEffectManager.Volume = Sound / 100.0f;

            SoundEffectManager.Mute(Muted);
            MusicManager.Mute(Muted);

            Localization.Load(Language);

            if (ContentPackNames.Any())
            {
                var contentPackNames = new List<string>(ContentPackNames);
                foreach (var contentPack in ContentPackNames)
                {
                    if (new ContentPacksFolder().CheckExists(contentPack) != ExistenceCheckResult.FolderExists)
                        contentPackNames.Remove(contentPack);
                    else
                    {
                        var contentPackFolder = new ContentPacksFolder().GetFolder(contentPack);
                        var contentPackException = contentPackFolder.CreateFile("exceptions.dat", CreationCollisionOption.OpenIfExists);
                        // TODO: Check
                        ContentPackManager.Load(contentPackException.Path);
                    }

                }
                ContentPackNames = contentPackNames.ToArray();
            }

            Core.GraphicsManager.PreferMultiSampling = PreferMultiSampling;
        }
    }
}
