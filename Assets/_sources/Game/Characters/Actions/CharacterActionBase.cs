using UnityEngine;

namespace Game.Characters.Actions
{

    public abstract class CharacterActionBase : ScriptableObject
    {
        public abstract void Act(Character actor);
    }
}
