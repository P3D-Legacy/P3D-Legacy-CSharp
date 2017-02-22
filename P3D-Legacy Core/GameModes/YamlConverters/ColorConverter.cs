using System;
using System.Linq;

using Microsoft.Xna.Framework;

using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace P3D.Legacy.Core.GameModes.YamlConverters
{
    public class ColorConverter : IYamlTypeConverter
    {
        private static readonly char[] delimiter = new[] { ' ' };

        public static byte? ToByte(string value)
        {
            byte n;
            if (!Byte.TryParse(value, out n))
                return null;
            return n;
        }


        public bool Accepts(Type type) => type == typeof(Color);

        public object ReadYaml(IParser parser, Type type)
        {
            var value = ((Scalar)parser.Current).Value;
            var values = value.Split(delimiter, StringSplitOptions.RemoveEmptyEntries).Select(val => new { val, number = ToByte(val) }).Where(val => val.number != null).Select(val => val.number.Value).ToArray();
            if (values.Length != 3) values = new byte[] { 0, 0, 0 };

            parser.MoveNext();
            return new Color(values[0], values[1], values[2]);
        }

        public void WriteYaml(IEmitter emitter, object value, Type type)
        {
            var color = (Color) value;
            emitter.Emit(new Scalar(null, null, $"{color.R} {color.G} {color.B}", ScalarStyle.Plain, true, false));
        }
    }
}