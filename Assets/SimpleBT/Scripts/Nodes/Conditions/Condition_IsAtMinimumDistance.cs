using System.Collections.Generic;
using SimpleBT.Core;
using UnityEngine;

namespace SimpleBT.NonEditor.Nodes
{
    public class Condition_IsAtMinimumDistance : Condition, INodeKeyAssignable
    {
        [SerializeField] private string keyTarget;
        [SerializeField] private string keyDistance;

        private GameObject target;
        private float distance;
        
        public void AssignKeys(List<string> keys)
        {
            keyTarget = keys[0];
            keyDistance = keys[1];
        }

        protected override void Initialize()
        {
            target = blackboard.GetComplexValue<GameObject>(keyTarget);
            distance = blackboard.GetValue<float>(keyDistance);
        }

        public override bool Check()
        {
            if (!target) { return false; }
            
            return (target.transform.position - blackboard.transform.position).magnitude <= distance;
        }
    }
}
