using System;

using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

using GameRuleList = P3D.Legacy.Core.GameModes.GameMode.GameRuleList;
using GameRuleObject = P3D.Legacy.Core.GameModes.GameMode.GameRuleList.GameRuleObject;
using GameRuleString = P3D.Legacy.Core.GameModes.GameMode.GameRuleList.GameRuleString;
using GameRuleDouble = P3D.Legacy.Core.GameModes.GameMode.GameRuleList.GameRuleDouble;
using GameRuleInteger = P3D.Legacy.Core.GameModes.GameMode.GameRuleList.GameRuleInteger;
using GameRuleBoolean = P3D.Legacy.Core.GameModes.GameMode.GameRuleList.GameRuleBoolean;

namespace P3D.Legacy.Core.GameModes.YamlConverters
{
    // http://aaubry.net/pages/frequently-asked-questions.html
    public class GameRuleListConverter : IYamlTypeConverter
    {
        public bool Accepts(Type type) => type == typeof(GameRuleList);

        public object ReadYaml(IParser parser, Type type)
        {
            var list = new GameRuleList();

            parser.Expect<MappingStart>();
            while (parser.Allow<MappingEnd>() == null)
            {
                var ruleName = parser.Expect<Scalar>().Value;
                var ruleValue = parser.Expect<Scalar>().Value;
                list.Add(ruleName, ParseGameRuleObject(ruleValue));
            }

            return list;
        }
        private static GameRuleObject ParseGameRuleObject(string value)
        {
            if (bool.TryParse(value, out bool b))
                return new GameRuleBoolean(b);

            if (int.TryParse(value, out int i))
                return new GameRuleInteger(i);

            if (double.TryParse(value, out double d))
                return new GameRuleDouble(d);

            return new GameRuleString(value);
        }

        public void WriteYaml(IEmitter emitter, object value, Type type)
        {
            var list = (GameRuleList) value;

            emitter.Emit(new MappingStart(null, null, true, MappingStyle.Block));
            foreach (var column in list)
            {
                emitter.Emit(new Scalar(column.Key));
                emitter.Emit(new Scalar(column.Value.ToString()));
            }
            emitter.Emit(new MappingEnd());
        }
    }
}