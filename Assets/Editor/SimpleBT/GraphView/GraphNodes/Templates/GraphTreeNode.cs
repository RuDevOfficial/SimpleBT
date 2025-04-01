using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace SimpleBT.Editor.GraphNodes
{
    [System.Serializable]
    public abstract class GraphTreeNode : Node
    {
        [SerializeField] public string GUID;
        [SerializeField] public Rect Rect;

        public string ClassReference;
        public string Title;

        public GraphTreeNode() { }
        
        public GraphTreeNode(string path) : base(path) { }
        
        public virtual void Instantiate() {
            GUID = Guid.NewGuid().ToString();
        }

        public virtual void Set() { title = Title; }

        public abstract void GenerateInterface();

        public abstract List<string> GetValues();

        public abstract void ReloadValues(List<string> values);
    }
}



