namespace SimpleBT.NonEditor.Nodes
{
    using Core;

    public abstract class ConditionNode : ExecutionNode
    {
        protected override Status Tick() { return Check() == true ? Status.Success : Status.Failure; }
        public abstract bool Check();
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