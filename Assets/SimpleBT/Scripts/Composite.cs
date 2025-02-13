using System.Collections.Generic;
using System.Linq;

namespace SimpleBT.Composite
{
    using Core;
    using NonEditor;

    // Base class for nodes capable of having children
    public abstract class Composite : Node
    {
        protected List<INode> _children = new List<INode>();
        protected int _childrenIndex = 0;

        public Composite() : base() {}
        public Composite(params INode[] nodes) { _children = nodes.ToList(); }
        
        // Checks if the composite has any children fist
        public override Status OnTick() { return _children.Count == 0 ? Status.Success : Tick(); }
        
        public override void RegisterBlackboard(SBTBlackboard sbtBlackboard)
        {
            base.RegisterBlackboard(sbtBlackboard);
            foreach(INode node in _children) { node.RegisterBlackboard(sbtBlackboard); }
        }
    }

}
