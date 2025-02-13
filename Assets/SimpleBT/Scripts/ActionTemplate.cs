using SimpleBT.Core;

namespace SimpleBT.Templates
{
    public class ActionNodeTemplate : ActionNode
    {
        #region Setting Up Constructor
        private string _keyValue;
        
        // When calling the Action, add a string parameter to get the key values 
        // before accessing the blackboard
        public ActionNodeTemplate(string keyValue) : base()
        {
            _keyValue = keyValue;
        }
        #endregion

        #region Initializing Values
        // On Initialize get those values from the blackboard
        // or perhaps the gameObject associated to the it
        private int value;
        
        protected override void Initialize()
        {
            value = blackboard.GetValue<int>(_keyValue);
        }
        #endregion

        #region Logic Execution
        // Execute ticking logic here, it can either return Running (keeps ticking) or
        // return Failure or Success depending on your action logic.
        protected override Status Tick()
        {
            return Status.Running;
        }
        #endregion
    }

    public class ConditionTemplate : ConditionNode
    {
        #region Setting Up Constructor
        private string _keyValue;
        
        // When calling the Condition, add a string parameter to get the key values 
        // before accessing the blackboard
        public ConditionTemplate(string keyValue) : base()
        {
            _keyValue = keyValue;
        }
        #endregion
        
        #region Initializing Values
        // On Initialize get those values from the blackboard
        // or perhaps the gameObject associated to the it
        private int value;
        
        protected override void Initialize()
        {
            value = blackboard.GetValue<int>(_keyValue);
        }
        #endregion

        #region Executing the Check
        // Check only triggers in one tick and can only return true or
        // false.
        protected override bool Check()
        {
            return value > 10;
        }
        #endregion
    }
}

