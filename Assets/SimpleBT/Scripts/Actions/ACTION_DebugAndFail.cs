using SimpleBT.Core;
using UnityEngine;
public class ACTION_DebugAndFail : ActionNode
{
    private string _keyMessage;
    public ACTION_DebugAndFail(string keyMessage)
    {
        _keyMessage = keyMessage;
    }

    private string _message;

    protected override void Initialize()
    {
        _message = SbtBlackboard.GetValue<string>(_keyMessage);
    }

    protected override Status Tick()
    {
        Debug.Log(_message);
        return Status.Failure;
    }
}
