using SimpleBT.Editor.Utils;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace SimpleBT.Editor.GraphNodes
{
    [System.Serializable]
    public class ExecutionNode : GraphTreeNode
    {
        public ExecutionNode() { NodeName = "ExecutionNode"; }
        
        public override void Draw()
        {
            base.Draw();
            
            this.GeneratePort(Direction.Input, Port.Capacity.Single);
        }
    }

    public class DebugActionNode : ExecutionNode
    {
        public string Message;
        public DebugActionNode() { NodeName = "DebugActionNode"; }

        public override void Draw()
        {
            base.Draw();
        }
    }
}
