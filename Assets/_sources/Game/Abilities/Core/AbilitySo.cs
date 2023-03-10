using UnityEngine;

namespace Game.Abilities
{
    public abstract class AbilitySo : ScriptableObject
    {
        public abstract void Use(AbilityContext context);
    }
}
