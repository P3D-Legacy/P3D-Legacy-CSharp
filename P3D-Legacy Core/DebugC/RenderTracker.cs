namespace P3D.Legacy.Core.DebugC
{
    public static class RenderTracker
    {
        /// <summary>
        /// The amount of vertices rendered in the last frame.
        /// </summary>
        public static int DrawnVertices { get; set; } = 0;

        /// <summary>
        /// The maximum amount of vertices that are present in the current scene.
        /// </summary>
        public static int MaxVertices { get; set; } = 0;

        /// <summary>
        /// The distance of the vertex to the camera, that is the furthest away from the camera.
        /// </summary>
        public static int MaxDistance { get; set; } = 0;
    }
}
