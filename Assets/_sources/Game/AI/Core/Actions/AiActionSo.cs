using Game.Characters;
using UnityEngine;

namespace Game.AI
{
    public abstract class AiActionSo : ScriptableObject
    {
        public abstract void Act(AiContext context);
    }
}
