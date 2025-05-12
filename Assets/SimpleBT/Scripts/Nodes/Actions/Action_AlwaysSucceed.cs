using SimpleBT.Core;

namespace SimpleBT.NonEditor.Nodes
{
    public class Action_AlwaysSucceed : Node
    {
        protected override void Initialize() { }
        protected override Status Tick() { return Status.Success; }
    }
}
