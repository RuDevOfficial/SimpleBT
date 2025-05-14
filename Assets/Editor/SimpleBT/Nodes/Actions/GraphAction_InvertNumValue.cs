using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace SimpleBT.Editor.GraphNodes
{
    [System.Serializable]
    public class GraphAction_InvertNumValue : GraphAction
    {
        private TextField _keyTF;
        [SerializeField] private string _key;
        
        public GraphAction_InvertNumValue()
        {
            Title = "Blackboard\nInvert Numerical Value";
            ClassReference = "Action_InvertNumValue";
        }

        public override void GenerateInterface()
        {
            base.GenerateInterface();
            _keyTF = new TextField("Key:");
            _keyTF.RegisterValueChangedCallback((evt) => { _key = evt.newValue; });
            extensionContainer.Add(_keyTF);
        }
        
        public override List<string> GetValues() { return new List<string>() { _key }; }
        public override void ReloadValues(List<string> values) { _keyTF.value = values[0]; }
    }

}
