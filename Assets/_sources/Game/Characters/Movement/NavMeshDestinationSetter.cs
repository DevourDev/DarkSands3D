using Unity.Netcode;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

namespace Game.Characters.Movement
{
    [RequireComponent(typeof(NavMeshAgent))]
    public sealed class NavMeshDestinationSetter : NetworkBehaviour
    {
        [SerializeField] private InputAction _setDestinationInputAction;
        [SerializeField] private InputAction _pointerScreenPositionInputAction;
        [SerializeField] private float _acceleration = 999f;

        private NavMeshAgent _agent;
        private Camera _cam;


        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _cam = Camera.main;
        }

        private void OnEnable()
        {
            if (!IsOwner)
                return;

            _setDestinationInputAction.Disable();
            _pointerScreenPositionInputAction.Disable();
        }

        private void OnDisable()
        {
            if (!IsOwner)
                return;

            _setDestinationInputAction.Enable();
            _pointerScreenPositionInputAction.Enable();
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            if (IsOwner)
                Init();
        }

        private void Init()
        {
            _setDestinationInputAction.performed += SetDestination;
            _setDestinationInputAction.Enable();
            _pointerScreenPositionInputAction.Enable();
        }

        private void SetDestination(InputAction.CallbackContext _)
        {
            var ray = _cam.ScreenPointToRay(_pointerScreenPositionInputAction.ReadValue<Vector2>());
            float maxDist = _cam.farClipPlane;
            int layerMask = LayerMask.GetMask("Default");
            var qti = QueryTriggerInteraction.Ignore;

            if (Physics.Raycast(ray, out var hit, maxDist, layerMask, qti))
            {
                RequestSetDestinationServerRpc(hit.point);
            }
        }

        [ServerRpc(RequireOwnership = true)]
        private void RequestSetDestinationServerRpc(Vector3 point)
        {
            Debug.Log(point);
            var character = GetComponent<Character>();
            _agent.speed = character.MaxSpeed;
            _agent.acceleration = _acceleration;
            _agent.SetDestination(point);
            _agent.isStopped = false;
        }
    }
}
