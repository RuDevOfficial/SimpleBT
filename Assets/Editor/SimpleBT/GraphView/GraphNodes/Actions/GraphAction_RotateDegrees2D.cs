using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using SimpleBT.Editor.Utils;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace SimpleBT.Editor.GraphNodes
{
    [System.Serializable]
    public class GraphAction_RotateDegrees2D : GraphAction
    {
        private TextField _degreesField;
        private TextField _timeField;
        private DropdownField _interpolationField;
        private CurveField _curveField;

        [SerializeField] private string _keyDegrees;
        [SerializeField] private string _keyTime;
        [SerializeField] private string _keyInterpolation;
        [SerializeField] private string _keyCurve;
        
        public GraphAction_RotateDegrees2D()
        {
            Title = "Rotate Degrees 2D";
            ClassReference = "Action_RotateDegrees2D";
        }

        public override void GenerateInterface()
        {
            base.GenerateInterface();
            
            _degreesField = new TextField("Degrees:");
            _timeField = new TextField("Time:");
            _interpolationField = new DropdownField("Interpolation:", SBTEditorUtils.ReturnEnumToList<RotationInterpolationType>(), 0);
            _curveField = new CurveField();

            _degreesField.value = "0";
            _timeField.value = "0";
            
            _degreesField.RegisterValueChangedCallback(evt => _keyDegrees = evt.newValue);
            _timeField.RegisterValueChangedCallback(evt => _keyTime = evt.newValue);
            _interpolationField.RegisterValueChangedCallback(evt => _keyInterpolation = evt.newValue);
            _curveField.RegisterValueChangedCallback(evt =>
            {
                var keyCurve = "";

                for (var i = 0; i < evt.newValue.keys.Length; i++)
                {
                    Keyframe oldKeyFrame = evt.newValue.keys[i];
                    var keyFrameString = "";
                    if (i != 0) { keyFrameString += "_"; }
                    
                    keyFrameString += MathF.Round(oldKeyFrame.time, 2) + "|";
                    keyFrameString += MathF.Round(oldKeyFrame.value, 2) + "|";
                    keyFrameString += MathF.Round(oldKeyFrame.inTangent, 2) + "|";
                    keyFrameString += MathF.Round(oldKeyFrame.outTangent, 2) + "|";
                    keyFrameString += MathF.Round(oldKeyFrame.inWeight, 2) + "|";
                    keyFrameString += MathF.Round(oldKeyFrame.outWeight, 2) + "|";
                    keyFrameString += oldKeyFrame.weightedMode;

                    keyFrameString = keyFrameString.Replace(',', '.');
                    keyCurve += keyFrameString;
                }

                _keyCurve = keyCurve;
            });
            
            extensionContainer.Add(_degreesField);
            extensionContainer.Add(_timeField);
            extensionContainer.Add(_interpolationField);
            extensionContainer.Add(_curveField);
        }

        public override List<string> GetValues()
        {
            return new List<string>() {
                _keyDegrees,
                _keyTime, 
                _keyInterpolation,
                _keyCurve
            };
        }

        public override void ReloadValues(List<string> values)
        {
            _degreesField.value = values[0];
            _timeField.value = values[1];
            _interpolationField.value = values[2];
            
            var curve = new AnimationCurve(SBTNonEditorUtils.GetKeyFrames(values[3], '_', '|'));
            _curveField.value = curve;
        }
    }

}
