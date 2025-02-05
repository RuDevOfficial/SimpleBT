using System.Collections.Generic;
using UnityEditor.Graphs.AnimationBlendTree;
using UnityEngine;

namespace SimpleBT.Editor.Data
{
    using SimpleBT.Editor.GraphNodes;
    
    [System.Serializable]
    public class NodeData
    {
        [SerializeReference] public GraphTreeNode Node;
        public string fromGUID;
        public List<string> toGUIDs = new List<string>();
        
        // Specific node data for Condition
        public string VariableName;
        public ConditionBox.Condition Condition;
        public string VariableCheckName;
    }
}
