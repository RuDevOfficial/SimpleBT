using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace SimpleBT.Editor.GraphNodes
{
    [System.Serializable]
    public class GraphAction_OverrideGameObjectPosition3D : GraphAction
    {
        private TextField _keyGameObjectField = new TextField("Key: ");
        private TextField _keyPositionField = new TextField("Position: ");
        private Toggle _isLocalField = new Toggle("Use Local: ");

        [SerializeField] private string _keyGameObject;
        [SerializeField] private string _keyPosition;
        [SerializeField] private string _keyLocal = "False";

        public GraphAction_OverrideGameObjectPosition3D()
        {
            Title = "Override\nGameObject Position 3D";
            ClassReference = "Action_OverrideGameObjectPosition3D";
        }

        public override void GenerateInterface()
        {
            base.GenerateInterface();
            
            _keyGameObjectField.RegisterValueChangedCallback(evt => _keyGameObject = evt.newValue);
            _keyPositionField.RegisterValueChangedCallback(evt => _keyPosition = evt.newValue);
            _isLocalField.RegisterValueChangedCallback(evt => _keyLocal = evt.newValue.ToString());
            
            extensionContainer.Add(_keyGameObjectField);
            extensionContainer.Add(_keyPositionField);
            extensionContainer.Add(_isLocalField);
        }

        public override List<string> GetValues() { return new List<string>() { _keyGameObject, _keyPosition, _keyLocal }; }
        public override void ReloadValues(List<string> values) { _keyGameObjectField.value = values[0]; _keyPositionField.value = values[1]; _isLocalField.value = bool.Parse(values[2]); }
    }
}
