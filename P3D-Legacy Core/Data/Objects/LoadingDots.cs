namespace P3D.Legacy.Core.Objects
{
    public class LoadingDots
    {
        private static float PointsDelay = 0f;

        public static void Update()
        {
            PointsDelay += 0.1f;
            if (PointsDelay >= 4f)
            {
                PointsDelay = 0f;
            }
        }

        public static string Dots
        {
            get
            {
                string p = "";
                if (PointsDelay >= 1f)
                {
                    p += ".";
                }
                if (PointsDelay >= 2f)
                {
                    p += ".";
                }
                if (PointsDelay >= 3f)
                {
                    p += ".";
                }
                return p;
            }
        }

    }
}
