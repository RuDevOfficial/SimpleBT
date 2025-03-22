using System.Collections.Generic;
using SimpleBT.Core;
using SimpleBT.Editor.Utils;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace SimpleBT.Editor.GraphNodes
{
    [System.Serializable]
    public class BehaviorTreeGraphNode : GraphTreeNode
    {
        public string BranchName;
        public TextField _textField;
        
        public BehaviorTreeGraphNode()
        {
            Title = "Branch";
            ClassReference = "BehaviorTree";
        }

        public override void GenerateInterface()
        {
            this.GeneratePort(Direction.Input, Port.Capacity.Single);
            
            _textField = new TextField();
            _textField.RegisterValueChangedCallback((evt) =>
            {
                BranchName = evt.newValue.FilterValue();
            });
            
            extensionContainer.Add(_textField);
        }

        public override List<string> GetValues() {
            return new List<string>() { BranchName };
        }

        public override void ReloadValues(List<string> values) {
            BranchName = values[0];
            _textField.value = values[0];
        }
    }
}
