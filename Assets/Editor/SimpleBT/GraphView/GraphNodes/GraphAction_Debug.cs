using System;
using System.Collections.Generic;
using System.Linq;
using SimpleBT.Editor.Utils;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace SimpleBT.Editor.GraphNodes
{
    public class GraphAction_Debug : GraphTreeNode
    {
        public string DebugMessage = "Default Message";
        public ActionStatus Status = ActionStatus.Success;
        private TextField messageField;
        private DropdownField DropDown;
        
        public GraphAction_Debug() { NodeName = "Action_Debug"; }
        
        public override void Draw()
        {
            base.Draw();
            
            this.GeneratePort(Direction.Input, Port.Capacity.Single);

            TextElement title = new TextElement();
            title.text = "  Message: ";

            messageField = new TextField();
            messageField.multiline = true;
            messageField.RegisterValueChangedCallback(evt => DebugMessage = evt.newValue);
            
            string[] conditions = Enum.GetNames(typeof(ActionStatus));
            DropDown = new DropdownField(conditions.ToList(), 0, FormatSelectedValueCallback);
            
            extensionContainer.Add(DropDown);
            extensionContainer.Add(title);
            extensionContainer.Add(messageField);
            
            RefreshExpandedState();
        }

        public override List<string> GetValues() { return new List<string>()
        {
            DebugMessage,
            Status.ToString()
        }; }

        public override void ReloadValues(List<string> values)
        {
            DebugMessage = values[0];
            messageField.value = DebugMessage;
            
            Status = (ActionStatus)Enum.Parse(typeof(ActionStatus), values[1]);
            DropDown.value = values[1];
        }
        
        private string FormatSelectedValueCallback(string arg)
        {
            Status = (ActionStatus)Enum.Parse(typeof(ActionStatus), arg);
            return arg;
        }
    }

    public enum ActionStatus
    {
        Success,
        Failure
    }
}