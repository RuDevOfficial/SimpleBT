using System;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace SimpleBT.Editor.GraphNodes
{
    using Utils;
    
    [System.Serializable]
    public class GraphAction_RotateDegrees2D : GraphAction
    {
        private TextField _degreesTF;
        private TextField _timeTF;
        private DropdownField _interpolationDropdown;
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
            
            _degreesTF = new TextField("Degrees:");
            _timeTF = new TextField("Time:");
            _interpolationDropdown = new DropdownField("Interpolation:", SBTEditorUtils.ReturnEnumToList<RotationInterpolationType>(), 0);
            _curveField = new CurveField();

            _degreesTF.RegisterValueChangedCallback(evt => _keyDegrees = evt.newValue);
            _timeTF.RegisterValueChangedCallback(evt => _keyTime = evt.newValue);
            _interpolationDropdown.RegisterValueChangedCallback(evt => _keyInterpolation = evt.newValue);
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
            
            extensionContainer.Add(_degreesTF);
            extensionContainer.Add(_timeTF);
            extensionContainer.Add(_interpolationDropdown);
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
            _degreesTF.value = values[0];
            _timeTF.value = values[1];
            _interpolationDropdown.value = values[2];
            
            var curve = new AnimationCurve(SBTNonEditorUtils.GetKeyFrames(values[3], '_', '|'));
            _curveField.value = curve;
        }
    }

}
