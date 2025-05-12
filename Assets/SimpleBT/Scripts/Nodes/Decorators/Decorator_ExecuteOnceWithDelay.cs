using System.Collections.Generic;
using SimpleBT.NoneEditor.Nodes;
using UnityEngine;

namespace SimpleBT.NonEditor.Nodes
{
    using Core;
    
    public class Decorator_ExecuteOnceWithDelay : Decorator, INodeKeyAssignable
    {
        public string _keyDelay;
        private float _currentTime;
        private float _delay;
        private bool _triggered ;
        
        protected override Status Tick()
        {
            if (!_triggered) {
                _triggered = true;
                _delay = blackboard.GetValue<float>(_keyDelay);
                _currentTime = _delay;
            }
            
            if (_currentTime < _delay) {
                _currentTime += Time.deltaTime;
                return Status.Running;
            }
            
            _currentTime = 0.0f;
            return Child.OnTick();
        }

        public void AssignKeys(List<string> keys)
        {
            _keyDelay = keys[0];
        }
    }
}
