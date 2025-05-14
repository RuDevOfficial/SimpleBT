using System.Collections.Generic;
using SimpleBT.Editor.GraphNodes;
using UnityEditor.Graphs.AnimationBlendTree;
using UnityEngine;

namespace SimpleBT.Editor.Data
{
    using Editor.GraphNodes;
    
    [System.Serializable]
    public class NodeData
    {
        [SerializeReference] public GraphTreeNode Node;
        public string fromGUID;
        public List<string> toGUIDs = new List<string>();
        public List<string> Values = new List<string>();
    }
}
