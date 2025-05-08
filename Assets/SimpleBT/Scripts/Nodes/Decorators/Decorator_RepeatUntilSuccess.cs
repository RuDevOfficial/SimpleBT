using SimpleBT.Core;
using SimpleBT.NoneEditor.Nodes;

namespace SimpleBT.NonEditor.Nodes
{
    public class Decorator_RepeatUntilSuccess : Decorator
    {
        protected override Status Tick()
        {
            Status childStatus = Child.OnTick();
            return childStatus != Status.Success ? Status.Running : Status.Success;
        }
    }

}
