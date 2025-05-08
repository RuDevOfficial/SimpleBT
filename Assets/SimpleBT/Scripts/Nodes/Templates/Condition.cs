namespace SimpleBT.NonEditor.Nodes
{
    using Core;

    public abstract class Condition : Node
    {
        protected override Status Tick() { return Check() == true ? Status.Success : Status.Failure; }
        protected abstract bool Check();
    }
}

public enum ConditionType
{
    Equal,
    NotEqual,
    Less_Than,
    Less_Or_Equal_Than,
    More_Than,
    More_Or_Equal_Than,
    IsNull,
    IsNotNull
}