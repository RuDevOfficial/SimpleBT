using SimpleBT.Editor.Utils;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace SimpleBT.Editor.GraphNodes
{
    [System.Serializable]
    public abstract class ExecutionGraphNode : GraphTreeNode
    {
        public ExecutionGraphNode()
        {
            Title = "ExecutionNode";
            ClassReference = "ExecutionNode";
        }
        
        public override void GenerateInterface()
        {
            this.GeneratePort(Direction.Input, Port.Capacity.Single);
        }
    }
}
