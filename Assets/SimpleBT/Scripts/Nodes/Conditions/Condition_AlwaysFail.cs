namespace SimpleBT.NonEditor.Nodes
{
    public class Condition_AlwaysFail : Condition
    {
        protected override bool Check() { return false; }
    }
}
