using Game.AI;
using Game.Characters.Stats.DynamicStats;
using UnityEngine;
using UnityEngine.AI;

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


    [DisallowMultipleComponent]
    public sealed class Character : MonoBehaviour
    {
        [SerializeField] private TeamSo _team;
        [SerializeField] private float _maxSpeed = 5f;
        [SerializeField] private float _visionRadius;
        [SerializeField] private float _visionAngle;
        [SerializeField] private Target _target;
        [SerializeField] private NavMeshHelper _navMeshHelper;

        private readonly DynamicStatsCollection _dynamicStatsCollection = new();
        private readonly SensorsDataCollection _sensorsDataCollection = new();


        public TeamSo Team { get => _team; set => _team = value; }
        public float MaxSpeed { get => _maxSpeed; set => SetMaxSpeed(value); }
        public float VisionRadius { get => _visionRadius; set => _visionRadius = value; }
        public float VisionAngle { get => _visionAngle; set => _visionAngle = value; }
        public Target Target { get => _target; set => _target = value; }


        public DynamicStatsCollection DynamicStatsCollection => _dynamicStatsCollection;
        public SensorsDataCollection SensorsDataCollection => _sensorsDataCollection;


        private void Awake()
        {
            SyncNavMeshStats();
        }


        public void GoToPoint(Vector3 point)
        {
            _navMeshHelper.GoToPoint(point);
        }


        public void Chase(Transform target)
        {
            //it is not necessarily _target
            _navMeshHelper.ChaseTarget(target);
        }


        public void StopMoving()
        {
            _navMeshHelper.Stop();
        }


        private void SetMaxSpeed(float value)
        {
            _maxSpeed = value;
            SyncNavMeshStats();
        }

        private void SyncNavMeshStats()
        {
            _navMeshHelper.MaxSpeed = _maxSpeed;
        }
    }
}
