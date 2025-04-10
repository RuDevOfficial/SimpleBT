using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace SimpleBT.Editor.GraphNodes
{
    [System.Serializable]
    public abstract class GraphAction_SingleTextFieldTemplate : GraphAction
    {
        protected TextField _textField;
        [SerializeField] protected string _keyField;
        
        public override void GenerateInterface()
        {
            base.GenerateInterface();
            
            _textField = new TextField("Field: ");
            _textField.RegisterValueChangedCallback(evt => _keyField = evt.newValue);
            extensionContainer.Add(_textField);
        }
        
        public override List<string> GetValues() { return new List<string>() { _keyField }; }
        public override void ReloadValues(List<string> values) { _textField.value = values[0]; }
    }

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
    
    [System.Serializable]
    public abstract class GraphAction_DoubleTextFieldTemplate : GraphAction_SingleTextFieldTemplate
    {
        protected TextField _secondTextField;
        [SerializeField] protected string _secondKeyField;

        public override void GenerateInterface()
        {
            base.GenerateInterface();
            
            _secondTextField = new TextField("Field: ");
            _secondTextField.RegisterValueChangedCallback(evt => _secondKeyField = evt.newValue);
            extensionContainer.Add(_secondTextField);
        }
        
        public override List<string> GetValues() { return new List<string>() { _keyField, _secondKeyField }; }

        public override void ReloadValues(List<string> values)
        {
            _textField.value = values[0];
            _secondTextField.value = values[1];
        }
    }
}
