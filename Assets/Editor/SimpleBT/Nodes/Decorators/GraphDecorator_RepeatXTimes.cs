using SimpleBT.NonEditor.Nodes;

namespace SimpleBT.Editor.GraphNodes
{
    [System.Serializable]
    public class GraphDecorator_RepeatXTimes : GraphDecorator_SingleTextField
    {
        public GraphDecorator_RepeatXTimes()
        {
            Title = "Repeat\nX Times";
            ClassReference = "Decorator_RepeatXTimes";
        }

        public override void GenerateInterface()
        {
            base.GenerateInterface();

            _textField.label = "Amount: ";
        }
    }

}
