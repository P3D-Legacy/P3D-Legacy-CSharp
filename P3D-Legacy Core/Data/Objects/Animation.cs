using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace P3D.Legacy.Core.Objects
{
    /// <summary>
    /// A class which can display an animation.
    /// </summary>
    public class Animation : BasicObject
    {
        public enum PlayMode
        {
            Playing,
            Stopped,
            Paused
        }

        public Rectangle TextureRectangle => new Rectangle(CurrentColumn * Width, CurrentRow * Height, Width, Height);
        public float TotalElapsed { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        public float AnimationSpeed { get; set; }
        public int CurrentRow { get; set; }
        public int CurrentColumn { get; set; }
        public int StartRow { get; set; }
        public int StartColumn { get; set; }
        public PlayMode Running { get; private set; } = PlayMode.Playing;
        
        public Animation(Texture2D texture, int rows, int columns, int width, int height, int animationSpeed, int startRow, int startColumn) : base(texture, width, height, new Vector2(0, 0))
        {

            Rows = rows;
            Columns = columns;
            AnimationSpeed = (1.0f / Convert.ToSingle(animationSpeed));

            TotalElapsed = 0;
            CurrentRow = startRow;
            CurrentColumn = startColumn;
            StartRow = startRow;
            StartColumn = startColumn;
        }

        public void Update(float elapsed)
        {
            if (Running == PlayMode.Playing)
            {
                TotalElapsed += elapsed;
                if (TotalElapsed > AnimationSpeed)
                {
                    TotalElapsed -= AnimationSpeed;

                    CurrentColumn += 1;
                    if (CurrentColumn >= Columns)
                    {
                        CurrentRow += 1;
                        CurrentColumn = StartColumn;

                        if (CurrentRow >= Rows)
                        {
                            CurrentRow = StartRow;
                        }
                    }
                }
            }
        }


        /// <summary>
        /// Starts the animation.
        /// </summary>
        public void Start()
        {
            Running = PlayMode.Playing;
        }

        /// <summary>
        /// Stops the animation and returns to start.
        /// </summary>
        public void Stop()
        {
            Running = PlayMode.Stopped;
            CurrentRow = StartRow;
            CurrentColumn = StartColumn;
        }

        /// <summary>
        /// Returns to start and starts the animation afterwards.
        /// </summary>
        public void Restart()
        {
            Running = PlayMode.Playing;
            CurrentRow = StartRow;
            CurrentColumn = StartColumn;
        }

        /// <summary>
        /// Pauses the animation.
        /// </summary>
        public void Pause()
        {
            Running = PlayMode.Paused;
        }
    }
}
