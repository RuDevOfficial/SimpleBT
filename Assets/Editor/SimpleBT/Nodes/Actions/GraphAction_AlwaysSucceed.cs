using System.Collections.Generic;

namespace SimpleBT.Editor.GraphNodes
{
    public class GraphAction_AlwaysSucceed : GraphAction
    {
        public GraphAction_AlwaysSucceed()
        {
            Title = "Always Succeed";
            ClassReference = "Action_AlwaysSucceed";
        }

        public override List<string> GetValues() { return null; }
        public override void ReloadValues(List<string> values) { }
    }

}
