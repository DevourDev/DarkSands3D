using System;
using Game.Characters;
using Game.Utils;

namespace Game.Abilities
{
    public sealed class AbilityContext : IDisposable
    {
        public sealed class AbilityContextsPool : Pool<AbilityContext>
        {
            protected override AbilityContext CreateEntity()
            {
                return new();
            }

            protected override void DestroyEntity(AbilityContext entity)
            {
            }

            protected override void HandleEntityBeforeRent(AbilityContext entityForRent)
            {
            }

            protected override void HandleReturnedEntity(AbilityContext returnedEntity)
            {
                returnedEntity.Caster = null;
#if FULL_CLEAR_POOLED_CONTEXT
                //placeholder
#endif
            }
        }


        private static readonly AbilityContextsPool _pool = new();


        private AbilityContext()
        {
        }


        public Character Caster { get; set; }


        public void Dispose()
        {
            _pool.Return(this);
        }


        public static AbilityContext CreateContext()
        {
            return _pool.Rent();
        }
    }
}
