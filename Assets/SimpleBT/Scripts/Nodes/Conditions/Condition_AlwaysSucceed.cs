namespace SimpleBT.NonEditor.Nodes
{
    public class Condition_AlwaysSucceed : Condition
    {
        public override bool Check() { return true; }
        protected override void Initialize() { }
    }
}
