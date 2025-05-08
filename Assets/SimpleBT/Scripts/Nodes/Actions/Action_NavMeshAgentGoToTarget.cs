using System.Collections.Generic;
using SimpleBT.Core;
using UnityEngine;
using UnityEngine.AI;

namespace SimpleBT.NonEditor.Nodes
{
    public class Action_NavMeshAgentGoToTarget : Node, INodeKeyAssignable
    {
        [SerializeField] private string _keyTarget;

        private GameObject _target;
        private NavMeshAgent _agent;
        
        public void AssignKeys(List<string> keys)
        {
            _keyTarget = keys[0];
        }
        
        protected override void Initialize()
        {
            _agent = blackboard.gameObject.GetComponent<NavMeshAgent>();
        }
        
        protected override Status Tick()
        {
            if (!_target) {
                _target = blackboard.GetComplexValue<GameObject>(_keyTarget);
                _agent.destination = _target.transform.position;
            }
            
            if (_agent.pathPending) return Status.Running;
            if (!(_agent.remainingDistance <= _agent.stoppingDistance)) return Status.Running;

            if (!_agent.hasPath || _agent.velocity.sqrMagnitude == 0f) {
                _target = null;
                return Status.Success;
            }

            return Status.Running;
        }
    }

}
