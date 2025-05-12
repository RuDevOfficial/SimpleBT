using System.Collections.Generic;
using SimpleBT.Core;
using UnityEngine;

namespace SimpleBT.NonEditor.Nodes
{
    // If the Condition does NOT require any keys to then instantiate you can remove:
    // The "INodeKeyAssignable" interface and AssignKeys method
    public class Condition_CanSeeTarget3D : Condition, INodeKeyAssignable
    {
        [SerializeField] private string _keyTarget;
        [SerializeField] private string _keyLayerMask;
        [SerializeField] private string _keyRadius;

        private GameObject _target;
        private LayerMask _layerMask;
        private float _radius;

        // debugging purposes
        private Vector3 _hitPosition = Vector3.zero;
        private bool _hitTarget = false;
        
        public void AssignKeys(List<string> keys)
        {
            _keyTarget = keys[0];
            _keyLayerMask = keys[1];
            _keyRadius = keys[2];
        }

        protected override void Initialize()
        {
            _target = blackboard.GetComplexValue<GameObject>(_keyTarget);
            _layerMask = int.Parse(_keyLayerMask);
            _radius = blackboard.GetValue<float>(_keyRadius);
        }

        protected override bool Check()
        {
            // Get changing values here.
            Vector3 rayDirection = (_target.transform.position - blackboard.gameObject.transform.position).normalized;
            Physics.Raycast(blackboard.gameObject.transform.position, rayDirection, out RaycastHit hit, _radius, _layerMask);

            if (!hit.collider) return false;
            
            _hitPosition = hit.point;
            _hitTarget = hit.collider.gameObject == _target;
            return _hitTarget;
        }

        public override void OnDrawGizmos()
        {
            Gizmos.color = _hitTarget ? Color.green : Color.red;
            Gizmos.DrawLine(blackboard.gameObject.transform.position, _hitPosition);
        }
    }
}


