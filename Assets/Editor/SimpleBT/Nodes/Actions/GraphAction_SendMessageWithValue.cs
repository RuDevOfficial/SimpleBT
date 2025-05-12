using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace SimpleBT.Editor.GraphNodes
{
    using Utils;
    using NonEditor;
    
    [Serializable]
    public class GraphAction_SendMessageWithValue : GraphAction_SendMessage
    {
        private TextField _valueTF;
        private DropdownField _typeDropdown;
        private DropdownField _optionsDropdown;
        
        [SerializeField] private string _keyValue = "0";
        [SerializeField] private string _keyType = "Int";
        [SerializeField] private string _keyOptions = SendMessageOptions.DontRequireReceiver.ToString();
        
        public GraphAction_SendMessageWithValue()
        {
            Title = "Send Message with Value";
            ClassReference = "Action_SendMessageWithValue";
        }

        public override void GenerateInterface()
        {
            base.GenerateInterface();
            
            _valueTF.AddToClassList("ValueField");
            
            _valueTF = new TextField("Value: ");
            _typeDropdown = new DropdownField("Type:", SBTEditorUtils.ReturnEnumToList<VariableType>(), 0);
            _optionsDropdown = new DropdownField("Options:", SBTEditorUtils.ReturnEnumToList<SendMessageOptions>(), 0);
            
            _typeDropdown.choices.Remove(VariableType.GameObject.ToString());
            
            _valueTF.RegisterValueChangedCallback(evt => _keyValue = evt.newValue);
            _typeDropdown.RegisterValueChangedCallback(evt => _keyType = evt.newValue);
            _optionsDropdown.RegisterValueChangedCallback(evt => _keyOptions = evt.newValue);
            
            VisualElement container = new VisualElement();
            container.AddToClassList("ValueDropdownContainer");
            container.Add(_valueTF);
            container.Add(_typeDropdown);

            extensionContainer.Add(container);
            extensionContainer.Add(_optionsDropdown);
        }

        public override List<string> GetValues()
        {
            var list = base.GetValues();
            list.Add(_keyValue);
            list.Add(_keyType);
            list.Add(_keyOptions);
            return list;
        }

        public override void ReloadValues(List<string> values)
        {
            base.ReloadValues(values);
            
            _valueTF.value = values[1];
            _typeDropdown.value = values[2];
            _optionsDropdown.value = values[3];
        }
    }
}
