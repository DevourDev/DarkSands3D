using UnityEngine;

namespace Game.Abilities
{
    public abstract class AbilityBase : ScriptableObject
    {
        public abstract void Use(AbilityContext context);
    }
}
