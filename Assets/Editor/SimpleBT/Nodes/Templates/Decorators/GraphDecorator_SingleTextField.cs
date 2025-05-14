using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace SimpleBT.Editor.GraphNodes
{
    [System.Serializable]
    public class GraphDecorator_SingleTextField : GraphDecorator
    {
        protected TextField _textField;
        [SerializeField] protected string _keyField;
        
        public GraphDecorator_SingleTextField()
        {
            Title = "Single Text";
            ClassReference = "Decorator_SingleTextField";
        }
        
        public override void GenerateInterface()
        {
            base.GenerateInterface();
            
            _textField = new TextField("Field: ");
            _textField.RegisterValueChangedCallback(evt => _keyField = evt.newValue);
            _textField.AddToClassList("SingleTextField");
            extensionContainer.Add(_textField);
        }
        
        public override List<string> GetValues() { return new List<string>() { _keyField }; }
        public override void ReloadValues(List<string> values) { _textField.value = values[0]; }
    }
}
