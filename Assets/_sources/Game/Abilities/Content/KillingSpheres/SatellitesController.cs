using System;
using UnityEngine;

namespace Game.Abilities
{
    //todo: add RegisterSatellites(GameObject[] satellites, float speed, float radius) <- all satellites
    //      will have equal initial settings (except initial angle)

    //todO: add UnregisterSatellites
    public sealed class SatellitesController : MonoBehaviour
    {
        public const int MaxArrayLength = 2146435071;
        public const int MinArrayLength = 8;

        [SerializeField] private Satellite _prefab;
        [SerializeField] private Transform _origin;
        [SerializeField] private float _speed;
        [SerializeField] private float _radius = 40f;

        private Satellite[] _elements;
        private int _elementsCount;


        [Obsolete("pooling not implemented", true)]
        public void RegisterSatellite(float speed, float radius)
        {
            //take from pool
        }

        public void RegisterSatellite(GameObject satellite,
            float speed, float radius)
        {
            if (!satellite.TryGetComponent<Satellite>(out var satelliteComp))
            {
                satelliteComp = satellite.AddComponent<Satellite>();
            }

            satelliteComp.Init(radius, speed, GenerateInitialAngle());
            RegisterSatelliteHelper(satelliteComp);
        }

        internal void UnregisterSatellite(Satellite satellite)
        {
            UnregisterSatelliteHelper(satellite);
        }


        private void RegisterSatelliteHelper(Satellite satellite)
        {
            if (_elements == null || _elements.Length < MinArrayLength)
                _elements = new Satellite[MinArrayLength];

            if (_elements.Length == _elementsCount)
            {
                Array.Resize(ref _elements,
                    System.Math.Clamp(_elementsCount * 2, 0, MaxArrayLength));
            }

            _elements[_elementsCount] = satellite;
            ++_elementsCount;

            //set position
            satellite.RotateAround_Math(_origin.position, 0f);

            //return to pool if pooled
        }

        private void UnregisterSatelliteHelper(Satellite satellite)
        {
            var c = _elementsCount;
            var span = _elements.AsSpan(0, c);

            //Unity Components' HashCodes are InstanceIDs,
            //so they can not be non-unique (I believe)
            int hc = satellite.GetHashCode();

            for (int i = 0; i < c; i++)
            {
                if (span[i].GetHashCode() == hc)
                {
                    --_elementsCount;
                    span[i] = span[_elementsCount];
                    span[_elementsCount] = null;
                }
            }

#if UNITY_EDITOR
            UnityEngine.Debug.LogError($"attempt to remove satellite {satellite}, " +
                $"but unable to find it in collection.");

            foreach (var el in _elements)
            {
                if (el == satellite)
                {
                    for (int i = 0; i < 50; i++)
                    {
                        UnityEngine.Debug.LogError("!!!! MISSING ELEMENT FOUND!");
                    }
                }
            }
#endif
        }

        private float GenerateInitialAngle()
        {
            //todo: return uniform angle (related
            //to existing satellites)

            return UnityEngine.Random.Range(0f, 360f);
        }

        private void Update()
        {
            RotateElements();
        }

        private void RotateElements()
        {
            float deltaTime = Time.deltaTime;
            float angle = _speed * deltaTime;

            var c = _elementsCount;
            var span = _elements.AsSpan(0, c);
            Vector3 point = _origin.position;

            for (int i = -1; ++i < c;)
            {
                span[i].RotateAround_Math(in point, in angle);
            }
        }
    }
}
