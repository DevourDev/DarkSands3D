using Game.Abilities;
using UnityEngine;

namespace Game.AI
{
    [CreateAssetMenu(menuName = "Ai/Actions/Use Ability")]
    public sealed class UseAbilityAction : AiActionSo
    {
        [SerializeField] private AbilitySo _ability;


        public override void Act(AiContext context)
        {
            var abilityContext = AbilityContext.CreateContext();
            abilityContext.Caster = context.Agent;
            _ability.Use(abilityContext);
            abilityContext.Dispose();
        }
    }
}
