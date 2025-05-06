using System.Linq;
using System.Collections.Generic;
using SimpleBT.Editor.GraphNodes;
using UnityEngine;
using UnityEngine.UIElements;

namespace SimpleBT.Editor.GraphNodes
{
    [System.Serializable]
    public class GraphAction_GetRandomChildFromParent : GraphAction
    {
        private TextField _parentField;
        private TextField _keyField;

        [SerializeField] private string _parentKey;
        [SerializeField] private string _key;
        
        public GraphAction_GetRandomChildFromParent()
        {
            Title = "Get Random Child From Parent";
            ClassReference = "Action_GetRandomChildFromParent";
        }

        public override void GenerateInterface()
        {
            base.GenerateInterface();
            
            _parentField = new TextField("Parent GameObject: ");
            _keyField = new TextField("Key: ");
            
            _parentField.RegisterValueChangedCallback(evt => _parentKey = evt.newValue);
            _keyField.RegisterValueChangedCallback(evt => _key = evt.newValue);
            
            extensionContainer.Add(_parentField);
            extensionContainer.Add(_keyField);
        }

        public override List<string> GetValues()
        {
            return new List<string>()
            {
                _parentKey,
                _key
            };
        }

        public override void ReloadValues(List<string> values)
        {
            _parentField.value = values[0];
            _keyField.value = values[1];
        }
    }
}
