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
            BehaviourTree behaviourTree = ScriptableObject.CreateInstance<BehaviourTree>();
            executor.BT = behaviourTree;

            GraphTreeNode root = null;

            List<GraphElement> elements = graph.graphElements.ToList();

            for (int i = 0; i < elements.Count; i++)
            {
                if (elements[i] is GraphTreeNode node)
                {
                    if (node.title == "Root")
                    {
                        root = node;
                        behaviourTree.GUID = node.GUID; continue;
                    }
                    
                    SBTNonEditorUtils.PopulateTreeList(
                        node.title,
                        node.GUID,
                        behaviourTree
                    );
                }
            }

            for (int i = 0; i < collection.Nodes.Length; i++)
            { 
                NodeData data = collection.Nodes[i];
                string fromGuid = data.fromGUID;

                if (data.fromGUID == root.GUID) { continue; }
                
                foreach (string toGuid in data.toGUIDs)
                {
                    behaviourTree.LinkNodes(
                        fromGuid,
                        toGuid,
                        behaviourTree
                        );
                }
            }

            for (int i = 0; i < collection.Nodes.Length; i++)
            { 
                NodeData data = collection.Nodes[i];
                if (data.fromGUID != behaviourTree.GUID) { continue; }
                
                if (data.toGUIDs.Count > 0)
                {
                    Core.Node node = behaviourTree.GetNodeByGuid(data.toGUIDs[0]);
                    behaviourTree.AssignRoot(node);
                }
                break;
            }

            // Populate the tree first
            /*
            foreach (NodeData data in collection.Nodes)
            {
                if (data.Node.title == "Root") { continue; }



                SBTNonEditorUtils.PopulateTreeList(
                    data.Node.title,
                    data.fromGUID,
                    behaviourTree);
                break;

                foreach (GraphElement element in elements)
                {
                    if (element is not GraphTreeNode node) continue;

                    if (node.GUID == data.fromGUID)
                    {
                        SBTNonEditorUtils.PopulateTreeList(
                            node.title,
                            node.GUID,
                            behaviourTree);
                        break;
                    }
                }
            }
            */
        }
    }
}
