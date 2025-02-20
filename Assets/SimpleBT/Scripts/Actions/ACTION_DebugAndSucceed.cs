using UnityEngine;
using SimpleBT.Core;

public class ACTION_DebugAndSucceed : Action_Node
{
    private string _keyMessage;
    public ACTION_DebugAndSucceed() { _keyMessage = "Default message"; }
    public ACTION_DebugAndSucceed(string message) { _keyMessage = message; }

    private string _message;

    protected override void Initialize()
    {
        _message = blackboard.GetValue<string>(_keyMessage);
    }

    protected override Status Tick()
    {
        Debug.Log(_message);
        return Status.Success;
    }
}