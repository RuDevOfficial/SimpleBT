using System;
using System.Collections.Generic;
using SimpleBT.Core;
using Random = System.Random;

namespace SimpleBT.NonEditor.Nodes
{
    public class Composite_RandomSequence : Composite, INodeKeyAssignable
    {
        private List<int>  _generatedOrder;
        private List<int> _numberList;
        private Random _randomGenerator;

        private void OnEnable() { GenerateRandomSequence(); }

        public void AssignKeys(List<string> keys) { }
        
        protected override Status ExecuteFlow()
        {
            if (_generatedOrder.Count == 0) { Repopulate(); }
            
            _childrenIndex = _generatedOrder[^1];
            Status childStatus = _children[_childrenIndex].OnTick();
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

        public void GenerateRandomSequence()
        {
            _numberList = new List<int>();
            _generatedOrder = new List<int>(_children.Count);
            _randomGenerator ??= new Random(Guid.NewGuid().GetHashCode());
            
            Repopulate();
        }
        

    }

}
