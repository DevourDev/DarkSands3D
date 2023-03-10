using System.Collections.Generic;

namespace Game.Abilities
{
    public sealed class AbilitiesStatesCollection
    {
        private readonly Dictionary<AbilitySo, AbilityState> _states = new();


        public AbilitiesStatesCollection()
        {

        }


        internal TState GetState<TState>(AbilitySo ability)
            where TState : AbilityState, new()
        {
            if(!_states.TryGetValue(ability, out var stateRaw))
            {
                stateRaw = new();
                _states.Add(ability, stateRaw);
            }

            return (TState)stateRaw;
        }
    }
}
