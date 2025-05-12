using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using SimpleBT.Editor.Utils;
using UnityEngine;
using UnityEngine.UIElements;

namespace SimpleBT.Editor.GraphNodes
{
    [System.Serializable]
    public class GraphAction_Wait : GraphAction
    {
        private TextField _secondsTF;
        private DropdownField _actionStatusDropdown;

        [SerializeField] private string _keySeconds;
        [SerializeField] private string _keyActionStatus;
        
        public GraphAction_Wait() : base()
        {
            Title = "Wait";
            ClassReference = "Action_Wait";
        }
        
        public override void GenerateInterface()
        {
            base.GenerateInterface();
            
            _secondsTF = new TextField("Seconds: ");
            _actionStatusDropdown = new DropdownField(SBTEditorUtils.ReturnEnumToList<ActionStatus>(), 0);
            
            _secondsTF.RegisterValueChangedCallback(evt => _keySeconds = evt.newValue);
            _actionStatusDropdown.RegisterValueChangedCallback(evt => _keyActionStatus = evt.newValue);
            
            extensionContainer.Add(_secondsTF);
            extensionContainer.Add(_actionStatusDropdown);
        }

        public override List<string> GetValues()
        {
            return new List<string>() {
                _keySeconds,
                _keyActionStatus
            };
        }

        public override void ReloadValues(List<string> values)
        {
            _secondsTF.value = values[0];
            _actionStatusDropdown.value = values[1];
        }
    }
}
