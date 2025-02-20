using System;
using System.Collections.Generic;
using SimpleBT.Core;
using UnityEngine;

namespace SimpleBT.NonEditor.Nodes
{
    public class Action_Debug : Action_Node
    {
        [SerializeField] private string _message;
        [SerializeField] private string _result;

        protected override void Initialize() { }

        public override void AssignValues(List<string> values)
        {
            _message = values[0];
            _result = values[1];
        }

        protected override Status Tick()
        {
            Debug.Log(_message);
            Status status = (Status)Enum.Parse(typeof(Status), _result);
            return status;
        }
    }
}

