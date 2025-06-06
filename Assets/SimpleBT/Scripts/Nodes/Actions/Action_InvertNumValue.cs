﻿using System.Collections.Generic;
using SimpleBT.Core;
using UnityEngine;

namespace SimpleBT.NonEditor.Nodes
{
    public class Action_InvertNumValue : Node, INodeKeyAssignable
    {
        [SerializeField] private string keyNumValue;
        
        public void AssignKeys(List<string> keys) { keyNumValue = keys[0]; }
        protected override void Initialize() { }
        protected override Status Tick() {
            keyNumValue = keyNumValue.ToUpper();
            
            if (!_blackboard.ContainsKey(keyNumValue)) return Status.Failure;
            float newValue = _blackboard.GetValue<float>(keyNumValue) * -1;
            _blackboard.AddValue(keyNumValue, newValue);
            return Status.Success;
        }
    }
}
