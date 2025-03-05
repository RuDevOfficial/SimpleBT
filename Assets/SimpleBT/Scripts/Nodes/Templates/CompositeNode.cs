using System.Collections.Generic;
using SimpleBT.NonEditor;
using UnityEngine;

namespace SimpleBT.Core
{
    public abstract class CompositeNode : Node, INodeMother
    {
        [SerializeReference] protected List<Node> _children = new List<Node>();
        protected int _childrenIndex = 0;

        // Checks if the composite has any children fist
        public override Status OnTick() { return _children.Count == 0 ? Status.Success : Tick(); }
        
        public override void RegisterBlackboard(SBTBlackboard sbtBlackboard)
        {
            base.RegisterBlackboard(sbtBlackboard);
            foreach(Node node in _children) { node.RegisterBlackboard(sbtBlackboard); }
        }

        public virtual void AddChild(Node child) { _children.Add(child); }
    }
}
