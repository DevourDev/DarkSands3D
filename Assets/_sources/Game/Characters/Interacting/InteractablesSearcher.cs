using Game.Inputs;
using UnityEngine;

namespace Game.Characters.Interacting
{
    public sealed class InteractablesSearcher : MonoBehaviour
    {
        [SerializeField] private Character _character;
        [SerializeField] private Transform _searchOrigin;
        [SerializeField] private float _searchRadius;

        private readonly Collider[] _buffer = new Collider[128];

        private Interactable _activeInteractable;
        private PlayerControls _controls;


        private void Awake()
        {
            _controls = new();
            _controls.Character.Interact.performed += HandleInteractPerformed;
        }

        private void OnDestroy()
        {
            _controls.Dispose();
            _controls = null;
        }

        private void OnEnable()
        {
            _controls?.Enable();
        }

        private void OnDisable()
        {
            _controls?.Disable();
        }

        private void FixedUpdate()
        {
            DetectInteractablesInRadius();
        }


        private void HandleInteractPerformed(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            InteractWithActiveInteractable();
        }

        private void InteractWithActiveInteractable()
        {
            if (_activeInteractable == null)
                return;

            _activeInteractable.Interact(_character);
        }

        private void DetectInteractablesInRadius()
        {
            int count = Physics.OverlapSphereNonAlloc(_searchOrigin.position, _searchRadius, _buffer,
                GameLayers.Interactables, QueryTriggerInteraction.Ignore);

            if (count == 0)
            {
                ClearActiveInteractable();
                return;
            }

            var closestInteractableGo = FindClosestInteractable(count);
            var interactable = closestInteractableGo.GetComponent<Interactable>();

            if (_activeInteractable == interactable)
                return;

            ClearActiveInteractable();
            SetNewActiveInteractable(interactable);
        }

        private void ClearActiveInteractable()
        {
            if (_activeInteractable != null)
            {
                if (_activeInteractable.TryGetComponent<HighlighterBase>(out var highlighter))
                {
                    highlighter.Downlight(_character);
                }

                _activeInteractable = null;
            }
        }

        private void SetNewActiveInteractable(Interactable interactable)
        {
            _activeInteractable = interactable;

            if (interactable == null)
                return;

            if (_activeInteractable.TryGetComponent<HighlighterBase>(out var highlighter))
            {
                highlighter.Highlight(_character);
            }
        }

        private GameObject FindClosestInteractable(int count)
        {
            Vector3 origin = transform.position;
            Transform closestTr = _buffer[0].transform;
            float closestDist = (origin - closestTr.position).sqrMagnitude;

            for (int i = 1; i < count; i++)
            {
                var tr = _buffer[i].transform;
                var dist = (origin - tr.position).sqrMagnitude;

                if (dist < closestDist)
                {
                    closestDist = dist;
                    closestTr = tr;
                }
            }

            return closestTr.gameObject;
        }
    }
}
