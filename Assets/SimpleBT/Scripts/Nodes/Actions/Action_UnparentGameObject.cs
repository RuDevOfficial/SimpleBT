using SimpleBT.Core;
using UnityEngine;

namespace SimpleBT.NonEditor.Nodes
{
    public class Action_UnparentGameObject : Action_ParentObjectToSelf
    {
        protected override Status Tick()
        {
            _gameObject = blackboard.GetComplexValue<GameObject>(_gameObjectKey);
            if(!_gameObject) { return Status.Failure; }
            
            _gameObject.transform.SetParent(null);
            _gameObject = null;
            
            return Status.Success;
        }
    }
}
