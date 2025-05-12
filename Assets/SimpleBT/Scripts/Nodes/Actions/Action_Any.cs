using System;
using System.Collections.Generic;
using SimpleBT.Core;
using UnityEngine;

namespace SimpleBT.NonEditor.Nodes
{
    // This class itself should never be used
    public class Action_Any : Node, INodeKeyAssignable
    {
        private Type _actionType;
        private Node _actionNode;
        
        public void AssignKeys(List<string> keys) { }
        protected override void Initialize() { _actionNode = ScriptableObject.CreateInstance(_actionType) as Node; }
        protected override Status Tick() { return _actionNode.OnTick(); }
    }
}

