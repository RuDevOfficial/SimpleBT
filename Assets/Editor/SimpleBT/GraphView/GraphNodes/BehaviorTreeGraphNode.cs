using System;
using System.Collections.Generic;
using SimpleBT.Editor.Utils;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace SimpleBT.Editor.GraphNodes
{
    [System.Serializable]
    public class BehaviorTreeGraphNode : GraphTreeNode
    {
        [SerializeReference] private DropdownField _dropdown;
        [SerializeField] private string lastDropDownValue;
        public string ReferencedBehaviorName;
        
        public BehaviorTreeGraphNode()
        {
            Title = "Branch";
            ClassReference = "BehaviorTree";

            RegisterCallback<DragExitedEvent>(OnDragExited);
        }

        public override void GenerateInterface()
        {
            this.GeneratePort(Direction.Input, Port.Capacity.Single);
            _dropdown = new DropdownField(SBTEditorUtils.GetAllBehaviorNames(), 0, name =>
            {
                if (name == ReferencedBehaviorName)
                {   
                    EditorUtility.DisplayDialog("Recursive Error", "Cannot call itself as a branch. Memory leak would occur", "OK");
                    return string.IsNullOrEmpty(lastDropDownValue) ? "Choose Behavior" : lastDropDownValue;
                }
                
                lastDropDownValue = name;
                return lastDropDownValue;
            });
            extensionContainer.Add(_dropdown);
        }

        public override List<string> GetValues() {
            return new List<string>() { ReferencedBehaviorName, lastDropDownValue };
        }

        public override void ReloadValues(List<string> values)
        {
            ReferencedBehaviorName = values[0];
            lastDropDownValue = values[1];
            _dropdown.value = values[1];
        }
        
        private void OnDragExited(DragExitedEvent evt)
        {
            foreach (Object obj in DragAndDrop.objectReferences) {
                if (SBTEditorUtils.TryGetBehaviorFile(obj, out string fileName)) {
                    _dropdown.value = fileName;
                    break;
                }
            }
        }
    }
    
    // TODO Modify the key assignment code so an interface is required in order to reload values
    // Specially since some actions do not need to assign any data
    public interface IGraphNodeKeyAssignable
    {
        public void ReloadValues(List<string> values);
        public List<string> GetValues();
    }
}
