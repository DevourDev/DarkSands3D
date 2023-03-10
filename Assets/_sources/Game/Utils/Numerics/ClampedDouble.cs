using System;
using System.Runtime.CompilerServices;

namespace Game.Utils.Numerics
{
    public sealed class ClampedDouble : IReadOnlyClampedDouble
    {
        private double _min;
        private double _max;
        private double _value;


        public ClampedDouble(double minClamp, double maxClamp, double value)
        {
            _min = minClamp;
            _max = maxClamp;
            _value = value;
        }


        public double Min => _min;
        public double Max => _max;
        public double Value => _value;


        public event Action<IReadOnlyClampedDouble, double, double> OnValueChanged;
        public event Action<IReadOnlyClampedDouble, double, double> OnMinClampChanged;
        public event Action<IReadOnlyClampedDouble, double, double> OnMaxClampChanged;
        public event Action<IReadOnlyClampedDouble, double, double> OnMinReached;
        public event Action<IReadOnlyClampedDouble, double, double> OnMaxReached;


        public void Set(double min, double max, double value)
        {
#if UNITY_EDITOR
            EnsureValueClamped(value, min, max);
#endif
            double prevMin = _min;
            double prevMax = _max;
            double prevValue = _value;

            _min = min;
            _max = max;
            Clamp(in value, in min, in max, out _value, out var reachedMin, out var reachedMax);

            OnMinClampChanged?.Invoke(this, prevMin, min);
            OnMaxClampChanged?.Invoke(this, prevMax, max);
            SetValueHelper(_value, value - prevValue, _value - prevValue, reachedMin, reachedMax);
        }

        public void SetValue(double newValue)
        {
            ChangeValue(newValue - _value);
        }

        public void ChangeValue(double delta)
        {
#if UNITY_EDITOR
            ThrowIfNotClamped();
#endif

            if (delta == 0d)
                return;

            double desired = _value + delta;
            Clamp(in desired, in _min, in _max, out var safeDesired, out var reachedMin, out var reachedMax);
            double safeDelta = safeDesired - _value;
            SetValueHelper(safeDesired, delta, safeDelta, reachedMin, reachedMax);
        }


        private void SetValueHelper(double newValue, double rawDelta, double safeDelta, bool reachedMin, bool reachedMax)
        {
            _value = newValue;
            OnValueChanged?.Invoke(this, rawDelta, safeDelta);

            if (reachedMin)
                OnMinClampChanged?.Invoke(this, rawDelta, safeDelta);

            if (reachedMax)
                OnMaxClampChanged?.Invoke(this, rawDelta, safeDelta);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void Clamp(in double value, in double min, in double max,
                                  out double clampedValue,
                                  out bool reachedMin, out bool reachedMax)
        {
#if UNITY_EDITOR
            if (max < min)
                throw new ArgumentException($"max should not be less than min: " +
                    $"(max: {max}, min: {min})");
#endif
            reachedMin = reachedMax = false;
            clampedValue = value;

            if (value >= max)
            {
                clampedValue = max;
                reachedMax = true;
            }

            if (value <= min)
            {
                clampedValue = min;
                reachedMin = true;
            }
        }



#if UNITY_EDITOR
        private void ThrowIfNotClamped()
        {
            EnsureValueClamped(_value, _min, _max);
        }

        private static void EnsureValueClamped(double value, double min, double max)
        {
            if (value < min)
                throw new Exception($"value is not clamped: {value} < {min})");

            if (value > max)
                throw new Exception($"value is not clamped: {value} > {max})");
        }
#endif
    }
}