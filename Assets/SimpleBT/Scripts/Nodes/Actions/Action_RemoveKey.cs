using System.Collections.Generic;
using SimpleBT.Core;
using UnityEngine;

namespace SimpleBT.NonEditor.Nodes
{
    public class Action_RemoveKey : ExecutionNode, INodeKeyAssignable
    {
        [SerializeField] private string _key;
        public void AssignKeys(List<string> keys) { _key = keys[0]; }
        protected override void Initialize() { }
        protected override Status Tick()
        {
            blackboard.RemoveValue(_key);
            return Status.Success;
        }
    }
}
