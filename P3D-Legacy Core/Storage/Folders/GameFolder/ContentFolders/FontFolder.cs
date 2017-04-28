using System.Collections.Generic;
using System.Linq;

using P3D.Legacy.Core.Storage.Files.ContentFiles;

using PCLExt.FileStorage;

namespace P3D.Legacy.Core.Storage.Folders.ContentFolders
{
    public class FontFolder : BaseContentChildFolder
    {
        public FontFolder(IFolder folder, BaseContentFolder parent) : base(folder, parent) { }

        public FontFile GetFontFile(string name) => new FontFile(GetFile(name), this);
        public IList<FontFile> GetFontFiles() => GetFiles("*.xnb").Select(file => new FontFile(file, this)).ToList();
    }
}