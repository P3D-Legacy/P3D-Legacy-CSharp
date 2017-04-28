using System;

using Microsoft.Xna.Framework.Graphics;

using P3D.Legacy.Core.Storage.Folders.ContentFolders;

using PCLExt.FileStorage;

namespace P3D.Legacy.Core.Storage.Files.ContentFiles
{
    public class TextureFile : BaseChildContentFile
    {
        public static implicit operator Texture2D(TextureFile textureFile) => textureFile.Texture2D;

        private Texture2D _texture2D;
        private Texture2D Texture2D => _texture2D ?? (_texture2D = ContentManager.Load<Texture2D>(ContentLocalPathWithoutExtension));

        public TextureFile(IFile file, BaseContentChildFolder parent) : base(file, parent) { }

        public override bool ForceLoad()
        {
            try { return Texture2D != null; }
            catch (Exception) { return false; }
        }
    }
}