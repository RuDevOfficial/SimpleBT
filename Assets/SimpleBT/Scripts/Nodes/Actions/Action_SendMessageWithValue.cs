using System;
using System.Collections.Generic;
using SimpleBT.Core;
using UnityEngine;

namespace SimpleBT.NonEditor.Nodes
{
    public class Action_SendMessageWithValue : Action_SendMessage
    {
        [SerializeField] private string _keyValue;
        [SerializeField] private string _keyType;
        [SerializeField] private string _keyOptions;

        private object _value;
        private SendMessageOptions _options;
        
        public override void AssignKeys(List<string> keys) {
            base.AssignKeys(keys);
            _keyValue = keys[1];
            _keyType = keys[2];
            _keyOptions = keys[3];
        }

        protected override void Initialize()
        {
            VariableType type = Enum.Parse<VariableType>(_keyType);

            if (blackboard.ContainsKey(_keyType)) { blackboard.GetRawValue(_keyType, out _value); }
            else
            {
                Type valueType = type.ConvertToType();
                _value = type == VariableType.GameObject ? 
                    _keyValue.ConvertComplexValue(valueType) : 
                    _keyValue.ConvertValue(valueType, _keyValue);
            }

            _options = blackboard.GetValue<SendMessageOptions>(_keyOptions);
        }

        protected override Status Tick()
        {
            blackboard.gameObject.SendMessage(_keyMethodName, _value, _options);
            return Status.Success;
        }
    }
}
