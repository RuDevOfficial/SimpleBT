using SimpleBT.Core;
using SimpleBT.Decorator;

namespace SimpleBT.NonEditor.Nodes
{
    using Decorator;
    
    public class RepeatForever : Decorator
    {
        public RepeatForever(Node node) : base(node) { }

        protected override Status Tick()
        {
            _child.OnTick();

            return Status.Running;
        }
    }
}
