using System.Collections.Generic;

namespace Game.Characters.Stats.DynamicStats
{
    public sealed class DynamicStatsCollection
    {
        private readonly Dictionary<DynamicStatSo, DynamicStatData> _stats = new();


        public DynamicStatsCollection()
        {

        }


        public event System.Action<DynamicStatsCollection, DynamicStatSo, DynamicStatData> OnStatRegistered;


        public void RegisterStat(DynamicStatSo stat, double min, double max, double initialValue)
        {
            var data = new DynamicStatData(stat, min, max, initialValue);
            _stats.Add(stat, data);
        }

        public bool TryGetStatData(DynamicStatSo stat, out DynamicStatData data)
        {
            return _stats.TryGetValue(stat, out data);
        }


        public void Add(DynamicStatAmount amount)
        {
            if (_stats.TryGetValue(amount.Stat, out var sd))
                sd.Add(amount.Amount);
        }

        public void Add(DynamicStatAmount[] amounts)
        {
            var dict = _stats;

            foreach (var amount in amounts)
            {
                if (!dict.TryGetValue(amount.Stat, out var statData))
                    continue;

                statData.Add(amount.Amount);
            }
        }


        public void Remove(DynamicStatAmount amount)
        {
            if (_stats.TryGetValue(amount.Stat, out var sd))
                sd.Remove(amount.Amount);
        }

        public void Remove(DynamicStatAmount[] amounts)
        {
            var dict = _stats;

            foreach (var amount in amounts)
            {
                if (!dict.TryGetValue(amount.Stat, out var statData))
                    continue;

                statData.Remove(amount.Amount);
            }
        }

        public bool CanRemoveAll(DynamicStatAmount[] amounts)
        {
            var dict = _stats;

            foreach (var amount in amounts)
            {
                if (!dict.TryGetValue(amount.Stat, out var statData))
                    return false;

                if (statData.Value < amount.Amount)
                    return false;
            }

            return true;
        }

        public bool TryRemoveAll(DynamicStatAmount[] amounts)
        {
            if (!CanRemoveAll(amounts))
                return false;

            RemoveAllInternal(amounts);
            return true;
        }

        internal void RemoveAllInternal(DynamicStatAmount[] amounts)
        {
            var dict = _stats;

            foreach (var amount in amounts)
            {
                if (!dict.TryGetValue(amount.Stat, out var statData))
                    throw new System.Collections.Generic.KeyNotFoundException(amount.Stat.ToString());

                if (statData.Value < amount.Amount)
                    throw new System.Exception($"{amount.Stat}: {statData.Value} < {amount.Amount}");

                statData.Remove(amount.Amount);
            }
        }


    }
}
