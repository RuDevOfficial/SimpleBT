using SimpleBT.Editor.Utils;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace SimpleBT.Editor.GraphNodes
{
    [System.Serializable]
    public class ExecutionGraphNode : GraphTreeNode
    {
        public ExecutionGraphNode() { NodeName = "ExecutionNode"; }
        
        public override void Draw()
        {
            base.Draw();
            
            this.GeneratePort(Direction.Input, Port.Capacity.Single);
        }
    }
}
