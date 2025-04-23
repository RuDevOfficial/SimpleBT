using System.Collections.Generic;
using SimpleBT.Core;
using UnityEngine;

namespace SimpleBT.NonEditor.Nodes
{
    public class Condition_IsGameObjectClose2D : Condition, INodeKeyAssignable
    {
        [SerializeField] protected string _keyTag;
        [SerializeField] private string _keyRadius;
        [SerializeField] private string _keyBlackboardToggle;
        [SerializeField] protected string _keyParameter;

        protected float _radius;
        protected bool _storeValue;
        
        public void AssignKeys(List<string> keys)
        {
            _keyTag = keys[0];
            _keyRadius = keys[1];
            _keyBlackboardToggle = keys[2];
            _keyParameter = keys[3];
        }
        
        protected override void Initialize()
        {
            _radius = blackboard.GetValue<float>(_keyRadius);
            _storeValue = blackboard.GetValue<bool>(_keyBlackboardToggle);
        }
        
        public override bool Check()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(blackboard.gameObject.transform.position, _radius);
            foreach(Collider2D collider in colliders) {
                if (!collider.gameObject.CompareTag(_keyTag)) continue;

                if (_storeValue) { blackboard.AddValue(_keyParameter.ToUpper(), collider.gameObject); }
                return true;
            }

            return false;
        }
    }
}
