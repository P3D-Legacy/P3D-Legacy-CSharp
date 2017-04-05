using Microsoft.Xna.Framework.Content;

using P3D.Legacy.Core.Storage.Folders.ContentFolders;
using P3D.Legacy.Shared.Storage.Files;

using PCLExt.FileStorage;

namespace P3D.Legacy.Core.Storage.Files.ContentFiles
{
    public abstract class BaseChildContentFile : BaseFile
    {
        public BaseChildContentFolder Parent { get; }
        public ContentManager ContentManager => Parent.ContentFolder.ContentManager;

        public string ContentLocalPath => Path.Remove(0, Parent.ContentFolder.Path.Length).TrimStart('/', '\\');
        public string ContentLocalPathWithoutExtension => ContentLocalPath.Replace(System.IO.Path.GetExtension(Name), "");
        public string InContentLocalPath => Path.Remove(0, Parent.Path.Length).TrimStart('/', '\\');
        public string InContentLocalPathWithoutExtension => InContentLocalPath.Replace(System.IO.Path.GetExtension(Name), "");
        public string NameWithoutExtension => System.IO.Path.GetFileNameWithoutExtension(Name);

        public bool IsStandard => Parent.ContentFolder.LocalPath == "Content";

        public BaseChildContentFile(IFile file, BaseChildContentFolder contentFolder) : base(file) { Parent = contentFolder; }

        public abstract bool ForceLoad();
    }
}
