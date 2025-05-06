using UnityEngine;

namespace SimpleBT.NoneEditor.Nodes
{
    using SimpleBT.Core;
    using NonEditor;
    
    public abstract class Decorator : Node, INodeMother
    {
        public Node Child;

        public override Status OnTick() { return Child == null ? Status.Success : Tick(); }
        
        public override void RegisterBlackboard(SBTBlackboard sbtBlackboard)
        {
            base.RegisterBlackboard(sbtBlackboard);
            Child.RegisterBlackboard(sbtBlackboard);
        }

        public void AddChild(Node child) { Child = child; }
        public override void OnDrawGizmos() {
            Child?.OnDrawGizmos();
        }
    }
}
