using Game.Utils.Numerics;

namespace Game.Characters.Stats.DynamicStats
{
    public sealed class DynamicStatData
    {
        private readonly DynamicStatSo _dynamicStat;
        private readonly ClampedDouble _clampedDouble;


        public DynamicStatData(DynamicStatSo dynamicStatSo, double min, double max, double value)
        {
            _dynamicStat = dynamicStatSo;
            _clampedDouble = new(min, max, value);

            _clampedDouble.OnMaxClampChanged += HandleClampedDoubleMaxClampChanged;
            _clampedDouble.OnValueChanged += HandleClampedDoubleValueChanged;
            _clampedDouble.OnMinReached += HandleClampedDoubleMinReached;
            _clampedDouble.OnMaxReached += HandleCalmpedDoubleMaxReached;
        }


        public DynamicStatSo DynamicStat => _dynamicStat;
        public double Min => _clampedDouble.Min;
        public double Max => _clampedDouble.Max;
        public double Value => _clampedDouble.Value;


        /// <summary>
        /// sender, prev max bound, new
        /// </summary>
        public event System.Action<DynamicStatData, double, double> OnMaxBoundChanged;

        /// <summary>
        /// sender, raw delta, safe
        /// </summary>
        public event System.Action<DynamicStatData, double, double> OnValueIncreased;

        /// <summary>
        /// sender, raw delta, safe (non-negatives)
        /// </summary>
        public event System.Action<DynamicStatData, double, double> OnValueDecreased;

        /// <summary>
        /// sender, raw delta, safe
        /// </summary>
        public event System.Action<DynamicStatData, double, double> OnMinReached;

        /// <summary>
        /// sender, raw delta, safe
        /// </summary>
        public event System.Action<DynamicStatData, double, double> OnMaxReached;


        /// <param name="v">POSITIVE value</param>
        public void Add(double v)
        {
            //soft fix
            if (v <= 0)
                return;

            _clampedDouble.ChangeValue(v);
        }

        /// <param name="v">POSITIVE value</param>
        public void Remove(double v)
        {
            //soft fix
            if (v <= 0)
                return;

            _clampedDouble.ChangeValue(-v);
        }


        private void HandleClampedDoubleMaxClampChanged(IReadOnlyClampedDouble sender, double prevMinClamp, double newMinClamp)
        {
            OnMaxBoundChanged?.Invoke(this, prevMinClamp, newMinClamp);
        }

        private void HandleClampedDoubleValueChanged(IReadOnlyClampedDouble sender, double rawDelta, double safeDelta)
        {
            if (rawDelta < 0)
                OnValueDecreased?.Invoke(this, -rawDelta, -safeDelta);
            else
                OnValueIncreased?.Invoke(this, rawDelta, safeDelta);

        }

        private void HandleClampedDoubleMinReached(IReadOnlyClampedDouble sender, double rawDelta, double safeDelta)
        {
            OnMinReached?.Invoke(this, rawDelta, safeDelta);
        }

        private void HandleCalmpedDoubleMaxReached(IReadOnlyClampedDouble sender, double rawDelta, double safeDelta)
        {
            OnMaxReached?.Invoke(this, rawDelta, safeDelta);
        }
    }
}
