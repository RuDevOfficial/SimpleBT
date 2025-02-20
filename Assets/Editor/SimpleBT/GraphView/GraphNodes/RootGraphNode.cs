using SimpleBT.Editor.Utils;
using UnityEditor.Experimental.GraphView;

namespace SimpleBT.Editor.GraphNodes
{
    [System.Serializable]
    public class RootGraphNode : GraphTreeNode
    {
        public RootGraphNode() { NodeName = "Root"; }
    
        public override void Draw()
        {
            base.Draw();

            this.GeneratePort(Direction.Output, Port.Capacity.Single);
        }
    }
}


