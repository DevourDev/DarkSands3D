using UnityEngine;

namespace Game
{
    public static class GameLayers
    {
        public static LayerMask Interactables { get; internal set; }

        /// <summary>
        /// vision detection colliders layer (not hitboxes layer)
        /// </summary>
        public static LayerMask Characters { get; internal set; }
        public static LayerMask VisionBlockers { get; internal set; }
    }
}
