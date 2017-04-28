using System.Linq;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using P3D.Legacy.Core.Resources.Managers;
using P3D.Legacy.Core.Storage.Files.ContentFiles;
using P3D.Legacy.Core.Storage.Folders.ContentFolders;

using PCLExt.FileStorage;

namespace P3D.Legacy.Core.Storage.Folders
{
    public abstract class BaseContentFolder : BaseGameChildFolder
    {
        public FontFolder FontFolder => new FontFolder(CreateFolder("Fonts", CreationCollisionOption.OpenIfExists), this);
        public MusicFolder MusicFolder => new MusicFolder(CreateFolder("Music", CreationCollisionOption.OpenIfExists), this);
        public SoundEffectsFolder SoundEffectsFolder => new SoundEffectsFolder(CreateFolder("Sound Effects", CreationCollisionOption.OpenIfExists), this);

        public ContentManager ContentManager { get; }

        protected BaseContentFolder(IFolder folder) : base(folder) { ContentManager = new ContentManager(Core.GameInstance.Services, LocalPath); }

        public bool TextureExist(string path)
        {
            path = path.Replace(@"\", @"|");
            string[] arr = path.Split('|');
            string filename = arr.Last();
            string[] folders = arr.Length > 1 ? arr.Take(arr.Length - 1).ToArray() : null;

            IFolder folder = new ContentFolder();
            if (folders != null)
                foreach (var folderName in folders)
                    folder = folder.GetFolder(folderName);

            var mainContentExist = folder.CheckExists($"{filename}.xnb") == ExistenceCheckResult.FileExists || folder.CheckExists($"{filename}.png") == ExistenceCheckResult.FileExists;

            if (mainContentExist)
                return true;

            if (Core.GameOptions.ContentPackNames.Any())
            {
                foreach (var contentPackName in Core.GameOptions.ContentPackNames)
                {
                    IFolder contentPackContentFolder = new ContentPacksFolder().GetContentPack(contentPackName);
                    if (folders != null)
                        foreach (var folderName in folders)
                            contentPackContentFolder = contentPackContentFolder.GetFolder(folderName);

                    if (contentPackContentFolder.CheckExists($"{filename}.xnb") == ExistenceCheckResult.FileExists || contentPackContentFolder.CheckExists($"{filename}.png") == ExistenceCheckResult.FileExists)
                        return true;
                }
            }

            return false;
        }
        public Texture2D GetTexture(string path)
        {
            path = path.Replace(@"\", @"|");
            string[] arr = path.Split('|');
            string filename = arr.Last();
            string[] folders = arr.Length > 1 ? arr.Take(arr.Length - 1).ToArray() : null;

            IFile file = null;
            IFolder folder = new ContentFolder();
            if (folders != null)
                foreach (var folderName in folders)
                    folder = folder.GetFolder(folderName);

            if (folder.CheckExists($"{filename}.xnb") != ExistenceCheckResult.FileExists)
            {
                if (folder.CheckExists($"{filename}.png") == ExistenceCheckResult.FileExists)
                    file = folder.GetFile($"{filename}.png");
            }
            else
                file = folder.GetFile($"{filename}.xnb");

            if (file != null)
                return new TextureFile(file, new TextureFolder(folder, this));

            if (Core.GameOptions.ContentPackNames.Any())
            {
                foreach (var contentPackName in Core.GameOptions.ContentPackNames)
                {
                    IFolder contentPackContentFolder = new ContentPacksFolder().GetContentPack(contentPackName);
                    if (folders != null)
                        foreach (var folderName in folders)
                            contentPackContentFolder = contentPackContentFolder.GetFolder(folderName);

                    if (contentPackContentFolder.CheckExists($"{filename}.xnb") != ExistenceCheckResult.FileExists)
                    {
                        if (contentPackContentFolder.CheckExists($"{filename}.png") == ExistenceCheckResult.FileExists)
                            file = contentPackContentFolder.GetFile($"{filename}.png");
                    }
                    else
                        file = contentPackContentFolder.GetFile($"{filename}.xnb");

                    if (file != null)
                        return new TextureFile(file, new TextureFolder(folder, this));
                }
            }

            return TextureManager.DefaultTexture;
        }
    }

    public class ContentFolder : BaseContentFolder
    {
        // CoreResources
        public ContentFolder() : base(new GameFolder().CreateFolder("Content", CreationCollisionOption.OpenIfExists)) { }
        // GameMode Content && ContentPack Content
        public ContentFolder(IFolder folder) : base(folder) { }


        public ContentFolder(string localPath) : base(FromLocalPath(localPath)) { }
    }
}