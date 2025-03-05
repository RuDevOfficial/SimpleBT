using System;
using System.Collections.Generic;

namespace SimpleBT.Editor.GraphNodes
{
    [System.Serializable]
    public class ConditionGraphNode : ExecutionGraphNode
    {
        public ConditionBox ConditionBox;

        public ConditionGraphNode()
        {
            Title = "Condition";
            ClassReference = "Condition";
        }
        
        public override void GenerateInterface()
        {
            base.GenerateInterface();

            ConditionBox = new ConditionBox();
            ConditionBox.Instantiate();
            extensionContainer.Add(ConditionBox);

            RefreshPorts();
            RefreshExpandedState();
        }

        public override List<string> GetValues()
        {
            List<string> values = new List<string>
            {
                ConditionBox.VariableName.value,
                ConditionBox.Condition.ToString(),
                ConditionBox.VariableChecked.value
            };

            return values;
        }

        public override void ReloadValues(List<string> values)
        {
            ConditionBox.VariableName.value = values[0];
            ConditionBox.DropDown.value = values[1];
            ConditionBox.Condition = (ConditionType)Enum.Parse(typeof(ConditionType), values[1]);
            ConditionBox.VariableChecked.value = values[2];
        }
    }
}