using System;
using Game.Characters;
using Game.Utils;

namespace Game.AI
{
    public sealed class AiContext : IDisposable
    {
        public sealed class AiContextsPool : Pool<AiContext>
        {
            protected override AiContext CreateEntity()
            {
                return new();
            }

            protected override void DestroyEntity(AiContext entity)
            {
            }

            protected override void HandleEntityBeforeRent(AiContext entityForRent)
            {
            }

            protected override void HandleReturnedEntity(AiContext returnedEntity)
            {
                returnedEntity.Agent = null; //gc
#if FULL_CLEAR_POOLED_CONTEXT
                returnedEntity.DeltaTime = 0;
#endif
            }
        }


        private static readonly AiContextsPool _pool = new();


        private AiContext()
        {
        }


        public Character Agent { get; set; }

        /// <summary>
        /// tick system cross ticks time
        /// </summary>
        public float DeltaTime { get; set; }


        public static AiContext CreateContext()
        {
            return _pool.Rent();
        }


        public void Dispose()
        {
            _pool.Return(this);
        }
    }

}
