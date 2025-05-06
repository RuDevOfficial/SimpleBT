using UnityEngine;

namespace SimpleBT.NonEditor.Nodes
{
    public class Condition_IsGameObjectClose3D : Condition_IsGameObjectClose2D
    {
        public override bool Check()
        {
            Collider[] colliders = Physics.OverlapSphere(blackboard.gameObject.transform.position, _radius);
            foreach(Collider collider in colliders) {
                if (!collider.gameObject.CompareTag(_keyTag)) continue;
                if (_storeValue) { blackboard.AddValue(_keyParameter, collider.gameObject); }
                return true;
            }

            return false;
        }

        public override void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(blackboard.gameObject.transform.position, _radius);
        }
    }
}
