using System.Collections.Generic;
using SimpleBT.Core;
using UnityEngine;

namespace SimpleBT.NonEditor.Nodes
{
    public class Action_OverrideGameObjectPosition3D : Node, INodeKeyAssignable
    {
        [SerializeField] private string _keyGameObject;
        [SerializeField] private string _keyPosition;
        [SerializeField] private string _keyLocal;
        
        private GameObject _gameObject;
        private Vector3 _position = Vector3.zero;
        private bool _isLocal = false;
        
        public void AssignKeys(List<string> keys)
        {
            _keyGameObject = keys[0];
            _keyPosition = keys[1];
            _keyLocal = keys[2];
        }
        
        protected override void Initialize()
        {
            _position = blackboard.GetValue<Vector3>(_keyPosition);
            _isLocal = blackboard.GetValue<bool>(_keyLocal);
        }
        
        protected override Status Tick()
        {
            _gameObject = blackboard.GetComplexValue<GameObject>(_keyGameObject);
            if (!_gameObject) { return Status.Failure; }
            
            if (_isLocal) { _gameObject.transform.localPosition = _position; }
            else { _gameObject.transform.position = _position; }
            _gameObject = null;
            
            return Status.Success;
        }
    }
}
