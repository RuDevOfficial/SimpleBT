using System.Collections.Generic;
using SimpleBT.Core;
using SimpleBT.Editor.Utils;
using SimpleBT.NonEditor.Nodes;
using UnityEngine;
using UnityEngine.UIElements;

namespace SimpleBT.Editor.GraphNodes
{
    public class GraphCondition_Compare : GraphCondition
    {
        protected TextField _firstValueField;
        protected DropdownField _comparisonField;
        
        [SerializeField] private string _keyValue;
        [SerializeField] private string _keyComparison;
        
        public GraphCondition_Compare()
        {
            Title = "Compare"; // Rename like: GraphCondition_DoSomething -> Do Something
            ClassReference = "Condition_Compare"; // Rename like: GraphCondition_DoSomething -> Condition_DoSomething
        }

        public override void GenerateInterface()
        {
            base.GenerateInterface();
            
            _firstValueField = new TextField("Value A: ");
            _comparisonField = new DropdownField("Compare To:", new List<string>(){"Empty"}, 0);
            
            _firstValueField.RegisterValueChangedCallback(evt => _keyValue = evt.newValue);
            _comparisonField.RegisterValueChangedCallback(evt => _keyComparison = evt.newValue);
            
            extensionContainer.Add(_firstValueField);
            extensionContainer.Add(_comparisonField);
        }

        public override List<string> GetValues()
        {
            return new List<string>()
            {
                _keyValue,
                _keyComparison
            };
        }

        public override void ReloadValues(List<string> values)
        {
            _firstValueField.value = values[0];
            _comparisonField.value = values[1];
        }
    }

    public class GraphCondition_CompareTwoValues : GraphCondition_Compare
    {
        private TextField _secondValueField;
        [SerializeField] private string _keySecondValue;
        
        public GraphCondition_CompareTwoValues()
        {
            Title = "Compare Two Values"; // Rename like: GraphCondition_DoSomething -> Do Something
            ClassReference = "Condition_CompareTwoValues"; // Rename like: GraphCondition_DoSomething -> Condition_DoSomething
        }

        public override void GenerateInterface()
        {
            base.GenerateInterface();
            _comparisonField.choices = SBTEditorUtils.ReturnEnumToList<Comparison>();
            
            _secondValueField = new TextField("Value B: ");
            _secondValueField.RegisterValueChangedCallback(evt => _keySecondValue = evt.newValue);
            
            extensionContainer.Add(_secondValueField);
        }
        
        public override List<string> GetValues()
        {
            List<string> values = base.GetValues();
            values.Add(_keySecondValue);
            return values;
        }
        
        public override void ReloadValues(List<string> values)
        {
            base.ReloadValues(values);
            _secondValueField.value = values[2];
        }
    }
}
