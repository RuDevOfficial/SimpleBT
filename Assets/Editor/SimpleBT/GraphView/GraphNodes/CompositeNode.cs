using SimpleBT.Editor.Utils;
using UnityEditor.Experimental.GraphView;

namespace SimpleBT.Editor.GraphNodes
{
    [System.Serializable]
    public class CompositeNode : GraphTreeNode
    {
        public CompositeNode() { NodeName = "Composite"; }

        public override void Draw()
        {
            base.Draw();

            this.GeneratePort(Direction.Input, Port.Capacity.Single);
            this.GeneratePort(Direction.Output, Port.Capacity.Single);
        }
    }
}