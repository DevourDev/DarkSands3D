using System;

namespace Game.Characters.Stats.DynamicStats
{
    [System.Serializable]
    public readonly struct DynamicStatAmount
    {
        public readonly DynamicStatSo Stat;
        public readonly double Amount;


        public DynamicStatAmount(DynamicStatSo stat, double amount)
        {
            Stat = stat;
            Amount = amount;
        }


        public bool IsNegative => double.IsNegative(Amount);


        public DynamicStatAmount Absolute(out bool wasNegative)
        {
            wasNegative = double.IsNegative(Amount);

            if (wasNegative)
                return new DynamicStatAmount(Stat, -Amount);

            return new DynamicStatAmount(Stat, Amount);
        }

        public DynamicStatAmount SetAmount(double newAmount)
        {
            return new DynamicStatAmount(Stat, newAmount);
        }


        public static DynamicStatAmount operator -(DynamicStatAmount left, double right)
        {
            return new DynamicStatAmount(left.Stat, left.Amount - right);
        }

        public static DynamicStatAmount operator -(double left, DynamicStatAmount right)
        {
            return new DynamicStatAmount(right.Stat, left - right.Amount);
        }

        public static DynamicStatAmount operator -(DynamicStatAmount left)
        {
            return new DynamicStatAmount(left.Stat, -left.Amount);
        }


        //todo: optimize (make unsafe with direct access)
        public static void Absolute(DynamicStatAmount[] amounts)
        {
            var c = amounts.Length;
            var span = amounts.AsSpan();

            for (int i = 0; i < c; i++)
            {
                var amount = span[i];

                if (amount.IsNegative)
                {
                    span[i] = -amount;
                }
            }
        }
    }
}
