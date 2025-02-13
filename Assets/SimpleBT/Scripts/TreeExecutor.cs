using System;
using UnityEngine;

namespace SimpleBT.NonEditor.Tree
{
    using SimpleBT.Core;
    using NonEditor;
    
    public class TreeExecutor : MonoBehaviour
    {
        public BehaviourTree BT;
        private SBTBlackboard _sbtBlackboard;
        private bool _succeeded = false;

        private void Awake()
        {
            _sbtBlackboard = GetComponent<SBTBlackboard>();

            if (_sbtBlackboard == null) { Debug.Log("Couldn't get the Blackboard, is it attached to the GameObject?"); }
        }

        private void Start()
        {
            BT = (BehaviourTree)ScriptableObject.CreateInstance(BT.GetType());
            BT.RegisterBlackboard(_sbtBlackboard);
        }

        private void Update()
        {
            if (_succeeded != false) return;
            
            Status state = BT.OnTick();

            if (state != Status.Running)
            {
                Debug.Log("I finished the execution");
                _succeeded = true;
            }
        }
    }

}
