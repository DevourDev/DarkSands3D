using Unity.Netcode;
using UnityEngine;

namespace Game.Utils.Network
{
    public sealed class OwnershipMarker : NetworkBehaviour
    {
        [SerializeField] private Renderer _renderer;
        [SerializeField] private Material _owningMat;
        [SerializeField] private Material _notOwningMat;

        private Material _activeMat;


        private void Awake()
        {
            _activeMat = _renderer.material;
        }

        private void Update()
        {
            if (!IsSpawned)
                return;

            SetActiveMat(IsOwner ? _owningMat : _notOwningMat);
        }

        private void SetActiveMat(Material material)
        {
            if (material == _activeMat)
                return;

            _activeMat = material;
            _renderer.material = material;
        }
    }
}
