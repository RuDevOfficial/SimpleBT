using System.Collections.Generic;
using SimpleBT;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class SBTGraphView : GraphView
{
    private SBTEditorWindow _editor;
    private SBTSearchWindow _searchWindow;
    private SimpleTree _tree;
    
    public SBTGraphView(SBTEditorWindow editor)
    {
        _editor = editor;
        
        Insert(0, new GridBackground());
        
        // Adding basic manipulators
        this.AddManipulator(new ContentZoomer());
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());
        
        //Adding Contextual Menu
        if (_searchWindow == null)
        {
            _searchWindow = ScriptableObject.CreateInstance<SBTSearchWindow>();
            _searchWindow.Initialize(this);
        }
        
        nodeCreationRequest = context => SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), _searchWindow);
        
        // Adding style
        var styeSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/SimpleBT/SBTStyles.uss");
        styleSheets.Add(styeSheet);
    }

    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        List<Port> compatiblePorts = new List<Port>();

        ports.ForEach(port =>
        {
            if (startPort == port) { return; }
            if (startPort.direction == port.direction) { return; }
            if (startPort.node == port.node) { return; }
            
            compatiblePorts.Add(port);
        });

        return compatiblePorts;
    }

    #region Repositioning Methods
    
    public Vector2 GetPrecisePosition(DropdownMenuAction actionEvent)
    {
        var viewPosition = new Vector2(viewTransform.position.x, viewTransform.position.y);
        return (actionEvent.eventInfo.localMousePosition - viewPosition) / viewTransform.scale;
    }
    
    public Vector2 GetLocalMousePosition(Vector2 position, bool isSearchWindow = false)
    {
        Vector2 worldMousePosition = position;

        if (isSearchWindow) {
            worldMousePosition -= _editor.position.position;
        }

        Vector2 localMousePosition = contentViewContainer.WorldToLocal(worldMousePosition);
        return localMousePosition;
    }
    
    #endregion

    public BehaviourTreeNode GetNodeByGUID(string guid)
    {
        foreach (Node node in nodes)
        {
            BehaviourTreeNode BTnode = (BehaviourTreeNode)node;
            if(BTnode.GUID == guid) { return BTnode; }
        }
        
        Debug.LogError($"Couldn't find node of guid {guid}");
        return null;
    }
    
    /*
    void OnElementsDeleted()
    {
        deleteSelection = ((operationName, user) =>
        {
            Type edgeType = typeof(Edge);
            
            List<GraphBTNode> nodesToDelete = new List<GraphBTNode>();
            List<Edge> edgesToDelete = new List<Edge>();

            foreach (GraphElement element in selection) {
                if (element is RootNode) { }
                else
                {
                    if (element is GraphBTNode node) {
                        nodesToDelete.Add(node);
                    }
                }

                if (element.GetType() == edgeType)
                {
                    Edge edge = (Edge)element;
                    edgesToDelete.Add(edge);
                }
            }

            DeleteElements(edgesToDelete);
            
            foreach (GraphBTNode node in nodesToDelete) {
                
                node.DisconnectAllPorts();
                RemoveElement(node);
            }
        });
    }*/


}