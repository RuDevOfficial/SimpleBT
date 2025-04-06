using SimpleBT.Core;
using UnityEngine;

namespace SimpleBT.NonEditor.Nodes
{
    public class Sequence : Composite
    {
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
