using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Game.Abilities
{
    public sealed class SimpleSinusoida : MonoBehaviour
    {
        [SerializeField] private bool _scale;
        [SerializeField] private bool _position;
        [SerializeField] private bool _local;
        [SerializeField] private bool _x;
        [SerializeField] private bool _y;
        [SerializeField] private bool _z;
        [SerializeField] private float _sinMultiplier = 1f;
        [SerializeField] private float _sinAddition = 0f;


        private void Update()
        {
            float x = (float)Math.Sin(Time.time) * _sinMultiplier + _sinAddition;

            if (_scale)
                SinusScale(x);

            if (_position)
                SinusPosition(x);
        }

        private void SinusPosition(float x)
        {
            var v = _local ? transform.localPosition : transform.position;
            ModifyVector(ref v, in x);

            if (_local)
                transform.localPosition = v;
            else
                transform.position = v;
        }

        private void SinusScale(float x)
        {
            var v = _local ? transform.localScale : transform.lossyScale;
            ModifyVector(ref v, in x);

            if (_local)
            {
                transform.localScale = v;
            }
            else
            {
                var parent = transform.parent;
                transform.SetParent(null);
                transform.localScale = v;
                transform.SetParent(parent);
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ModifyVector(ref Vector3 v, in float x)
        {
            if (_x)
                v.x = x;

            if (_y)
                v.y = x;

            if (_z)
                v.z = x;
        }
    }
}
   