using SimpleBT.Core;
using SimpleBT.Decorator;

namespace SimpleBT.NonEditor.Nodes
{
    using Decorator;
    
    public class Decorator_RepeatForever : Decorator
    {
        public Decorator_RepeatForever(Node node) : base(node) { }

        protected override Status Tick()
        {
            Child.OnTick();

            return Status.Running;
        }
    }
}
