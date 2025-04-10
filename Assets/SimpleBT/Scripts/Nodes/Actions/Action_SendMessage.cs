using System.Collections.Generic;
using SimpleBT.Core;
using UnityEngine;

namespace SimpleBT.NonEditor.Nodes
{
    public class Action_SendMessage : ExecutionNode, INodeKeyAssignable
    {
        [SerializeField] private string _keyMethodName;
        
        public void AssignKeys(List<string> keys) { _keyMethodName = keys[0]; }
        
        protected override void Initialize() { }

        protected override Status Tick()
        {
            blackboard.gameObject.SendMessage(_keyMethodName);
            return Status.Success;
        }
    }
}
