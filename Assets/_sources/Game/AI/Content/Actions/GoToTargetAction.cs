using UnityEngine;

namespace Game.AI
{
    [CreateAssetMenu(menuName = "Ai/Actions/Go To Target")]
    public sealed class GoToTargetAction : AiActionSo
    {
        //[SerializeField, Min(0f)] private float _speedMultiplier = 1f;


        public override void Act(AiContext context)
        {
            var agent = context.Agent;
            var target = agent.Target;

            switch (target.TargetType)
            {
                case Characters.TargetType.Character:
                    agent.Chase(target.GetCharacterTarget().transform);
                    return;
                case Characters.TargetType.Point:
                    agent.GoToPoint(target.GetPoint());
                    return;
                default:
                    break;
            }
        }
    }
}
