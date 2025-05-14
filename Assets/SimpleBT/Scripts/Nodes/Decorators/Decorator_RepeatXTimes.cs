using System.Collections.Generic;
using SimpleBT.Core;
using SimpleBT.NoneEditor.Nodes;
using UnityEngine;

namespace SimpleBT.NonEditor.Nodes
{
    public class Decorator_RepeatXTimes : Decorator, INodeKeyAssignable
    {
        [SerializeField] private string _keyTimes;
        protected int _maxTimes;
        protected int _times = 0;
        
        public void AssignKeys(List<string> keys) { _keyTimes = keys[0]; }

        protected override void Initialize() { _maxTimes = blackboard.GetValue<int>(_keyTimes); }

        protected override Status Tick()
        {
            _times += 1;
            Child.OnTick();
            if (_times < _maxTimes) return Status.Running;
            
            _times = 0;
            return Status.Success;
        }
    }

}
