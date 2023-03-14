using Game.Abilities;
using UnityEngine;

namespace Game.Characters.Actions
{
    [CreateAssetMenu(menuName = GameAssetsConstants.CharacterActions + "Stop Moving")]
    public sealed class StopMovingAction : CharacterActionBase
    {
        [SerializeField] private AbilityBase _ability;

        public override void Act(Character actor)
        {
            actor.StopMoving();
        }
    }
}
