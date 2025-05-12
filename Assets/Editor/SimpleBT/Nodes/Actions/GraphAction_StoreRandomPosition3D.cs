using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace SimpleBT.Editor.GraphNodes
{
    [System.Serializable]
    public class GraphAction_StoreRandomPosition3D : GraphAction
    {
        private TextField _minDistanceTF, _maxDistanceTF;
        private TextField _raycastHeightTF, _raycastDistanceTF;
        private TextField _layerMaskTF;
        private TextField _keyParameterTF;
        
        [SerializeField] private string _keyMinDistance, _keyMaxDistance;
        [SerializeField] private string _keyRaycastHeight, _keyRaycastDistance;
        [SerializeField] private string _keyLayerMask = "Default";
        [SerializeField] private string _keyParameterName = "RANDOM_POS";
        
        public GraphAction_StoreRandomPosition3D()
        {
            Title = "Store Random Position 3D";
            ClassReference = "Action_StoreRandomPosition3D";
        }

        public override void GenerateInterface()
        {
            base.GenerateInterface();
            
            // Distance Container
            VisualElement distanceContainer = new VisualElement(); // crate container
            distanceContainer.AddToClassList("TwoTextFieldElement"); // add it to the class list
            _minDistanceTF = new TextField(); _minDistanceTF.AddToClassList("TextFieldExtended"); // assign the text fields and their related properties
            _maxDistanceTF = new TextField(); _maxDistanceTF.AddToClassList("TextFieldExtended");
            _minDistanceTF.RegisterValueChangedCallback(evt => _keyMinDistance = evt.newValue);
            _maxDistanceTF.RegisterValueChangedCallback(evt => _keyMaxDistance = evt.newValue);
            distanceContainer.Add(new Label("Distance (Min/Max): ")); // add the label and fields to the container
            distanceContainer.Add(_minDistanceTF);
            distanceContainer.Add(_maxDistanceTF);
            
            // Ray Height + Distance Container
            VisualElement raycastContainer = new VisualElement(); // crate container
            raycastContainer.AddToClassList("TwoTextFieldElement"); // add it to the class list
            _raycastHeightTF = new TextField(); _raycastHeightTF.AddToClassList("TextFieldExtended"); // assign the text fields and their related properties
            _raycastDistanceTF = new TextField(); _raycastDistanceTF.AddToClassList("TextFieldExtended");
            _raycastHeightTF.RegisterValueChangedCallback(evt => _keyRaycastHeight = evt.newValue);
            _raycastDistanceTF.RegisterValueChangedCallback(evt => _keyRaycastDistance = evt.newValue);
            raycastContainer.Add(new Label("Raycast (Height/Distance): ")); // add the label and fields to the container
            raycastContainer.Add(_raycastHeightTF);
            raycastContainer.Add(_raycastDistanceTF);
            
            // Layer Mask
            _layerMaskTF = new TextField("LayerMask: ");
            _layerMaskTF.RegisterValueChangedCallback(evt => _keyLayerMask = evt.newValue);
            
            // Parameter Mask
            _keyParameterTF = new TextField("Key: ");
            _keyParameterTF.RegisterValueChangedCallback(evt => _keyParameterName = evt.newValue);
            
            // add all containers to the extended one
            extensionContainer.Add(distanceContainer);
            extensionContainer.Add(raycastContainer);
            extensionContainer.Add(_layerMaskTF);
            extensionContainer.Add(_keyParameterTF);
        }

        public override List<string> GetValues()
        {
            return new List<string>()
            {
                _keyMinDistance, _keyMaxDistance,
                _keyRaycastHeight, _keyRaycastDistance,
                _keyLayerMask,
                _keyParameterName
            };
        }

        public override void ReloadValues(List<string> values)
        {
            _minDistanceTF.value = values[0]; _maxDistanceTF.value = values[1];
            _raycastHeightTF.value = values[2]; _raycastDistanceTF.value = values[3];
            _layerMaskTF.value = values[4];
            _keyParameterTF.value = values[5];
        }
    }
}
