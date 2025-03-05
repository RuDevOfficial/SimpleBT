using SimpleBT.Editor.Utils;
using UnityEditor.Experimental.GraphView;

namespace SimpleBT.Editor.GraphNodes
{
    public abstract class GraphAction : GraphTreeNode
    {
        public GraphAction()
        {
            Title = "Action";
            ClassReference = "Action_Node";
        }

        public override void GenerateInterface()
        {
            this.GeneratePort(Direction.Input, Port.Capacity.Single);
        }
    }

}
