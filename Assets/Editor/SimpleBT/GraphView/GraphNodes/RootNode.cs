using SimpleBT.Editor.Utils;
using UnityEditor.Experimental.GraphView;

namespace SimpleBT.Editor.GraphNodes
{
    [System.Serializable]
    public class RootNode : GraphTreeNode
    {
        public RootNode() { NodeName = "Root"; }
    
        public override void Draw()
        {
            base.Draw();

            this.GeneratePort(Direction.Output, Port.Capacity.Single);
        }
    }
}


