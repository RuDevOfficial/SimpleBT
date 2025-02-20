using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace SimpleBT.NonEditor.Nodes
{
    using Core;
    
    public class Condition : ExecutionNode
    {
        public string KeyFirstVariable;
        public string KeySecondVariable;

        private object varAObject, varBObject;
        private Type varAType, varBType;
        
        public ConditionType condition;

        protected override void Initialize()
        {
            if (blackboard.GetRawValue(KeyFirstVariable, out varAObject) == false) {
                varAObject = blackboard.GetValue<float>(KeyFirstVariable);
            }
            
            varAType = varAObject?.GetType();
            
            if (blackboard.GetRawValue(KeySecondVariable, out varBObject) == false
                && condition != ConditionType.Null && condition != ConditionType.NotNull) {
                varBObject = blackboard.GetValue<float>(KeySecondVariable);
            }
            
            if (condition != ConditionType.Null && condition != ConditionType.NotNull) { varBType = varBObject?.GetType(); }
        }
        
        protected override Status Tick() { return Check() == true ? Status.Success : Status.Failure; }

        protected virtual bool Check()
        {
            if (varAType == typeof(int) || varAType == typeof(float)
                && varBType == typeof(int) || varBType == typeof(float))
            {
                float valueA = Convert.ToSingle(varAObject);
                float valueB = Convert.ToSingle(varBObject);
                
                switch (condition)
                {
                    case ConditionType.Equal: return Mathf.Approximately(valueA, valueB);
                    case ConditionType.NotEqual: return !Mathf.Approximately(valueA, valueB);
                    case ConditionType.Less_Than: return valueA < valueB;
                    case ConditionType.Less_Or_Equal_Than: return valueA <= valueB;
                    case ConditionType.More_Than: return valueA > valueB;
                    case ConditionType.More_Or_Equal_Than: return valueA >= valueB;
                    default: return false;
                }
            }
            else
            {
                switch (condition)
                {
                    case ConditionType.Equal: return varAObject == varBObject;
                    case ConditionType.NotEqual: return varAObject != varBObject;
                    case ConditionType.Null: return varAObject == null;
                    case ConditionType.NotNull: return varAObject != null;
                    default: return false;
                }
            }
        }

        public override void AssignValues(List<string> values)
        {
            KeyFirstVariable = values[0]?.ToUpper();
            condition = (ConditionType)Enum.Parse(typeof(ConditionType), values[1]);
            KeySecondVariable = values[2]?.ToUpper();
        }
    }
}

public enum ConditionType
{
    Equal,
    NotEqual,
    Less_Than,
    Less_Or_Equal_Than,
    More_Than,
    More_Or_Equal_Than,
    Null,
    NotNull
}