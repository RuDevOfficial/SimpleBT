using System.Linq;

namespace SimpleBT.Editor.GraphNodes
{
    [System.Serializable]
    public class GraphAction_OverrideTag : GraphAction_TextDropdownTemplate
    {
        public GraphAction_OverrideTag()
        {
            Title = "Override Tag";
            ClassReference = "Action_OverrideTag";
        }

        public override void GenerateInterface()
        {
            base.GenerateInterface();

            _textField.label = "GameObject: ";
            _dropdownField.label = "Tag: ";
            _dropdownField.choices = UnityEditorInternal.InternalEditorUtility.tags.ToList();
        }
    }

}
