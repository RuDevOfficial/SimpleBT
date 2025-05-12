using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace SimpleBT.Editor.GraphNodes
{
    [System.Serializable]
    public class GraphAction_GetRandomChildFromParent : GraphAction
    {
        private TextField _parentTF;
        private TextField _keyTF;

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
            
            _parentTF = new TextField("Parent GameObject: ");
            _keyTF = new TextField("Key: ");
            
            _parentTF.RegisterValueChangedCallback(evt => _parentKey = evt.newValue);
            _keyTF.RegisterValueChangedCallback(evt => _key = evt.newValue);
            
            extensionContainer.Add(_parentTF);
            extensionContainer.Add(_keyTF);
        }

        public override List<string> GetValues()
        {
            return new List<string>() {
                _parentKey,
                _key
            };
        }

        public override void ReloadValues(List<string> values)
        {
            _parentTF.value = values[0];
            _keyTF.value = values[1];
        }
    }
}
