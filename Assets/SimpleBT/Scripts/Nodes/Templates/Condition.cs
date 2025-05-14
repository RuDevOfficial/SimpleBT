namespace SimpleBT.NonEditor.Nodes
{
    using Core;

    public abstract class Condition : Node
    {
        protected override Status Tick() { return Check() == true ? Status.Success : Status.Failure; }
        protected abstract bool Check();
    }
}
