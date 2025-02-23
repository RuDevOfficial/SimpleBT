namespace SimpleBT.Decorator
{
    using SimpleBT.Core;
    using NonEditor;
    
    public abstract class Decorator : Node
    {
        public Node Child;

        public Decorator(Node node) { Child = node; }
        
        public override Status OnTick() { return Child == null ? Status.Success : Tick(); }
        
        public override void RegisterBlackboard(SBTBlackboard sbtBlackboard)
        {
            base.RegisterBlackboard(sbtBlackboard);
            Child.RegisterBlackboard(sbtBlackboard);
        }

        public override void AddChild(Node child)
        {
            Child = child;
        }
    }
}
