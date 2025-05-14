using System.Collections.Generic;
using SimpleBT.Core;
using UnityEngine;

namespace SimpleBT.NonEditor.Nodes
{
    public class Action_OverrideTag : Node, INodeKeyAssignable
    {
        [SerializeField] private string _keyTarget;
        [SerializeField] private string _keyTag;

        private GameObject _target;
        
        public void AssignKeys(List<string> keys)
        {
            _keyTarget = keys[0];
            _keyTag = keys[1];
        }

        protected override void Initialize() { }

        protected override Status Tick()
        {
            _target = blackboard.GetComplexValue<GameObject>(_keyTarget);
            if (!_target) { return Status.Failure ; }
            
            _target.tag = _keyTag;
            _target = null;
            return Status.Success;
        }
    }
}
