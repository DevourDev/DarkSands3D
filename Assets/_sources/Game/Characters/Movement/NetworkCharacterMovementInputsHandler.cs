using Unity.Netcode;
using UnityEngine;

namespace Game.Characters.Movement
{
    public sealed class NetworkCharacterMovementInputsHandler : NetworkBehaviour
    {
        [SerializeField] private Character _character;
        [SerializeField] private VelocityBasedNavMeshMovementController _controller;


        [ServerRpc(RequireOwnership = true)]
        public void SetMoveVectorServerRpc(Vector2 vector)
        {
            Vector2 xz = Vector2.ClampMagnitude(vector, 1f) * _character.MaxSpeed;
            _controller.Velocity = new(xz.x, 0f, xz.y);
        }
    }
}
