namespace SimpleBT.Decorator
{
    using SimpleBT.Core;
    using NonEditor;
    
    public abstract class Decorator : Node
    {
        protected INode _child;

        public Decorator(INode node) { _child = node; }
        
        public override Status OnTick() { return _child == null ? Status.Success : Tick(); }
        
        public override void RegisterBlackboard(SBTBlackboard sbtBlackboard)
        {
            base.RegisterBlackboard(sbtBlackboard);
            _child.RegisterBlackboard(sbtBlackboard);
        }
    }
}
