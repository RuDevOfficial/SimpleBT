using System.Collections.Generic;
using SimpleBT.Core;
using UnityEngine;

namespace SimpleBT.NonEditor.Nodes
{
    public class Action_Wait : ExecutionNode, INodeKeyAssignable
    {
        [SerializeField] private string keyTime;
        [SerializeField] private string keyStatus;

        private float _time;
        private float _currentTime = 0.0f;
        
        private Status _status;

        public void AssignKeys(List<string> keys)
        {
            keyTime = keys[0];
            keyStatus = keys[1];
        }

        protected override void Initialize() {
            _time = blackboard.GetValue<float>(keyTime); 
            _status = blackboard.GetValue<Status>(keyStatus);
        }

        protected override Status Tick() {
            if (_currentTime < _time) {
                _currentTime += Time.deltaTime;
                return Status.Running;
            }
            else {
                _currentTime = 0.0f;
                return _status;
            }
        }

        public override void OnAbort()
        {
            _currentTime = 0.0f;
        }
    }
}