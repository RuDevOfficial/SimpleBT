using System;
using System.Collections.Generic;
using SimpleBT.Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace SimpleBT.NonEditor.Nodes
{
    public class Action_Debug : ExecutionNode, INodeKeyAssignable
    {
        [SerializeField] private string _keyMessage;
        [SerializeField] private string _keyResult;

        private Status _status;
        
        public void AssignKeys(List<string> keys)
        {
            _keyMessage = keys[0];
            _keyResult = keys[1];
        }
        
        protected override void Initialize() {
            _status = blackboard.GetValue<Status>(_keyResult);
        }

        protected override Status Tick()
        {
            Debug.Log(_keyMessage);
            return _status;
        }
    }

}

