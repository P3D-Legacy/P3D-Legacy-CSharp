namespace P3D.Legacy.Core.World
{
    public interface IBackdropRenderer
    {
        void Initialize();
        void Update();
        void Draw();
        void Clear();
        void AddBackdrop(IBackdrop backdrop);
    }
}
