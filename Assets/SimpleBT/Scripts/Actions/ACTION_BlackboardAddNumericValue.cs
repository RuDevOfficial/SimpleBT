using System.Globalization;
using SimpleBT.Core;
using UnityEngine;

public class ACTION_BlackboardAddNumericValue : ActionNode
{
    private string _keyName;
    private string _value;
    
    public ACTION_BlackboardAddNumericValue(string keyName, string value)
    {
        _keyName = keyName;
        _value = value;
    }
    protected override void Initialize() { }

    protected override Status Tick()
    {
        if (int.TryParse(_value, NumberStyles.Integer, CultureInfo.InvariantCulture, out var newInt))
        {
            SbtBlackboard.AddNewVariable(_keyName, newInt); 
            Debug.Log("Int value " + newInt + " added to " + SbtBlackboard.gameObject.name);
        }
        else
        {
            float newFloat = float.Parse(_value, NumberStyles.Float, CultureInfo.InvariantCulture);
            SbtBlackboard.AddNewVariable(_keyName, newFloat); 
            Debug.Log("Float value " + newFloat + " added to " + SbtBlackboard.gameObject.name);
        }

        return Status.Success;
    }
}