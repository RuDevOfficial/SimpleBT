using SimpleBT.Editor.Data;
using SimpleBT.NonEditor.Tree;
using UnityEngine;

namespace SimpleBT.Editor.BehaviorGeneration
{
    using GraphNodes;
    using SimpleBT.NonEditor.Nodes;
    using Utils;
    
    public static class SBTBehaviorGeneration
    {
        /// <summary>
        /// Generates a subtree. It must have been called from the starting generation logic
        /// </summary>
        /// <param name="collection">The node collection for the new subtree</param>
        /// <param name="parent">The parent node of the behavior</param>
        private static void GenerateSubTree(NodeCollection collection, BehaviorTree parent)
        {
            SBTEditorUtils.CreateFolder("Assets", "SimpleBT");
            SBTEditorUtils.CreateFolder("Assets/SimpleBT", "GraphData");
            
            GraphTreeNode root = null;

            for (int i = 0; i < collection.Nodes.Length; i++)
            {   
                var node = collection.Nodes[i].Node;
                
                if (node is RootGraphNode)
                {
                    root = node;
                    parent.GUID = node.GUID; 
                    continue;
                }
                
                parent.PopulateTreeList(
                    node.ClassReference,
                    node.GUID,
                    node.GetValues()
                );
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
                    parent.LinkNodes(
                        fromGuid,
                        toGuid
                        );
                }
            }

            // Finally check who is connected to the root graph node and assign it
            // as root of the behavior tree
            for (int i = 0; i < collection.Nodes.Length; i++)
            { 
                NodeData data = collection.Nodes[i];
                if (data.fromGUID != parent.GUID) { continue; }
                
                if (data.toGUIDs.Count > 0)
                {
                    Core.Node node = parent.GetNodeByGUID(data.toGUIDs[0]);
                    parent.AssignRoot(node);
                }
                break;
            }

            foreach (var node in parent.CompleteNodeList)
            {
                if (node is BehaviorTree tree && node != parent)
                {
                    BehaviorCollection subCollection = SBTDataManager.LoadBehaviorCollectionToJson(tree.RelatedBranch);
                    GenerateSubTree(subCollection.NodeCollection, tree);
                }
            }
        }
        
        /// <summary>
        /// Generates the behavior tree based off the GraphNode's "Class Reference" field.
        /// If a node is a behavior it calls for a subtree generation
        /// </summary>
        /// <param name="collection">The incoming node collection</param>
        /// <param name="executor">The executor script which will hold the behavior</param>
        public static void Generate(NodeCollection collection, TreeExecutor executor)
        {
            SBTEditorUtils.CreateFolder("Assets", "SimpleBT");
            SBTEditorUtils.CreateFolder("Assets/SimpleBT", "GraphData");
            
            // Create the root SO and assign it to the treeExecutor class 
            BehaviorTree BT = ScriptableObject.CreateInstance<BehaviorTree>();
            BT.name = collection.BehaviorName;
            executor.BT = BT;
            
            GraphTreeNode root = null;

            for (int i = 0; i < collection.Nodes.Length; i++)
            {   
                var node = collection.Nodes[i].Node;
                
                if (node is RootGraphNode)
                {
                    root = node;
                    BT.GUID = node.GUID; 
                    continue;
                }

                BT.PopulateTreeList(
                    node.ClassReference,
                    node.GUID,
                    node.GetValues()
                );
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
                    BT.LinkNodes(
                        fromGuid,
                        toGuid
                        );
                }
            }

            // Finally check who is connected to the root graph node and assign it
            // as root of the behavior tree
            for (int i = 0; i < collection.Nodes.Length; i++)
            { 
                NodeData data = collection.Nodes[i];
                if (data.fromGUID != BT.GUID) { continue; }
                
                if (data.toGUIDs.Count > 0)
                {
                    Core.Node node = BT.GetNodeByGUID(data.toGUIDs[0]);
                    BT.AssignRoot(node);
                }
                break;
            }

            foreach (var node in BT.CompleteNodeList)
            {
                if (node is not BehaviorTree tree) continue;
                
                BehaviorCollection subCollection = 
                    SBTDataManager.LoadBehaviorCollectionToJson(tree.RelatedBranch);
                GenerateSubTree(subCollection.NodeCollection, tree);
            }
        }
    }
}
