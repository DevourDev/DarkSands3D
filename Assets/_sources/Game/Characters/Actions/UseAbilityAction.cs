using Game.Abilities;
using UnityEngine;

namespace Game.Characters.Actions
{
    [CreateAssetMenu(menuName = GameAssetsConstants.CharacterActions + "Use Ability")]
    public sealed class UseAbilityAction : CharacterActionBase
    {
        [SerializeField] private AbilityBase _ability;


        public override void Act(Character actor)
        {
            var context = AbilityContext.CreateContext();
            context.Caster = actor;
            context.Target = actor.Target;

            _ability.Use(context);
        }
    }
}
