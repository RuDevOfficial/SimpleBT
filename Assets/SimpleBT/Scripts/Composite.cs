using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SimpleBT.Composite
{
    using Core;
    using NonEditor;

    // Base class for nodes capable of having children
    public abstract class Composite : Node
    {
        [SerializeReference] protected List<Node> _children = new List<Node>();
        protected int _childrenIndex = 0;

        public Composite() : base() {}
        public Composite(params Node[] nodes) { _children = nodes.ToList(); }
        
        // Checks if the composite has any children fist
        public override Status OnTick() { return _children.Count == 0 ? Status.Success : Tick(); }
        
        public override void RegisterBlackboard(SBTBlackboard sbtBlackboard)
        {
            base.RegisterBlackboard(sbtBlackboard);
            foreach(Node node in _children) { node.RegisterBlackboard(sbtBlackboard); }
        }

        public override void AddChild(Node child)
        {
            _children.Add(child);
        }
    }

}
