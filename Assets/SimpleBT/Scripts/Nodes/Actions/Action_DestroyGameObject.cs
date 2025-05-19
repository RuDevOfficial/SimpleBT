using System.Collections.Generic;
using SimpleBT.Core;
using UnityEngine;

namespace SimpleBT.NonEditor.Nodes
{
    public class Action_DestroyGameObject : Node, INodeKeyAssignable
    {
        [SerializeField] private string _keyTarget;
        private GameObject _target;
        
        public void AssignKeys(List<string> keys) { _keyTarget = keys[0]; }
        
        protected override void Initialize() {
            _target = _blackboard.GetComplexValue<GameObject>(_keyTarget);
        }
        
        protected override Status Tick()
        {
            if (!_target) { return Status.Failure; }
            
            Destroy(_target);
            _blackboard.RemoveValue(_keyTarget);
            return Status.Success;
        }
    }
}
