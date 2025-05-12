using System.Collections.Generic;
using SimpleBT.Editor.Utils;
using UnityEditor.Experimental.GraphView;

namespace SimpleBT.Editor.GraphNodes
{
    [System.Serializable]
    public class GraphComposite : GraphTreeNode
    {
        public GraphComposite()
        {
            Title = "Composite";
            ClassReference = "Composite";
        }

        public override void GenerateInterface()
        {
            AddToClassList("Composite");
            
            this.GeneratePort(Direction.Input, Port.Capacity.Single);
            this.GeneratePort(Direction.Output, Port.Capacity.Multi);
        }
        
        public override List<string> GetValues() { return null; }
        public override void ReloadValues(List<string> values) { }
    }
}