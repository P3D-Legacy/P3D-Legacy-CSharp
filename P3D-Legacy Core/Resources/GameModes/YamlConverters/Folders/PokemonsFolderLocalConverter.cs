using System;

using P3D.Legacy.Core.Storage.Folders;

using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace P3D.Legacy.Core.GameModes.YamlConverters
{
    public class PokemonsFolderLocalConverter : IYamlTypeConverter
    {
        public bool Accepts(Type type) => type == typeof(PokemonDataFolder);

        public object ReadYaml(IParser parser, Type type)
        {
            var localPath = ((Scalar) parser.Current).Value;
            parser.MoveNext();
            return new PokemonDataFolder(localPath);
        }

        public void WriteYaml(IEmitter emitter, object value, Type type)
        {
            var folder = (PokemonDataFolder) value;
            emitter.Emit(new Scalar(null, null, folder.LocalPath, ScalarStyle.Plain, true, false));
        }
    }
}