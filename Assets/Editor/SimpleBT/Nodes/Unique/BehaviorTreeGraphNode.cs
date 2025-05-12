using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace SimpleBT.Editor.GraphNodes
{
    using UnityEditor.Experimental.GraphView;
    using Utils;
    using Object = UnityEngine.Object;

    [System.Serializable]
    public class BehaviorTreeGraphNode : GraphTreeNode
    {
        [SerializeReference] private DropdownField _dropdown;
        [SerializeField] private string lastDropDownValue;
        [SerializeField] private int _lastValue;
        
        public string ReferencedBehaviorName;
        
        public BehaviorTreeGraphNode()
        {
            Title = "Behavior Tree";
            ClassReference = "BehaviorTree";

            RegisterCallback<DragExitedEvent>(OnDragExited);
        }

        public override void GenerateInterface()
        {
            this.GeneratePort(Direction.Input, Port.Capacity.Single);
            
            AddToClassList("Branch");
            
            _dropdown = new DropdownField(SBTEditorUtils.GetAllBehaviorNames(), 0, name =>
            {
                lastDropDownValue = name;
                return lastDropDownValue;
            });
            extensionContainer.Add(_dropdown);
        }

        public override List<string> GetValues() {
            return new List<string>() {
                ReferencedBehaviorName,
                lastDropDownValue
            };
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
                if (!SBTEditorUtils.TryGetBehaviorFile(obj, out string fileName)) continue;
                
                _dropdown.value = fileName;
                break;
            }
        }
    }
}
