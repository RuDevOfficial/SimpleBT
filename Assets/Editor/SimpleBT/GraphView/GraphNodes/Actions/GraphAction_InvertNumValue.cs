using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace SimpleBT.Editor.GraphNodes
{
    public class GraphAction_InvertNumValue : GraphAction
    {
        private TextField keyField = new TextField("Key: ");
        public string key;
        
        public GraphAction_InvertNumValue()
        {
            Title = "Invert Numerical Value";
            ClassReference = "Action_InvertNumValue";
        }

        public override void GenerateInterface()
        {
            base.GenerateInterface();
            
            keyField.RegisterValueChangedCallback((evt) => { key = evt.newValue; });
            
            //change style later
            keyField.labelElement.style.minWidth = 10;
            keyField.style.minWidth = 10;
            
            extensionContainer.Add(keyField);
        }
        
        public override List<string> GetValues() { return new List<string>() { key }; }

        public override void ReloadValues(List<string> values)
        {
            keyField.value = values[0];
            key = values[0];
        }
    }

}
