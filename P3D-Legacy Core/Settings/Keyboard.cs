using System.Threading.Tasks;

using Microsoft.Xna.Framework.Input;

using P3D.Legacy.Core.Storage;

using PCLExt.FileStorage;

using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace P3D.Legacy.Core.Settings
{
    public class Keyboard
    {
        public static SerializerBuilder SerializerBuilder { get; } = new SerializerBuilder().EmitDefaults();
        public static DeserializerBuilder DeserializerBuilder { get; } = new DeserializerBuilder().IgnoreUnmatchedProperties();

        public static Keyboard Default => new Keyboard
        {
            ForwardMove = Keys.W, LeftMove = Keys.A, BackwardMove = Keys.S, RightMove = Keys.D,
            Up = Keys.Up, Down = Keys.Down, Left = Keys.Left, Right = Keys.Right,
            Enter1 = Keys.Enter, Enter2 = Keys.Space,
            Back1 = Keys.E, Back2 = Keys.E,
            Escape = Keys.Escape,
            Inventory = Keys.E, Chat = Keys.T, Special = Keys.Q, MuteMusic = Keys.M, CameraLock = Keys.C,
            GUIControl = Keys.F1, ScreenShot = Keys.F2, DebugControl = Keys.F3, LightKey = Keys.F4, PerspectiveSwitch = Keys.F5 , FullScreen = Keys.F11,
            OnlineStatus = Keys.Tab,
        };

        public static async void SaveKeyboard(Keyboard options)
        {
            var serializer = SerializerBuilder.Build();
            await StorageInfo.KeyboardFile.WriteAllTextAsync(serializer.Serialize(options));
        }
        public static async Task<Keyboard> LoadKeyboard()
        {
            var deserializer = DeserializerBuilder.Build();
            try
            {
                var deserialized = deserializer.Deserialize<Keyboard>(await StorageInfo.KeyboardFile.ReadAllTextAsync());
                if (deserialized == null)
                {
                    SaveKeyboard(Default);
                    deserialized = Default;
                }
                return deserialized;
            }
            catch (YamlException)
            {
                SaveKeyboard(Default);
                var deserialized = deserializer.Deserialize<Keyboard>(await StorageInfo.KeyboardFile.ReadAllTextAsync());
                return deserialized;
            }
        }



        public Keys ForwardMove { get; set; }
        public Keys LeftMove { get; set; }
        public Keys BackwardMove { get; set; }
        public Keys RightMove { get; set; }

        public Keys Up { get; set; }
        public Keys Down { get; set; }
        public Keys Left { get; set; }
        public Keys Right { get; set; }

        public Keys Enter1 { get; set; }
        public Keys Enter2 { get; set; }

        public Keys Back1 { get; set; }
        public Keys Back2 { get; set; }

        public Keys Escape { get; set; }

        public Keys Inventory { get; set; }
        public Keys Chat { get; set; }
        public Keys Special { get; set; }
        public Keys MuteMusic { get; set; }

        public Keys CameraLock { get; set; }
        public Keys GUIControl { get; set; }
        public Keys ScreenShot { get; set; }
        public Keys DebugControl { get; set; }
        public Keys LightKey { get; set; }
        public Keys PerspectiveSwitch { get; set; }
        public Keys FullScreen { get; set; }
        
        public Keys OnlineStatus { get; set; }
    }
}
