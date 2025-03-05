using System;
using System.Collections.Generic;
using SimpleBT.Editor.Data;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace SimpleBT.Editor.BehaviorGeneration
{
    using GraphNodes;
    using SimpleBT.NonEditor.Tree;
    using Utils;
    
    public static class SBTBehaviorGeneration
    {
        public static void Generate(SBTGraphView graph, NodeCollection collection, TreeExecutor executor)
        {
            SBTUtils.CreateFolder("Assets", "SimpleBT");
            SBTUtils.CreateFolder("Assets/SimpleBT", "Behaviors");
            
            // Create the root SO and assign it to the treeExecutor class 
            BehaviorTree behaviorTree = ScriptableObject.CreateInstance<BehaviorTree>();
            executor.BT = behaviorTree;

            GraphTreeNode root = null;

            List<GraphElement> elements = graph.graphElements.ToList();

            // First, generate and add these nodes to a list
            for (int i = 0; i < elements.Count; i++)
            {
                if (elements[i] is GraphTreeNode node)
                {
                    if (node.title == "Root")
                    {
                        root = node;
                        behaviorTree.GUID = node.GUID; continue;
                    }
                    
                    behaviorTree.PopulateTreeList(
                        node.ClassReference,
                        node.GUID,
                        node.GetValues()
                    );
                }
            }

            // Second, check the GUID of each node, see if they have outgoing connections
            // and link them
            for (int i = 0; i < collection.Nodes.Length; i++)
            { 
                NodeData data = collection.Nodes[i];
                string fromGuid = data.fromGUID;

                if (data.fromGUID == root.GUID) { continue; }
                
                foreach (string toGuid in data.toGUIDs)
                {
                    behaviorTree.LinkNodes(
                        fromGuid,
                        toGuid,
                        behaviorTree
                        );
                }
            }

            // Finally check who is connected to the root graph node and assign it
            // as root of the behavior tree
            for (int i = 0; i < collection.Nodes.Length; i++)
            { 
                NodeData data = collection.Nodes[i];
                if (data.fromGUID != behaviorTree.GUID) { continue; }
                
                if (data.toGUIDs.Count > 0)
                {
                    Core.Node node = behaviorTree.GetNodeByGUID(data.toGUIDs[0]);
                    behaviorTree.AssignRoot(node);
                }
                break;
            }
        }
    }
}
