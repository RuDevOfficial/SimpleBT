using System.Collections.Generic;
using SimpleBT.Core;
using UnityEngine;

namespace SimpleBT.NonEditor.Nodes
{
    public class Action_RotateDegrees2D : Node, INodeKeyAssignable
    {
        [SerializeField] private string _keyDegrees;
        [SerializeField] private string _keyTime;
        [SerializeField] private string _keyInterpolation;
        [SerializeField] private string _keyFrames;

        private AnimationCurve _curve;
        protected float _degrees;
        private float _time;
        private RotationInterpolationType _interpolation = RotationInterpolationType.Lerp;

        private float _currentTime;
        private Quaternion _startingQuaternion;
        protected Quaternion _targetQuaternion;

        public virtual void AssignKeys(List<string> keys)
        {
            _keyDegrees = keys[0];
            _keyTime = keys[1];
            _keyInterpolation = keys[2];
            _keyFrames = keys[3];
        }

        protected override void Initialize()
        {
            _degrees = _blackboard.GetValue<float>(_keyDegrees);
            _time = _blackboard.GetValue<float>(_keyTime);
            _interpolation = _blackboard.GetValue<RotationInterpolationType>(_keyInterpolation);
            _curve = new AnimationCurve(SBTNonEditorUtils.GetKeyFrames(_keyFrames, '_', '|'));
        }

        protected override Status Tick()
        {
            if (_currentTime == 0) {
                _startingQuaternion = _blackboard.transform.rotation;
                SetDefaultValues(); 
            }
            
            _currentTime += Time.deltaTime;

            if (_currentTime < _time) {
                float currentValue = _curve.Evaluate(_currentTime / _time);
                UpdateRotation(currentValue);
                return Status.Running;
            }

            _blackboard.transform.rotation = _targetQuaternion;
            _currentTime = 0;
            return Status.Success;
        }

        private void UpdateRotation(float currentValue)
        {
            _blackboard.transform.rotation = _interpolation switch
            {
                RotationInterpolationType.Slerp => Quaternion.SlerpUnclamped(_startingQuaternion, _targetQuaternion, currentValue),
                _ => Quaternion.LerpUnclamped(_startingQuaternion, _targetQuaternion, currentValue)
            };
        }

        protected virtual void SetDefaultValues()
        {
            _targetQuaternion = _blackboard.transform.rotation * Quaternion.Euler(0, 0, _degrees);
        }
    }

}

public enum RotationInterpolationType { Lerp, Slerp }
