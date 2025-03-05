using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UIElements;

namespace SimpleBT.Editor.GraphNodes
{
    public class GraphAction_Wait : GraphAction
    {
        private TextField _textField;

        public GraphAction_Wait()
        {
            Title = "Wait";
            ClassReference = "Action_Wait";
        }
        
        public override void GenerateInterface()
        {
            base.GenerateInterface();
            
            _textField = new TextField("Seconds: ");
            _textField.value = "0";
            _textField.ElementAt(0).style.minWidth = 10;
            _textField.RegisterValueChangedCallback(evt =>
            {
                if (int.TryParse(evt.newValue, NumberStyles.Integer, CultureInfo.InvariantCulture, out int seconds)) {
                    _textField.value = Mathf.Max(0, seconds).ToString();
                }
            });
            
            extensionContainer.Add(_textField);
        }

        public override List<string> GetValues()
        {
            return new List<string>()
            {
                _textField.value
            };
        }

        public override void ReloadValues(List<string> values)
        {
            _textField.value = values[0];
        }
    }

}
