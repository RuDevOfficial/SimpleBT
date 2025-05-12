using System.Collections.Generic;
using SimpleBT.Core;
using UnityEngine;

namespace SimpleBT.NonEditor.Nodes
{
    public class Action_RotateConstantly2D : Node, INodeKeyAssignable
    {
        [SerializeField] private string _keySpeed;
        
        protected float _speed;

        public virtual void AssignKeys(List<string> keys) { _keySpeed = keys[0]; }

        protected override void Initialize() { _speed = blackboard.GetValue<float>(_keySpeed); }

        protected override Status Tick() {
            blackboard.transform.rotation *= Quaternion.Euler(GetDesiredRotation() * Time.deltaTime);
            return Status.Running;
        }

        protected virtual Vector3 GetDesiredRotation() { return new Vector3(0, 0, _speed) ; }
    }

}
