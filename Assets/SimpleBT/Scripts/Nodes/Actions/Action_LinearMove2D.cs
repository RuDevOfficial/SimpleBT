using System.Collections.Generic;
using SimpleBT.Core;
using UnityEngine;

namespace SimpleBT.NonEditor.Nodes
{
    public class Action_LinearMove2D : ExecutionNode, INodeKeyAssignable
    {
        [SerializeField] private string keyXVel;
        [SerializeField] private string keyYVel;

        private Vector2 velocity = new Vector2();

        private Rigidbody2D rb2D;
        
        public void AssignKeys(List<string> keys)
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
            velocity.x = blackboard.GetValue<float>(keyXVel);
            velocity.y = blackboard.GetValue<float>(keyYVel);
            
            if (!rb2D) return Status.Failure;

            if (velocity.x == 0) { rb2D.linearVelocityY = velocity.y; }
            else if (velocity.y == 0) { rb2D.linearVelocityX = velocity.x; }
            else { rb2D.linearVelocity = velocity; }

            return Status.Success;
        }

        public override void OnAbort()
        {
            if (velocity.x != 0) { rb2D.linearVelocityX = 0; }
            if (velocity.y != 0) { rb2D.linearVelocityY = 0; }
        }
    }
}
