using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace SimpleBT.Editor.GraphNodes
{
    [System.Serializable]
    public class GraphDecorator_ExecuteOnceWithDelay : GraphDecorator
    {
        private TextField _delayField;
        
        [SerializeField] private string keyDelay;

        public GraphDecorator_ExecuteOnceWithDelay()
        {
            Title = "Execute Once With Delay";
            ClassReference = "Decorator_ExecuteOnceWithDelay";
        }
        
        public override void GenerateInterface()
        {
            base.GenerateInterface();
            _delayField = new TextField("Delay: ");
            _delayField.RegisterValueChangedCallback(evt => keyDelay = evt.newValue);
            extensionContainer.Add(_delayField);
        }

        public override List<string> GetValues() { return new List<string>() { keyDelay }; }
        public override void ReloadValues(List<string> values) { _delayField.value = values[0]; }
    }
}
