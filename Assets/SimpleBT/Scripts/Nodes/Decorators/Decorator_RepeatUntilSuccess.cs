using SimpleBT.Core;
using SimpleBT.NoneEditor.Nodes;

namespace SimpleBT.NonEditor.Nodes
{
    public class Decorator_RepeatUntilSuccess : Decorator
    {
        protected override Status Tick()
        {
            Status childStatus = Child.OnTick();

            if (childStatus != Status.Success) { return Status.Running; }
            return Status.Success;
        }
    }
}
