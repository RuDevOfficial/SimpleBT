using System.Collections.Generic;
using SimpleBT.Core;
using SimpleBT.Decorator;

namespace SimpleBT.NonEditor.Nodes
{
    public class ConditionNodeAlwaysFail : ConditionNode
    {
        public override bool Check() { return false; }
        protected override void Initialize() { }
    }
}
