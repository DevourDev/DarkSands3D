using UnityEngine;

namespace Game.AI
{
    public abstract class AiStateBase : ScriptableObject
    {
        public abstract AiStateBase Evaluate(AiContext context);
    }
}
