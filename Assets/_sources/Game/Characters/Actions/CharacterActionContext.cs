using System;
using Game.Utils;

namespace Game.Characters.Actions
{
    public sealed class CharacterActionContext : IDisposable
    {
        private sealed class CharacterActionContextsPool : Pool<CharacterActionContext>
        {
            protected override CharacterActionContext CreateEntity()
            {
                return new();
            }

            protected override void DestroyEntity(CharacterActionContext entity)
            {
            }

            protected override void HandleEntityBeforeRent(CharacterActionContext entityForRent)
            {
            }

            protected override void HandleReturnedEntity(CharacterActionContext returnedEntity)
            {
                returnedEntity.Caster = null;
#if FULL_CLEAR_POOLED_CONTEXT
                returnedEntity.Target = default;
#endif
            }
        }


        private static readonly CharacterActionContextsPool _pool = new();


        private CharacterActionContext()
        {
        }


        public Character Caster { get; set; }
        public Target Target { get; set; }


        public void Dispose()
        {
            _pool.Return(this);
        }


        public static CharacterActionContext CreateContext()
        {
            return _pool.Rent();
        }
    }
}
