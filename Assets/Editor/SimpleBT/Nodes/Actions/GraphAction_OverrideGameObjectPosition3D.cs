using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace SimpleBT.Editor.GraphNodes
{
    [System.Serializable]
    public class GraphAction_OverrideGameObjectPosition3D : GraphAction
    {
        private TextField _keyGameObjectTF;
        private TextField _keyPositionTF;
        private Toggle _isLocalFieldToggle;

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
            
            _keyGameObjectTF = new TextField("Key: ");
            _keyPositionTF = new TextField("Position: ");
            _isLocalFieldToggle = new Toggle("Use Local: ");
            
            _keyGameObjectTF.RegisterValueChangedCallback(evt => _keyGameObject = evt.newValue);
            _keyPositionTF.RegisterValueChangedCallback(evt => _keyPosition = evt.newValue);
            _isLocalFieldToggle.RegisterValueChangedCallback(evt => _keyLocal = evt.newValue.ToString());
            
            extensionContainer.Add(_keyGameObjectTF);
            extensionContainer.Add(_keyPositionTF);
            extensionContainer.Add(_isLocalFieldToggle);
        }

        public override List<string> GetValues()
        {
            return new List<string>() {
                _keyGameObject, 
                _keyPosition,
                _keyLocal
            };
        }

        public override void ReloadValues(List<string> values)
        {
            _keyGameObjectTF.value = values[0];
            _keyPositionTF.value = values[1]; 
            _isLocalFieldToggle.value = bool.Parse(values[2]);
        }
    }
}
