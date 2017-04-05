using System.Linq;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using P3D.Legacy.Core.Extensions;
using P3D.Legacy.Core.Resources;
using P3D.Legacy.Core.Resources.Managers;
using P3D.Legacy.Core.Storage.Files.ContentFiles;
using P3D.Legacy.Core.Storage.Folders.ContentFolders;
using P3D.Legacy.Shared.Storage.Folders;

using PCLExt.FileStorage;

namespace P3D.Legacy.Core.Storage.Folders
{
    public abstract class BaseContentFolder : BaseFolder
    {
        public ContentManager ContentManager { get; }

        public BaseContentFolder(IFolder folder) : base(folder) { ContentManager = new ContentManager(Core.GameInstance.Services, LocalPath); }
        public BaseContentFolder(string path) : base(path) { ContentManager = new ContentManager(Core.GameInstance.Services, LocalPath); }
    }

    public class ContentFolder : BaseContentFolder
    {
        public FontFolder FontFolder => new FontFolder(CreateFolder("Fonts", CreationCollisionOption.OpenIfExists), this);
        public MusicFolder MusicFolder => new MusicFolder(CreateFolder("Music", CreationCollisionOption.OpenIfExists), this);
        public SoundEffectsFolder SoundEffectsFolder => new SoundEffectsFolder(CreateFolder("Sound Effects", CreationCollisionOption.OpenIfExists), this);

        public ContentFolder(IFolder folder) : base(folder) { }
        public ContentFolder(string path) : base(path) { }

        public bool TextureExist(string path)
        {
            path = path.Replace(@"\", @"|");
            string[] arr = path.Split('|');
            string filename = arr.Last();
            string[] folders = arr.Length > 1 ? arr.Take(arr.Length - 1).ToArray() : null;

            IFolder folder = StorageInfo.ContentFolder;
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
                    IFolder contentPackContentFolder = StorageInfo.ContentPacksFolder.GetContentPack(contentPackName);
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
            IFolder folder = StorageInfo.ContentFolder;
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

            if(file != null)
                return new TextureFile(file, new TextureFolder(folder, this));

            if (Core.GameOptions.ContentPackNames.Any())
            {
                foreach (var contentPackName in Core.GameOptions.ContentPackNames)
                {
                    IFolder contentPackContentFolder = StorageInfo.ContentPacksFolder.GetContentPack(contentPackName);
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
}