using SimpleBT.Composite.Prebuilt;
using SimpleBT.NonEditor.Tree;
using UnityEngine;

[CreateAssetMenu(fileName = "BackAndForthBT", menuName = "SimpleBT/Behaviours/Examples/BackAndForth")]
public class BackAndForthBT : BehaviourTree
{
    protected override void Build()
    {
        _root = new RepeatForever(
            new Sequence(
                new ACTION_Rigidbody2DJump("-1", "1", "200"),
                new ACTION_Wait("3"),
                new ACTION_Rigidbody2DJump("1", "1", "200"),
                new ACTION_Wait("3")
                )
            );
    }
}
