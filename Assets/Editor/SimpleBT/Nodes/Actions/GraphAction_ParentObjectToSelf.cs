using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace SimpleBT.Editor.GraphNodes
{
    [System.Serializable]
    public class GraphAction_ParentObjectToSelf : GraphAction
    {
        private TextField _gameObjectTF;
        
        [SerializeField] private string _keyGameObject;
        
        public GraphAction_ParentObjectToSelf()
        {
            Title = "Parent\nObject To Self";
            ClassReference = "Action_ParentObjectToSelf";
        }

        public override void GenerateInterface()
        {
            base.GenerateInterface();
            
            _gameObjectTF = new TextField("Key: ");
            _gameObjectTF.RegisterValueChangedCallback(evt => _keyGameObject = evt.newValue);
            extensionContainer.Add(_gameObjectTF);
        }

        public override List<string> GetValues() { return new List<string>() { _keyGameObject }; }
        
        public override void ReloadValues(List<string> values) { _gameObjectTF.value = values[0]; }
    }

}
