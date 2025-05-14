using System.Collections.Generic;

namespace SimpleBT.Editor.GraphNodes
{
    public class GraphCondition_AlwaysFail : GraphCondition
    {
        public GraphCondition_AlwaysFail()
        {
            Title = "Always Fail";
            ClassReference = "Condition_AlwaysFail";
        }

        public override List<string> GetValues() { return null; }
        public override void ReloadValues(List<string> values) { }
    }

}
