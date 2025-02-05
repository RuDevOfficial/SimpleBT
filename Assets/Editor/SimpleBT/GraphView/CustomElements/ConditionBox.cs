using System;
using System.Globalization;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace SimpleBT.Editor.GraphNodes
{
    [System.Serializable]
    public class ConditionBox : VisualElement
    {
        [SerializeReference] public TextField VariableName;
        private string _lastVariableNameValue;
        
        [SerializeReference] public DropdownField DropDown;
        [SerializeReference] public TextField VariableChecked;
        private string _lastVariableCheckedValue;
        
        public Condition ConditionType = Condition.Equal;
        
        public ConditionBox()
        {
            VariableName = new TextField();
            VariableName.value = "Variable Name or Value";
            VariableName.RegisterValueChangedCallback(evt => {
                if (string.IsNullOrEmpty(_lastVariableCheckedValue)) {
                    Debug.LogError("There is no value or variable on the input field!");
                }
                
                _lastVariableNameValue = evt.newValue;
            });

            string[] conditions = Enum.GetNames(typeof(Condition));
            DropDown = new DropdownField(conditions.ToList(), 0, FormatSelectedValueCallback);

            VariableChecked = new TextField();
            VariableChecked.value = "Variable Name or Value";
            VariableChecked.RegisterValueChangedCallback(evt => {
                if (string.IsNullOrEmpty(evt.newValue)) { DropDown.value = "Null"; }
                else if (string.IsNullOrEmpty(_lastVariableCheckedValue)) {
                    if (DropDown.value == "Null") { DropDown.value = "Equal"; }
                }
                
                _lastVariableCheckedValue = evt.newValue;
            });
            
            Add(VariableName);
            Add(DropDown);
            Add(VariableChecked);
        }

        private string FormatSelectedValueCallback(string arg)
        {
            ConditionType = (Condition)Enum.Parse(typeof(Condition), arg);
            if (ConditionType == Condition.Null) { VariableChecked.value = null; }
            
            return arg;
        }
        
        public enum Condition
        {
            Equal,
            Less_Than,
            Less_Or_Equal_Than,
            More_Than,
            More_Or_Equal_Than,
            Null
        }
    }
}
