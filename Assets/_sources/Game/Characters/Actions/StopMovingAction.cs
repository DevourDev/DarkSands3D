using UnityEngine;

namespace Game.Characters.Actions
{
    [CreateAssetMenu(menuName = GameAssetsConstants.CharacterActions + "Stop Moving")]
    public sealed class StopMovingAction : CharacterActionBase
    {
        public override void Act(Character actor)
        {
            actor.StopMoving();
        }
    }
}
