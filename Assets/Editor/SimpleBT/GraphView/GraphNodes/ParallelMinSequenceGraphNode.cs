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
        private Port outputPort;
        private TextField field;
        
        public ParallelMinSequenceGraphNode() { NodeName = "ParallelMinSequence"; }

        public override void Draw()
        {
            base.Draw();
            outputPort ??= outputContainer.Q<Port>("");
            
            field = new TextField("Minimum: ");
            field.value = MinimumAmount.ToString();
            field.SetEnabled(false);
            field.RegisterValueChangedCallback((evt) =>
            {
                string newValue = evt.newValue;

                if (int.TryParse(newValue, 
                        NumberStyles.Integer, 
                        CultureInfo.InvariantCulture.NumberFormat,
                        out int number) == false)
                {
                    EditorUtility.DisplayDialog("Error", "Value must be an integer", "OK");
                    field.value = evt.previousValue;
                    return;
                }
                else
                {
                    if (number >= 1)
                    {
                        int connectionCount = outputPort.connections.Count();
                        number = Mathf.Min(connectionCount, number);
                        Debug.Log(number);
                        
                        MinimumAmount = number;
                        field.value = MinimumAmount.ToString();
                        return; 
                    }
                    
                    EditorUtility.DisplayDialog("Error", "Minimum can't be less than 1", "OK");
                    field.value = evt.previousValue;
                    MinimumAmount = int.Parse(evt.previousValue);
                    return;
                }
            });
            extensionContainer.Add(field);
            
            // This listener will enable the port if there is a connection, opposite if none
            outputPort.AddManipulator(new EdgeConnector<Edge>(new ParrallelMinListener(field, outputPort)));
        }
    }
    
    public class ParrallelMinListener : IEdgeConnectorListener
    {
        private TextField _field = null;
        private Port _port;

        public ParrallelMinListener(TextField field, Port port)
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
