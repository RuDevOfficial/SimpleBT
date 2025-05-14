using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UIElements;

namespace SimpleBT.Editor.GraphNodes
{
    [System.Serializable]
    public class GraphCondition_IsNearLedge2D : GraphCondition
    {
        private TextField _offsetDistanceTF;
        private TextField _rayLengthTextFieldTF;
        private TextField _layerNameTextField;
        
        [SerializeField] private float _separationDistance = 1;
        [SerializeField] private float _rayLength = 1;
        [SerializeField] private string _layerName = "Default";
        
        public GraphCondition_IsNearLedge2D()
        {
            Title = "Is Near Ledge 2D";
            ClassReference = "Condition_IsNearLedge2D";
        }

        public override void GenerateInterface()
        {
            base.GenerateInterface();
            
            _offsetDistanceTF = new TextField("Offset From Center ");
            _rayLengthTextFieldTF = new TextField("Ray Length: ");
            _layerNameTextField = new TextField("LayerMask: ");
            
            _offsetDistanceTF.value = "1";
            _rayLengthTextFieldTF.value = "1";
            _layerNameTextField.value = "Default";
            
            _offsetDistanceTF.RegisterValueChangedCallback(evt => _separationDistance = float.Parse(evt.newValue, CultureInfo.InvariantCulture));
            _rayLengthTextFieldTF.RegisterValueChangedCallback(evt => _rayLength = float.Parse(evt.newValue, CultureInfo.InvariantCulture));
            _layerNameTextField.RegisterValueChangedCallback(evt => _layerName = evt.newValue);
            
            extensionContainer.Add(_offsetDistanceTF);
            extensionContainer.Add(_rayLengthTextFieldTF);
            extensionContainer.Add(_layerNameTextField);
        }

        public override List<string> GetValues() 
        {
            return new List<string>() {
                _separationDistance.ToString(CultureInfo.InvariantCulture),
                _rayLength.ToString(CultureInfo.InvariantCulture),
                _layerName,
            };
        }

        public override void ReloadValues(List<string> values) {
            _offsetDistanceTF.value = values[0];
            _rayLengthTextFieldTF.value = values[1];
            _layerNameTextField.value = values[2];
        }
    }
}
