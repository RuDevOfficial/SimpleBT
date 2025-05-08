using System;
using System.Collections.Generic;
using SimpleBT.Editor.Utils;
using SimpleBT.NonEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace SimpleBT.Editor.GraphNodes
{
    [Serializable]
    public class GraphAction_SendMessageWithValue : GraphAction_SendMessage
    {
        private TextField _valueTextField;
        private DropdownField _typeField;
        private DropdownField _optionsDropDownField;
        
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
            
            _valueTextField = new TextField("Value: ");
            _valueTextField.value = "0";
            _valueTextField.AddToClassList("ValueField");
            _typeField = new DropdownField("Type:", SBTEditorUtils.ReturnEnumToList<VariableType>(), 0);
            _typeField.value = "Int";
            _typeField.choices.Remove(VariableType.GameObject.ToString());
            _optionsDropDownField = new DropdownField("Options:", SBTEditorUtils.ReturnEnumToList<SendMessageOptions>(), 0);
            _optionsDropDownField.value = SendMessageOptions.RequireReceiver.ToString();
            
            _valueTextField.RegisterValueChangedCallback(evt => _keyValue = evt.newValue);
            _typeField.RegisterValueChangedCallback(evt => _keyType = evt.newValue);
            _optionsDropDownField.RegisterValueChangedCallback(evt => _keyOptions = evt.newValue);
            
            VisualElement container = new VisualElement();
            container.AddToClassList("ValueDropdownContainer");
            container.Add(_valueTextField);
            container.Add(_typeField);

            extensionContainer.Add(container);
            extensionContainer.Add(_optionsDropDownField);
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
            
            _valueTextField.value = values[1];
            _typeField.value = values[2];
            _optionsDropDownField.value = values[3];
        }
    }
}
