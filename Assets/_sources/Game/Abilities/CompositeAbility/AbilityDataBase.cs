using Game.Characters;

namespace Game.Abilities
{
    public abstract class AbilityDataBase
    {
        private readonly AbilityBase _ability;
        private readonly Character _caster;


        protected AbilityDataBase(AbilityBase ability, Character caster)
        {
            _ability = ability;
            _caster = caster;
        }


        protected AbilityBase Ability => _ability;
        protected Character Caster => _caster;
    }
}
