using System;
using System.Collections.Generic;
using System.Linq;
using SimpleBT.Core;
using SimpleBT.Editor.Utils;
using UnityEngine;
using UnityEngine.UIElements;

namespace SimpleBT.Editor.GraphNodes
{
    [System.Serializable]
    public class GraphAction_Follow2D : GraphAction
    {
        private TextField _targetField;
        private TextField _speedField;
        private Toggle _useTransformToggle;
        private DropdownField _ignoreDropdown;
        private TextField _distanceField;

        [SerializeField] protected string _keyTarget;
        [SerializeField] protected string _keySpeed;
        [SerializeField] protected string _keyToggle = "False";
        [SerializeField] protected RigidbodyMoveFlag _keyRigidbodyMoveFlag;
        [SerializeField] protected string _keyDistance;
        
        public GraphAction_Follow2D()
        {
            Title = "Follow 2D";
            ClassReference = "Action_Follow2D";
        }

        public override void GenerateInterface()
        {
            base.GenerateInterface();

            _targetField = new TextField("GameObject: ");
            _speedField = new TextField("Speed: ");
            _useTransformToggle = new Toggle("Use Transform? ");
            _distanceField = new TextField("Stop Distance: ");
            
            VisualElement dropdownContainer = new VisualElement();
            dropdownContainer.AddToClassList("DropdownContainer");
            string[] conditions = Enum.GetNames(typeof(RigidbodyMoveFlag));
            _ignoreDropdown = new DropdownField(conditions.ToList(), 0, FormatSelectedValueCallback);
            dropdownContainer.Add(new Label("Ignore Flag: "));
            dropdownContainer.Add(_ignoreDropdown);
            
            _targetField.RegisterValueChangedCallback(evt => _keyTarget = evt.newValue);
            _speedField.RegisterValueChangedCallback(evt => _keySpeed = evt.newValue);
            _useTransformToggle.RegisterValueChangedCallback(evt => _keyToggle = evt.newValue.ToString());
            _distanceField.RegisterValueChangedCallback(evt => _keyDistance = evt.newValue);
            
            extensionContainer.Add(_targetField);
            extensionContainer.Add(_speedField);
            extensionContainer.Add(_useTransformToggle);
            extensionContainer.Add(_distanceField);
            extensionContainer.Add(dropdownContainer);
        }

        public override List<string> GetValues()
        {
            return new List<string>()
            {
                _keyTarget,
                _keySpeed,
                _keyToggle,
                _keyRigidbodyMoveFlag.ToString(),
                _keyDistance
            };
        }

        public override void ReloadValues(List<string> values)
        {
            _targetField.value = values[0];
            _speedField.value = values[1];
            _useTransformToggle.value = bool.Parse(values[2]);
            _ignoreDropdown.value = values[3];
            _distanceField.value = values[4];
        }
        
        private string FormatSelectedValueCallback(string arg)
        {
            _keyRigidbodyMoveFlag = (RigidbodyMoveFlag)Enum.Parse(typeof(RigidbodyMoveFlag), arg);
            return arg;
        }
    }

}