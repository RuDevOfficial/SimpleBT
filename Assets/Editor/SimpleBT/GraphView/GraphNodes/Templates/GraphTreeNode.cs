using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace SimpleBT.Editor.GraphNodes
{
    [System.Serializable]
    public class GraphTreeNode : Node
    {
        [SerializeField] public string GUID;
        [SerializeField] public Rect Rect;

        protected string NodeName;
    
        public GraphTreeNode() { }

        public virtual void Instantiate()
        {
            GUID = Guid.NewGuid().ToString();
        }

        public virtual void Draw() { title = NodeName; }
        
        public virtual List<string> GetValues() { return new List<string>(); }

        public virtual void ReloadValues(List<string> values) { }
    }
}



