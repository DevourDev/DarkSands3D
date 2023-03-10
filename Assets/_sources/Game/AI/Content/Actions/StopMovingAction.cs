using UnityEngine;

namespace Game.AI
{
    [CreateAssetMenu(menuName = "Ai/Actions/Stop Moving")]
    public sealed class StopMovingAction : AiActionSo
    {
        public override void Act(AiContext context)
        {
            context.Agent.StopMoving();
        }
    }
}
