using System.Collections.Generic;

namespace Game.Abilities
{
    public sealed class AbilityDatasCollection
    {
        private readonly Dictionary<AbilityBase, AbilityDataBase> _datas = new();


        public bool TryGetAbilityData<TData>(AbilityBase ability, out TData abilityData)
            where TData : AbilityDataBase
        {
            if (!_datas.TryGetValue(ability, out var rawData))
            {
                abilityData = default!;
                return false;
            }

            abilityData = (TData)rawData;
            return true;
        }

        public TData GetOrCreateAbilityData<TData>(AbilityBase ability)
            where TData : AbilityDataBase, new()
        {
            if (!_datas.TryGetValue(ability, out var rawData))
            {
                rawData = new TData();
                _datas.Add(ability, rawData);
            }

            return (TData)rawData;
        }
    }
}
