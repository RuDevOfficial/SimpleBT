using UnityEngine;

namespace SimpleBT.Core
{
    using NonEditor;

    public interface INode
    {
         Status OnTick();
         void RegisterBlackboard(SBTBlackboard sbtBlackboard);
    }
    
    public abstract class Node : ScriptableObject, INode
    {
        protected SBTBlackboard SbtBlackboard;

        public Node() { }
        
        // This method is split in two in order for Conditions and Actions to implement
        // its own order of operations.
        public virtual Status OnTick() { return Tick(); }

        public virtual void RegisterBlackboard(SBTBlackboard sbtBlackboard)
        {
            SbtBlackboard = sbtBlackboard;
        }

        protected abstract Status Tick();

        // Every child node of a composite will have the blackboard registered
        // thanks to a recursive call
    }
    
    
    public abstract class ExecutionNode : Node
    {  
        protected bool _initialized = false;
        
        public ExecutionNode() : base () { }
        
        // This method is meant to be overwritten
        protected abstract void Initialize();
        
        // This method is overriden to allow for initialization 
        // before logic execution
        public override Status OnTick()
        {
            if (_initialized == false)
            {
                _initialized = true;
                Initialize();
            }

            return Tick();
        }
    }

    public abstract class ConditionNode : ExecutionNode
    {
        public ConditionNode() : base() { }
        
        // This method converts a bool value into the necessary enum values
        protected override Status Tick() { return Check() == true ? Status.Success : Status.Failure; }
        
        // This method is meant to be overwritten
        protected abstract bool Check();
    }

    // This class is meant to be there for readability
    public abstract class ActionNode : ExecutionNode
    {
        public ActionNode() : base() {}
    }

    public enum Status
    {
        Running,
        Success,
        Failure
    }
}