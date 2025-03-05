using System;
using System.Collections.Generic;
using SimpleBT.Core;
using UnityEngine;

namespace SimpleBT.NonEditor.Nodes
{
    public class Action_Wait : ExecutionNode
    {
        [SerializeField] private string keyTime;

        private float _time;
        private float _currentTime = 0.0f;

        public override void AssignKeys(List<string> keys)
        {
            keyTime = keys[0];
        }

        protected override void Initialize()
        {
            _time = blackboard.GetValue<float>(keyTime);
        }

        protected override Status Tick()
        {
            if (_currentTime < _time)
            {
                _currentTime += Time.deltaTime;
                return Status.Running;
            }
            else
            {
                _currentTime = 0.0f;
                return Status.Success;
            }
        }
    }

}