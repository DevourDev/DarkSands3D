using System.Security.Permissions;
using UnityEngine;

namespace Game.Characters.Stats.DynamicStats
{
    [CreateAssetMenu(menuName = "Game/Characters/Stats/Dynamic Stat")]
    public sealed class DynamicStatSo : ScriptableObject //todo: replace with SoDatabaseElement, rename
    {
        [SerializeField] private string _statName; //todo: replace with MultiCulturalText
        [SerializeField] private Color _color;
        [SerializeField] private Sprite _icon;


        public string StatName => _statName;
        public Color Color => _color;
        public Sprite Icon => _icon;
    }
}
