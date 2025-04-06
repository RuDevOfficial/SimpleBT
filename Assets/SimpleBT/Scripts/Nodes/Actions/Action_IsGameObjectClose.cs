using System.Collections.Generic;
using SimpleBT.Core;
using UnityEngine;

namespace SimpleBT.NonEditor.Nodes
{
    public class Condition_IsGameObjectClose : Condition, INodeKeyAssignable
    {
        [SerializeField] private string _keyTag;
        [SerializeField] private string _keyDistance;
        [SerializeField] private string _keyBlackboardToggle;
        [SerializeField] private string _keyParameter;

        private float _distance;
        private bool _toggle;
        
        public void AssignKeys(List<string> keys)
        {
            _keyTag = keys[0];
            _keyDistance = keys[1];
            _keyBlackboardToggle = keys[2];
            _keyParameter = keys[3];
        }
        
        protected override void Initialize()
        {
            _distance = blackboard.GetValue<float>(_keyDistance);
            _toggle = blackboard.GetValue<bool>(_keyBlackboardToggle);
        }
        
        public override bool Check()
        {
            //if(_toggle) { blackboard.AddValue(_keyParameter.ToUpper(), _target); }

            return false;
        }
    }
}
