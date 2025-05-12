namespace SimpleBT.Editor.GraphNodes
{
    [System.Serializable]
    public class GraphAction_RotateConstantly2D : GraphAction_SingleTextFieldTemplate
    {
        public GraphAction_RotateConstantly2D()
        {
            Title = "Rotate Constantly 2D";
            ClassReference = "Action_RotateConstantly2D";
        }

        public override void GenerateInterface()
        {
            base.GenerateInterface();

            _textField.label = "Speed";
        }
    }

}
