using System.Collections.Generic;
using UnityEngine;

namespace SimpleBT.NonEditor.Nodes
{
    public class Action_RotateConstantly3D : Action_RotateConstantly2D
    {
        [SerializeField] private string _keyAxisRotation;
        
        private AxisRotationType _axisRotation;

        public override void AssignKeys(List<string> keys) {
            base.AssignKeys(keys);
            _keyAxisRotation = keys[1];
        }

        protected override void Initialize() {
            base.Initialize();
            _axisRotation = blackboard.GetValue<AxisRotationType>(_keyAxisRotation);
        }

        protected override Vector3 GetDesiredRotation()
        {
            return _axisRotation switch
            {
                AxisRotationType.X => new Vector3(_speed, 0, 0),
                AxisRotationType.Y => new Vector3(0, _speed, 0),
                AxisRotationType.Z => new Vector3(0, 0, _speed),
                AxisRotationType.XY => new Vector3(_speed, _speed, 0),
                AxisRotationType.XZ => new Vector3(_speed, 0, _speed),
                AxisRotationType.YZ => new Vector3(0, _speed, _speed),
                _ => new Vector3(_speed, _speed, _speed)
            };
        }
    }
}
