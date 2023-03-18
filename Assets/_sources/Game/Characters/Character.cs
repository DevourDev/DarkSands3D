using Game.AI;
using Game.Characters.Stats.DynamicStats;
using UnityEngine;

namespace Game.Characters
{
    [DisallowMultipleComponent]
    public sealed class Character : MonoBehaviour //todo?: change monobeh to networkbeh (or not)
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
