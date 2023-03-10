using System.Diagnostics.Eventing.Reader;
using UnityEngine;

namespace Game.Abilities
{
    public sealed class SimpleRotator : MonoBehaviour
    {
        [SerializeField] private Vector3 _axis;
        [SerializeField] private float _rotation;


        private void Update()
        {
            transform.Rotate(_axis, _rotation * Time.deltaTime);
        }

    }
}
   