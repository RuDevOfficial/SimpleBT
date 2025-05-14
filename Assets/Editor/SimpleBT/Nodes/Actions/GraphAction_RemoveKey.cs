namespace SimpleBT.Editor.GraphNodes
{
    [System.Serializable]
    public class GraphAction_RemoveKey : GraphAction_SingleTextFieldTemplate
    {
        public GraphAction_RemoveKey()
        {
            Title = "Remove Key";
            ClassReference = "Action_RemoveKey";
        }

        public override void GenerateInterface()
        {
            base.GenerateInterface();

            _textField.label = "Key: ";
        }
    }
}
