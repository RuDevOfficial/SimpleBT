﻿using System.Collections.Generic;
using SimpleBT.Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace SimpleBT.NonEditor.Nodes
{
    public class Action_Follow2D : Node, INodeKeyAssignable
    {
        [SerializeField] protected string _keyTarget;
        [SerializeField] protected string _keySpeed;
        [SerializeField] protected string _keyUseTransform;
        [SerializeField] protected string _keyIgnoreFlag;
        [SerializeField] protected string _keyDistance;

        protected GameObject _target = null;
        protected bool _useTransform;
        protected float _velocity;
        private Rigidbody2D _rb2D;
        protected RigidbodyMoveFlag _rigidbodyMoveFlag;
        protected float _distance;
        
        public void AssignKeys(List<string> keys)
        {
            _keyTarget = keys[0];
            _keySpeed = keys[1];
            _keyUseTransform = keys[2];
            _keyIgnoreFlag = keys[3];
            _keyDistance = keys[4];
        }

        protected override void Initialize()
        {
            _useTransform = _blackboard.GetValue<bool>(_keyUseTransform);
            _rigidbodyMoveFlag = _blackboard.GetValue<RigidbodyMoveFlag>(_keyIgnoreFlag);
            _distance = _blackboard.GetValue<float>(_keyDistance);
            _rb2D = _blackboard.gameObject.GetComponent<Rigidbody2D>();
        }

        protected override Status Tick()
        {
            if (!_target)
            {
                _target = _blackboard.GetComplexValue<GameObject>(_keyTarget); 
                if (!_target) { return Status.Failure; }
            }
            
            if (_useTransform == false && !_rb2D) { return Status.Failure; }
            
            _velocity = _blackboard.GetValue<float>(_keySpeed);
            Vector2 direction = (_target.transform.position - _blackboard.gameObject.transform.position).normalized;
            
            if (_useTransform) {
                _blackboard.gameObject.transform.position 
                    = (Vector2)_blackboard.gameObject.transform.position + direction * (_velocity * Time.deltaTime);
            }
            else
            {
                if (_rb2D.bodyType == RigidbodyType2D.Kinematic) { _rb2D.MovePosition((Vector2)_blackboard.gameObject.transform.position + direction * (_velocity * Time.deltaTime), _rigidbodyMoveFlag); }
                else { _rb2D.linearVelocity = direction * (_velocity * Time.fixedDeltaTime); }
            }

            bool result = Vector2.Distance(_target.transform.position, _blackboard.gameObject.transform.position) <= _distance;
            if (!result) return Status.Running;
            
            _target = null;
            return Status.Success;
        }
    }

}