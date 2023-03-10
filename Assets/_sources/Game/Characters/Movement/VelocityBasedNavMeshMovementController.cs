using UnityEngine;
using UnityEngine.AI;

namespace Game.Characters.Movement
{
    [RequireComponent(typeof(NavMeshAgent))]
    public sealed class VelocityBasedNavMeshMovementController : MonoBehaviour
    {
        private NavMeshAgent _agent;


        public Vector3 AgentVelocity => _agent.velocity;

        public Vector3 Velocity
        {
            get => _agent.velocity;
            set => SetAgentVelocity(value);
        }


        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }


        private void SetAgentVelocity(Vector3 value)
        {
            _agent.ResetPath();
            _agent.isStopped = true;
            _agent.acceleration = 0f;
            _agent.velocity = value;
        }
    }
}
