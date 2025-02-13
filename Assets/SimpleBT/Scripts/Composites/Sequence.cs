using SimpleBT.Core;

namespace SimpleBT.Composite.Prebuilt
{
    // Ticks children in order, goes to the next if it returns Success, fails if the currently ticked
    // children returns failure
    public class Sequence : Composite
    {
        public Sequence() : base() { }
        public Sequence(params INode[] nodes) : base(nodes) { }

        protected override Status Tick()
        {
            Status childStatus = _children[_childrenIndex].OnTick();
            switch (childStatus)
            {
                case Status.Success:
                    _childrenIndex += 1;

                    if (_childrenIndex > _children.Count - 1)
                    {
                        _childrenIndex = 0;
                        return Status.Success;
                    }
                    else { return Status.Running; }

                case Status.Failure:
                    _childrenIndex = 0;
                    return Status.Failure;

                default: return Status.Running;
            }
        }
    }
}
