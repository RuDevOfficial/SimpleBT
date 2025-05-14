using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace SimpleBT.Editor.GraphNodes
{
    using Core;
    using Utils;
    
    [System.Serializable]
    public class GraphAction_Follow2D : GraphAction
    {
        private TextField _targetTF;
        private TextField _speedTF;
        private Toggle _useTransformToggle;
        private DropdownField _rigidbodyFlagDropdown;
        private TextField _distanceTF;

        [SerializeField] private string _keyTarget;
        [SerializeField] private string _keySpeed;
        [SerializeField] private string _keyToggle = "False";
        [SerializeField] private string _keyRigidbodyMoveFlag;
        [SerializeField] private string _keyDistance;
        
        public GraphAction_Follow2D()
        {
            Title = "Follow 2D";
            ClassReference = "Action_Follow2D";
        }

        public override void GenerateInterface()
        {
            base.GenerateInterface();

            _targetTF = new TextField("GameObject: ");
            _speedTF = new TextField("Speed: ");
            _useTransformToggle = new Toggle("Use Transform? ");
            _distanceTF = new TextField("Stop Distance: ");
            _rigidbodyFlagDropdown = new DropdownField("Ignore Flag: ", SBTEditorUtils.ReturnEnumToList<RigidbodyMoveFlag>(), 0);
            
            _targetTF.RegisterValueChangedCallback(evt => _keyTarget = evt.newValue);
            _speedTF.RegisterValueChangedCallback(evt => _keySpeed = evt.newValue);
            _useTransformToggle.RegisterValueChangedCallback(evt => _keyToggle = evt.newValue.ToString());
            _distanceTF.RegisterValueChangedCallback(evt => _keyDistance = evt.newValue);
            _rigidbodyFlagDropdown.RegisterValueChangedCallback(evt => _keyRigidbodyMoveFlag = evt.newValue.ToString());
            
            extensionContainer.Add(_targetTF);
            extensionContainer.Add(_speedTF);
            extensionContainer.Add(_useTransformToggle);
            extensionContainer.Add(_distanceTF);
            extensionContainer.Add(_rigidbodyFlagDropdown);
        }

        public override List<string> GetValues()
        {
            return new List<string>() {
                _keyTarget,
                _keySpeed,
                _keyToggle,
                _keyRigidbodyMoveFlag,
                _keyDistance
            };
        }

        public override void ReloadValues(List<string> values)
        {
            _targetTF.value = values[0];
            _speedTF.value = values[1];
            _useTransformToggle.value = bool.Parse(values[2]);
            _rigidbodyFlagDropdown.value = values[3];
            _distanceTF.value = values[4];
        }
    }
}