using System;
using System.Collections.Generic;
using SimpleBT.Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace SimpleBT.NonEditor.Nodes
{
    public class Action_Debug : Action_Node
    {
        [SerializeField] private string KeyMessage;
        [SerializeField] private string KeyResult;

        private Status _result;

        public override void AssignKeys(List<string> keys)
        {
            KeyMessage = keys[0];
            KeyResult = keys[1];
        }
        
        protected override void Initialize()
        {
            KeyMessage = blackboard.GetValue<string>(KeyMessage);
            _result = blackboard.GetValue<Status>(KeyResult);
        }

        protected override Status Tick()
        {
            Debug.Log(KeyMessage);
            Status status = (Status)Enum.Parse(typeof(Status), KeyResult);
            return status;
        }
    }

}

