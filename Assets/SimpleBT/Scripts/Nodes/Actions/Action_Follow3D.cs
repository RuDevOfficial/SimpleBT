using SimpleBT.Core;
using UnityEngine;

namespace SimpleBT.NonEditor.Nodes
{
    public class Action_Follow3D : Action_Follow2D
    {
        private Rigidbody _rb;
        
        protected override void Initialize()
        {
            _useTransform = _blackboard.GetValue<bool>(_keyUseTransform);
            _rb = _blackboard.gameObject.GetComponent<Rigidbody>();
            _rigidbodyMoveFlag = _blackboard.GetValue<RigidbodyMoveFlag>(_keyIgnoreFlag);
            _distance = _blackboard.GetValue<float>(_keyDistance);
        }
        
        protected override Status Tick()
        {
            if (!_target)
            {
                if (_useTransform == false && !_rb) { return Status.Failure; }

                _target = _blackboard.GetComplexValue<GameObject>(_keyTarget); 
                _velocity = _blackboard.GetValue<float>(_keySpeed);
                
                if (!_target) { return Status.Failure; }
            }
            
            Vector3 direction = (_target.transform.position - _blackboard.gameObject.transform.position).normalized;
            
            if (_useTransform) { _blackboard.gameObject.transform.position += direction * (_velocity * Time.deltaTime); }
            else
            {
                if (_rb.isKinematic) { _rb.MovePosition(_blackboard.gameObject.transform.position + direction * (_velocity * Time.fixedDeltaTime), _rigidbodyMoveFlag); }
                else { _rb.linearVelocity = direction * (_velocity * Time.fixedDeltaTime);}
            }

            bool result = Vector3.Distance(_target.transform.position, _blackboard.gameObject.transform.position) <= _distance;
            if (!result) return Status.Running;
            
            _target = null;
            return Status.Success;
        }

        public override void OnAbort()
        {
            _target = null;
        }
    }

}
