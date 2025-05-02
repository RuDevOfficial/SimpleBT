using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace SimpleBT.Editor.GraphNodes
{
    [System.Serializable]
    public class GraphAction_Any : GraphAction
    {
        [SerializeReference] private TextField _actionName;
        [SerializeReference] private TextField _parameters;
        [SerializeReference] private Toggle _toggle;
        
        public GraphAction_Any()
        {
            Title = "GraphAction_Any"; // Rename like: GraphAction_DoSomething -> Do Something
            ClassReference = "Action_Any"; // Rename like: GraphAction_DoSomething -> Action_DoSomething
        }

        public override void GenerateInterface()
        {
            base.GenerateInterface();
            
            // Add any custom interface after
            _actionName = new TextField("Action Name: ");
            _parameters = new TextField("Parameters: ");

            _toggle = new Toggle("Is Built-In? ");
            
            extensionContainer.Add(_actionName);
            extensionContainer.Add(_parameters);
            extensionContainer.Add(_toggle);
        }

        public override List<string> GetValues()
        {
            // If your GraphAction requires keys return a new list like this:
            // return new List<string>() { value0, value1, ... }
            // If not leave it like this
            return new List<string>()
            {
                _actionName.value,
                _parameters.value,
                _toggle.value.ToString()
            };
        }

        public override void ReloadValues(List<string> values)
        {
            // If your GraphAction requires to get values it must reload them as well
            // in the same order as you put the values on GetValues() like...
            // _textField.value = values[0]
            // _dropDownField.value = values[1]
            
            // If not you can leave this empty
            _actionName.value = values[0];
            _parameters.value = values[1];
            _toggle.value = bool.Parse(values[2]);
        }
    }
}
