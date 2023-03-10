namespace Game.Utils.Numerics
{
    public interface IReadOnlyClampedDouble
    {
        public double Min { get; }
        public double Max { get; }
        public double Value { get; }


        /// <summary>
        /// sender, prev, new
        /// </summary>
        event System.Action<IReadOnlyClampedDouble, double, double> OnMinClampChanged;

        /// <summary>
        /// sender, prev, new
        /// </summary>
        event System.Action<IReadOnlyClampedDouble, double, double> OnMaxClampChanged;

        /// <summary>
        /// sender, raw delta, safe delta
        /// </summary>
        event System.Action<IReadOnlyClampedDouble, double, double> OnValueChanged;

        /// <summary>
        /// sender, raw delta, safe delta
        /// </summary>
        event System.Action<IReadOnlyClampedDouble, double, double> OnMinReached;

        /// <summary>
        /// sender, raw delta, safe delta
        /// </summary>
        event System.Action<IReadOnlyClampedDouble, double, double> OnMaxReached;
    }
}
