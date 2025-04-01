using System.Collections.Generic;

namespace SimpleBT.Editor.GraphNodes
{
    public class GraphAction_Stop : GraphAction
    {
        public GraphAction_Stop()
        {
            Title = "Stop Movement";
            ClassReference = "Action_Stop";
        }
        
        public override List<string> GetValues() { return null; }
        public override void ReloadValues(List<string> values) { }
    }
}