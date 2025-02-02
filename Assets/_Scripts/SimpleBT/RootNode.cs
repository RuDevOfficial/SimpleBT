using UnityEditor.Experimental.GraphView;

namespace SimpleBT
{
    [System.Serializable]
    public class RootNode : BehaviourTreeNode
    {
        public RootNode() { NodeName = "Root"; }
    
        public override void Draw()
        {
            base.Draw();

            Port outputPort = this.InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Single,
                typeof(bool));
            outputPort.portName = "";
            outputContainer.Add(outputPort);
        }
    }
}


