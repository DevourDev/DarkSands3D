using System.Collections.Generic;
using Game.Characters.Stats.DynamicStats;
using UnityEngine;

namespace Game.Abilities
{
    public sealed class CompositeAbility : AbilityBase
    {
        [SerializeField] private DynamicStatAmount[] _costs;
#if UNITY_EDITOR
        [SerializeField] private bool _fixCosts;
#endif


        public DynamicStatAmount[] Costs => _costs;


#if UNITY_EDITOR
        private void OnValidate()
        {
            if(_fixCosts)
            {
                _fixCosts = false;

                var dict = new Dictionary<DynamicStatSo, double>();

                foreach (var cost in _costs)
                {
                    if(!dict.TryAdd(cost.Stat, cost.Amount))
                    {
                        dict[cost.Stat] += cost.Amount;
                    }
                }

                _costs = new DynamicStatAmount[dict.Count];
                int i = 0;

                foreach (var kvp in dict)
                {
                    _costs[i++] = new DynamicStatAmount(kvp.Key, kvp.Value);
                }

                UnityEditor.EditorUtility.SetDirty(this);
            }
        }
#endif

        public bool CheckCosts(DynamicStatsCollection dynamicStatsCollection)
        {
            return dynamicStatsCollection.CanRemoveAll(_costs);
        }


        public override void Use(AbilityContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}
