using Game.Characters;
using UnityEngine;

namespace Game
{
    public class SpeedSetterTrigger : MonoBehaviour
    {
        [SerializeField] private float _speed = 100f;


        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Character>(out var character))
            {
                character.MaxSpeed =  _speed;
            }
        }
    }
}
