using SimpleBT.Core;
using SimpleBT.Decorator;

namespace SimpleBT.NonEditor.Nodes
{
    using Decorator;
    
    public class Decorator_RepeatForever : DecoratorNode
    {
        protected override Status Tick()
        {
            Child.OnTick();
            return Status.Running;
        }
    }
}
