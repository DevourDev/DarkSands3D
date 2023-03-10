using UnityEngine;

namespace Game.AI
{
    public sealed class SimpleAiTranslator : AiStateBase
    {
        [SerializeField] private ConditionBase _condition;

        [SerializeField] private AiStateBase _successTranslation;
        [SerializeField] private AiStateBase _failureTranslation;


        public override AiStateBase Evaluate(AiContext context)
        {
            if (_condition.Evaluate(context))
                return _successTranslation;

            return _failureTranslation;
        }
    }
}
