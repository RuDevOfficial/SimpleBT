using System.Collections.Generic;
using SimpleBT.Editor.GraphNodes;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace SimpleBT.Editor
{
    [System.Serializable]
    public class SBTGraphView : GraphView
    {
        private UnityEditor.Experimental.GraphView.Blackboard _blackboard;
        private SBTEditorWindow _editor;
        private SBTSearchWindow _searchWindow;
        
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

        public GraphTreeNode GetNodeByGUID(string guid)
        {
            foreach (Node node in nodes)
            {
                GraphTreeNode BTnode = (GraphTreeNode)node;
                if(BTnode.GUID == guid) { return BTnode; }
            }
            
            Debug.LogError($"Couldn't find node of guid {guid}");
            return null;
        }
    }
}
    