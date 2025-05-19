using SimpleBT.Core;
using UnityEngine;

namespace SimpleBT.NonEditor.Nodes
{
    public class Action_GoToPosition3D : Action_GoToPosition2D
    {
        private Vector3 _position3D;
        private Vector3 _direction3D;
        private Rigidbody _rigidbody;

        protected override void Initialize()
        {
            _useTransform = _blackboard.GetValue<bool>(_keyUseTransform);
            _rigidbody = _blackboard.gameObject.GetComponent<Rigidbody>();
            _rigidbodyMoveFlag = _blackboard.GetValue<RigidbodyMoveFlag>(_keyIgnoreFlag);
        }

        protected override Status Tick()
        {
            if (_time == 0.0f)
            {
                _position3D = _blackboard.GetValue<Vector3>(_keyPosition);
                _speed = _blackboard.GetValue<float>(_keySpeed);
                _direction3D = (_position3D - _blackboard.gameObject.transform.position).normalized;
                _timeToReach = (_position3D - _blackboard.gameObject.transform.position).magnitude / _speed;
                
                if (!_rigidbody && _useTransform == false) { Debug.LogWarning("No rigidbody component found."); return Status.Failure; }
                if (_rigidbody) { if (_rigidbody.isKinematic == false) { Debug.LogWarning("To use Action_GoToPosition3D with Rigidbodies select IsKinematic to True"); return Status.Failure; } }
            }

            if (_useTransform)
            {
                if (_time < _timeToReach) {
                    _time += Time.deltaTime;
                    _blackboard.gameObject.transform.position += _direction3D * (_speed * Time.deltaTime);
                    return Status.Running;
                }
                else
                {
                    _time = 0.0f;
                    _blackboard.gameObject.transform.position = _position3D;
                    return Status.Success;
                }
            }
            else
            {
                if (_time < _timeToReach)
                {
                    _time += Time.deltaTime;
                    _rigidbody.MovePosition(_rigidbody.position + _direction3D * (_speed * Time.fixedDeltaTime), _rigidbodyMoveFlag);
                    return Status.Running;
                }
                else
                {
                    _time = 0.0f;
                    //_rigidbody.MovePosition(_position3D, _rigidbodyMoveFlag);
                    return Status.Success;
                }
            }
        }
    }
}
