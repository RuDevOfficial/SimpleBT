using System;
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
        [SerializeReference] public DropdownField DropDown; 
        [SerializeReference] public TextField VariableChecked;
        private string _lastVariableCheckedValue;
        private string _lastVariableNameValue;
        
        public ConditionType Condition;

        public void Instantiate()
        {
            VariableName = new TextField();
            VariableName.value = "Variable Name or Value";

            string[] conditions = Enum.GetNames(typeof(ConditionType));
            DropDown = new DropdownField(conditions.ToList(), 0, FormatSelectedValueCallback);

            VariableChecked = new TextField();
            VariableChecked.value = "Variable Name or Value";
            VariableChecked.RegisterValueChangedCallback(evt => {
                if (string.IsNullOrEmpty(evt.newValue)) { DropDown.value = "Null"; }
                else if (string.IsNullOrEmpty(_lastVariableCheckedValue)) {
                    if (DropDown.value is "Null" or "NotNull") { DropDown.value = "Equal"; }
                }
                
                _lastVariableCheckedValue = evt.newValue;
            });
            
            Add(VariableName);
            Add(DropDown);
            Add(VariableChecked);
        }
        
        private string FormatSelectedValueCallback(string arg)
        {
            Condition = (ConditionType)Enum.Parse(typeof(ConditionType), arg);
            if (Condition is ConditionType.Null or ConditionType.NotNull) { VariableChecked.value = null; }
            
            return arg;
        }
    }
}
