using Game.Characters.Movement;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Inputs
{
    public class PlayerControlsHandler : NetworkBehaviour
    {
        [SerializeField] private NetworkCharacterMovementInputsHandler _controller;

        private PlayerControls _controls;


        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            Debug.Log("spawned: " + IsOwner);

            if (!IsOwner)
                return;

            InitControls();
            Subscribe();
            _controls.Enable();
        }

        public override void OnGainedOwnership()
        {
            Debug.Log("gained: " + IsOwner);

            //if (!IsOwner)
            //    return;

            //InitControls();
            //Subscribe();
            //_controls.Enable();
        }

        public override void OnLostOwnership()
        {
            base.OnLostOwnership();
            Debug.Log("lost: " + IsOwner);
        }

        private void InitControls()
        {
            _controls = new();
        }

        private void Subscribe()
        {
            var ch = _controls.Character;
            ch.Move.performed += HandleMoveInput;
        }

        private void HandleMoveInput(InputAction.CallbackContext context)
        {
            _controller.SetMoveVectorServerRpc(context.ReadValue<Vector2>());
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            DisposeControls();
            //no need to unsub cuz event is going to be GCed
        }

        private void DisposeControls()
        {
            _controls?.Dispose();
        }
    }
}
