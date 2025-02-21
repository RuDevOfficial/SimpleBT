using SimpleBT.Editor.Utils;
using UnityEditor.Experimental.GraphView;

namespace SimpleBT.Editor.GraphNodes
{
    public class GraphAction : GraphTreeNode
    {
        public GraphAction() { NodeName = "Action"; }

        public override void Draw()
        {
            base.Draw();
            
            this.GeneratePort(Direction.Input, Port.Capacity.Single);
        }
    }
}
