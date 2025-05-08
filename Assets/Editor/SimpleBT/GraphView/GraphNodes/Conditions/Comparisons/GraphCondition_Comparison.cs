using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace SimpleBT.Editor.GraphNodes
{
    [System.Serializable]
    public class GraphCondition_Comparison : GraphCondition
    {
        public TextField FieldA;
        public TextField FieldB;
        public DropdownField DropDown; 
        
        public string VariableAValue;
        public string VariableBValue;
        public ConditionType Condition;

        public GraphCondition_Comparison()
        {
            Title = "Comparison Condition";
            ClassReference = "Condition_Comparison";
        }
        
        public override void GenerateInterface()
        {
            base.GenerateInterface();

            FieldA = new TextField();
            FieldA.RegisterValueChangedCallback(evt => VariableAValue = evt.newValue);
            
            FieldB = new TextField();
            FieldB.RegisterValueChangedCallback(evt => VariableBValue = evt.newValue);
            
            List<string> conditions = Enum.GetNames(typeof(ConditionType)).ToList();
            DropDown = new DropdownField(conditions, (int)Condition, FormatSelectedValueCallback);
            
            extensionContainer.Add(FieldA);
            extensionContainer.Add(DropDown);
            extensionContainer.Add(FieldB);
        }

        private string FormatSelectedValueCallback(string arg)
        {
            Condition = Enum.Parse<ConditionType>(arg);
            if (Condition is ConditionType.IsNull or ConditionType.IsNotNull) { FieldB.value = ""; }
            return arg;
        }

        public override List<string> GetValues()
        {
            List<string> values = new List<string>
            {
                VariableAValue,
                VariableBValue,
                Condition.ToString(),
            };

            return values;
        }

        public override void ReloadValues(List<string> values)
        {
            FieldA.value = values[0];
            FieldB.value = values[1];
            DropDown.value = values[2];
        }
    }
}