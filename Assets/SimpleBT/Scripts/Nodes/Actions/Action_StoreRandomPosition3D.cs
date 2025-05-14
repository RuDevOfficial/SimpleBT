using System.Collections.Generic;
using SimpleBT.Core;
using Unity.VisualScripting;
using UnityEngine;

namespace SimpleBT.NonEditor.Nodes
{
    public class Action_StoreRandomPosition3D : Node, INodeKeyAssignable
    {
        [SerializeField] private string _keyMinDistance, _keyMaxDistance;
        [SerializeField] private string _keyRaycastHeight, _keyRaycastDistance;
        [SerializeField] private string _keyLayerMask = "Default";
        [SerializeField] private string _keyParameter;
        
        private float _minDistance, _maxDistance;
        private float _raycastHeight, _raycastDistance;
        private LayerMask _layerMask;
        
        public void AssignKeys(List<string> keys)
        {
            _keyMinDistance = keys[0]; _keyMaxDistance = keys[1];
            _keyRaycastHeight = keys[2]; _keyRaycastDistance = keys[3];
            _keyLayerMask = keys[4];
            _keyParameter = keys[5];
        }
        
        protected override void Initialize()
        {
            _minDistance = blackboard.GetValue<float>(_keyMinDistance);
            _maxDistance = blackboard.GetValue<float>(_keyMaxDistance);
            
            _raycastHeight = blackboard.GetValue<float>(_keyRaycastHeight);
            _raycastDistance = blackboard.GetValue<float>(_keyRaycastDistance);

            _layerMask = LayerMask.NameToLayer(_keyLayerMask);
        }

        protected override Status Tick()
        {
            if (string.IsNullOrEmpty(_keyParameter)) {
                Debug.LogWarning("No KEY was assigned in Action_StoreRandomPosition3D");
                return Status.Failure; 
            }

            Vector3 randomPosition = blackboard.gameObject.transform.position + new Vector3(Random.Range(_minDistance, _maxDistance), _raycastHeight, Random.Range(_minDistance, _maxDistance));
            bool hit = Physics.Raycast(new Ray(randomPosition, Vector3.down), out RaycastHit hitinfo, _raycastDistance, 1 << _layerMask);
            blackboard.AddValue(_keyParameter.ToUpper(), hitinfo.point);
            return hit ? Status.Success : Status.Failure;
        }
    }
}
