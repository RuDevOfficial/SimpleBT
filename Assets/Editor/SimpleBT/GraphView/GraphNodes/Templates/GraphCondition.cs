using SimpleBT.Editor.Utils;
using UnityEditor.Experimental.GraphView;

namespace SimpleBT.Editor.GraphNodes
{
    [System.Serializable]
    public abstract class GraphCondition : ExecutionGraphNode
    {
        public override void GenerateInterface()
        {
            AddToClassList("Condition");
            
            this.GeneratePort(Direction.Input, Port.Capacity.Single);
        }
    }
}
