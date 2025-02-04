using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace SimpleBT.Editor.GraphNodes
{
    [System.Serializable]
    public class GraphTreeNode : Node
    {
        [SerializeField] public string GUID;
        [SerializeField] public Rect Rect;

        public string NodeName;
    
        public GraphTreeNode() { }

        public virtual void Instantiate()
        {
            GUID = Guid.NewGuid().ToString();
        }

        public virtual void Draw() { title = NodeName; }
    }
}



