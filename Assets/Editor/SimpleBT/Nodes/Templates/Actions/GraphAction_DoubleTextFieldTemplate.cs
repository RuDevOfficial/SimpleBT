using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace SimpleBT.Editor.GraphNodes
{
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
