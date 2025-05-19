using System.Collections.Generic;
using SimpleBT.Core;
using UnityEngine;

namespace SimpleBT.NonEditor.Nodes
{
    // TODO make it work if it has a value attached and not just written on the node
    public class Action_SetActive : Node
    {
        [SerializeField] private string Key;
        [SerializeField] protected string KeyGameobjectName;
        [SerializeField] private string KeyTag;
        [SerializeField] private string KeyInstanceID;
        [SerializeField] private string KeySetActive;
        
        private int _instanceID;
        private bool _setActive;

        protected GameObject _gameObject;
        
        public void AssignKeys(List<string> values)
        {
            KeyGameobjectName = values[0];
            KeyTag = values[1];
            KeySetActive = values[2];
            KeyInstanceID = values[3];
        }
        
        protected override void Initialize()
        {
            _setActive = _blackboard.GetValue<bool>(KeySetActive);
            _gameObject = _blackboard.GetComplexValue<GameObject>($"{KeyGameobjectName}, {KeyTag}, {KeyInstanceID}");
        }
        
        protected override Status Tick()
        {
            if (FoundGameObject()) {
                _gameObject.SetActive(_setActive); 
                return Status.Success;
            } 
            else {
                Debug.LogWarning($"Could not find GameObject of name {KeyGameobjectName}");
                return Status.Failure;
            }
        }
        
        protected bool FoundGameObject() { return _gameObject != null; }
    }

}
