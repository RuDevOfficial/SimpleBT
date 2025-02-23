using System.Collections.Generic;
using SimpleBT.Editor.Utils;
using UnityEditor.Experimental.GraphView;

namespace SimpleBT.Editor.GraphNodes
{
    [System.Serializable]
    public class CompositeNode : GraphTreeNode
    {
        public CompositeNode()
        {
            Title = "Composite";
            ClassReference = "Composite";
        }

        public override void GenerateInterface()
        {
            this.GeneratePort(Direction.Input, Port.Capacity.Single);
            this.GeneratePort(Direction.Output, Port.Capacity.Multi);
        }

        // Both are and will always be null
        public override List<string> GetValues() { return null; }
        public override void ReloadValues(List<string> values) { }
    }
}