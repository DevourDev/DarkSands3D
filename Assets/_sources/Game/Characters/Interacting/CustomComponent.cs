using UnityEngine;

namespace Game.Characters.Interacting
{
    public sealed class CustomComponent : MonoBehaviour
    {
        private Character _character;
        private float _f0;
        private float _f1;
        private float _f2;


        private void Awake()
        {
            _character = FindObjectOfType<Character>();
        }

        private void Update()
        {
            _f0 = UnityEngine.Random.value;
            _f1 = UnityEngine.Random.value;
            _f2 = UnityEngine.Random.value;
        }


        public override string ToString()
        {
            _f0 = UnityEngine.Random.value;
            _f1 = UnityEngine.Random.value;
            _f2 = UnityEngine.Random.value;

            return $"I am ok, fs: {_f0}, {_f1}, {_f2}, character: {_character}";
        }
    }
}
