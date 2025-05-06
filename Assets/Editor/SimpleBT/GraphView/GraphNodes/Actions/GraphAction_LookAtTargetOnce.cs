namespace SimpleBT.Editor.GraphNodes
{
    [System.Serializable]
    public class GraphAction_LookAtTargetOnce : GraphAction_SingleTextFieldTemplate
    {
        public GraphAction_LookAtTargetOnce()
        {
            Title = "Look At Target Once";
            ClassReference = "Action_LookAtTargetOnce";
        }

        public override void GenerateInterface()
        {
            base.GenerateInterface();
            _textField.label = "Target: ";
        }
    }
}
