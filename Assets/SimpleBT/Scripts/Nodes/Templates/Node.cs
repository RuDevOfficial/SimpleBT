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
        public Node Parent;
        
        private bool _isInitialized = false;
        protected SBTBlackboard _blackboard;

        public virtual Status OnTick()
        {
            if (_isInitialized) return Tick();
            
            Initialize(); _isInitialized = true;
            return Tick(); 
        }
        
        protected virtual void Initialize() { }
        protected abstract Status Tick();
        public virtual void OnAbort() {  }
        public virtual void OnDrawGizmos() {  }
        
        public virtual void RegisterBlackboard(SBTBlackboard sbtBlackboard) { _blackboard = sbtBlackboard; }
    }
    
    public enum Status { Running, Success, Failure }
    public enum RigidbodyMoveFlag { NONE, X, Y, Z, XY, XZ, YZ }
}
