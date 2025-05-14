using System.Collections.Generic;
using SimpleBT.Core;
using UnityEngine;

namespace SimpleBT.NonEditor.Nodes
{
    public class Action_GetRandomChildFromParent : Node, INodeKeyAssignable
    {
        [SerializeField] private string _keyGameObjectParent;
        [SerializeField] private string _keyResult;
        
        private GameObject _parent;
        
        public void AssignKeys(List<string> keys)
        {
            _keyGameObjectParent = keys[0];
            _keyResult = keys[1];
        }

        protected override void Initialize() {
            _parent = blackboard.GetComplexValue<GameObject>(_keyGameObjectParent);
        }

        protected override Status Tick()
        {
            if (_parent == null) { return Status.Failure; }
            
            int randomNumber = Random.Range(0, _parent.transform.childCount);
            if (randomNumber == 0) { return Status.Failure; }
            
            blackboard.AddValue(_keyResult, _parent.transform.GetChild(randomNumber).gameObject);
            return Status.Success;
        }
    }
}

