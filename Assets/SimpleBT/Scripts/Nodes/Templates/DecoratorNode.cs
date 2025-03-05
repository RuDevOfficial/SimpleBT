namespace SimpleBT.Decorator
{
    using SimpleBT.Core;
    using NonEditor;
    
    public abstract class DecoratorNode : Node, INodeMother
    {
        public Node Child;

        public override Status OnTick() { return Child == null ? Status.Success : Tick(); }
        
        public override void RegisterBlackboard(SBTBlackboard sbtBlackboard)
        {
            base.RegisterBlackboard(sbtBlackboard);
            Child.RegisterBlackboard(sbtBlackboard);
        }

        public void AddChild(Node child) { Child = child; }
    }
}
