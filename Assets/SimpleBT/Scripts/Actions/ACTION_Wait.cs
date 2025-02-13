using SimpleBT.Core;
using UnityEngine;
public class ACTION_Wait : ActionNode
{
    private string _keyTime;

    public ACTION_Wait(string keyTime)
    {
        _keyTime = keyTime;
    }

    private float _time;
    private float _currentTime = 0.0f;

    protected override void Initialize()
    {
        _time = blackboard.GetValue<float>(_keyTime);
    }

    protected override Status Tick()
    {
        if (_currentTime < _time)
        {
            _currentTime += Time.deltaTime;
            return Status.Running;
        }
        else
        {
            _currentTime = 0.0f;
            return Status.Success;
        }
    }
}