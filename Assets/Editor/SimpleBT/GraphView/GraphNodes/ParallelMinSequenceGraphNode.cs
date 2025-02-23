using System.Globalization;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace SimpleBT.Editor.GraphNodes
{
    [System.Serializable]
    public class ParallelMinSequenceGraphNode : CompositeNode
    {
        public int MinimumAmount = 0;
        private Port _outputPort;
        private IntegerField _intField;

        public ParallelMinSequenceGraphNode()
        {
            Title = "Parallel Min Sequence";
            ClassReference = "ParallelMinSequence";
        }

        public override void GenerateInterface()
        {
            base.GenerateInterface();
            _outputPort ??= outputContainer.Q<Port>("");
            
            _intField = new IntegerField("Minimum: ");
            _intField.value = MinimumAmount;
            _intField.SetEnabled(false);
            _intField.RegisterValueChangedCallback((evt) =>
            {
                if (evt.newValue < 0) { return; }
                else
                {
                    int connectionCount = _outputPort.connections.Count();
                    if (evt.newValue > connectionCount) { 
                        EditorUtility.DisplayDialog("Error", "Can't add a minimum superior to the current connections!", "OK");
                        _intField.value = connectionCount;
                        return;
                    }
                }
                
                MinimumAmount = evt.newValue;
            });
            extensionContainer.Add(_intField);
            
            // This listener will enable the port if there is a connection, opposite if none
            _outputPort.AddManipulator(new EdgeConnector<Edge>(new ParrallelMinListener(_intField, _outputPort)));
        }
    }
    
    public class ParrallelMinListener : IEdgeConnectorListener
    {
        private IntegerField _field = null;
        private Port _port;

        public ParrallelMinListener(IntegerField field, Port port)
        {
            this._field = field;
            this._port = port;
        }
        
        public void OnDropOutsidePort(Edge edge, Vector2 position) { }

        public void OnDrop(GraphView graphView, Edge edge)
        {
            _field.SetEnabled(true);
        }
    }
}
