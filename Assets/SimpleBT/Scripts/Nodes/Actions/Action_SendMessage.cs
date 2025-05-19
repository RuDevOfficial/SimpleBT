using System.Collections.Generic;
using SimpleBT.Core;
using UnityEngine;

namespace SimpleBT.NonEditor.Nodes
{
    public class Action_SendMessage : Node, INodeKeyAssignable
    {
        [SerializeField] protected string _keyMethodName;
        
        public virtual void AssignKeys(List<string> keys) { _keyMethodName = keys[0]; }
        
        protected override void Initialize() { }

        protected override Status Tick()
        {
            _blackboard.gameObject.SendMessage(_keyMethodName);
            return Status.Success;
        }
    }

}
