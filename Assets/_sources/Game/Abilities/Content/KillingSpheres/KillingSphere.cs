using Game.Characters;
using Game.Characters.Stats.DynamicStats;
using UnityEngine;

namespace Game.Abilities
{
    public sealed class KillingSphere : MonoBehaviour
    {
        private DynamicStatAmount _dmg;
        private float _sphereRadius;
        private double _dmgDealed;
        private bool _limited;
        private double _dmgLimit;


        public DynamicStatAmount Dmg => _dmg;
        public float SphereRadius => _sphereRadius;
        public double DmgDealed => _dmgDealed;
        public bool Limited => _limited;
        public double DmgLimit => _dmgLimit;


        public event System.Action<KillingSphere> OnInited;
        public event System.Action<KillingSphere, Character> OnCharacterHitted;
        public event System.Action<KillingSphere> OnBeforeDestruction;



        public void Init(DynamicStatAmount dmg, float sphereRadius)
        {
            _dmg = dmg;
            _sphereRadius = sphereRadius;
        }

        public void Init(DynamicStatAmount dmg, float sphereRadius, double dmgLimit)
        {
            Init(dmg, sphereRadius);
            _limited = true;
            _dmgLimit = dmgLimit;
        }


        private void HitCharacter(Character target)
        {
            target.DynamicStatsCollection.Remove(_dmg);
        }

        private void HandleDmgDealt()
        {
            _dmgDealed += _dmg.Amount;

            if (!_limited)
                return;

            double left = _dmgLimit - _dmgDealed;

            if (left > 0)
                return;

            Die();
        }

        private void Die()
        {
            OnBeforeDestruction(this);
            Destroy(gameObject);
        }
    }
}
