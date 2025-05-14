using SimpleBT.Core;
using SimpleBT.NoneEditor.Nodes;

namespace SimpleBT.NonEditor.Nodes
{
    public class Decorator_RepeatUntilFailure : Decorator
    {
        protected override Status Tick()
        {
            Status childStatus = Child.OnTick();
            return childStatus != Status.Failure ? Status.Running : Status.Failure;
        }
    }
}
