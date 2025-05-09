﻿using System.Collections.Generic;
using System.Linq;
using SimpleBT.Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace SimpleBT.NonEditor.Nodes
{
    public class Condition_IsNearLedge2D : Condition, INodeKeyAssignable
    {
        [SerializeField] private string keySeparationDistance;
        [SerializeField] private string keyLength;
        [SerializeField] private string keyLayerMaskName;
        
        private LayerMask _layerMask;

        private Rigidbody2D rb2D;
        
        private float rayLength;
        private float raySeparationDistance;
        
        Vector2 leftVector;
        Vector2 rightVector;

        public void AssignKeys(List<string> keys)
        {
            keySeparationDistance = keys[0]; 
            keyLength = keys[1]; 
            keyLayerMaskName = keys[2]; 
        }

        protected override void Initialize() {
            rayLength = blackboard.GetValue<float>(keyLength);
            raySeparationDistance = blackboard.GetValue<float>(keySeparationDistance);
            _layerMask = LayerMask.NameToLayer(keyLayerMaskName);
            rb2D = blackboard.GetComponent<Rigidbody2D>();
        }

        public override bool Check()
        {
            if (!rb2D) {  Debug.LogError("Is Near Edge 2D requires Rigidbody 2D"); return false; }
            
            leftVector = (Vector2)blackboard.gameObject.transform.position + new Vector2(rb2D.linearVelocityX >= 0 ? 1 : -1, 0) * raySeparationDistance;

            List<RaycastHit2D> leftHits = Physics2D.RaycastAll(leftVector, Vector2.down, rayLength, 1 << _layerMask).ToList();
            
            // Filter out the raycasting gameObject
            for (var i = leftHits.Count - 1; i >= 0; i--)
            {
                if (leftHits[i].transform.gameObject != blackboard.gameObject) continue;
                leftHits.RemoveAt(i); break;
            }
            
            return leftHits.Count == 0;
        }
    }

}
