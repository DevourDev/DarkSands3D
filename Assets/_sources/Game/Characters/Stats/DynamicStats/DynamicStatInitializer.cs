using UnityEngine;

namespace Game.Characters.Stats.DynamicStats
{
    [RequireComponent(typeof(Character))]
    public sealed class DynamicStatInitializer : MonoBehaviour
    {
        [SerializeField] private DynamicStatSo _dynamicStat;
        [SerializeField, Min(0f)] private double _min;
        [SerializeField, Min(1f)] private double _max;
        [SerializeField, Min(0f)] private double _initialValue;


        private void Awake()
        {
            var character = GetComponent<Character>();
            var statsCollection = character.DynamicStatsCollection;
            statsCollection.RegisterStat(_dynamicStat, _min, _max, _initialValue);
            Destroy(this);
        }
    }
}
