using UnityEditor.Experimental.GraphView;

public class CompositeNode : BehaviourTreeNode
{
    public CompositeNode() { }
    
    public override void Draw()
    {
        title = "Sequence";

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

