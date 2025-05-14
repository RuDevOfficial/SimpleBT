using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace SimpleBT.Editor.GraphNodes
{
    using Core;
    using Utils;
    
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
        [SerializeField] protected string _keyRigidbodyMoveFlag;

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
            _ignoreDropdown = new DropdownField(SBTEditorUtils.ReturnEnumToList<RigidbodyMoveFlag>(), 0);

            _positionTF.RegisterValueChangedCallback(evt => _keyPosition = evt.newValue);
            _speedTF.RegisterValueChangedCallback(evt => _keySpeed = evt.newValue);
            _useTransformToggle.RegisterValueChangedCallback(evt => _keyUseTransform = evt.newValue.ToString());
            _ignoreDropdown.RegisterValueChangedCallback(evt => _keyRigidbodyMoveFlag = evt.newValue.ToString());

            extensionContainer.Add(_positionTF);
            extensionContainer.Add(_speedTF);
            extensionContainer.Add(_useTransformToggle);
            extensionContainer.Add(_ignoreDropdown);
        }

        public override List<string> GetValues()
        {
            return new List<string>() {
                _keyPosition,
                _keySpeed,
                _keyUseTransform,
                _keyRigidbodyMoveFlag
            };
        }

        public override void ReloadValues(List<string> values)
        {
            _positionTF.value = values[0];
            _speedTF.value = values[1];
            _useTransformToggle.value = bool.Parse(values[2]);
            _ignoreDropdown.value = values[3];
        }
    }
}
