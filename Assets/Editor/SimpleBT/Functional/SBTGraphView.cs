using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace SimpleBT.Editor
{
    using Data;
    using GraphNodes;
    using Utils;

    [Serializable]
    public class SBTGraphView : GraphView
    {
        private UnityEditor.Experimental.GraphView.Blackboard _blackboard;
        public SBTEditorWindow EditorReference;
        private SBTSearchWindow _searchWindow;
        
        public SBTGraphView(SBTEditorWindow editorReference)
        {
            EditorReference = editorReference;
            
            Insert(0, new GridBackground());
            
            // Adding basic manipulators
            this.AddManipulator(new ContentZoomer());
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
            
            //Adding Search Window
            if (_searchWindow == null)
            {
                _searchWindow = ScriptableObject.CreateInstance<SBTSearchWindow>();
                _searchWindow.Initialize(this);
            }
            
            nodeCreationRequest = context => SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), _searchWindow);
            
            // Adding style
            SBTSettingsData settingsData = SBTDataManager.LoadSettingsFromJson();
            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(settingsData.StyleDataPath + "/SBTStyles.uss");
            styleSheets.Add(styleSheet);
        }

        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            base.BuildContextualMenu(evt);

            // Removes all entries except "Create Node"
            for (int i = evt.menu.MenuItems().Count - 1; i > 0 ; i--) { evt.menu.RemoveItemAt(i); }
            
            // Adds custom entries
            evt.menu.AppendAction("Create Custom Node", SBTEditorUtils.ShowTemplatePopupWindow);
            
            // Create Custom Node SO Script
            evt.menu.AppendAction("Create Custom Entries SO", SBTEditorUtils.CreateCustomEntriesSOAsset);
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

            if (isSearchWindow) { worldMousePosition -= EditorReference.position.position; }

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
    