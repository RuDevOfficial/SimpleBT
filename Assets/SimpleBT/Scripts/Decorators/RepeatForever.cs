using SimpleBT.Core;
using SimpleBT.Decorator;

public class RepeatForever : Decorator
{
    public RepeatForever(INode node) : base(node) { }

    protected override Status Tick()
    {
        _child.OnTick();

        return Status.Running;
    }
}