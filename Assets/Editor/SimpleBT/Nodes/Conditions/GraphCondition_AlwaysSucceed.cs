using System.Collections.Generic;

namespace SimpleBT.Editor.GraphNodes
{
    public class GraphCondition_AlwaysSucceed : GraphCondition
    {
        public GraphCondition_AlwaysSucceed()
        {
            Title = "Always Succeed";
            ClassReference = "Condition_AlwaysSucceed";
        }
        
        public override List<string> GetValues() { return null; }
        public override void ReloadValues(List<string> values) { }
    }
}
