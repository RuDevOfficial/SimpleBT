using System.Collections.Generic;
using SimpleBT.Core;
using UnityEngine;

namespace SimpleBT.NonEditor.Nodes
{
    public class Action_InvertNumValue : ExecutionNode, INodeKeyAssignable
    {
        [SerializeField] private string keyNumValue;
        
        public void AssignKeys(List<string> keys) { keyNumValue = keys[0]; }
        protected override void Initialize() { }
        protected override Status Tick() {
            keyNumValue = keyNumValue.ToUpper();
            
            if (!blackboard.ContainsKey(keyNumValue)) return Status.Failure;
            float newValue = blackboard.GetValue<float>(keyNumValue) * -1;
            blackboard.AddValue(keyNumValue, newValue);
            return Status.Success;
        }
    }
}
