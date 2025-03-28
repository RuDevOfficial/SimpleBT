using SimpleBT.Editor.Utils;
using UnityEditor.Experimental.GraphView;

namespace SimpleBT.Editor.GraphNodes
{
    public abstract class GraphDecorator : GraphTreeNode
    {
        public GraphDecorator()
        {
            Title = "Decorator";
            ClassReference = "Decorator";
        }

        public override void GenerateInterface()
        {
            AddToClassList("Decorator");
            
            this.GeneratePort(Direction.Input, Port.Capacity.Single);
            this.GeneratePort(Direction.Output, Port.Capacity.Single);
        }
    }

}
