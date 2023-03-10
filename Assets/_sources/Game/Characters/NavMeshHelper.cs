using UnityEngine;
using UnityEngine.AI;

namespace Game.Characters
{
    [RequireComponent(typeof(NavMeshAgent))]
    public sealed class NavMeshHelper : MonoBehaviour
    {
        private const float _maxError = 1f;
        private NavMeshAgent _agent;

        private Transform _target;
        private Vector3 _point;


        public float MaxSpeed { get => _agent.speed; set => _agent.speed = value; }
        public float AngularSpeed { get => _agent.angularSpeed; set => _agent.angularSpeed = value; }
        public float Acceleration { get => _agent.acceleration; set => _agent.acceleration = value; }
        public float StoppingDistance { get => _agent.stoppingDistance; set => _agent.stoppingDistance = value; }

        public Transform Target { get => _target; set => _target = value; }

        public Vector3 Destination => _point;


        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        public void ChaseTarget(Transform target)
        {
            Resume();

            if (target == _target)
                return;

            _target = target;
            _point = target.position;
            _agent.SetDestination(_point);
        }

        public void GoToPoint(Vector3 point)
        {
            _target = null;
            _point = point;
            Resume();

            if (!_agent.SetDestination(point))
            {
                Debug.LogError($"failed to change destination to point {point}");
                return;
            }

            if (_agent.isStopped)
            {
                Debug.LogError($"called {nameof(GoToPoint)} {point} but agent isStopped");
            }
        }


        public void Stop()
        {
            _agent.isStopped = true;
            enabled = false;
        }

        public void Resume()
        {
            _agent.isStopped = false;
            enabled = true;
        }

        private void Update()
        {
            if (_target == null)
                return;

            Vector3 p = _target.position;

            if (!_agent.hasPath || (p - _point).sqrMagnitude > _maxError)
            {
                _point = p;
                _agent.SetDestination(p);
            }
        }

    }
}
