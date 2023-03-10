using System.Collections;
using System.Collections.Generic;
using Game.Characters;
using UnityEngine;

namespace Game
{
    public class SpeedChangerTrigger : MonoBehaviour
    {
        [SerializeField] private float _speedChange = 2f;


        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Character>(out var character))
            {
                character.MaxSpeed += _speedChange;
            }
        }
    }
}
