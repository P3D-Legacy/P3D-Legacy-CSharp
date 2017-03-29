using System;
using System.IO;

using P3D.Legacy.Core.Storage;
using P3D.Legacy.Core.Storage.Folders;

using PCLExt.FileStorage;

using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace P3D.Legacy.Core.GameModes.YamlConverters
{
    public class ScriptsFolderLocalConverter : IYamlTypeConverter
    {
        public bool Accepts(Type type) => type == typeof(ScriptsFolder);

        private static string LocalPath(IFolder folder) => folder.Path.Remove(0, StorageInfo.MainFolder.Path.Length);
        public object ReadYaml(IParser parser, Type type)
        {
            var value = ((Scalar) parser.Current).Value;
            parser.MoveNext();
            return new ScriptsFolder(Path.Combine(StorageInfo.MainFolder.Path, value));
        }

        public void WriteYaml(IEmitter emitter, object value, Type type)
        {
            var folder = (ScriptsFolder) value;
            emitter.Emit(new Scalar(null, null, LocalPath(folder), ScalarStyle.Plain, true, false));
        }
    }
}