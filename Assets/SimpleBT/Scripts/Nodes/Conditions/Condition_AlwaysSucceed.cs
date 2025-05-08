namespace SimpleBT.NonEditor.Nodes
{
    public class Condition_AlwaysSucceed : Condition
    {
        protected override bool Check() { return true; }
        protected override void Initialize() { }
    }
}
