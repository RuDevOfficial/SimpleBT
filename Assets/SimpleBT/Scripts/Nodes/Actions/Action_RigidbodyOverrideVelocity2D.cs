using System.Collections.Generic;
using SimpleBT.Core;
using UnityEngine;

namespace SimpleBT.NonEditor.Nodes
{
    public class Action_RigidbodyOverrideVelocity2D : ExecutionNode
    {
        private string KeyVelocity;
        
        private Rigidbody2D _rb2D;
        Vector3 velocity;

        public override void AssignKeys(List<string> keys)
        {
            
        }

        protected override void Initialize()
        {
            velocity = blackboard.GetValue<Vector2>(KeyVelocity);
            blackboard.gameObject.TryGetComponent(out _rb2D);
        }

        protected override Status Tick()
        {
            if (_rb2D == null) { return Status.Failure; }
            
            _rb2D.linearVelocity = Vector2.zero;
            
            return Status.Failure;
        }
    }
}
