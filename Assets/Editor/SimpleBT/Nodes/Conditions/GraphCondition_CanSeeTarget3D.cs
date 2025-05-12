using System;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace SimpleBT.Editor.GraphNodes
{
    [System.Serializable]
    public class GraphCondition_CanSeeTarget3D : GraphCondition
    {
        private TextField _targetField;
        private LayerMaskField _layerMaskField;
        private TextField _radiusField;
        
        [SerializeField] private string _targetFieldValue;
        [SerializeField] private string _layerMaskValue;
        [SerializeField] private string _radiusValue;
        
        public GraphCondition_CanSeeTarget3D()
        {
            Title = "Can See Target 3D"; // Rename like: GraphCondition_DoSomething -> Do Something
            ClassReference = "Condition_CanSeeTarget3D"; // Rename like: GraphCondition_DoSomething -> Condition_DoSomething
        }

        public override void GenerateInterface()
        {
            base.GenerateInterface();
        
            _targetField = new TextField("Target: ");
            _layerMaskField = new LayerMaskField("Ray Block Layers: ");
            _radiusField = new TextField("Radius: ");
            
            _targetField.RegisterValueChangedCallback(evt => _targetFieldValue = evt.newValue);
            _layerMaskField.RegisterValueChangedCallback(evt =>
            {
                string binary = Convert.ToString(evt.newValue, 2);
                _layerMaskValue = binary;
            });
            _radiusField.RegisterValueChangedCallback(evt => _radiusValue = evt.newValue);
            
            extensionContainer.Add(_targetField);
            extensionContainer.Add(_layerMaskField);
            extensionContainer.Add(_radiusField);
        }

        public override List<string> GetValues()
        {
            return new List<string>() {
                _targetFieldValue,
                _layerMaskValue,
                _radiusValue
            };
        }

        public override void ReloadValues(List<string> values)
        {
            _targetField.value = values[0];
            _layerMaskField.value = int.Parse(values[1]);
            _radiusField.value = values[2];
        }
    }
}