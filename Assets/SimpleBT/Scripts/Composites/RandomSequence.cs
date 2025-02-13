using System;
using System.Collections.Generic;
using SimpleBT.Core;
using UnityEngine;
using Random = System.Random;

namespace SimpleBT.Composite.Prebuilt
{
    public class RandomSequence : Composite
    {
        private List<int>  _generatedOrder;
        private List<int> _numberList;
        private Random _randomGenerator;

        public RandomSequence(params INode[] nodes) : base(nodes)
        {
            _numberList = new List<int>();
            _generatedOrder = new List<int>(_children.Count);
            _randomGenerator = new Random(Guid.NewGuid().GetHashCode());
            
            Repopulate();
        }
        
        protected override Status Tick()
        {
            if (_generatedOrder.Count == 0) { Repopulate(); }
            
            Status childStatus = _children[_generatedOrder[^1]].OnTick();
            switch (childStatus)
            {
                case Status.Success:
                    _generatedOrder.RemoveAt(_generatedOrder.Count - 1);
                    
                    if (_generatedOrder.Count == 0) { return Status.Success; }
                    else { return Status.Running; }

                case Status.Failure:
                    _generatedOrder.Clear();
                    return Status.Failure;

                default: return Status.Running;
            }
        }
        
        private void Repopulate()
        {
            _numberList.Clear();
            _generatedOrder.Clear();
            
            for (int i = 0; i < _children.Count; i++) {
                _numberList.Add(_numberList.Count);
            }
            
            for (int i = 0; i < _children.Count; i++)
            {
                int randomIndex = _randomGenerator.Next(0, _numberList.Count);
                int number = _numberList[randomIndex];
                
                _generatedOrder.Add(number);
                _numberList.RemoveAt(randomIndex);
            }
        }
    }
}
