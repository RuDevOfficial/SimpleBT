using SimpleBT.Core;

namespace SimpleBT.NonEditor.Nodes
{
    // Ticks children in order, goes to the next if it returns Failure, stops if it returns Success.
    // If no children succeeded, it will return Failure.
    
    using Composite;
    
    public class Selector : Composite
    {
        public Selector() : base() { }
        public Selector(params Node[] nodes) : base (nodes) { }
        
        protected override Status Tick()
        {
            Status childStatus = _children[_childrenIndex].OnTick();
            switch (childStatus)
            {
                case Status.Success:
                    _childrenIndex = 0;
                    return Status.Success;

                case Status.Failure:
                    _childrenIndex += 1;
                    if (_childrenIndex > _children.Count - 1) 
                    { 
                        _childrenIndex = 0; 
                        return Status.Failure;
                    }
                    else { return Status.Running; }

                default: return Status.Running;
            }
        }
    }
}
