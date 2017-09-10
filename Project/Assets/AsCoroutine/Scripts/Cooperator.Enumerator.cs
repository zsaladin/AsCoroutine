using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsCoroutine
{
    public partial class Cooperator
    {
        public class Enumerator : IEnumerator
        {
            private Cooperator _tail;
            private List<Cooperator> _cooperators;
            private int _currIndex;
            

            public object Current { get; private set; }

            public Enumerator(Cooperator tail)
            {
                _tail = tail;
                GetCooperators();
                Reset();
            }

            public virtual bool MoveNext()
            {
                bool isRepeating = false;
                while (0 <= --_currIndex)
                {
                    Cooperator currCooperator = _cooperators[_currIndex];

                    object prevInstruction = Current;
                    object resultInstruction;
                    if (currCooperator.Instruct(prevInstruction, out resultInstruction))
                    {
                        Current = resultInstruction;
                        return true;
                    }
                    else
                    {
                        if (isRepeating)
                            return true;

                        if (currCooperator.IsRepeat(prevInstruction))
                        {
                            isRepeating = true;
                            _currIndex += 2;
                        }
                    }
                }
                return false;
            }

            public virtual void Reset()
            {
                _currIndex = _cooperators.Count;
            }

            private void GetCooperators()
            {
                _cooperators = new List<Cooperator>();

                Cooperator curr = _tail;
                while (curr != null)
                {
                    _cooperators.Add(curr);
                    curr = curr.Parent;
                }
            }
        }
    }
}