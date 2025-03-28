using System.Collections.Generic;
using SimpleBT.NonEditor;
using SimpleBT.NonEditor.Nodes;
using UnityEngine;

namespace SimpleBT.Core
{
    public abstract class Composite : Node, INodeMother
    {
        [SerializeReference] protected List<Node> _children = new List<Node>();
        protected int _childrenIndex = 0;

        // Checks if the composite has any children fist
        public override Status OnTick() { return _children.Count == 0 ? Status.Success : Tick(); }
        public override void OnAbort() { foreach(var node in _children) { node.OnAbort();} }
        
        public override void RegisterBlackboard(SBTBlackboard sbtBlackboard)
        {
            base.RegisterBlackboard(sbtBlackboard);
            foreach(Node node in _children) { node.RegisterBlackboard(sbtBlackboard); }
        }
        
        public Condition GetFirstConditional() {
            for (int i = 0; i < _children.Count; i++) {
                if (_children[i] is Condition condition) {
                    return condition;
                }
            }
            
            Debug.LogWarning("This composite doesn't have an initial conditional");
            return null;
        }

        public virtual void AddChild(Node child) { _children.Add(child); }
    }
}
