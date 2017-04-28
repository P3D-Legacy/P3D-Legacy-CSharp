using System;

using Microsoft.Xna.Framework.Graphics;

using P3D.Legacy.Core.Data;
using P3D.Legacy.Core.Storage.Folders.ContentFolders;

using PCLExt.FileStorage;

namespace P3D.Legacy.Core.Storage.Files.ContentFiles
{
    public class FontFile : BaseChildContentFile
    {
        public static implicit operator SpriteFont(FontFile fontFile) => fontFile.SpriteFont;
        public static implicit operator FontContainer(FontFile fontFile) => new FontContainer(fontFile.NameWithoutExtension, fontFile.SpriteFont);

        private SpriteFont _spriteFont;
        private SpriteFont SpriteFont => _spriteFont ?? (_spriteFont = ContentManager.Load<SpriteFont>(ContentLocalPathWithoutExtension));

        public FontFile(IFile file, BaseContentChildFolder parent) : base(file, parent) { }

        public override bool ForceLoad()
        {
            try { return SpriteFont != null; }
            catch (Exception) { return false; }
        }
    }
}