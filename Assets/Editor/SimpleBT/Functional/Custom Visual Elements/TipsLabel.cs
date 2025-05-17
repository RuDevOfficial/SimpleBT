using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

namespace SimpleBT.Editor
{
    public class TipsLabel : ToolbarButton
    {
        private float _lastTimePassed = 0.0f;
        private const float MAX_TIP_TIME = 10.0f;

        private List<string> _tipList;
        
        public TipsLabel() { }
        
        public void SetDefaults()
        {
            focusable = false;
            displayTooltipWhenElided = true;

            _tipList = GetTips();
        }

        public void Update()
        {
            if (!(Time.realtimeSinceStartup > _lastTimePassed + MAX_TIP_TIME)) return;
            
            _lastTimePassed = Time.realtimeSinceStartup; 
            SwitchTip();
        }

        private void SwitchTip()
        {
            int randomNumber = Random.Range(0, _tipList.Count - 1);
            text = $"Tip #{randomNumber + 1}: {_tipList[randomNumber]}";
        }

        private List<string> GetTips()
        {
            return new List<string>()
            {
                // General Tips
                "Remember to save manually!",
                "Actions are RED, Conditions are YELLOW, Decorators are GREEN and Composites are BLUE!",
                "All blackboard dictionary keys are in CAPS",
                
                // Nodes
                "A Root must be present before any node!",
                "Make custom nodes by right-clicking and selecting ''Create Custom Node''!",
                "Create a new behavior by simply changing the name on the top left and saving! (and clean everything too)",
                "Want to reference the agent itself on a behavior? Type ''SELF'' on the field, no need to add it to the blackboard!",
                
                // Blackboard
                "Create a new variable by pressing the + button on the top right of the blackboard",
                "Change the parameter name by double clicking it",
                "Delete blackboard parameters individually by clicking it and pressing the ''delete'' key",
                "Remove all parameters from a blackboard by pressing the ''Clear Blackboard'' button",
                "No two parameters can be renamed the same on a blackboard",
                
                // Common Problems
                "Custom section still empty? Make sure your scriptable object is inside ''Custom Nodes SO''!",
                "Behaviors not loading after selecting a .simple file? Check that the settings window has the folder path, if not, save them!",
                "Sub-behavior causing problems? Make sure the parameters referenced in the sub-behavior are also present in the main one!",
                "Your custom node is not appearing after selecting it? Make sure that the entry has ''Custom_'' before it! Like Custom_GraphAction_NewAction",
                "Nodes not executing in order? Make sure you have connected the ports in order (from left to right)"
            };
        }
    }
}
