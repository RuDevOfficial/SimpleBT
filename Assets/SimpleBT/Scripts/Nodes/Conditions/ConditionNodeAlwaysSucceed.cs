using System.Collections.Generic;
using SimpleBT.Core;
using SimpleBT.Decorator;

namespace SimpleBT.NonEditor.Nodes
{
    public class ConditionNodeAlwaysSucceed : ConditionNode
    {
        public override bool Check() { return true; }
        protected override void Initialize() { }
    }
}
