using System.Collections.Generic;
using SimpleBT.Core;
using UnityEngine;

namespace SimpleBT.NonEditor.Nodes
{
    // If the Condition does NOT require any keys to then instantiate you can remove:
    // The "INodeKeyAssignable" interface and AssignKeys method
    public class Condition_CompareBool : Condition, INodeKeyAssignable
    {
        [SerializeField] private string _keyValue;
        [SerializeField] private string _keyComparison;
        
        public void AssignKeys(List<string> keys)
        {
            _keyValue = keys[0];
            _keyComparison = keys[1];
        }

        protected override void Initialize() { }

        protected override bool Check()
        {
            bool value = blackboard.GetValue<bool>(_keyValue);
            bool comparison = bool.Parse(_keyComparison);
            return value == comparison;
        }
    }

}




