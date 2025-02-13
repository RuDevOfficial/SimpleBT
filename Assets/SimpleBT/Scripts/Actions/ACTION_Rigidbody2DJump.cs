using SimpleBT.Core;
using UnityEngine;
public class ACTION_Rigidbody2DJump : ActionNode
{
    private Rigidbody2D _rb2D;
    private string _keyX, _keyY;
    private string _keyForce;

    private Vector2 vector;
    private float force;

    public ACTION_Rigidbody2DJump(string keyX, string keyY, string keyForce)
    {
        _keyX = keyX;
        _keyY = keyY;
        _keyForce = keyForce;
    }

    protected override void Initialize()
    {
        _rb2D = blackboard.gameObject.GetComponent<Rigidbody2D>();
        force = blackboard.GetValue<float>(_keyForce);

        vector = new Vector2(blackboard.GetValue<float>(_keyX), blackboard.GetValue<float>(_keyY));
    }

    protected override Status Tick()
    {
        if(_rb2D == null) { return Status.Failure; }
        
        vector.Normalize();
        vector *= force;
        
        _rb2D.AddForce(vector);
        return Status.Success;
    }

}
