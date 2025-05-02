using System;
using System.Collections.Generic;
using SimpleBT.Core;
using UnityEngine;

namespace SimpleBT.NonEditor.Nodes
{
    // If the Action does NOT require any keys to then instantiate you can remove:
    // The "INodeKeyAssignable" interface and AssignKeys method
    public class Action_Any : ExecutionNode, INodeKeyAssignable
    {
        private Type _actionType;
        private ExecutionNode _actionNode;
        
        public void AssignKeys(List<string> keys)
        {
            // If the node requires keys assign them here like
            // _keyTarget = keys[0]
        }

        protected override void Initialize()
        {
            // Initialize unchanging values here. Only gets triggered once in runtime.
            // To understand when to use blackboard.GetValue<type>() or blackboard.GetComplexValue<type>() check the github repo

            _actionNode = ScriptableObject.CreateInstance(_actionType) as ExecutionNode;
        }

        protected override Status Tick()
        {
            return _actionNode.OnTick();
        }
    }
}

