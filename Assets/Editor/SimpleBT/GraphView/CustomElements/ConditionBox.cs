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
        public TextField VariableA;
        public TextField VariableB;
        public DropdownField DropDown; 
        
        public string VariableAValue;
        public string VariableBValue;
        public ConditionType Condition;

        public void Instantiate()
        {
            VariableA = new TextField();
            VariableA.value = "Variable Name or Value";
            VariableA.RegisterValueChangedCallback(evt => {
                VariableAValue = evt.newValue;
            });

            string[] conditions = Enum.GetNames(typeof(ConditionType));
            DropDown = new DropdownField(conditions.ToList(), 0, FormatSelectedValueCallback);
            DropDown.value = Condition.ToString();

            VariableB = new TextField();
            VariableB.value = "Variable Name or Value";
            VariableB.RegisterValueChangedCallback(evt => {
                if (string.IsNullOrEmpty(evt.newValue)) { DropDown.value = "Null"; }
                else if (string.IsNullOrEmpty(VariableBValue)) {
                    if (DropDown.value is "Null" or "NotNull") { DropDown.value = "Equal"; }
                }
                
                VariableBValue = evt.newValue;
            });
            
            Add(VariableA);
            Add(DropDown);
            Add(VariableB);
        }
        
        private string FormatSelectedValueCallback(string arg)
        {
            Condition = (ConditionType)Enum.Parse(typeof(ConditionType), arg);
            if (Condition is ConditionType.IsNull or ConditionType.IsNotNull) { VariableB.value = null; }
            
            return arg;
        }
    }
}