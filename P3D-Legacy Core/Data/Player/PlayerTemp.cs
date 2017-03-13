namespace P3D.Legacy.Core.Interfaces
{
    public class PlayerTemp
    {
        public int DayCareCycle { get; set; } = 256;


        public PlayerTemp() { Reset(); }


        public void Reset() => DayCareCycle = 256;
    }
}