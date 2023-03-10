using UnityEngine;

namespace Game.Characters
{
    public sealed class SqrMagnutudeTester : MonoBehaviour
    {
        [SerializeField] private Transform _tr0;
        [SerializeField] private Transform _tr1;
        [SerializeField] private float _distance;
        [SerializeField] private float _sqrMagnutude;


        private void OnDrawGizmos()
        {
            _sqrMagnutude = (_tr0.position - _tr1.position).sqrMagnitude;
            _distance = (float)System.Math.Sqrt(_sqrMagnutude);
        }
    }
}
