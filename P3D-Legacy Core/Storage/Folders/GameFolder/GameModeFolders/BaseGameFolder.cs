using P3D.Legacy.Shared.Storage.Folders;

using PCLExt.FileStorage;

namespace P3D.Legacy.Core.Storage.Folders
{
    public abstract class GameModeFolder : BaseGameChildFolder
    {
        protected GameModeFolder() : base(new MainFolder()) { }
        protected GameModeFolder(IFolder folder) : base(folder) { }
    }
    public abstract class BaseGameModeChildFolder : BaseGameChildFolder
    {
        protected BaseGameModeChildFolder() : base(new MainFolder()) { }
        protected BaseGameModeChildFolder(IFolder folder) : base(folder) { }
    }
}