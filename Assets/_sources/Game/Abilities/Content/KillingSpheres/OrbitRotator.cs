using UnityEngine;

namespace Game.Abilities
{

    public sealed class OrbitRotator : MonoBehaviour
    {
        [SerializeField] private Transform _origin;
        [SerializeField] private float _radius;
        /// <summary>
        /// degrees per second
        /// </summary>
        [SerializeField] private float _angularSpeed;
        [SerializeField] private bool _lookTowardsDirection;
        [SerializeField] private Vector3 _axis = Vector3.up;

        /// <summary>
        /// 0 - 1 value
        /// </summary>
        private float _loopProgress;
        [SerializeField] private bool _lookDirUpdateNeeded;


        public Transform Origin { get => _origin; set => SetOrigin(value); }
        public float Radius { get => _radius; set => _radius = value; }
        public float DegreesPerSecond { get => _angularSpeed; set => _angularSpeed = value; }
        public bool LookTowardsDirection { get => _lookTowardsDirection; set => _lookTowardsDirection = value; }
        public Vector3 Axis { get => _axis; set => SetAxis(value); }


        /// <summary>
        /// raises every full loop (when item returns to the starting position)
        /// </summary>
        public event System.Action<OrbitRotator> OnLoop;


        private void Start()
        {
            OnLoop += (x) => Debug.Log("LOOPED!");

        }


        private void Update()
        {
            Evaluate(Time.deltaTime);
        }

        public void Evaluate(float deltaTime)
        {
            if (!enabled)
                return;

            float rotation = _angularSpeed * deltaTime;

            _loopProgress += Mathf.InverseLerp(0f, 360f, rotation);

            bool looped = _loopProgress >= 1f;

            if (looped)
                _loopProgress -= (int)_loopProgress;

            if (_lookTowardsDirection)
                RotateAroundAndLookForward(rotation);
            else
                throw new System.NotImplementedException();

            if (looped)
                OnLoop?.Invoke(this);
        }

        private void RotateAroundAndLookForward(float angle)
        {
            if (_lookDirUpdateNeeded)
            {
                _lookDirUpdateNeeded = false;
                Vector3 from = transform.position;
                transform.RotateAround(_origin.transform.position, _axis, angle);
                Vector3 to = transform.position;
                UpdateLookDirection(from, to);
            }
            else
            {
                transform.RotateAround(_origin.transform.position, _axis, angle);
            }
        }

        private void SetLookDirectionDirty()
        {
            _lookDirUpdateNeeded = true;
        }

        private void UpdateLookDirection(Vector3 from, Vector3 to)
        {
            Vector3 direction = to - from;
            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }

        private void SetAxis(Vector3 value)
        {
            _axis = value;
            SetLookDirectionDirty();
        }

        private void SetOrigin(Transform value)
        {
            _origin = value;
            SetLookDirectionDirty();
        }
    }
}
