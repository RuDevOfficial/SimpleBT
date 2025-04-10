using System;
using System.Collections.Generic;
using System.Linq;
using SimpleBT.Core;
using UnityEngine;
using UnityEngine.UIElements;

namespace SimpleBT.Editor.GraphNodes
{
    [System.Serializable]
    public class GraphAction_GoToPosition2D : GraphAction
    {
        private TextField _positionTF;
        private TextField _speedTF;
        private Toggle _useTransformToggle;
        private DropdownField _ignoreDropdown;

        [SerializeField] protected string _keyPosition;
        [SerializeField] protected string _keySpeed;
        [SerializeField] protected string _keyUseTransform;
        [SerializeField] protected RigidbodyMoveFlag _keyRigidbodyMoveFlag;

        public GraphAction_GoToPosition2D()
        {
            Title = "Go To Position 2D";
            ClassReference = "Action_GoToPosition2D";
        }

        public override void GenerateInterface()
        {
            base.GenerateInterface();
            
            _positionTF = new TextField("Position:");
            _speedTF = new TextField("Speed: ");
            _useTransformToggle = new Toggle("Use Transform? ");

            VisualElement dropdownContainer = new VisualElement();
            dropdownContainer.AddToClassList("DropdownContainer");
            string[] conditions = Enum.GetNames(typeof(RigidbodyMoveFlag));
            _ignoreDropdown = new DropdownField(conditions.ToList(), 0, FormatSelectedValueCallback);
            dropdownContainer.Add(new Label("Ignore Flag: "));
            dropdownContainer.Add(_ignoreDropdown);
            
            _positionTF.RegisterValueChangedCallback(evt => _keyPosition = evt.newValue);
            _speedTF.RegisterValueChangedCallback(evt => _keySpeed = evt.newValue);
            _useTransformToggle.RegisterValueChangedCallback(evt => _keyUseTransform = evt.newValue.ToString());

            _useTransformToggle.value = false;
            
            extensionContainer.Add(_positionTF);
            extensionContainer.Add(_speedTF);
            extensionContainer.Add(_useTransformToggle);
            extensionContainer.Add(dropdownContainer);
        }

        public override List<string> GetValues()
        {
            return new List<string>()
            {
                _keyPosition,
                _keySpeed,
                _keyUseTransform,
                _keyRigidbodyMoveFlag.ToString()
            };
        }

        public override void ReloadValues(List<string> values)
        {
            _positionTF.value = values[0];
            _speedTF.value = values[1];
            _useTransformToggle.value = bool.Parse(values[2]);
            _ignoreDropdown.value = values[3];
        }
        
        private string FormatSelectedValueCallback(string arg)
        {
            _keyRigidbodyMoveFlag = (RigidbodyMoveFlag)Enum.Parse(typeof(RigidbodyMoveFlag), arg);
            return arg;
        }
    }

}
