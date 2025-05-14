using System.Collections.Generic;
using UnityEngine;

namespace SimpleBT.NonEditor.Nodes
{
    public class Action_RotateDegrees3D : Action_RotateDegrees2D
    {
        [SerializeField] private string _keyRotationType;
        
        private AxisRotationType _axisRotationType = AxisRotationType.X;
        
        public override void AssignKeys(List<string> keys)
        {
            base.AssignKeys(keys);
            _keyRotationType = keys[4];
        }

        protected override void Initialize()
        {
            base.Initialize();
            _axisRotationType = blackboard.GetValue<AxisRotationType>(_keyRotationType);
        }

        protected override void SetDefaultValues()
        {
            _targetQuaternion = _axisRotationType switch
            {
                AxisRotationType.X => blackboard.transform.rotation * Quaternion.Euler(_degrees, 0, 0),
                AxisRotationType.Y => blackboard.transform.rotation * Quaternion.Euler(0, _degrees, 0),
                AxisRotationType.Z => blackboard.transform.rotation * Quaternion.Euler(0, 0, _degrees),
                AxisRotationType.XY => blackboard.transform.rotation * Quaternion.Euler(_degrees, _degrees, 0),
                AxisRotationType.XZ => blackboard.transform.rotation * Quaternion.Euler(_degrees, 0, _degrees),
                AxisRotationType.YZ => blackboard.transform.rotation * Quaternion.Euler(0, _degrees, _degrees),
                _ => blackboard.transform.rotation * Quaternion.Euler(_degrees, _degrees, _degrees)
            };
        }
    }
    
    public enum AxisRotationType { X, Y, Z, XY, XZ, YZ, XYZ }
}
