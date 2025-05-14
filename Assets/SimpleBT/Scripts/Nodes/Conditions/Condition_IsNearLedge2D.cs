using System.Collections.Generic;
using System.Linq;
using SimpleBT.Core;
using UnityEngine;

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
        private Vector2 _leftVector;
        private Vector2 _rightVector;

        private List<RaycastHit2D> _hits = new List<RaycastHit2D>();

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

        protected override bool Check()
        {
            if (!rb2D) {  Debug.LogError("Is Near Edge 2D requires Rigidbody 2D"); return false; }
            
            _leftVector = (Vector2)blackboard.gameObject.transform.position + new Vector2(rb2D.linearVelocityX >= 0 ? 1 : -1, 0) * raySeparationDistance;

            _hits = Physics2D.RaycastAll(_leftVector, Vector2.down, rayLength, 1 << _layerMask).ToList();
            
            // Filter out the raycasting gameObject
            for (var i = _hits.Count - 1; i >= 0; i--)
            {
                if (_hits[i].transform.gameObject != blackboard.gameObject) continue;
                _hits.RemoveAt(i); break;
            }
            
            return _hits.Count == 0;
        }

        public override void OnDrawGizmos()
        {
            Gizmos.color = _hits.Count > 0 ? Color.green : Color.red;
            Gizmos.DrawLine(_leftVector, _leftVector + Vector2.down * rayLength);
        }
    }

}
