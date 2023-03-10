using UnityEngine;

namespace Game.Utils
{

    /// <summary>
    /// OnTick Event raises from UPDATE LOOP
    /// </summary>
    public sealed class TickSystem : MonoBehaviour
    {
        [SerializeField, Min(0.00001f)] private float _tickRate = 5f;

        private float _delta;
        private float _cd;


        public float TickRate => _tickRate;

        /// <summary>
        /// delta time
        /// </summary>
        public event System.Action<float> OnTick;


        private void Start()
        {
            SetTickRate(_tickRate);
        }

        private void Update()
        {
            if ((_cd -= Time.deltaTime) > 0)
                return;

            ResetCD();
            OnTick?.Invoke(_delta);
        }

        //internal to add helper to expose it to client code
        internal void SetTickRate(float tickRate)
        {
            _tickRate = tickRate;
            _delta = 1f / tickRate;
            ResetCD();
        }

        private void ResetCD()
        {
            _cd = _delta;
        }


        /// <summary>
        /// call only from main thread
        /// </summary>
        /// <param name="tickRate"></param>
        /// <returns></returns>
        public static TickSystem Create(float tickRate)
        {
            var go = new GameObject();
            var ts = go.AddComponent<TickSystem>();
            ts._tickRate = tickRate;
            return ts;
        }
    }
}
