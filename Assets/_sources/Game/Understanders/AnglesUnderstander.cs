#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Game.Understanders
{
    public sealed class AnglesUnderstander : MonoBehaviour
    {
#if UNITY_EDITOR
        [SerializeField] private bool _mirrorA;
        [SerializeField] private Transform _aStartTr;
        [SerializeField] private Transform _aEndTr;

        [SerializeField] private bool _mirrorB;
        [SerializeField] private Transform _bStartTr;
        [SerializeField] private Transform _bEndTr;

        [SerializeField] private float _angle;
        [SerializeField] private float _signedAngle;

        [SerializeField] private float _replacedAngle;
        [SerializeField] private float _replacedSignedAngle;

        [SerializeField, Range(1f, 10f)] private float _lineThickness = 5f;
        [SerializeField, Range(0.01f, 2f)] private float _headRadius;
        [SerializeField] private Color _aColor = Color.cyan;
        [SerializeField] private Color _bColor = Color.red;


        private void OnDrawGizmos()
        {
            if (_aStartTr == null || _aEndTr == null
                || _bStartTr == null || _bEndTr == null)
                return;

            Vector2 aStart = new(_aStartTr.position.x, _aStartTr.position.z);
            Vector2 aEnd = new(_aEndTr.position.x, _aEndTr.position.z);

            Vector2 bStart = new(_bStartTr.position.x, _bStartTr.position.z);
            Vector2 bEnd = new(_bEndTr.position.x, _bEndTr.position.z);

            Vector2 a = _mirrorA ? aStart - aEnd : aEnd - aStart;
            Vector2 b = _mirrorB ? bStart - bEnd : bEnd - bStart;

            _angle = Vector2.Angle(a, b);
            _signedAngle = Vector2.SignedAngle(a, b);

            _replacedAngle = Vector2.Angle(b, a);
            _replacedSignedAngle = Vector2.SignedAngle(b, a);

            Handles.color = _aColor;
            Handles.DrawLine(_aStartTr.position, _aEndTr.position, _lineThickness);
            Gizmos.color = _aColor;
            Gizmos.DrawSphere(_mirrorA ? _aStartTr.position : _aEndTr.position, _headRadius);

            Handles.color = _bColor;
            Handles.DrawLine(_bStartTr.position, _bEndTr.position, _lineThickness);
            Gizmos.color = _bColor;
            Gizmos.DrawSphere(_mirrorB ? _bStartTr.position : _bEndTr.position, _headRadius);

        }
#endif
    }
}
