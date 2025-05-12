using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace SimpleBT.Editor.GraphNodes
{
    [System.Serializable]
    public abstract class GraphAction_TextDropdownTemplate : GraphAction_SingleTextFieldTemplate
    {
        protected DropdownField _dropdownField;
        [SerializeField] protected string _keyDropdownField;

        public override void GenerateInterface()
        {
            base.GenerateInterface();
            
            _dropdownField = new DropdownField("Field: ");
            _dropdownField.RegisterValueChangedCallback(evt => _keyDropdownField = evt.newValue);
            extensionContainer.Add(_dropdownField);
        }

        public override List<string> GetValues() { return new List<string>() { _keyField, _keyDropdownField }; }

        public override void ReloadValues(List<string> values)
        {
            _textField.value = values[0];
            _dropdownField.value = values[1];
        }
    }
}
