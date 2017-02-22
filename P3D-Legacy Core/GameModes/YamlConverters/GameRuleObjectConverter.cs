using System;

using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

using GameRuleObject = P3D.Legacy.Core.GameModes.GameMode.GameRuleList.GameRuleObject;

namespace P3D.Legacy.Core.GameModes.YamlConverters
{
    // Not used
    public class GameRuleObjectConverter : IYamlTypeConverter
    {
        public bool Accepts(Type type) => type.IsSubclassOf(typeof(GameRuleObject));

        public object ReadYaml(IParser parser, Type type)
        {
            var value = ((Scalar) parser.Current).Value;
            parser.MoveNext();

            if (bool.TryParse(value, out bool b))
                return new GameMode.GameRuleList.GameRuleBoolean(b);

            if (int.TryParse(value, out int i))
                return new GameMode.GameRuleList.GameRuleInteger(i);

            return new GameMode.GameRuleList.GameRuleString(value);
        }

        public void WriteYaml(IEmitter emitter, object value, Type type)
        {
            var gameRuleObject = (GameRuleObject) value;
            emitter.Emit(new Scalar(null, null, gameRuleObject.ToString(), ScalarStyle.Plain, true, false));
        }
    }
}
