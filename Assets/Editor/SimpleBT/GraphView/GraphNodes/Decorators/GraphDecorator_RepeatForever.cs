using System.Collections.Generic;

namespace SimpleBT.Editor.GraphNodes
{
    public class GraphDecorator_RepeatForever : GraphDecorator
    {
        public GraphDecorator_RepeatForever()
        {
            Title = "Repeat Forever";
            ClassReference = "Decorator_AlwaysSucceed";
        }
        
        public override List<string> GetValues() { return null; }
        public override void ReloadValues(List<string> values) { }
    }

}
