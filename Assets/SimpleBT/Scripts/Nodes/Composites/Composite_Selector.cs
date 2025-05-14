using SimpleBT.Core;

namespace SimpleBT.NonEditor.Nodes
{
    public class Composite_Selector : Composite
    {
        protected override Status ExecuteFlow()
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
