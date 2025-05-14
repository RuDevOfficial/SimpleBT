using SimpleBT.Core;
using SimpleBT.NoneEditor.Nodes;

namespace SimpleBT.NonEditor.Nodes
{
    public class Decorator_RepeatForever : Decorator
    {
        protected override Status Tick()
        {
            Child?.OnTick();
            return Status.Running;
        }
    }

}
