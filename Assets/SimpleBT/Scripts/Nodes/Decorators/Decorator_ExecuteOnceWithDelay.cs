using System.Collections.Generic;
using System.Globalization;
using SimpleBT.Core;
using SimpleBT.Decorator;
using UnityEngine;

namespace SimpleBT.NonEditor.Nodes
{
    public class Decorator_ExecuteOnceWithDelay : DecoratorNode, INodeKeyAssignable
    {
        public string keyDelay;
        
        private float _currentTime = 0.0f;
        private float Delay = 0.0f;

        private bool triggered = false;
        
        protected override Status Tick()
        {
            if (!triggered) {
                triggered = true;
                Delay = blackboard.GetValue<float>(keyDelay);
                _currentTime = Delay;
            }
            
            if (_currentTime < Delay) {
                _currentTime += Time.deltaTime;
                return Status.Running;
            }
            
            _currentTime = 0.0f;
            return Child.OnTick();
        }

        public void AssignKeys(List<string> keys)
        {
            keyDelay = keys[0];
        }
    }
}
