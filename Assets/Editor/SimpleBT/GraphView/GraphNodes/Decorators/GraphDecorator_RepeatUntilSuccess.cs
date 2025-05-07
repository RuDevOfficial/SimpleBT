using System.Collections.Generic;

namespace SimpleBT.Editor.GraphNodes
{
    public class GraphDecorator_RepeatUntilSuccess : GraphDecorator
    {
        public GraphDecorator_RepeatUntilSuccess()
        {
            Title = "Repeat Until Success";
            ClassReference = "Decorator_RepeatUntilSuccess";
        }
        
        public override List<string> GetValues() { return null; }
        public override void ReloadValues(List<string> values) { }
    }
}
