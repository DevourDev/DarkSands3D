using DevourDev.Unity.ScriptableObjects;

namespace Game.Characters.Actions
{
    public abstract class CharacterActionBase : SoDatabaseElement
    {
        public abstract void Act(Character actor);
    }
}
