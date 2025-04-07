using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace SimpleBT.Editor.GraphNodes
{
    [System.Serializable]
    public class GraphAction_DestroyGameObject : GraphAction
    {
        private TextField _targetField;

        [SerializeField] private string _keyTarget;
        
        public GraphAction_DestroyGameObject()
        {
            Title = "Destroy GameObject";
            ClassReference = "Action_DestroyGameObject";
        }

        public override void GenerateInterface()
        {
            base.GenerateInterface();

            _targetField = new TextField("GameObject: ");
            _targetField.RegisterValueChangedCallback(evt => _keyTarget = evt.newValue);
            extensionContainer.Add(_targetField);
        }

        public override List<string> GetValues() { return new List<string>() { _keyTarget }; }
        public override void ReloadValues(List<string> values) { _targetField.value = values[0]; }
    }
}
