using UnityEngine;
using UnityEngine.Assertions;

namespace SimpleBT.NonEditor.Tree
{
    using Core;
    using NonEditor;
    
    public class TreeExecutor : MonoBehaviour
    {
        public Node BT;
        private SBTBlackboard _sbtBlackboard;

        private bool _executing; // For debugging purposes OnDrawGizmos
        private bool _finished;

        private void Awake()
        {
            _sbtBlackboard = GetComponent<SBTBlackboard>();
            Assert.IsNotNull(_sbtBlackboard, "Blackboard not found in GameObject" + gameObject.name);
        }

        private void Start()
        {
            BT.RegisterBlackboard(_sbtBlackboard);
            _executing = true;
        }

        private void Update()
        {
            if (_finished == true) return;
            
            Status state = BT.OnTick();
            if (state == Status.Running) return;
            
            Debug.Log("I finished the execution with... " + state);
            _finished = true;
        }

        private void OnDrawGizmos()
        {
            if (_executing == false) { return; }
            
            BT.OnDrawGizmos();
        }
    }

}



