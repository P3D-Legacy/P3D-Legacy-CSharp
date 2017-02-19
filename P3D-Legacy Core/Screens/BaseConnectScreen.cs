namespace P3D.Legacy.Core.Screens
{
    public abstract class BaseConnectScreen : Screen
    {
        public enum Modes
        {
            Connect,
            Disconnect
        }

        public static bool Connected = false;


        protected static BaseConnectScreen TempConnectScreen;

        protected static bool NeedToSwitch = false;
        public static void Setup(BaseConnectScreen connectScreen)
        {
            TempConnectScreen = connectScreen;
            NeedToSwitch = true;
        }

        public static void UpdateConnectSet()
        {
            if (NeedToSwitch == true)
            {
                NeedToSwitch = false;
                Core.SetScreen(TempConnectScreen);
            }
        }
    }
}
