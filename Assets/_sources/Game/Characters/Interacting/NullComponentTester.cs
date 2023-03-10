using UnityEngine;

namespace Game.Characters.Interacting
{
    public sealed class NullComponentTester : MonoBehaviour
    {
        [SerializeField] private Component _component;
        [SerializeField, TextArea(5, 10)] private string _status;
        [SerializeField, TextArea(5, 10)] private string _status2;
        [SerializeField, TextArea(5, 10)] private string _status3;
        [SerializeField] private bool _destroy;
        [SerializeField] private bool _destroyed;

        private Component _component2;
        private CustomComponent _cc;
        private bool _replaced;


        private void Awake()
        {
            _cc = (CustomComponent)_component;
            _component2 = _cc;
        }

        private void Update()
        {
            if (_destroy)
            {
                _destroy = false;

                if (_component != null && _component.gameObject != null)
                {
                    Destroy(_component.gameObject);
                    _destroyed = true;
                }
                else
                {
                    Destroy(_cc);
                }
            }

            if(!_replaced && _component == null)
            {
                _component = _cc;
                _replaced = true;
            }

            _status = _cc.ToString();
            _status2 = _component2.ToString();
            _status3 = _component.ToString();
        }
    }
}
