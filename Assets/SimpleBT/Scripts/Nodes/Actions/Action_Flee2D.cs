using System.Collections.Generic;
using SimpleBT.Core;
using UnityEngine;

namespace SimpleBT.NonEditor.Nodes
{
    public class Action_Flee2D : ExecutionNode, INodeKeyAssignable
    {
        [SerializeField] protected string _keyTarget;
        [SerializeField] private string _keySpeed;
        [SerializeField] private string _keyUseTransform;
        [SerializeField] private string _keyMoveFlag;
        [SerializeField] private string _keySafeDistance;

        protected GameObject _target;
        protected float _speed;
        protected bool _useTransform;
        protected RigidbodyMoveFlag _flag;
        protected float _safeDistance;

        private Rigidbody2D _rb2D;

        public void AssignKeys(List<string> keys)
        {
            _keyTarget = keys[0];
            _keySpeed = keys[1];
            _keyUseTransform = keys[2];
            _keyMoveFlag = keys[3];
            _keySafeDistance = keys[4];
        }
        
        protected override void Initialize()
        {
            _speed = blackboard.GetValue<float>(_keySpeed);
            _safeDistance = blackboard.GetValue<float>(_keySafeDistance);
            _useTransform = blackboard.GetValue<bool>(_keyUseTransform);
            _rb2D = blackboard.gameObject.GetComponent<Rigidbody2D>();
            _flag = blackboard.GetValue<RigidbodyMoveFlag>(_keyMoveFlag);
        }
        
        protected override Status Tick()
        {
            if (!_target) {
                _target = blackboard.GetComplexValue<GameObject>(_keyTarget);
                if (!_target) { return Status.Failure; }

                if (_useTransform == false && !_rb2D) {
                    Debug.LogWarning("Missing Rigidbody2D");
                    return Status.Failure;
                }
            }
            
            Vector2 direction = -1 * (_target.transform.position - blackboard.gameObject.transform.position).normalized;
            
            if (_useTransform) {
                blackboard.gameObject.transform.position 
                    = (Vector2)blackboard.gameObject.transform.position + direction * (_speed * Time.deltaTime);
            }
            else
            {
                if (_rb2D.bodyType == RigidbodyType2D.Kinematic) { _rb2D.MovePosition((Vector2)blackboard.gameObject.transform.position + direction * (_speed * Time.deltaTime), _flag); }
                else { _rb2D.linearVelocity = direction * (_speed * Time.fixedDeltaTime); }
            }

            return Vector2.Distance(_target.transform.position, blackboard.gameObject.transform.position) 
                   >= _safeDistance ? Status.Success : Status.Running;
        }
    }

}
