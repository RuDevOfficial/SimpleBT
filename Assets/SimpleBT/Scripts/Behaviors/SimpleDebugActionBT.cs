using SimpleBT.Composite.Prebuilt;
using SimpleBT.NonEditor.Tree;
using UnityEngine;

[CreateAssetMenu(fileName = "SimpleDebugAction", menuName = "SimpleBT/Behaviours/Examples/SimpleDebugAction")]
public class SimpleDebugActionBT : BehaviourTree
{
    protected override void Build()
    {
        _root =
            new Sequence(
                new ACTION_DebugAndSucceed("First I do this and wait..."),
                new ACTION_Wait("3"),
                new ACTION_DebugAndSucceed("and I did wait that much!")
                );
    }
}