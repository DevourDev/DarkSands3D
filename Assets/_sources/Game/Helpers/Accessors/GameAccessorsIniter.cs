using Game.Characters.Interacting;
using UnityEngine;

namespace Game.Helpers.Accessors
{

    internal sealed class GameAccessorsIniter : MonoBehaviour
    {
        [SerializeField] private HighlighterBase _defaultInteractableHighlighterPrefab;


        private void Awake()
        {
            GameAccessors.InteractableHighlighter = _defaultInteractableHighlighterPrefab;
        }
    }
}
