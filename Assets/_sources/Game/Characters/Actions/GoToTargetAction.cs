using UnityEngine;

namespace Game.Characters.Actions
{
    [CreateAssetMenu(menuName = GameAssetsConstants.CharacterActions + "Go To Target")]
    public sealed class GoToTargetAction : CharacterActionBase
    {
        public override void Act(Character actor)
        {
            var target = actor.Target;

            switch (target.TargetType)
            {
                case Characters.TargetType.Character:
                    actor.Chase(target.GetCharacterTarget().transform);
                    return;
                case Characters.TargetType.Point:
                    actor.GoToPoint(target.GetPoint());
                    return;
                default:
                    break;
            }
        }
    }
}
