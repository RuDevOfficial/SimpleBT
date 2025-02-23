using SimpleBT.Core;
using UnityEngine;

namespace SimpleBT.NonEditor.Nodes
{
    public class Action_SetActiveToggle : Action_SetActive
    {
        protected override Status Tick()
        {
            if (FoundGameObject()) {
                _gameObject.SetActive(!_gameObject.activeSelf); 
                return Status.Success;
            } 
            else {
                Debug.LogWarning($"Could not find GameObject of name {KeyGameobjectName}");
                return Status.Failure;
            }
        }
    }
}
