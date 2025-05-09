using SimpleBT.Editor.Utils;
using SimpleBT.NonEditor.Nodes;

namespace SimpleBT.Editor.GraphNodes
{
    [System.Serializable]
    public class GraphAction_RotateConstantly3D : GraphAction_TextDropdownTemplate
    {
        public GraphAction_RotateConstantly3D()
        {
            Title = "Rotate Constantly 3D";
            ClassReference = "Action_RotateConstantly3D";
        }

        public override void GenerateInterface()
        {
            base.GenerateInterface();

            _textField.label = "Speed";
            _dropdownField.label = "Rotation Axis:";
            
            _dropdownField.choices = SBTEditorUtils.ReturnEnumToList<AxisRotationType>();
        }
    }
}
