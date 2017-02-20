using System;
using System.Diagnostics;

using Microsoft.Xna.Framework;

namespace P3D.Legacy.Core.Debug
{
    public class FPSMonitor
    {

        public double Value;

        public TimeSpan Sample;
        private Stopwatch sw;

        private int Frames;
        public FPSMonitor()
        {
            this.Sample = TimeSpan.FromMilliseconds(100);

            Value = 0;
            Frames = 0;
            sw = Stopwatch.StartNew();
        }

        public void Update(GameTime GameTime)
        {
            if (sw.Elapsed > Sample)
            {
                this.Value = Frames / sw.Elapsed.TotalSeconds;

                this.sw.Reset();
                this.sw.Start();
                this.Frames = 0;
            }
        }

        public void DrawnFrame()
        {
            this.Frames += 1;
        }

    }
}
