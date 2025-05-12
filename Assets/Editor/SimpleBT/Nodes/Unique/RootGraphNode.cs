using System.Collections.Generic;
using SimpleBT.Editor.Utils;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace SimpleBT.Editor.GraphNodes
{
    [System.Serializable]
    [UxmlElement]
    public partial class RootGraphNode : GraphTreeNode
    {
        public RootGraphNode()
        {
            Title = "Root";
            ClassReference = "Root";
        }
    
        public override void GenerateInterface() { this.GeneratePort(Direction.Output, Port.Capacity.Single); }
        public override List<string> GetValues() { return null; }
        public override void ReloadValues(List<string> values) { }
    }

}


