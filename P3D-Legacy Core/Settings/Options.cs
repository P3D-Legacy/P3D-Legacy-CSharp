using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;

using P3D.Legacy.Core.Resources;
using P3D.Legacy.Core.Resources.Sound;
using P3D.Legacy.Core.Settings.YamlConverters;
using P3D.Legacy.Core.Storage;

using PCLExt.FileStorage;

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
            Music = 0.0f, Sound = 0.0f, Muted = false, ForceMusic = false,
            RenderDistance = 3, LoadOffsetMaps = 0, MaxOffsetLevel = 0,
            ViewBobbing = true,  LightingEnabled = true, GamePadEnabled = true, StartedOfflineGame = false, PreferMultiSampling = false,
            ContentPackNames = { },
            ShowDebug = 0, ShowDebugConsole = false, ShowGUI = true, DrawViewBox = false,
            GraphicStyle = 1,
            Language = new CultureInfo("en"),
            WindowSize = new Vector2(1200, 680)
        };

        public static async void SaveOptions(Options options)
        {
            var serializer = SerializerBuilder.Build();
            await StorageInfo.OptionsFile.WriteAllTextAsync(serializer.Serialize(options));
        }
        public static async Task<Options> LoadOptions()
        {
            var deserializer = DeserializerBuilder.Build();
            try
            {
                var deserialized = deserializer.Deserialize<Options>(await StorageInfo.OptionsFile.ReadAllTextAsync());
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
                var deserialized = deserializer.Deserialize<Options>(await StorageInfo.OptionsFile.ReadAllTextAsync());
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

        public int ShowDebug { get; set; }
        public bool ShowDebugConsole { get; set; }
        public bool ShowGUI { get; set; }
        public bool DrawViewBox { get; set; }

        public int GraphicStyle { get; set; }

        public CultureInfo Language { get; set; } = new CultureInfo("en");
        
        public Vector2 WindowSize { get; set; }



        public async Task Parse()
        {
            if (WindowSize.X == 0 && WindowSize.Y == 0)
                WindowSize = new Vector2(1200, 680);


            MusicManager.MasterVolume = Music / 100.0f;

            SoundManager.Volume = Sound / 100.0f;

            SoundManager.Mute(Muted);
            MediaPlayer.IsMuted = Muted;

            Localization.Load(Language);

            ContentPackManager.CreateContentPackFolder();
            if (ContentPackNames.Any())
            {
                var contentPackNames = new List<string>(ContentPackNames);
                foreach (var contentPack in ContentPackNames)
                {
                    if (await StorageInfo.ContentPacksFolder.CheckExistsAsync(contentPack) != ExistenceCheckResult.FolderExists)
                        contentPackNames.Remove(contentPack);
                    else
                    {
                        var contentPackFolder = await StorageInfo.ContentPacksFolder.GetFolderAsync(contentPack);
                        var contentPackException = await contentPackFolder.CreateFileAsync("exceptions.dat", CreationCollisionOption.OpenIfExists);
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
