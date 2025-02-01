using System.Collections.Generic;
using SimpleBT;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

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
        
        // Adding contextual items
        //this.AddManipulator(CreateNewContextualMenuManipulator("Create Node"));
        
        // Adding style
        var styeSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/SimpleBT/SBTStyles.uss");
        styleSheets.Add(styeSheet);
        
        // Override Deletion callback
        //OnElementsDeleted();
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
    
    #endregion

    #region Node Creation, Deletion & Modification

    public void CreateNode(System.Type type)
    {
        
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
    
    #endregion
    
    #region Saving

    public void Save()
    {

    }
    
    #endregion

}