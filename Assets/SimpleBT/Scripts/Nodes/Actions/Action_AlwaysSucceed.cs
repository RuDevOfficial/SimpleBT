using System;
using SimpleBT.Core;

namespace SimpleBT.NonEditor.Nodes
{
    public class Action_AlwaysSucceed : ExecutionNode
    {
        protected override void Initialize() { }
        protected override Status Tick() { return Status.Success; }
    }
}
