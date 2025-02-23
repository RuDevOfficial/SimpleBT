using System;
using System.Collections.Generic;
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
        public string GUID;
        protected SBTBlackboard blackboard;

        public Node() { }
        
        // This method is split in two in order for Conditions and Actions to implement
        // its own order of operations.
        public virtual Status OnTick() { return Tick(); }

        public virtual void RegisterBlackboard(SBTBlackboard sbtBlackboard)
        {
            blackboard = sbtBlackboard;
        }

        protected abstract Status Tick();

        // Every child node of a composite will have the blackboard registered
        // thanks to a recursive call
        
        public virtual void AddChild(Node child) { }
        
        public virtual void AssignKeys(List<string> keys) { }
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

    // This class is meant to be there for readability
    public abstract class Action_Node : ExecutionNode
    {
        public Action_Node() : base() {}
    }

    public enum Status
    {
        Running,
        Success,
        Failure
    }
}