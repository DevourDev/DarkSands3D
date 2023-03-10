using Game.Characters;
using UnityEngine;

namespace Game.AI
{
    //todo: unseal class to extend targets
    [CreateAssetMenu(menuName = "Ai/Conditions/Distance To Target")]
    public sealed class DistanceToTargetCondition : ConditionBase
    {
        //no point to add equal check - it will arbitrary never true
        public enum CompareMode
        {
            More,
            Less,
        }


        [SerializeField] private CompareMode _mode;
        //[SerializeField] private Ability _ability;
        [SerializeField] private float _value;


        public sealed override bool Evaluate(AiContext context)
        {
            var agent = context.Agent;
            float sqrDist = (agent.transform.position - GetTargetPoint(agent)).sqrMagnitude;
            return CompareToValue(sqrDist);
        }


        private bool CompareToValue(float v)
        {
            //var value = _ability.SqrRange

            return _mode switch
            {
                CompareMode.More => v > _value,
                CompareMode.Less => v < _value,
                _ => throw new System.Exception("unexpected enum value " + _mode)
            };
        }

        //todo: make abstract
        protected Vector3 GetTargetPoint(Character agent)
        {
            return agent.Target.GetPoint();
        }
    }
}
