using System;
using System.IO;
using P3D.Legacy.Core.Storage;
using PCLExt.FileStorage;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace P3D.Legacy.Core.GameModes.YamlConverters
{
    public class PokeFolderLocalConverter : IYamlTypeConverter
    {
        public bool Accepts(Type type) => type == typeof(PokeFolder);

        private static string LocalPath(IFolder folder) => folder.Path.Remove(0, StorageInfo.MainFolder.Path.Length);
        public object ReadYaml(IParser parser, Type type)
        {
            var value = ((Scalar) parser.Current).Value;
            parser.MoveNext();
            return new PokeFolder(Path.Combine(StorageInfo.MainFolder.Path, value));
        }

        public void WriteYaml(IEmitter emitter, object value, Type type)
        {
            var folder = (PokeFolder) value;
            emitter.Emit(new Scalar(null, null, LocalPath(folder), ScalarStyle.Plain, true, false));
        }
    }
}