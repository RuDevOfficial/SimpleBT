using System.Collections.Generic;
using SimpleBT.Editor.Utils;
using UnityEngine.UIElements;

namespace SimpleBT.Editor.GraphNodes
{
    [System.Serializable]
    public class GraphAction_Follow2D : GraphAction
    {
        private TextField _targetField;
        private TextField _speedField;
        private Toggle _toggle;

        public string keyTarget;
        public string keySpeed;
        public string keyToggle;
        
        public GraphAction_Follow2D()
        {
            Title = "Follow 2D";
            ClassReference = "Action_Follow2D";
        }

        public override void GenerateInterface()
        {
            base.GenerateInterface();

            _targetField = new TextField("Target: ");
            _targetField.RegisterValueChangedCallback(evt => keyTarget = evt.newValue);
            extensionContainer.Add(_targetField);
            
            _speedField = new TextField("Speed: ");
            _speedField.RegisterValueChangedCallback(evt => keySpeed = evt.newValue);
            extensionContainer.Add(_speedField);
            
            _toggle = new Toggle("Use Transform? ");
            _toggle.RegisterValueChangedCallback(evt => keyToggle = evt.newValue.ToString());
            extensionContainer.Add(_toggle);
        }

        public override List<string> GetValues()
        {
            return new List<string>()
            {
                keyTarget,
                keySpeed,
                keyToggle
            };
        }

        public override void ReloadValues(List<string> values)
        {
            _targetField.value = values[0];
            _speedField.value = values[1];
            _toggle.value = bool.Parse(values[2]);
        }
    }
}