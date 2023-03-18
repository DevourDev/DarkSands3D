using UnityEngine;

namespace Game.Abilities
{
    [System.Serializable]
    public sealed class AnimatorAction
    {
        [SerializeField] private string _paramName;
        [SerializeField] private AnimatorControllerParameterType _paramType;
        [SerializeField] private float _floatValue;
        [SerializeField] private int _intValue;
        [SerializeField] private bool _boolValue;

        private int? _paramId;


        public AnimatorAction(string paramName, float floatValue)
        {
            _paramName = paramName;
            _paramType = AnimatorControllerParameterType.Float;
            _floatValue = floatValue;
        }

        public AnimatorAction(string paramName, int intValue)
        {
            _paramName = paramName;
            _paramType = AnimatorControllerParameterType.Int;
            _intValue = intValue;
        }

        public AnimatorAction(string paramName, bool boolValue)
        {
            _paramName = paramName;
            _paramType = AnimatorControllerParameterType.Bool;
            _boolValue = boolValue;
        }

        public AnimatorAction(string paramName)
        {
            _paramName = paramName;
            _paramType = AnimatorControllerParameterType.Trigger;
        }


        public void Act(Animator animator)
        {
            if (animator == null)
                return;

            if (string.IsNullOrEmpty(_paramName))
                return;

            if (!_paramId.HasValue)
                _paramId = Animator.StringToHash(_paramName);

            switch (_paramType)
            {
                case AnimatorControllerParameterType.Float:
                    animator.SetFloat(_paramId.Value, _floatValue);
                    break;
                case AnimatorControllerParameterType.Int:
                    animator.SetInteger(_paramId.Value, _intValue);
                    break;
                case AnimatorControllerParameterType.Bool:
                    animator.SetBool(_paramId.Value, _boolValue);
                    break;
                case AnimatorControllerParameterType.Trigger:
                    animator.SetTrigger(_paramId.Value);
                    break;
                default:
                    break;
            }
        }
    }
}
