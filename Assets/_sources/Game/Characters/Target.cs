using UnityEngine;

namespace Game.Characters
{
    [System.Serializable]
    public struct Target
    {
        [SerializeField] private TargetType _targetType;
        [SerializeField] private Character _character;
        [SerializeField] private Vector3 _point;


        public TargetType TargetType => _targetType;


        public void Init(Character characterTarget)
        {
            _targetType = TargetType.Character;
            _character = characterTarget;
        }

        public void Init(Vector3 pointTarget)
        {
            _targetType = TargetType.Point;
            _character = null;
            _point = pointTarget;
        }

        public void Init()
        {
            _targetType = TargetType.None;
            _character = null;
        }


        public Vector3 GetPoint()
        {
            return _targetType switch
            {
                TargetType.Point => _point,
                TargetType.Character => _character.transform.position,
                _ => default,
            };
        }

        public Character GetCharacterTarget()
        {
            if (_targetType == TargetType.Character)
                return _character;

            return null;
        }
    }
}
