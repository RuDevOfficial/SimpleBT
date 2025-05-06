namespace SimpleBT.Editor.GraphNodes
{
    [System.Serializable]
    public class GraphAction_SendMessage : GraphAction_SingleTextFieldTemplate
    {
        public GraphAction_SendMessage()
        {
            Title = "Send Message";
            ClassReference = "Action_SendMessage";
        }

        public override void GenerateInterface()
        {
            base.GenerateInterface();
            _textField.label = "Message: ";
        }
    }

}
