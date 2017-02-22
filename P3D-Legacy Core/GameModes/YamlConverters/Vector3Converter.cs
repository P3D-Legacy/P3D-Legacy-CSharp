using System;
using System.Globalization;
using System.Linq;

using Microsoft.Xna.Framework;

using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace P3D.Legacy.Core.GameModes.YamlConverters
{
    public class Vector3Converter : IYamlTypeConverter
    {
        private static readonly char[] delimiter = new[] { ' ' };

        public static float? ToSingle(string value)
        {
            float n;
            if (!Single.TryParse(value, out n))
                return null;
            return n;
        }


        public bool Accepts(Type type) => type == typeof(Vector3);

        public object ReadYaml(IParser parser, Type type)
        {
            var value = ((Scalar) parser.Current).Value;
            var values = value.Split(delimiter, StringSplitOptions.RemoveEmptyEntries).Select(val => new { val, number = ToSingle(val) }).Where(val => val.number != null).Select(val => val.number.Value).ToArray();
            if (values.Length != 3) values = new float[] { 0.0f, 0.0f, 0.0f };

            parser.MoveNext();
            return new Vector3(values[0], values[1], values[2]);
        }

        public void WriteYaml(IEmitter emitter, object value, Type type)
        {
            var vector3 = (Vector3) value;
            emitter.Emit(new Scalar(null, null, $"{vector3.X.ToString(CultureInfo.InvariantCulture)} {vector3.Y.ToString(CultureInfo.InvariantCulture)} {vector3.Z.ToString(CultureInfo.InvariantCulture)}", ScalarStyle.Plain, true, false));
        }
    }
}