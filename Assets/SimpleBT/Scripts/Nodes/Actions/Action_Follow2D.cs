using System.Collections.Generic;
using SimpleBT.Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace SimpleBT.NonEditor.Nodes
{
    public class Action_Follow2D : ExecutionNode, INodeKeyAssignable
    {
        [SerializeField] private string keyTarget;
        [SerializeField] private string keySpeed;
        [SerializeField] private string keyUseTransform;

        private GameObject _target;
        private bool _useTransform;
        private float _velocity;
        private Rigidbody2D _rb2D;
        
        public void AssignKeys(List<string> keys)
        {
            keyTarget = keys[0];
            keySpeed = keys[1];
            keyUseTransform = keys[2];
        }

        protected override void Initialize()
        {
            _target = blackboard.GetComplexValue<GameObject>(keyTarget);
            _useTransform = blackboard.GetValue<bool>(keyUseTransform);
            _rb2D = blackboard.gameObject.GetComponent<Rigidbody2D>();
        }

        protected override Status Tick()
        {
            if (!_target) { return Status.Failure; }
            if (_useTransform == false && !_rb2D) { return Status.Failure; }
            
            _velocity = blackboard.GetValue<float>(keySpeed);
            Vector2 direction = (_target.transform.position - blackboard.gameObject.transform.position).normalized;
            
            if (_useTransform) {
                blackboard.gameObject.transform.position = (Vector2)blackboard.gameObject.transform.position + direction * (_velocity * Time.deltaTime);
            }
            else { _rb2D.linearVelocity = direction * (_velocity * Time.fixedDeltaTime); }
            
            return Status.Success;
        }
    }
}