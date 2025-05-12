using System.Collections.Generic;
using SimpleBT.Core;
using UnityEngine;

namespace SimpleBT.NonEditor.Nodes
{
    public class Action_ParentObjectToSelf : Node, INodeKeyAssignable
    {
        [SerializeField] protected string _gameObjectKey;
        
        protected GameObject _gameObject;
        
        public void AssignKeys(List<string> keys) { _gameObjectKey = keys[0]; }
        protected override void Initialize() {  }
        
        protected override Status Tick()
        {
            _gameObject = blackboard.GetComplexValue<GameObject>(_gameObjectKey);
            
            if(!_gameObject) { return Status.Failure; }
            
            _gameObject.transform.SetParent(blackboard.gameObject.transform);
            _gameObject = null;
            return Status.Success;
        }
    }

}
