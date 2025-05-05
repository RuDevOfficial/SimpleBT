using System.Collections.Generic;
using SimpleBT.NonEditor.Nodes;
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

        public override bool Check()
        {
            // Get changing values here.
            Vector3 rayDirection = (_target.transform.position - blackboard.gameObject.transform.position).normalized;
            Physics.Raycast(blackboard.gameObject.transform.position, rayDirection, out RaycastHit hit, _radius, _layerMask);

            if (hit.collider)
            {
                Debug.Log(hit.collider.gameObject);
            }
    
            // Conditions gets ticked only once and must return true or false (success or failure)
            return false;
        }
    }



}


