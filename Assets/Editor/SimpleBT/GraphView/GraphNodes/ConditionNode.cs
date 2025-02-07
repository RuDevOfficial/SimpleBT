namespace SimpleBT.Editor.GraphNodes
{
    public class ConditionNode : ExecutionNode
    {
        public ConditionBox ConditionBox;
        
        public ConditionNode() { NodeName = "ConditionNode"; }
        
        public override void Draw()
        {
            base.Draw();

            ConditionBox = new ConditionBox();
            extensionContainer.Add(ConditionBox);

            RefreshPorts();
            RefreshExpandedState();
        }
    }
}