using System.Collections.Generic;
using SimpleBT.Core;
using UnityEngine;

namespace SimpleBT.NonEditor.Nodes
{
    public class Action_LookAtTargetOnce : Node, INodeKeyAssignable
    {
        [SerializeField] private string _keyTarget;
        
        private GameObject _target;
        
        public void AssignKeys(List<string> keys) { _keyTarget = keys[0]; }
        
        protected override void Initialize() { }
        
        protected override Status Tick()
        {
            _target = blackboard.GetComplexValue<GameObject>(_keyTarget);
            if (_target == null) { return Status.Failure;  }

            blackboard.gameObject.transform.LookAt(_target.transform.position);
            
            _target = null;
            return Status.Success;
        }
    }
}
