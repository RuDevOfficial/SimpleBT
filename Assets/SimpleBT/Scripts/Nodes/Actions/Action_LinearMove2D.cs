using System.Collections.Generic;
using SimpleBT.Core;
using UnityEngine;

namespace SimpleBT.NonEditor.Nodes
{
    public class Action_LinearMove2D : Node, INodeKeyAssignable
    {
        [SerializeField] protected string keyXVel;
        [SerializeField] protected string keyYVel;

        private Vector2 _velocity;

        private Rigidbody2D rb2D;
        
        public virtual void AssignKeys(List<string> keys)
        {
            keyXVel = keys[0];
            keyYVel = keys[1];
        }

        protected override void Initialize()
        {
            blackboard.gameObject.TryGetComponent<Rigidbody2D>(out rb2D);
        }
        
        protected override Status Tick()
        {
            if (!rb2D) return Status.Failure;
            
            _velocity.x = blackboard.GetValue<float>(keyXVel);
            _velocity.y = blackboard.GetValue<float>(keyYVel);

            if (_velocity.x == 0) { rb2D.linearVelocityY = _velocity.y * Time.fixedDeltaTime; }
            else if (_velocity.y == 0) { rb2D.linearVelocityX = _velocity.x * Time.fixedDeltaTime; }
            else { rb2D.linearVelocity = _velocity; }

            return Status.Success;
        }

        public override void OnAbort()
        {
            if (_velocity.x != 0) { rb2D.linearVelocityX = 0; }
            if (_velocity.y != 0) { rb2D.linearVelocityY = 0; }
        }
    }
}
