using System.Collections.Generic;
using System.Globalization;
using UnityEngine.UIElements;

namespace SimpleBT.Editor.GraphNodes
{
    [System.Serializable]
    public class GraphCondition_IsNearLedge2D : GraphCondition
    {
        private TextField _offsetDistance;
        private TextField _rayLengthTextField;
        private TextField _layerNameTextField;
        
        public float _separationDistance = 1;
        public float _rayLength = 1;
        public string _layerName = "Default";
        
        public GraphCondition_IsNearLedge2D()
        {
            Title = "Is Near Ledge 2D";
            ClassReference = "Condition_IsNearLedge2D";
        }

        public override void GenerateInterface()
        {
            base.GenerateInterface();
            
            _offsetDistance = new TextField("Offset From Center ");
            _rayLengthTextField = new TextField("Ray Length: ");
            _layerNameTextField = new TextField("LayerMask: ");
            
            _offsetDistance.value = "1";
            _rayLengthTextField.value = "1";
            _layerNameTextField.value = "Default";
            
            _offsetDistance.RegisterValueChangedCallback(evt => _separationDistance = float.Parse(evt.newValue, CultureInfo.InvariantCulture));
            _rayLengthTextField.RegisterValueChangedCallback(evt => _rayLength = float.Parse(evt.newValue, CultureInfo.InvariantCulture));
            _layerNameTextField.RegisterValueChangedCallback(evt => _layerName = evt.newValue);
            
            _offsetDistance.labelElement.style.minWidth = 10; // Style delete that later
            _rayLengthTextField.labelElement.style.minWidth = 10; // Style delete that later
            _layerNameTextField.labelElement.style.minWidth = 10; // Style delete that later
            
            extensionContainer.Add(_offsetDistance);
            extensionContainer.Add(_rayLengthTextField);
            extensionContainer.Add(_layerNameTextField);
        }

        public override List<string> GetValues() {
            return new List<string>()
            {
                _separationDistance.ToString(CultureInfo.InvariantCulture),
                _rayLength.ToString(CultureInfo.InvariantCulture),
                _layerName,
            };
        }

        public override void ReloadValues(List<string> values) {
            _offsetDistance.value = values[0];
            _rayLengthTextField.value = values[1];
            _layerNameTextField.value = values[2];
        }
    }
}
