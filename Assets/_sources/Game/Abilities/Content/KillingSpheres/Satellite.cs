using UnityEngine;

namespace Game.Abilities
{
    public sealed class Satellite : MonoBehaviour
    {
        private float _radius;
        private float _speed;
        private float _angle;


        public void Init(float radius, float speed, float angle)
        {
            _radius = radius;
            _speed = speed;
            _angle = angle;
        }


        public void RotateAround_Math(in Vector3 point, in float deltaTime)
        {
            ChangeAngle(deltaTime);
            transform.position = GetOnUnitCirclePoint(_angle * Mathf.Rad2Deg) * _radius + point;
        }

        public void RotateAround_Unity(in Vector3 point, in float deltaTime)
        {
            ChangeAngle(deltaTime);
            transform.RotateAround(point, Vector3.up, _speed * deltaTime);
        }


        private void ChangeAngle(float deltaTime)
        {
            _angle = (_angle + _speed * deltaTime) % 360f;
        }

        private static Vector3 GetOnUnitCirclePoint(float angleRad)
        {
            return new Vector3((float)System.Math.Cos(angleRad),
                                0f,
                               (float)System.Math.Sin(angleRad));
        }
    }
}
