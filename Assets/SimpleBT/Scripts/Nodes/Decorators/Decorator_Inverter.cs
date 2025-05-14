using SimpleBT.Core;
using SimpleBT.NoneEditor.Nodes;

namespace SimpleBT.NonEditor.Nodes
{
    public class Decorator_Inverter : Decorator
    {
        protected override Status Tick()
        {
            Status childStatus = Child.OnTick();

            return childStatus switch
            {
                Status.Success => Status.Failure,
                Status.Failure => Status.Success,
                _ => Status.Running
            };
        }
    }

}
