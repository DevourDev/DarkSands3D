using UnityEngine;

namespace Game.Characters.Interacting
{

    public abstract class HighlighterBase : MonoBehaviour
    {
        private bool _highlighted;


        public bool Highlighted => _highlighted;


        public void Highlight(Character character)
        {
            if (_highlighted)
                return;

            _highlighted = true;
            HighlightInternal(character);
        }

        public void Downlight(Character character)
        {
            if (!_highlighted)
                return;

            _highlighted = false;
            DownlightInternal(character);
        }


        protected abstract void HighlightInternal(Character character);
        protected abstract void DownlightInternal(Character character);
    }
}
