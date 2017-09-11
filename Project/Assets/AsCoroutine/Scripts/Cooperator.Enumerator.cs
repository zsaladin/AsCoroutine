using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsCoroutine
{
    public partial class Cooperator : IEnumerator
    {
        private List<Cooperator> _cooperators;
        private int _currentIndex = -1;
        private object _current;

        object IEnumerator.Current
        {
            get { return _current; }
        }

        bool IEnumerator.MoveNext()
        {
            if (_currentIndex < 0)
                (this as IEnumerator).Reset();

            bool isRepeating = false;
            while (0 <= --_currentIndex)
            {
                Cooperator currCooperator = _cooperators[_currentIndex];

                object prevInstruction = _current;
                object resultInstruction;
                if (currCooperator.Instruct(prevInstruction, out resultInstruction))
                {
                    _current = resultInstruction;
                    return true;
                }
                else
                {
                    if (isRepeating)
                        return true;

                    if (currCooperator.IsRepeat(prevInstruction))
                    {
                        isRepeating = true;
                        _currentIndex += 2;
                    }
                }
            }
            return false;
        }

        void IEnumerator.Reset()
        {
            _cooperators = new List<Cooperator>();

            Cooperator curr = this;
            while (curr != null)
            {
                _cooperators.Add(curr);
                curr = curr.Parent;
            }

            _currentIndex = _cooperators.Count;

            Reset();
        }

        protected virtual void Reset()
        {

        }

    }
}