using System;
using System.Collections.Generic;
using SimpleBT.Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace SimpleBT.NonEditor.Nodes
{
    public class Action_Debug : ExecutionNode, INodeKeyAssignable
    {
        [SerializeField] private string KeyMessage;
        [SerializeField] private string KeyResult;

        private Status status;
        
        public void AssignKeys(List<string> keys)
        {
            KeyMessage = keys[0];
            KeyResult = keys[1];
        }
        
        protected override void Initialize() {
            status = blackboard.GetValue<Status>(KeyResult);
        }

        protected override Status Tick()
        {
            Debug.Log(KeyMessage);
            return status;
        }
    }

}

