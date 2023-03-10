using UnityEngine;

namespace Game.AI
{
#if UNITY_EDITOR
    [System.Serializable]
    public sealed class AllyModeFilterInitor
    {
        [SerializeField] private AllyMode[] _allyFilter;


        public AllyMode GetAllyMode()
        {
            AllyMode am = 0;

            foreach (var af in _allyFilter)
            {
                am |= af;
            }

            return am;
        }
    }
#endif
}
