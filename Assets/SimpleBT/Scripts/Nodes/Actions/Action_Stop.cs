using SimpleBT.Core;
using UnityEngine;

namespace SimpleBT.NonEditor.Nodes
{
    public class Action_Stop : ExecutionNode
    {
        private Rigidbody2D rb2D;
        protected override void Initialize() { rb2D = blackboard.GetComponent<Rigidbody2D>(); }
        protected override Status Tick()
        {
            if (!rb2D) { return Status.Failure; }
            rb2D.linearVelocity *= 0;
            return Status.Success;
        }
    }
}