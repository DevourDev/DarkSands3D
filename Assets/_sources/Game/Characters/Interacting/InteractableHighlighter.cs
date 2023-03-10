using TMPro;
using UnityEngine;

namespace Game.Characters.Interacting
{
    public sealed class InteractableHighlighter : HighlighterBase
    {
        [SerializeField] private TMP_Text _text;


        public void Init(string text, Color color)
        {
            _text.text = text;
            _text.color = color;
        }


        protected override void HighlightInternal(Character character)
        {
            _text.gameObject.SetActive(true);
        }

        protected override void DownlightInternal(Character character)
        {
            _text.gameObject.SetActive(false);
        }
    }
}
