using UnityEngine;

namespace Game.Abilities
{
    public sealed class KillingSpheresResources : MonoBehaviour
    {
        [SerializeField] private KillingSphere _sherePrefab;

        private static KillingSpheresResources _inst;


        public static KillingSphere Prefab => _inst._sherePrefab;


        private void Awake()
        {
            _inst = this;
        }

    }
}
