using System;
using System.Collections.Generic;
using System.Linq;
using Game.Characters;
using Game.Characters.Actions;
using Game.Characters.Stats.DynamicStats;
using Game.Utils;
using UnityEngine;

namespace Game.Abilities
{
    [System.Serializable]
    public sealed class CompositeAbilityStage
    {
        [SerializeField] private DynamicStatAmount[] _costs;
        [SerializeField] private bool _isInterruptible;

        [SerializeField] private float _duration;
        [SerializeField] private AnimatorAction _animatorAction;

        [SerializeField] private CharacterActionBase[] _characterActions;

        [SerializeField, HideInInspector] private CompositeAbility _parent;
        [SerializeField, HideInInspector] private int _stageIndex;

        public bool IsInterruptible => _isInterruptible;
        public float Duration => _duration;


        internal int StageIndex => _stageIndex;


        internal void InitStage(CompositeAbility parent, int index)
        {
            _parent = parent;
            _stageIndex = index;
        }


        public IEnumerable<DynamicStatAmount> GetCosts()
        {
            if (_costs == null || _costs.Length == 0)
                return Enumerable.Empty<DynamicStatAmount>();

            return _costs;
        }


        public bool TryEnterState(Character character)
        {
            if (!CheckCosts(character))
                return false;

            SpendCosts(character);

            EnterState(character);
            return true;
        }

        public bool CheckCosts(Character character)
        {
            return character.DynamicStatsCollection.CanRemoveAll(_costs);
        }


        private void SpendCosts(Character character)
        {
            character.DynamicStatsCollection.Remove(_costs);
        }

        internal void EnterState(Character character)
        {
            if (_animatorAction != null)
                _animatorAction.Act(character.GetComponent<Animator>());


            foreach (var characterAction in _characterActions)
            {
                characterAction.Act(character);
            }
        }
    }

    public sealed class CompositeAbility : AbilityBase
    {
        [SerializeField] private CompositeAbilityStage[] _stages;


#if UNITY_EDITOR
        private void OnValidate()
        {
            bool isDirty = false;

            for (int i = 0; i < _stages.Length; i++)
            {
                CompositeAbilityStage stage = _stages[i];
                if (stage != null)
                {
                    stage.InitStage(this, i);
                    isDirty = true;
                }
            }

            if (isDirty)
                UnityEditor.EditorUtility.SetDirty(this);
        }
#endif

        public override void Use(AbilityContext context)
        {
            throw new System.NotImplementedException();
        }


        internal void ReportStageStarted(CompositeAbilityStage stage, Character character)
        {

        }
    }
}
