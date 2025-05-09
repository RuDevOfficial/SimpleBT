using System.Collections.Generic;
using SimpleBT.Editor.Utils;
using SimpleBT.NonEditor.Nodes;
using UnityEngine;
using UnityEngine.UIElements;

namespace SimpleBT.Editor.GraphNodes
{
    [System.Serializable]
    public class GraphAction_RotateDegrees3D : GraphAction_RotateDegrees2D
    {
        private DropdownField _rotationTypeField;
        [SerializeField] private string _keyRotationType;

        public GraphAction_RotateDegrees3D()
        {
            Title = "Rotate Degrees 3D";
            ClassReference = "Action_RotateDegrees3D";
        }

        public override void GenerateInterface()
        {
            base.GenerateInterface();
            _rotationTypeField = new DropdownField("Rotate Axis:", SBTEditorUtils.ReturnEnumToList<AxisRotationType>(), 0);
            _rotationTypeField.RegisterValueChangedCallback(evt => _keyRotationType = evt.newValue);
            extensionContainer.Insert(3, _rotationTypeField);
        }

        public override List<string> GetValues()
        {
            List<string> values = base.GetValues();
            values.Add(_keyRotationType);
            return values;
        }

        public override void ReloadValues(List<string> values)
        {
            base.ReloadValues(values);
            _keyRotationType = values[4];
        }
    }
}
