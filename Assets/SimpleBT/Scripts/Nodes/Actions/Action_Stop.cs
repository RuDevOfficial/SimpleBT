using SimpleBT.Core;
using UnityEngine;

namespace SimpleBT.NonEditor.Nodes
{
    public class Action_Stop : Node
    {
        private Rigidbody2D _rb2D;
        private Rigidbody _rb;

        protected override void Initialize()
        {
            _rb2D = blackboard.GetComponent<Rigidbody2D>();
            _rb = blackboard.GetComponent<Rigidbody>();
        }
        protected override Status Tick()
        {
            if (!_rb2D && !_rb) { return Status.Failure; }
            
            if (_rb2D) { _rb2D.linearVelocity *= 0; }
            else { _rb.linearVelocity *= 0; }
            
            return Status.Success;
        }
    }
}