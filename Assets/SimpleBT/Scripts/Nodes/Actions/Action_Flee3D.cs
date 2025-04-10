using SimpleBT.Core;
using UnityEngine;

namespace SimpleBT.NonEditor.Nodes
{
    public class Action_Flee3D : Action_Flee2D
    {
        private Rigidbody _rb;
        
        protected override void Initialize()
        {
            base.Initialize();
            _rb = blackboard.gameObject.GetComponent<Rigidbody>();
        }

        protected override Status Tick()
        {
            if (!_target) {
                _target = blackboard.GetComplexValue<GameObject>(_keyTarget);
                if (!_target) { return Status.Failure; }

                if (_useTransform == false && !_rb) {
                    Debug.LogWarning("Missing Rigidbody2D");
                    return Status.Failure;
                }
            }
            
            Vector3 direction = -1 * (_target.transform.position - blackboard.gameObject.transform.position).normalized;
            
            if (_useTransform) { blackboard.gameObject.transform.position += direction * (_speed * Time.deltaTime); }
            else
            {
                if (_rb.isKinematic) { _rb.MovePosition(blackboard.gameObject.transform.position + direction * (_speed * Time.fixedDeltaTime), _flag); }
                else { _rb.linearVelocity = direction * (_speed * Time.fixedDeltaTime);}
            }

            return Vector3.Distance(_target.transform.position, blackboard.gameObject.transform.position) 
                   >= _safeDistance ? Status.Success : Status.Running;
        }
    }

}
