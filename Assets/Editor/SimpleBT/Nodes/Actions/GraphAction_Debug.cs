using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace SimpleBT.Editor.GraphNodes
{
    [System.Serializable]
    public class GraphAction_Debug : GraphAction
    {
        private TextField _messageFieldTF;
        private DropdownField _dropDown;
        
        [SerializeField] private string _keyDebugMessage;
        [SerializeField] private string _keyStatus;

        public GraphAction_Debug()
        {
            Title = "Debug";
            ClassReference = "Action_Debug";
        }
        
        public override void GenerateInterface()
        {
            base.GenerateInterface();
            string[] conditions = Enum.GetNames(typeof(ActionStatus));

            _messageFieldTF = new TextField("Message: ");
            _dropDown = new DropdownField("Status", conditions.ToList(), 0);
            
            _messageFieldTF.RegisterValueChangedCallback(evt => _keyDebugMessage = evt.newValue);
            _dropDown.RegisterValueChangedCallback(evt => _keyStatus = evt.newValue);
            _messageFieldTF.multiline = true;
            
            extensionContainer.Add(_messageFieldTF);
            extensionContainer.Add(_dropDown);
            
            RefreshExpandedState();
        }

        public override List<string> GetValues() 
        {
            return new List<string>() {
                _keyDebugMessage,
                _keyStatus
            }; 
        }

        public override void ReloadValues(List<string> values)
        {
            _messageFieldTF.value = values[0];
            _dropDown.value = values[1];
        }
    }

    public enum ActionStatus
    {
        Success,
        Failure
    }

}