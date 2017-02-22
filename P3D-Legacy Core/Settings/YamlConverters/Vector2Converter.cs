using System;
using System.Globalization;
using System.Linq;

using Microsoft.Xna.Framework;

using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace P3D.Legacy.Core.Settings.YamlConverters
{
    public class Vector2Converter : IYamlTypeConverter
    {
        private static readonly char[] delimiter = new[] { ' ' };
        public static float? ToSingle(string value)
        {
            float n;
            if (!Single.TryParse(value, out n))
                return null;
            return n;
        }


        public bool Accepts(Type type) => type == typeof(Vector2);

        public object ReadYaml(IParser parser, Type type)
        {
            var value = ((Scalar) parser.Current).Value;
            var values = value.Split(delimiter, StringSplitOptions.RemoveEmptyEntries).Select(val => new { val, number = ToSingle(val) }).Where(val => val.number != null).Select(val => val.number.Value).ToArray();
            if (values.Length != 2) values = new float[] { 0.0f, 0.0f };

            parser.MoveNext();
            return new Vector2(values[0], values[1]);
        }

        public void WriteYaml(IEmitter emitter, object value, Type type)
        {
            var vector2 = (Vector2) value;
            emitter.Emit(new Scalar(null, null, $"{vector2.X.ToString(CultureInfo.InvariantCulture)} {vector2.Y.ToString(CultureInfo.InvariantCulture)}", ScalarStyle.Plain, true, false));
        }
    }
}
