using System.Collections.Generic;
using SimpleBT.Core;
using UnityEngine;

namespace SimpleBT.NonEditor.Nodes
{
    public class Action_Debug : Node, INodeKeyAssignable
    {
        [SerializeField] private string _keyMessage;
        [SerializeField] private string _keyResult;

        private string _message;
        private Status _status;
        
        public void AssignKeys(List<string> keys)
        {
            _keyMessage = keys[0];
            _keyResult = keys[1];
        }
        
        protected override void Initialize() {
            _status = _blackboard.GetValue<Status>(_keyResult);
            _message = _blackboard.GetValue<string>(_keyMessage);
        }

        protected override Status Tick()
        {
            Debug.Log(_message);
            return _status;
        }
    }

}

