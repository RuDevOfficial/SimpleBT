using System.Collections.Generic;
using SimpleBT.NonEditor;
using UnityEngine;

namespace SimpleBT.Core
{
    public interface INodeKeyAssignable { public void AssignKeys(List<string> keys); }
    public interface INodeMother { public void AddChild(Node child); }
    
    public abstract class Node : ScriptableObject
    {
        public string GUID;
        protected SBTBlackboard blackboard;

        public virtual Status OnTick() { return Tick(); }
        
        protected abstract Status Tick();
        public virtual void OnAbort() {  }
        public virtual void OnDrawGizmos() {  }
        
        public virtual void RegisterBlackboard(SBTBlackboard sbtBlackboard) { blackboard = sbtBlackboard; }
    }
    
    public enum Status { Running, Success, Failure }
    public enum RigidbodyMoveFlag { NONE, X, Y, Z, XY, XZ, YZ }
}
