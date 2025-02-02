using UnityEditor.Experimental.GraphView;

[System.Serializable]
public class CompositeNode : BehaviourTreeNode
{
    public CompositeNode() { NodeName = "Composite"; }
    
    public override void Draw()
    {
        base.Draw();

        Port inputPort = this.InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single,
            typeof(bool));
        inputPort.portName = "";
        inputContainer.Add(inputPort);

        Port outputPort = this.InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Multi,
            typeof(bool));
        outputPort.portName = "";
        outputContainer.Add(outputPort);
    }
}