namespace SimpleBT.NonEditor.Nodes
{
    public class Condition_AlwaysFail : Condition
    {
        public override bool Check() { return false; }
        protected override void Initialize() { }
    }
}
