using Microsoft.Xna.Framework;

namespace P3D.Legacy.Core.Pokemon
{
    /// <summary>
    /// A structure to store warp data in.
    /// </summary>
    public class WarpDataStruct
    {
        /// <summary>
        /// The destination map file.
        /// </summary>
        public string WarpDestination;

        /// <summary>
        /// The position to warp the player to.
        /// </summary>
        public Vector3 WarpPosition;

        /// <summary>
        /// The check to see if the player should get warped next tick.
        /// </summary>
        public bool DoWarpInNextTick;

        /// <summary>
        /// Amount of 90° rotations counter clockwise.
        /// </summary>
        public int WarpRotations;

        /// <summary>
        /// The correct camera yaw to set the camera to after the warping.
        /// </summary>
        public float CorrectCameraYaw;

        /// <summary>
        /// If the warp action got triggered by a warp block.
        /// </summary>
        public bool IsWarpBlock;

        public void SetDoWarpInNextTick(bool b) => DoWarpInNextTick = b;
    }
}