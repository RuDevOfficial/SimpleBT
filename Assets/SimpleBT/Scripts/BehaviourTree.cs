using SimpleBT.Core;
using UnityEngine;

namespace SimpleBT.NonEditor.Tree
{
    public abstract class BehaviourTree : ScriptableObject, INode
    {
        protected INode _root;
        
        public Status OnTick() { return _root.OnTick(); }
        private void Awake() { Build(); }

        public void RegisterBlackboard(SBTBlackboard sbtBlackboard) { _root.RegisterBlackboard(sbtBlackboard); }

        protected abstract void Build();
    }
}
