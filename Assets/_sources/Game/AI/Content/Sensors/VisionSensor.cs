using System;
using System.Collections.Generic;
using Game.Characters;
using UnityEngine;

namespace Game.AI
{
    public sealed class VisionSensor : SensorSo
    {
        public sealed class VisionData : SensorDataBase
        {
            //todo: switch to more efficient collection
            private readonly List<Character> _visibleCharacters = new();


            public List<Character> VisibleCharacters => _visibleCharacters;
        }


#if UNITY_EDITOR
        [SerializeField] private AllyModeFilterInitor _allyModeFilterCreator;
        [SerializeField] private bool _createFilter;
#endif
        [SerializeField, HideInInspector] private AllyMode _cachedFilter;

        private static readonly Collider[] _buffer = new Collider[1024];


        internal override int DataClassesCount => 1;

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_createFilter)
            {
                _createFilter = false;
                _cachedFilter = _allyModeFilterCreator.GetAllyMode();
                UnityEditor.EditorUtility.SetDirty(this);
            }
        }
#endif

        public override void Scan(AiContext context)
        {
            var agent = context.Agent;
            var sd = GetSensorData<VisionData>(agent);
            var visibleCharacters = sd.VisibleCharacters;
            visibleCharacters.Clear();
            sd.Relevant = true;

            float radius = agent.VisionRadius;
            Vector3 origin = agent.transform.position;
            int count = Physics.OverlapSphereNonAlloc(origin, radius, _buffer, GameLayers.Characters, QueryTriggerInteraction.Ignore);

            if (count == 0)
                return;

            //если будет очень плохая производительность - засунуть
            //все проверки в один луп по спану коллайдеров
            //либо вообще получать сразу Memory<Transform>/Memory<Character>
            //из LightWeightPhysics


            //prototype purposes
            foreach (var col in _buffer)
            {
                visibleCharacters.Add(col.GetComponent<Character>());
            }

            if (!FetchByAllyMode(in agent, in visibleCharacters))
                return;

            if (!FetchByAngle(in agent, in visibleCharacters))
                return;

            if (!FetchByObstacles(in agent, in visibleCharacters))
                return;

        }


        private bool FetchByAngle(in Character owner, in List<Character> buffer)
        {
            var c = buffer.Count;
            var thisTr = owner.transform;
            var visionAngle = owner.VisionAngle;
            Vector3 thisPos = thisTr.position;
            Vector3 forward = thisTr.forward;

            for (int i = c - 1; i >= 0; i--)
            {
                var tr = buffer[i].transform;
                var toTargetVec = tr.position - thisPos;
                Angle(in forward, in toTargetVec, out var angle);

                if (angle > visionAngle)
                {
                    buffer.RemoveAt(i);
                }
            }

            return buffer.Count > 0;
        }

        private bool FetchByObstacles(in Character owner, in List<Character> buffer)
        {
            var c = buffer.Count;
            Vector3 thisPos = owner.transform.position;
            var physScene = Physics.defaultPhysicsScene;
            var blockersMask = GameLayers.VisionBlockers;

            for (int i = c - 1; i >= 0; i--)
            {
                var tr = buffer[i].transform;
                Vector3 distVec = tr.position - thisPos;
                float dist = distVec.magnitude;

                if (physScene.Raycast(thisPos, distVec, dist, blockersMask, QueryTriggerInteraction.Ignore))
                {
                    buffer.RemoveAt(i);
                }
            }

            return buffer.Count > 0;
        }

        private bool FetchByAllyMode(in Character owner, in List<Character> buffer)
        {
            var ownerTeam = owner.Team;
            var filter = _cachedFilter;
            var c = buffer.Count;

            for (int i = c - 1; i >= 0; i--)
            {
                var otherTeam = buffer[i].Team;
                var allyMode = TeamSo.GetAllyMode(ownerTeam, otherTeam);

                if ((allyMode & _cachedFilter) == 0)
                    buffer.RemoveAt(i);
            }

            return buffer.Count > 0;
        }


        private static void Angle(in Vector3 a, in Vector3 b, out float angle)
        {
            const float rad2Deg = Mathf.Rad2Deg;
            const float sqrMin = Vector3.kEpsilonNormalSqrt;
            float dot = a.x * b.x + a.y * b.y + a.z * b.z; // Dot(a,b)
            float sqrLen0 = a.x * a.x + a.y * a.y + a.z * a.z; // a.sqrMagnitude
            float sqrLen1 = b.x * b.x + b.y * b.y + b.z * b.z; // b.sqrMagnitude
            float num = (float)System.Math.Sqrt(sqrLen0 * sqrLen1);

            if (num < sqrMin)
            {
                angle = 0f;
                return;
            }

            float cos = dot / num;
            angle = (float)Math.Acos(cos) * rad2Deg;
        }
    }
}
