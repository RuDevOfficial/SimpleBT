using UnityEngine;
using UnityEngine.Assertions;

namespace SimpleBT.NonEditor.Tree
{
    using SimpleBT.Core;
    using NonEditor;
    
    public class TreeExecutor : MonoBehaviour
    {
        public Node BT;
        private SBTBlackboard _sbtBlackboard;
        private bool _finished = false;

        private void Awake()
        {
            _sbtBlackboard = GetComponent<SBTBlackboard>();
            Assert.IsNotNull(_sbtBlackboard, "Blackboard not found in GameObject" + gameObject.name);
        }

        private void Start() { BT.RegisterBlackboard(_sbtBlackboard); }

        private void Update()
        {
            if (_finished != false) return;
            
            Status state = BT.OnTick();
            if (state != Status.Running)
            {
                Debug.Log("I finished the execution with... " + state);
                _finished = true;
            }
        }
    }

}



