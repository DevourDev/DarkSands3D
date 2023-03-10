using UnityEngine;

namespace Game
{
    internal sealed class GameLayersIniter : MonoBehaviour
    {
        [SerializeField] private LayerMask _interactables;
        [SerializeField] private LayerMask _characters;
        [SerializeField] private LayerMask _visionBlockers;


        private void Awake()
        {
            GameLayers.Interactables = _interactables;
            GameLayers.Characters = _characters;
            GameLayers.VisionBlockers = _visionBlockers;
        }
    }
}
