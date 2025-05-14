using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace SimpleBT.Editor.GraphNodes
{
    [System.Serializable]
    public class GraphAction_Any : GraphAction
    {
        private TextField _actionNameTF; 
        private TextField _parametersTF;
        private Toggle _toggle;

        [SerializeField] private string _keyActionName;
        [SerializeField] private string _keyParameters;
        [SerializeField] private string _keyToggle;
        
        public GraphAction_Any()
        {
            Title = "GraphAction_Any";
            ClassReference = "Action_Any";
        }

        public override void GenerateInterface()
        {
            base.GenerateInterface();
            
            _actionNameTF = new TextField("Action Name: ");
            _parametersTF = new TextField("Parameters: ");
            _toggle = new Toggle("Is Built-In? ");

            _actionNameTF.RegisterValueChangedCallback(evt => _keyActionName = evt.newValue);
            _parametersTF.RegisterValueChangedCallback(evt => _keyParameters = evt.newValue);
            _toggle.RegisterValueChangedCallback(evt => _keyToggle = evt.newValue.ToString());
            
            extensionContainer.Add(_actionNameTF);
            extensionContainer.Add(_parametersTF);
            extensionContainer.Add(_toggle);
        }

        public override List<string> GetValues() 
        {
            return new List<string>() {
                _keyActionName,
                _keyParameters,
                _keyToggle
            };
        }

        public override void ReloadValues(List<string> values)
        {
            _actionNameTF.value = values[0];
            _parametersTF.value = values[1];
            _toggle.value = bool.Parse(values[2]);
        }
    }
}
