using System.Collections.Generic;
using SimpleBT.Core;
using UnityEngine;

namespace SimpleBT.NonEditor.Nodes
{
    public class Action_GoToPosition2D : Node, INodeKeyAssignable
    {
        [SerializeField] protected string _keyPosition;
        [SerializeField] protected string _keySpeed;
        [SerializeField] protected string _keyUseTransform;
        [SerializeField] protected string _keyIgnoreFlag;

        private Vector2 _position;
        protected float _speed;
        protected bool _useTransform;
        protected RigidbodyMoveFlag _rigidbodyMoveFlag;
        
        protected float _timeToReach;
        protected float _time;
        private Vector2 _direction;
        private Rigidbody2D _rb2D;

        public void AssignKeys(List<string> keys)
        {
            _keyPosition = keys[0];
            _keySpeed = keys[1];
            _keyUseTransform = keys[2];
            _keyIgnoreFlag = keys[3];
        }

        protected override void Initialize()
        {
            _useTransform = _blackboard.GetValue<bool>(_keyUseTransform);
            _rb2D = _blackboard.gameObject.GetComponent<Rigidbody2D>();
            _rigidbodyMoveFlag = _blackboard.GetValue<RigidbodyMoveFlag>(_keyIgnoreFlag);
        }

        protected override Status Tick()
        {
            if (_time == 0.0f)
            {
                _position = _blackboard.GetValue<Vector2>(_keyPosition);
                _speed = _blackboard.GetValue<float>(_keySpeed);
                _direction = (_position - (Vector2)_blackboard.gameObject.transform.position).normalized;
                _timeToReach = (_position - (Vector2)_blackboard.gameObject.transform.position).magnitude / _speed;
                
                if (!_rb2D && _useTransform == false) { return Status.Failure; }
                if (_rb2D) { if (_rb2D.bodyType != RigidbodyType2D.Kinematic) { Debug.LogWarning("To use Action_GoToPosition2D with Rigidbodies select IsKinematic to True"); return Status.Failure; } }
            }
            
            //TODO make it so ignoring flags also work on transform!
            if (_useTransform)
            {
                if (_time < _timeToReach) {
                    _time += Time.deltaTime;
                    _blackboard.gameObject.transform.position = (Vector2)_blackboard.gameObject.transform.position + _direction * (_speed * Time.deltaTime);
                    return Status.Running;
                }
                else
                {
                    _time = 0.0f;
                    _blackboard.gameObject.transform.position = _position;
                    return Status.Success;
                }
            }
            else
            {
                if (_time < _timeToReach)
                {
                    _time += Time.deltaTime;
                    _rb2D.MovePosition(_rb2D.position + _direction * (_speed * Time.fixedDeltaTime), _rigidbodyMoveFlag);
                    return Status.Running;
                }
                else
                {
                    _time = 0.0f;
                    //_rb2D.MovePosition(_position, _rigidbodyMoveFlag);
                    return Status.Success;
                }
            }
        }

        public override void OnAbort()
        {
            _time = 0.0f;
        }
    }

}
