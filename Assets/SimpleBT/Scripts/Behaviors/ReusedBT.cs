using System;
using SimpleBT.Composite.Prebuilt;
using SimpleBT.NonEditor.Tree;
using UnityEngine;

[CreateAssetMenu(fileName = "Reused", menuName = "SimpleBT/Behaviours/Examples/Reused")]
public class ReusedBT : BehaviourTree
{
    private SimpleDebugActionBT SimpleDebugActionBT;

    protected override void Build()
    {
        SimpleDebugActionBT = CreateInstance<SimpleDebugActionBT>();

        _root =
            new Sequence(
                new ACTION_DebugAndSucceed("Wait, next one is reused!"),
                SimpleDebugActionBT
                )
            ;
    }
}